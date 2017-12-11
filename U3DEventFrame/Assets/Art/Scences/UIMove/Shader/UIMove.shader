// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/UIMove" 
{
    Properties 
	{
		 _MainTex ("Main (RGB)", 2D) = "white" { }
		

		 _DistanceX("Move XDistance",Float)=1.0

		  _DistanceY("Move YDistance",Float)=1.0
	

		 _MoveTime ("_MoveTime", float) = 5  

 }
    SubShader
	{

		 Tags
		 {
			 "Queue"="Transparent"
			 "RenderType"="Transparent"
			 "ForceNoShadowCasting"= "true"
			 "IgnoreProjector"= "true"
			
		 }
         Pass
		 {
			Lighting Off

			ZWrite  off
			ZTest  off
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;

			 fixed _DistanceX;
		    fixed _DistanceY;

			fixed _MoveTime;
		
			struct v2f 
			{
				float4  pos : SV_POSITION;
				float2  uv : TEXCOORD0;
				float4 color :COLOR;
			};

			half4 _MainTex_ST;
			half4 _AlphaTex_ST;

			v2f vert (appdata_full v)
			{
				v2f o;

			
				o.pos = UnityObjectToClipPos (v.vertex );



				fixed tmpOffset= (_Time.x*_MoveTime -floor(_Time.x*_MoveTime)) ;


				o.pos.x += tmpOffset*_DistanceX ;

				o.pos.y += tmpOffset* _DistanceY;
				o.uv =  v.texcoord;
				o.color = v.color;
				return o;
			}

			half4 frag (v2f i) : COLOR
			{
				half4 texcol = tex2D (_MainTex, i.uv);

	
				return texcol;
			}
			ENDCG
		 }
    }
}