Shader "Unlit/451ShaderWithTexture"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			Cull Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
                float3 vertexWC : TEXCOORD3;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
            float4x4 MyXformMat;

            float4 LightPosition;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(MyXformMat, v.vertex);
                o.vertex = mul(UNITY_MATRIX_VP, o.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.vertexWC = mul(UNITY_MATRIX_M, v.vertex); // this is in WC space!
                // this is not pretty but we don't have access to inverse-transpose ...
                float3 p = v.vertex + v.normal;
                p = mul(UNITY_MATRIX_M, p);  // now in WC space
                o.normal = normalize(p - o.vertexWC); // NOTE: this is in the world space!!
				return o;
			}
			
            // our own function
            fixed4 ComputeDiffuse(v2f i) {
                float3 l = normalize(LightPosition - i.vertexWC);
                return clamp(dot(i.normal, l), 0, 1);
            }

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
                
                float diff = ComputeDiffuse(i);
                return col * diff;
			}

			ENDCG
		}
	}
}