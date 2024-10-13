Shader "Custom/AdditiveGlowWithTexture"
{
    Properties
    {
        _MainTex ("Base Texture (RGB)", 2D) = "white" {}  // PNG 图像的主纹理
        _GlowTex ("Glow Texture (RGB)", 2D) = "white" {}  // 发光纹理
        _Color ("Color", Color) = (1,1,1,1)  // 光的颜色
        _GlowIntensity ("Glow Intensity", Range(0, 10)) = 1  // 发光强度
        _Radius ("Radius", Range(0, 1)) = 0.5  // 发光区域的半径
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha One  // 保持图像的透明度并使用加法混合模式来叠加光效
        ZWrite Off
        Cull Off
        Lighting Off  // 禁用光照

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;   // 主图像纹理（PNG图像）
            sampler2D _GlowTex;   // 光效纹理
            float4 _Color;        // 光的颜色
            float _GlowIntensity; // 光效的强度
            float _Radius;        // 光效的半径

            // 顶点着色器
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            // 片段着色器
            fixed4 frag (v2f i) : SV_Target
            {
                // 获取主图像的颜色
                fixed4 baseColor = tex2D(_MainTex, i.texcoord);

                // 获取光效的纹理颜色
                fixed4 glowColor = tex2D(_GlowTex, i.texcoord) * _Color;

                // 计算该像素到UV中心点 (0.5, 0.5) 的距离，用于径向渐变
                float dist = distance(i.texcoord, float2(0.5, 0.5));

                // 使用 smoothstep 函数创建光的渐变效果
                float glow = (1.0 - smoothstep(_Radius - 0.1, _Radius, dist)) * _GlowIntensity;

                // 将光效的颜色与渐变强度相乘
                glowColor *= glow;

                // 最终颜色 = 主图像颜色 + 光效叠加 (基于加法混合)
                return baseColor + glowColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}