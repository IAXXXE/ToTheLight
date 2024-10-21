// Shader "Custom/RetroCRTShader"
// {
//     Properties
//     {
//         _MainTex ("Texture", 2D) = "white" {}       // 主纹理
//         _BlurAmount ("Blur Amount", Range(0, 1)) = 0.5 // 模糊强度
//         _ScanlineAmount ("Scanline Amount", Range(0, 1)) = 0.5 // 扫描线强度
//     }
//     SubShader
//     {
//         Tags { "RenderType" = "Opaque" }
//         Pass
//         {
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #include "UnityCG.cginc"
 
//             sampler2D _MainTex;
//             float4 _MainTex_TexelSize;
//             float _BlurAmount;
//             float _ScanlineAmount;

//             struct v2f
//             {
//                 float4 pos : SV_POSITION;
//                 float2 uv : TEXCOORD0;
//             };

//             // 顶点函数
//             v2f vert(appdata_full v)
//             {
//                 v2f o;
//                 o.pos = UnityObjectToClipPos(v.vertex);
//                 o.uv = v.texcoord;
//                 return o;
//             }

//             // Fragment function 实现复古模糊和扫描线效果
//             fixed4 frag(v2f i) : SV_Target
//             {
//                 // 获取当前像素颜色
//                 fixed4 color = tex2D(_MainTex, i.uv);

//                 // 模拟 CRT 扫描线效果
//                 float scanline = sin(i.uv.y * _ScreenParams.y * 2.0 * 3.14159 * 3.0) * 0.5 + 0.5;
//                 color.rgb *= lerp(1.0, scanline, _ScanlineAmount);

//                 // 模糊效果：采样相邻像素
//                 float2 offset1 = float2(_MainTex_TexelSize.x, 0.0);
//                 float2 offset2 = float2(0.0, _MainTex_TexelSize.y);

//                 fixed4 blurColor = tex2D(_MainTex, i.uv + offset1) * 0.25 +
//                                    tex2D(_MainTex, i.uv - offset1) * 0.25 +
//                                    tex2D(_MainTex, i.uv + offset2) * 0.25 +
//                                    tex2D(_MainTex, i.uv - offset2) * 0.25;

//                 // 根据模糊强度进行插值
//                 color = lerp(color, blurColor, _BlurAmount);

//                 return color;
//             }
//             ENDCG
//         }
//     }
//     FallBack "Diffuse"
// }


// Shader "Custom/StrongBlurCRTShader"
// {
//     Properties
//     {
//         _MainTex ("Texture", 2D) = "white" {}
//         _BlurSize ("Blur Size", Float) = 1.0
//         _BlurStrength ("Blur Strength", Range(1, 10)) = 3.0  // 模糊强度
//     }

//     SubShader
//     {
//         Tags { "RenderType" = "Opaque" }
//         Pass
//         {
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #include "UnityCG.cginc"

//             sampler2D _MainTex;
//             float4 _MainTex_TexelSize;
//             float _BlurSize;
//             float _BlurStrength;

//             struct v2f
//             {
//                 float4 pos : SV_POSITION;
//                 float2 uv : TEXCOORD0;
//             };

//             v2f vert(appdata_img v)
//             {
//                 v2f o;
//                 o.pos = UnityObjectToClipPos(v.vertex);
//                 o.uv = v.texcoord;
//                 return o;
//             }

//             // 模糊算法 - 通过增加模糊强度和采样范围来增强模糊
//             fixed4 frag(v2f i) : SV_Target
//             {
//                 float2 texelSize = _MainTex_TexelSize.xy;
//                 float2 uv = i.uv;

//                 // 模糊采样点和权重
//                 float weight[5] = {0.4026, 0.2442, 0.0545, 0.0325, 0.0085};
//                 float2 offset[5] = {
//                     float2(0.0, 0.0),
//                     float2(texelSize.x * _BlurSize * _BlurStrength, 0.0),
//                     float2(-texelSize.x * _BlurSize * _BlurStrength, 0.0),
//                     float2(0.0, texelSize.y * _BlurSize * _BlurStrength),
//                     float2(0.0, -texelSize.y * _BlurSize * _BlurStrength)
//                 };

//                 fixed3 blurredColor = tex2D(_MainTex, uv + offset[0]).rgb * weight[0];

//                 // 遍历采样点，增强模糊
//                 for (int j = 1; j < 5; j++)
//                 {
//                     blurredColor += tex2D(_MainTex, uv + offset[j]).rgb * weight[j];
//                     blurredColor += tex2D(_MainTex, uv - offset[j]).rgb * weight[j];
//                 }

//                 return fixed4(blurredColor, 1.0);
//             }

//             ENDCG
//         }
//     }
// }


Shader "Custom/RetroPixelatedCRTShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 1.0
        _BlurStrength ("Blur Strength", Range(1, 10)) = 3.0  // 模糊强度
        _PixelSize ("Pixel Size", Float) = 10.0  // 像素化强度，数值越大，像素化越明显
        _NoiseStrength ("Noise Strength", Range(0, 1)) = 0.2  // 噪声强度
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BlurSize;
            float _BlurStrength;
            float _PixelSize;
            float _NoiseStrength;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_img v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            // 生成噪声
            float random(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            // 模糊和像素化算法
            fixed4 frag(v2f i) : SV_Target
            {
                // 模糊采样点和权重
                float weight[5] = {0.4026, 0.2442, 0.0545, 0.0325, 0.0085};
                float2 texelSize = _MainTex_TexelSize.xy;
                float2 uv = i.uv;

                // 像素化效果 - 将UV按PixelSize缩小，之后再还原
                uv = floor(uv * _PixelSize) / _PixelSize;

                // 模糊
                float2 offset[5] = {
                    float2(0.0, 0.0),
                    float2(texelSize.x * _BlurSize * _BlurStrength, 0.0),
                    float2(-texelSize.x * _BlurSize * _BlurStrength, 0.0),
                    float2(0.0, texelSize.y * _BlurSize * _BlurStrength),
                    float2(0.0, -texelSize.y * _BlurSize * _BlurStrength)
                };

                fixed3 blurredColor = tex2D(_MainTex, uv + offset[0]).rgb * weight[0];
                for (int j = 1; j < 5; j++)
                {
                    blurredColor += tex2D(_MainTex, uv + offset[j]).rgb * weight[j];
                    blurredColor += tex2D(_MainTex, uv - offset[j]).rgb * weight[j];
                }

                // 噪声效果 - 添加基于UV的随机噪声
                float noise = (random(i.uv) - 0.5) * _NoiseStrength;
                blurredColor += noise;

                return fixed4(blurredColor, 1.0);
            }

            ENDCG
        }
    }
}
