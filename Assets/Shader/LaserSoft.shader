Shader "Custom/LineRendererWithSoftEdge"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Intensity ("Intensity", Float) = 1.0
        _EdgeSoftness ("Edge Softness", Float) = 0.5
        _SoftStartDistance ("Soft Start Distance", Range(0, 0.5)) = 0.1 // 新增：柔化开始距离
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
            float _EdgeSoftness;
            float _SoftStartDistance;
            
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

                // 计算到边缘的距离
                float distanceToEdge = min(i.uv.y, 1.0 - i.uv.y);

                // 计算柔化因子
                float softnessRange = _EdgeSoftness - _SoftStartDistance;
                float edgeGradient = smoothstep(_SoftStartDistance, _EdgeSoftness, distanceToEdge);

                // 使用边缘柔化因子调整激光颜色
                fixed4 laserColor = _Color * _Intensity * edgeGradient;

                // 结合纹理颜色与激光颜色
                return texColor * laserColor;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}

// Shader "Custom/LineRendererWithSoftEdge"
// {
//     Properties
//     {
//         _MainTex ("Main Texture", 2D) = "white" {}  // 主纹理
//         _Color ("Color", Color) = (1,1,1,1)         // 基础颜色
//         _Intensity ("Intensity", Float) = 1.0       // 光束强度
//         _EdgeSoftness ("Edge Softness", Float) = 0.5 // 边缘柔化强度
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
//                 float2 uv : TEXCOORD0;  // UV 坐标
//             };

//             struct v2f
//             {
//                 float4 pos : SV_POSITION;
//                 float2 uv : TEXCOORD0;
//             };

//             sampler2D _MainTex;
//             float4 _Color;
//             float _Intensity;
//             float _EdgeSoftness;

//             v2f vert(appdata v)
//             {
//                 v2f o;
//                 o.pos = UnityObjectToClipPos(v.vertex);
//                 o.uv = v.uv;  // 传递 UV 坐标
//                 return o;
//             }

//             fixed4 frag(v2f i) : SV_Target
//             {
//                 // 从纹理中采样颜色
//                 fixed4 texColor = tex2D(_MainTex, i.uv);

//                 // 计算线条的边缘柔化（根据 uv.x 值来判断靠近边缘的距离）
//                 float edgeGradient = smoothstep(0.0, _EdgeSoftness, min(i.uv.y, 1.0 - i.uv.y));

//                 // 使用边缘柔化因子调整激光颜色
//                 fixed4 laserColor = _Color * _Intensity * edgeGradient;

//                 // 结合纹理颜色与激光颜色
//                 return texColor * laserColor;
//             }
//             ENDCG
//         }
//     }
//     FallBack "Transparent/Diffuse"
// }
