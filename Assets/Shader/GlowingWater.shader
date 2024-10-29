Shader "Custom/GlowingWater"
{
    Properties
    {
        _MainTex ("Base Texture (RGB)", 2D) = "white" {}  // 基础纹理
        _GlowTex ("Glow Texture (RGB)", 2D) = "white" {}  // 发光纹理
        _Color ("Glow Color", Color) = (1, 1, 1, 1)  // 发光颜色
        _GlowIntensity ("Glow Intensity", Range(0, 10)) = 1  // 发光强度
        _Radius ("Glow Radius", Range(0, 1)) = 0.5  // 发光半径
        _Scale("Wave Scale", Range(0, 50)) = 1  // 水波纹缩放
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha  // 处理透明度
        ZWrite Off  // 不写入深度缓冲区
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
                float2 uv : TEXCOORD0;  // 使用正确的UV坐标
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;  // 确保UV坐标传递正确
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;   // 基础纹理
            sampler2D _GlowTex;   // 发光纹理
            float4 _Color;        // 发光颜色
            float _GlowIntensity; // 发光强度
            float _Radius;        // 发光半径
            float _Scale;         // 水波纹缩放

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;  // 使用正确的UV坐标
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 获取主图像的颜色
                fixed4 baseColor = tex2D(_MainTex, i.uv);
                // 获取光效的纹理颜色
                fixed4 glowColor = tex2D(_GlowTex, i.uv) * _Color;

                // 计算到UV中心点(0.5, 0.5)的距离
                float dist = distance(i.uv, float2(0.5, 0.5));

                // 创建发光渐变
                float glow = (1.0 - smoothstep(_Radius - 0.1, _Radius, dist)) * _GlowIntensity;
                glowColor *= glow;

                // 动态水波效果
                float2 uv = i.uv;
                uv.x += 0.01 * sin(uv.x * 3.14 * _Scale + _Time.y);
                uv.y += 0.01 * sin(uv.y * 3.14 * _Scale + _Time.y);

                // 获取波动后的颜色
                fixed4 waveColor = tex2D(_MainTex, uv);

                // 最终颜色 = 基础颜色 + 发光叠加
                return baseColor + glowColor + waveColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
