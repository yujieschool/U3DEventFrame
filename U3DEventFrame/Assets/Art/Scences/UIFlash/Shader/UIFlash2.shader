// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/UIFlash2" {   
    Properties {   
        _image ("image", 2D) = "white" {}   
        _widthRatio ("_widthRatio", Range(0, 20)) = 1 //光线宽度系数，值越大效果越细  
        _Angle ("lean_Angle(-3.14, 3.14)", Range(-3.14, 3.14)) = 0  

		_BlendColorFrom ("Flash Color From", Color) = (0,0,0,1)

		 _MoveSpeed ("MoveSpeed", float) = 5  


		  _MoveTime ("_MoveTime", float) = 5  
    }   
       
    SubShader {      
        Tags {"Queue" = "Transparent"}        
        ZWrite Off      
        // 源rgb * a + 背景rgb * (1-源a)    
        Blend SrcAlpha OneMinusSrcAlpha  
  
        Pass {       
            CGPROGRAM       
            #pragma vertex vert       
            #pragma fragment frag       
            #include "UnityCG.cginc"  
            sampler2D _image;   
            float _widthRatio;   
            float _Angle; 
			
			fixed4  _BlendColorFrom ;  
           
		   fixed  _MoveSpeed ;


		   fixed  _MoveTime ;
            struct v2f {       
                float4 pos:SV_POSITION;       
                float2 uv : TEXCOORD0;      
            };     
     
            v2f vert(appdata_base v) {     
                v2f o;     
                o.pos = UnityObjectToClipPos (v.vertex);     
                o.uv = v.texcoord.xy;     
                return o;     
            }     
     
            fixed4 frag(v2f i) : COLOR0 {    
                // 对图片进行采样  
                fixed4 k = tex2D(_image, i.uv);  
                // 正常情况UV值变大，当前点位置颜色会增强  
                //下面用了lerp(1,0,f)，那么这里UV增大，当前点位置颜色会减弱 


				fixed  tmpOffset  = (1- (_Time.x*_MoveTime -floor(_Time.x*_MoveTime)) )*_MoveSpeed ;

	   

				fixed2 blink_uv = (i.uv + fixed2(tmpOffset,tmpOffset)) * _widthRatio - _widthRatio;  
                // 旋转矩阵，旋转30度，可以自己修改方向  
                fixed2x2 rotMat = fixed2x2(cos(_Angle),-sin(_Angle),sin(_Angle),cos(_Angle));   
                // 乘以旋转矩阵  
               blink_uv = mul(rotMat, blink_uv);  
                // 当y越靠近原点时，RGB值越大  
                // (1-f) * a + b * f;  
                // 起点f=0，颜色最亮；起点f=1，颜色最暗  
               fixed rgba = lerp(fixed(1),fixed(0),abs(blink_uv.x));  ;  
                if(k.a > 0.05)  
                {  
                    // 叠加RGB值，可以自己修改颜色  
                  //  k += lerp(_BlendColorFrom,k,rgba);

				  if(rgba >0.1)
				  {
				   fixed4 tmpRGB = _BlendColorFrom* rgba;
				   k += tmpRGB ;// fixed4(tmpRGB.r,tmpRGB.g,tmpRGB.b,saturate(rgba));   
				  }
				  
                }  
                return k;  
            }    
            ENDCG    
        }  
    }  
    FallBack Off     
}   