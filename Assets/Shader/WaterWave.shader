Shader "Custom/WaterSurface"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveSpeed ("Wave Speed", Range(0, 10)) = 1.0
        _WaveFrequency ("Wave Frequency", Range(0.1, 10)) = 2.0
        _WaveAmplitude ("Wave Amplitude", Range(0, 0.5)) = 0.1
        _WaveDirection ("Wave Direction", Float) = 0.0 // 0 = Up/Down, 1 = Left/Right
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha // 处理透明度
        ZWrite Off // 不写入深度缓冲区

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _WaveSpeed;
            float _WaveFrequency;
            float _WaveAmplitude;
            float _WaveDirection; // 控制波动方向

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float wave;
                if (_WaveDirection == 0.0) // 上下波动
                {
                    wave = sin(i.uv.x * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveAmplitude;
                    float2 uv = i.uv + float2(0, wave); // 仅在y方向波动
                    fixed4 col = tex2D(_MainTex, uv);
                    col.a = saturate(col.a);
                    return col;
                }
                else if(_WaveDirection == 1.0)// 左右波动
                {
                    wave = sin(i.uv.y * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveAmplitude;
                    float2 uv = i.uv + float2(wave, 0); // 仅在x方向波动
                    fixed4 col = tex2D(_MainTex, uv);
                    col.a = saturate(col.a);
                    return col;
                }
                else
                {
                    float wave = sin(i.uv.x * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveAmplitude;
                    float2 uv = i.uv + float2(0, wave);
                    fixed4 col = tex2D(_MainTex, uv);
                    // 确保颜色的alpha值不会超出范围
                    col.a = saturate(col.a);
                    return col; // 返回处理后的颜色
                }
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
