Shader "Custom/NormalVisualizer"
{
    Properties
    {
        [KeywordEnum(World, Object, View)] _Space("Normal Space", Float) = 0
    }

    SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque" 
            "Queue" = "Geometry"
        }

        Pass
        {
            Name "FORWARD"
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            // Multi-compile keyword for space switching in the Inspector
            #pragma shader_feature_local _SPACE_WORLD _SPACE_OBJECT _SPACE_VIEW

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos      : SV_POSITION;
                float3 normalWS : TEXCOORD0;
                float3 normalOS : TEXCOORD1;
                float3 normalVS : TEXCOORD2;
            };

            v2f vert (appdata v)
            {
                v2f o;

                // Transform Object Space position to Clip Space
                o.pos = UnityObjectToClipPos(v.vertex);

                // 1. Object Space Normal
                o.normalOS = v.normal;

                // 2. World Space Normal (using built-in matrix helper)
                o.normalWS = UnityObjectToWorldNormal(v.normal);

                // 3. View Space Normal (Camera Space)
                o.normalVS = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 N = float3(0, 0, 0);

                #if defined(_SPACE_OBJECT)
                    N = normalize(i.normalOS);
                #elif defined(_SPACE_VIEW)
                    N = normalize(i.normalVS);
                #else
                    // Default to World Space
                    N = normalize(i.normalWS);
                #endif

                // Remap [-1, 1] vector components to [0.0, 1.0] RGB color space
                float3 normalColor = N * 0.5 + 0.5;

                return fixed4(normalColor, 1.0);
            }
            ENDCG
        }
    }
}