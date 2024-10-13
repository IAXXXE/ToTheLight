Shader "Custom/CircleAdditiveLightSphereShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}  // 主纹理，可以使用一个简单的圆形渐变纹理
        _Color ("Color", Color) = (1,1,1,1)  // 光球颜色
        _Glow ("Glow Intensity", Range(0, 10)) = 1  // 发光强度
        _Radius ("Radius", Range(0, 1)) = 0.5  // 光球的半径
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend One One  // 加法混合模式，叠加颜色
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
            float _Radius;

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

                // 计算该像素到UV中心点 (0.5, 0.5) 的距离，模拟径向渐变
                float dist = distance(i.texcoord, float2(0.5, 0.5));

                // 使用smoothstep函数，模拟光球中心亮、边缘柔和的渐变
                float glow = (1.0 - smoothstep(_Radius - 0.1, _Radius, dist)) * _Glow;

                // 将颜色与发光渐变结合
                texColor *= glow;

                return texColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
