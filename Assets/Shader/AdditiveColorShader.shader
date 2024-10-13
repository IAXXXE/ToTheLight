// Shader "Custom/AdditiveColorShader"
// {
//     Properties
//     {
//         _MainTex ("Texture", 2D) = "white" {}
//         _Color ("Color", Color) = (1,1,1,1)
//     }
//     SubShader
//     {
//         Tags { "Queue"="Transparent" "RenderType"="Transparent" }
//         LOD 100

//         Blend One One  // 使用加法混合
//         ZWrite Off
//         Cull Off
//         Lighting Off

//         Pass
//         {
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag

//             #include "UnityCG.cginc"

//             struct appdata_t
//             {
//                 float4 vertex : POSITION;
//                 float2 texcoord : TEXCOORD0;
//             };

//             struct v2f
//             {
//                 float2 texcoord : TEXCOORD0;
//                 float4 vertex : SV_POSITION;
//             };

//             sampler2D _MainTex;
//             float4 _Color;

//             v2f vert (appdata_t v)
//             {
//                 v2f o;
//                 o.vertex = UnityObjectToClipPos(v.vertex);
//                 o.texcoord = v.texcoord;
//                 return o;
//             }

//             fixed4 frag (v2f i) : SV_Target
//             {
//                 fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
//                 return col;
//             }
//             ENDCG
//         }
//     }
//     FallBack "Diffuse"
// }

Shader "Custom/SoftAdditiveLightShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}  // 主纹理
        _Color ("Color", Color) = (1,1,1,1)  // 颜色
        _Glow ("Glow Intensity", Range(0, 10)) = 1  // 发光强度
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend One One  // 加法混合模式
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

            sampler2D _MainTex;
            float4 _Color;
            float _Glow;

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
                // 获取纹理颜色
                fixed4 texColor = tex2D(_MainTex, i.texcoord) * _Color;

                // 计算距离UV中心点的距离（径向渐变）
                float dist = distance(i.texcoord, float2(0.5, 0.5));

                // 渐变函数：中间亮，边缘柔和
                float glow = (1.0 - smoothstep(0.2, 0.8, dist)) * _Glow;

                // 将纹理颜色与渐变结合，产生边缘柔和的效果
                texColor *= glow;

                return texColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
