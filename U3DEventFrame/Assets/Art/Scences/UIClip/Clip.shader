// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Flip"
{
	Properties
	{
		_MainTex ("MainTexture", 2D) = "white" {}
		_MaskTex ("MaskTexture", 2D) = "white" {}

		
	}
	SubShader
	{




		LOD 100
	    Tags { "Queue"="Transparent" "IgnoreProjector"="true" "RenderType"="Transparent" }

		Cull Off
	    Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{


	 

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			//#include "UnityUI.cginc"
   //         #pragma multi_compile __ UNITY_UI_ALPHACLIP

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 maskuv : TEXCOORD1;
			};

		

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 maskuv : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MaskTex;
			float4 _MaskTex_ST;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.maskuv = TRANSFORM_TEX(v.maskuv,_MaskTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{


			fixed4 col = tex2D(_MainTex, i.uv);
		    fixed4 mask = tex2D(_MaskTex,i.uv);

		
				col.a = mask.a;
				return col;
			}
			ENDCG
		}
	}
}
