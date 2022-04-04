Shader "Unlit/Space"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Zoom("Zoom", Range(0.1, 10)) = 1.8

        _Brightness("Brightness", Range(0.0, 0.05)) = 0.0015
        _DistFading("Fading", Range(0.1, 1)) = 0.73
        _Speed("Speed", Range(0.0, 1)) = 0.73
        _Volume("Volume", Range(1, 25)) = 15
        _Iterations("Iterations", Range(1, 25)) = 15
        _DirectionX("DirectionX", Range(0, 125)) = 0.5
        _DirectionY("DirectionY", Range(0, 125)) = 0.5
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
            // make fog work
            #pragma multi_compile_fog

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

            float _Zoom;
            float _Speed;
            float _Brightness;
            float _Volume;
            float _Iterations;
            float _DirectionX;
            float _DirectionY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.y *= + _ScreenParams.y / _ScreenParams.x;
                return o;
            }

            float3 colorByDistance(float distance) {
                return float3(distance, distance * distance,
                    pow(distance, 4));
            }

            float mod(float x, float y) 
            {
                return x - y * floor(x / y);
            }

            float3 mod(float3 x, float3 y)
            {
                return x - y * floor(x / y);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 pos = float3(i.uv * _Zoom, 1);

                float time = mod(_Time.y * _Speed, 3600);
                float3 dir = float3(_DirectionX, _DirectionY, 0);

                float3 color = 0;
                float distance = 0.1;
                float fade = 1.0;

                for (int d = 0; d < _Volume; d++) 
                {
                    float3 p = pos * distance + dir;

                    float a = 0, prev_a = 0;
                    for (int i = 0; i < _Iterations; i++) {
                        p = abs(p) / dot(p, p) - 0.71;

                        a += abs(length(p) - prev_a);
                        prev_a = length(p);
                    }

                    a = pow(a, 3);
                    color += colorByDistance(distance) * a * _Brightness * fade;
                    distance += 0.15;
                    fade *= 0.7;
                }

                color *= 0.001;
                return float4(color, 1.0);
            }
            ENDCG
        }
    }
}
