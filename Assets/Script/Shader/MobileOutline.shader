Shader "Hidden/MobileOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Thickness ("Thickness", Range(0, 5)) = 1
        _Sensitivity ("Sensitivity", Range(0, 1)) = 0.5
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float _Thickness;
            float _Sensitivity;
            fixed4 _OutlineColor;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // محاسبه ضخامت بر اساس وضوح texture
                float2 thickness = _MainTex_TexelSize.xy * _Thickness;
                
                // نمونه‌برداری از پیکسل‌های اطراف
                fixed4 center = tex2D(_MainTex, i.uv);
                fixed4 up = tex2D(_MainTex, i.uv + float2(0, thickness.y));
                fixed4 down = tex2D(_MainTex, i.uv - float2(0, thickness.y));
                fixed4 left = tex2D(_MainTex, i.uv - float2(thickness.x, 0));
                fixed4 right = tex2D(_MainTex, i.uv + float2(thickness.x, 0));
                
                // محاسبه تفاوت رنگ‌ها
                float edge = abs(center.r - up.r);
                edge += abs(center.r - down.r);
                edge += abs(center.r - left.r);
                edge += abs(center.r - right.r);
                
                edge += abs(center.g - up.g);
                edge += abs(center.g - down.g);
                edge += abs(center.g - left.g);
                edge += abs(center.g - right.g);
                
                edge += abs(center.b - up.b);
                edge += abs(center.b - down.b);
                edge += abs(center.b - left.b);
                edge += abs(center.b - right.b);
                
                // نرمال‌سازی و اعمال حساسیت
                edge = saturate(edge * _Sensitivity * 10);
                
                // ترکیب رنگ اصلی با خطوط
                fixed4 result = lerp(center, _OutlineColor, edge);
                return result;
            }
            ENDCG
        }
    }
    
    FallBack "Mobile/Diffuse"
}