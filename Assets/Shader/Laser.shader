// Shader "Custom/AdditiveLaserBeamWithGradient"
// {
//     Properties
//     {
//         _MainTex ("Main Texture", 2D) = "white" {}  // 主纹理
//         _Color ("Color", Color) = (1,1,1,1)         // 基础颜色
//         _Intensity ("Intensity", Float) = 1.0       // 光束强度
//         _GradientStart ("Gradient Start", Float) = 0.8  // 渐变开始点
//         _GradientEnd ("Gradient End", Float) = 1.0  // 渐变结束点
//     }

//     SubShader
//     {
//         Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
//         LOD 200

//         Blend SrcAlpha One
//         ZWrite Off

//         Pass
//         {
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #include "UnityCG.cginc"

//             struct appdata
//             {
//                 float4 vertex : POSITION;
//                 float2 uv : TEXCOORD0;
//             };

//             struct v2f
//             {
//                 float4 pos : SV_POSITION;
//                 float2 uv : TEXCOORD0;
//             };

//             sampler2D _MainTex;
//             float4 _Color;
//             float _Intensity;
//             float _GradientStart;
//             float _GradientEnd;

//             v2f vert(appdata v)
//             {
//                 v2f o;
//                 o.pos = UnityObjectToClipPos(v.vertex);
//                 o.uv = v.uv;
//                 return o;
//             }

//             fixed4 frag(v2f i) : SV_Target
//             {
//                 // 从纹理中采样颜色
//                 fixed4 texColor = tex2D(_MainTex, i.uv);

//                 // 计算渐变的权重（根据 uv.y 的值来实现上下方向的渐变）
//                 float gradientFactor = smoothstep(_GradientStart, _GradientEnd, i.uv.y);

//                 // 激光的颜色（乘以渐变因子）
//                 fixed4 laserColor = _Color * _Intensity * gradientFactor;

//                 // 结合纹理颜色与激光颜色
//                 return texColor * laserColor;
//             }
//             ENDCG
//         }
//     }
//     FallBack "Transparent/Diffuse"
// }
Shader "Custom/AdditiveLaserBeamWithVerticalGradient"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}  // 主纹理
        _Color ("Color", Color) = (1,1,1,1)         // 基础颜色
        _Intensity ("Intensity", Float) = 1.0       // 光束强度
        _GradientStart ("Gradient Start", Float) = 0.8  // 渐变开始点
        _GradientEnd ("Gradient End", Float) = 1.0  // 渐变结束点
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        Blend SrcAlpha One
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;  // UV 坐标
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _Intensity;
            float _GradientStart;
            float _GradientEnd;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;  // 传递 UV 坐标
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 从纹理中采样颜色
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // 计算垂直方向的渐变（根据 uv.y 值来实现上下两侧的渐变）
                float verticalGradient = smoothstep(_GradientStart, _GradientEnd, abs(i.uv.y - 0.5) * 2.0);

                // 使用渐变因子调整激光颜色
                fixed4 laserColor = _Color * _Intensity * verticalGradient;

                // 结合纹理颜色与激光颜色
                return texColor * laserColor;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}
