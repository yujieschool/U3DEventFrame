// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/UI Wave"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_NoiseTex ("Wave Noise", 2D) = "white" {}//噪波贴图
		_Offset ("Vertex Offset", vector) = (0, 0, 0, 0)//表示顶点的偏移量
		_Indentity ("Indentity", float) = 0.1//表示水波的扭曲强度
		_SpeedX ("WaveSpeedX", float) = 0.08//噪波贴图延X方向的移动速度
		_SpeedY ("WaveSpeedY", float) = 0.04//噪波贴图延Y方向的移动速度
		_AlphaFadeIn ("AlphaFadeIn", float) = 0.0//水波的淡入位置
		_AlphaFadeOut ("AlphaFadeOut", float) = 1.0//水波的淡出位置
		_TwistFadeIn ("TwistFadeIn", float) = 1.0//扭曲的淡入位置
		_TwistFadeOut ("TwistFadeOut", float) = 1.01//扭曲的淡出位置
		_TwistFadeInIndentity ("TwistFadeInIndentity", float) = 1.0//扭曲的淡入强度
		_TwistFadeOutIndentity ("TwistFadeOutIndentity", float) = 1.0//扭曲的淡出强度
		_Color ("Tint", Color) = (1,1,1,1)

		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15
	}

	CGINCLUDE
	//定义顶点的输入结构
		struct appdata_ui
		{
			float4 vertex   : POSITION;
			float4 color    : COLOR;
			float2 texcoord : TEXCOORD0;
		};

		//定义顶点到片段的结构
		struct v2f_ui
		{
			float4 pos   : SV_POSITION;
			fixed4 color    : COLOR;
			half2 uv  : TEXCOORD0;
		};

		fixed4 _Color;

		//两个Pass通用的顶点函数
		void vert_ui(inout appdata_ui Input, out v2f_ui Output){

			Output.pos = UnityObjectToClipPos(Input.vertex);
				Output.uv = Input.texcoord;
			#ifdef UNITY_HALF_TEXEL_OFFSET
				Output.uv.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
			#endif
				Output.color = Input.color * _Color;
		}
	ENDCG

	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
			//第一个Pass，正常渲染
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"


			v2f_ui vert(appdata_ui v)
			{
				v2f_ui o;
				vert_ui(v, o);
				return o;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f_ui i) : SV_Target
			{
				half4 color = tex2D(_MainTex, i.uv) * i.color;
				clip (color.a - 0.01);
				return color;
			}
		ENDCG
		}

		Pass
		{
			//第二个Pass，渲染水波
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _NoiseTex;

			float4 _Offset;
			half _SpeedX;
			half _SpeedY;
			half _Indentity;
			float _AlphaFadeIn;
			float _AlphaFadeOut;
			half _TwistFadeIn;
			half _TwistFadeOut;
			fixed _TwistFadeInIndentity;
			fixed _TwistFadeOutIndentity;

			v2f_ui vert(appdata_ui v)
			{
				v2f_ui o;

				
				v.vertex = v.vertex - float4 (_Offset.xyz, 0);//偏移顶点坐标
				vert_ui(v, o);
				return o;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f_ui i) : SV_Target
			{
				//对淡入强度和淡出强度的插值
				fixed fadeT = saturate((_TwistFadeOut - i.uv.y) / (_TwistFadeOut - _TwistFadeIn));
				float2 tuv = (i.uv - float2(0.5, 0)) * fixed2(lerp(_TwistFadeOutIndentity, _TwistFadeInIndentity, fadeT), 1) + float2(0.5, 0);

				//计算噪波贴图的RG值，得到扭曲UV，
				float2 waveOffset = (tex2D(_NoiseTex, tuv + float2(0, _Time.y * _SpeedY)).rg + tex2D(_NoiseTex, tuv + float2(_Time.y * _SpeedX, 0)).rg) - 1;
				float2 ruv = float2(i.uv.x, 1 - i.uv.y) + waveOffset * _Indentity;

				//使用扭曲UV对纹理采样
				float4 c = tex2D (_MainTex, ruv);

				//对淡入Alpha和淡出Alpha的插值
				fixed fadeA = saturate((_AlphaFadeOut - ruv.y) / (_AlphaFadeOut - _AlphaFadeIn));
				c = c * _Color * i.color * fadeA;
				clip (c.a - 0.01);
				return c;
			}
		ENDCG
		}
	}
}
