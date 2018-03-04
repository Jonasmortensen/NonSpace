Shader "Custom/Scanline" {
	Properties {
		_Color1 ("Color 1", Color) = (1,1,1,1)
		_Width ("Width", Range(0,1)) = 0.1
		_Emission("Emission", Color) = (0,0,0,0)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert alpha


		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Width;
		fixed4 _Emission;
		fixed4 _Color1;


		void surf (Input IN, inout SurfaceOutput o) {
			if (uint((IN.worldPos.y + 10) * _Width) % 2) {
				o.Albedo = _Color1.rgb;
				o.Alpha = 0;
			}
			else {
				o.Albedo = _Color1.rgb;
				o.Alpha = 1;
				o.Emission = _Emission;
			}
		}
		ENDCG
	}
    Fallback "Transparent/VertexLit"
}
