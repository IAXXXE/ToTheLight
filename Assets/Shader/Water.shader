// Shader "Custom/WaterSurface"
// {
//     Properties
//     {
//         _MainTex ("Texture", 2D) = "white" {} // 主要纹理
//         _WaveSpeed ("Wave Speed", Range(0, 10)) = 1.0 // 波动速度
//         _WaveFrequency ("Wave Frequency", Range(0.1, 10)) = 2.0 // 波动频率
//         _WaveAmplitude ("Wave Amplitude", Range(0, 0.5)) = 0.1 // 波动幅度
//     }
//     SubShader
//     {
//         Tags { "RenderType" = "Transparent" }
//         LOD 100

//         Pass
//         {
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #include "UnityCG.cginc"

//             struct appdata_t
//             {
//                 float4 vertex : POSITION;
//                 float2 uv : TEXCOORD0; // 正确的UV坐标
//             };

//             struct v2f
//             {
//                 float4 pos : SV_POSITION;
//                 float2 uv : TEXCOORD0; // 传递UV坐标
//             };

//             sampler2D _MainTex; // 主要纹理
//             float _WaveSpeed; // 波动速度
//             float _WaveFrequency; // 波动频率
//             float _WaveAmplitude; // 波动幅度

//             v2f vert (appdata_t v)
//             {
//                 v2f o;
//                 o.pos = UnityObjectToClipPos(v.vertex);
//                 o.uv = v.uv; // 传递UV坐标
//                 return o;
//             }

//             fixed4 frag (v2f i) : SV_Target
//             {
//                 // 计算波动效果
//                 float wave = sin(i.uv.x * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveAmplitude;

//                 // 调整纹理坐标
//                 float2 uv = i.uv + float2(0, wave); // 仅在y方向上波动

//                 // 采样纹理
//                 fixed4 col = tex2D(_MainTex, uv);
//                 return col;
//             }
//             ENDCG
//         }
//     }
//     FallBack "Diffuse"
// }

Shader "Custom/Water1" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Scale("Scale",Range(0,50))=1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }

        Blend SrcAlpha OneMinusSrcAlpha // 处理透明度
        ZWrite Off // 不写入深度缓冲区

		Pass{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "Lighting.cginc"

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float _Scale;

				struct a2v{
					float4 pos:POSITION;
					float4 uv:TEXCOORD0;
				};

				struct v2f{
					float4 pos_VS:SV_POSITION;
					float2 uv:TEXCOORD0;
				};

				v2f vert(a2v v){
					v2f o;
					o.pos_VS = UnityObjectToClipPos(v.pos);
					o.uv=TRANSFORM_TEX(v.uv,_MainTex);
					return o;
				}

				float4 frag(v2f o):SV_TARGET{
					float2 uv = o.uv;
					o.uv.x+= 0.01*sin(uv.x*3.14*_Scale+_Time.y);
					o.uv.y+= 0.01*sin(uv.y*3.14*_Scale+_Time.y);
					fixed4 color = tex2D(_MainTex,o.uv);
                    color.a = saturate(color.a);
					return color;
				}
			ENDCG
		}
	}
}

