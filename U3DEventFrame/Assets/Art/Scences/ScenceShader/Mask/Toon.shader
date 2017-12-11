// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "ZG3/Player/Toon" {
    Properties {
        _MainTex ("BaseTexture", 2D) = "white" {}
		_Color ("Main Color", Color) = (1,1,1,1)
        _LightMapTex ("_LightMapTex", 2D) = "white" {}
		_FirstShadowMultColor("_FirstShadowMultColor", Color) = (0.5,0.5,0.5,1)
		_SecondShadowMultColor("_SecondShadowMultColor", Color) = (0.5,0.5,0.5,1)
		_FirstShadow ("_FirstShadowArea", float) = 0.5
		_SecondShadow ("_SecondShadowArea", float) = 0.5
		_Shininess ("Shininess", Range (0.01, 128)) = 10
		_SpecMulti ("_SpecMulti", float) = 0.2
		_LightSpecColor("_LightSpecColor", Color) = (1,1,1,1)
        _Outline_Width ("Outline_Width", Range(0, 5)) = 0.5
		_ColorOutline ("_ColorOutline", Color) = (1, 1, 1, 1)
        _Line_Color ("Line_Color", Color) = (0.5,0.5,0.5,1)


         _MaskColor ("Mask Color", Color) = (0.42,0.5,1,0.43)
    }
    SubShader {
		LOD 700
        Tags {
            "Queue"="Geometry+20" "RenderType"="Opaque"
        }

		 Blend SrcAlpha OneMinusSrcAlpha

		 Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Outline_sampler; uniform float4 _Outline_sampler_ST;
            uniform float _Outline_Width;
            uniform float4 _Line_Color;
			uniform float4 _ColorOutline;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
				float3 normalDir : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
              //  float4 _Outline_sampler_var = tex2Dlod(_Outline_sampler,float4(TRANSFORM_TEX(o.uv0, _Outline_sampler),0.0,0)); // Outlineの入り抜き調整Sampler
                o.pos = UnityObjectToClipPos(float4(v.vertex.xyz + v.normal*(1*_Outline_Width*0.001).r,1) );
				o.normalDir = UnityObjectToWorldNormal(v.normal).xyz;
				o.normalDir =v.normal;
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
				//return fixed4(i.normalDir, 0);
                return fixed4(_Line_Color.xyz * _ColorOutline.xyz,0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            // Cull Off


				

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 psp2 
            #pragma target 3.0

            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _LightMapTex; uniform float4 _LightMapTex_ST;
			uniform fixed4 _FirstShadowMultColor;
			uniform fixed4 _SecondShadowMultColor;
			uniform half _FirstShadow;
			uniform half _SecondShadow;
			uniform half _Shininess;
			uniform half _SpecMulti;
			uniform fixed3 _LightSpecColor;
			uniform fixed4 _Color;


           fixed4 _MaskColor;
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
				half4 color: COLOR;
				UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (appdata_full v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos(v.vertex );
				o.uv0 = v.texcoord;
				o.color = v.color;
				o.normalDir = UnityObjectToWorldNormal(v.normal);
				float NDL = dot(o.normalDir, _WorldSpaceLightPos0.xyz);
				//NDL、存alpha通道省点
				o.color.a = NDL * 0.5 + 0.5;
				o.uv0 = TRANSFORM_TEX(v.texcoord, _LightMapTex); 
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);

                return o;
            }

            float4 frag(VertexOutput i) : COLOR {


			fixed4 finalRGBA;


                half4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex)); 
                half4 _LightMapTex_var = tex2D(_LightMapTex, TRANSFORM_TEX(i.uv0, _LightMapTex)); 

				fixed4 lightMapColor = tex2D(_LightMapTex, i.uv0.xy);
				fixed4 albedo = tex2D(_MainTex, i.uv0.xy);
				//实际上一张图就够了，一些勾处标黑，非常黑的快会被当成2次阴影层计算
				fixed KageLightVar = lightMapColor.g * i.color.x;
				fixed KageLightIntensity = max(floor(-KageLightVar+ 1.5), 0);

				// >0.5 up, <0.5 down
				fixed2 kFixed = KageLightVar * fixed2(1.2, 1.25) + fixed2(-0.1, -0.125);
				if (KageLightIntensity != 0) {
					KageLightIntensity = kFixed.y;
				}
				else {
					KageLightIntensity = kFixed.x;
				}
//				KageLightIntensity = (KageLightIntensity != 0) ? kFixed.y : kFixed.x;

				fixed3 kageColor1 = albedo.rgb * _FirstShadowMultColor.xyz;
				half newKLight = KageLightIntensity + i.color.a;
				newKLight = max(floor(newKLight * 0.5 + (-_FirstShadow) + 1.0), 0);
				fixed3 LightAreaVSFirstShadow = kageColor1.xyz;
				if (int(newKLight) != 0) {
					LightAreaVSFirstShadow = albedo.rgb;
				}
				else
				{
				   finalRGBA.a= 0.2f ;
				}
//				fixed3 LightAreaVSFirstShadow = (int(newKLight) != 0) ? albedo.rgb : kageColor1.xyz;

				fixed3 kageColor2 = albedo.rgb * _SecondShadowMultColor.xyz;
				newKLight = KageLightVar + i.color.a;
				newKLight = max(floor(newKLight * 0.5 + (-_SecondShadow)+ 1.0), 0);
				fixed3 LightAreaVSSecondShadow = kageColor2.xyz;
				if (int(newKLight) != 0) {
					LightAreaVSSecondShadow = kageColor1.xyz;
				}
//				fixed3 LightAreaVSSecondShadow = (int(newKLight) != 0) ? kageColor1.xyz : kageColor2.xyz;

				fixed KageLightIntensity1 = max(floor(KageLightVar + 0.91f), 0);
				half3 MixedColor = LightAreaVSSecondShadow;
				if (KageLightIntensity1 != 0) {
					MixedColor = LightAreaVSFirstShadow;
				}
			
//				half3 MixedColor = (KageLightIntensity1 != 0) ? LightAreaVSFirstShadow :LightAreaVSSecondShadow;

				//float3 viewDirection 
				float3 viewDirection  = (-i.posWorld.xyz) + _WorldSpaceCameraPos.xyz;
				float3 LVD = normalize(viewDirection) + _WorldSpaceLightPos0.xyz;
				LVD = normalize(LVD);
				//half3 h = normalize (light.dir + viewDir);
				//float nh = max (0, dot (s.Normal, h));
				//float spec = pow (nh, s.Specular*128.0) * s.Gloss;

				float spec =  exp2(log2(max(dot( i.normalDir, LVD), 0)) * _Shininess);

				//lightMapColor.b is mask, black return 1, Later (reverseSpec != 0) = 0
				float reverseSpec = (1 -spec) + (1 -lightMapColor.b);
				reverseSpec = max(floor(reverseSpec), 0.0);

				half3 sepcColor = _SpecMulti * _LightSpecColor.xyz * lightMapColor.r;
				if (reverseSpec != 0) {
					sepcColor.xyz = 0;
				}
		
//				sepcColor.xyz = (reverseSpec != 0) ? 0 : sepcColor.xyz;

				MixedColor = MixedColor + sepcColor.xyz;

				
				finalRGBA.rgb = MixedColor * _Color.xyz;
				//finalRGBA.rgb = i.color.rgb;
				//finalRGBA.a = albedo.a;

				//finalRGBA.a =  _MaskColor.a;
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
		}
    }


}
