// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/UIFlash" {   
    Properties {   
        _image ("image", 2D) = "white" {}   
        _widthRatio ("_widthRatio", Range(0, 20)) = 1 //光线宽度系数，值越大效果越细  
        _percent ("_percent(-1, 2)", Range(-3, 3)) = -1  
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
            float _percent;   
           

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


				fixed  tmpOffset  = 1- (_Time.y -floor(_Time.y)) *2;

	   
		      

				fixed2 blink_uv = (i.uv + fixed2(tmpOffset,0)) * _widthRatio - _widthRatio;  
                // 旋转矩阵，旋转30度，可以自己修改方向  
              //  fixed2x2 rotMat = fixed2x2(0.866,0.5,-0.5,0.866);   
                // 乘以旋转矩阵  
                //blink_uv = mul(rotMat, blink_uv);  
                // 当x越靠近原点时，RGB值越大  
                // (1-f) * a + b * f;  
                // 起点f=0，颜色最亮；起点f=1，颜色最暗  
                fixed rgba = lerp(fixed(1),fixed(0),abs(blink_uv.x));  
                if(k.a > 0.05)  
                {  
                    // 叠加RGB值，可以自己修改颜色  
                    k +=  fixed4(saturate(rgba),saturate(rgba),saturate(rgba),saturate(rgba));  
                }  
                return k;  
            }    
            ENDCG    
        }  
    }  
    FallBack Off     
}   