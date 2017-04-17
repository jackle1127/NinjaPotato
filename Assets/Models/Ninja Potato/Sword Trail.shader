Shader "Custom/SwordTrail" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader {
		Tags {"Queue"="Transparent" "RenderType"="Fade" }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		ZWrite Off

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 textureColor = tex2D(_MainTex, IN.uv_MainTex);
			float alpha = textureColor.a * (textureColor.r + textureColor.g + textureColor.b) / 3;
			alpha *= alpha;
 			o.Emission = textureColor.rgb;
			o.Alpha = alpha;
			o.Albedo = textureColor;
			o.Metallic = 0;
			o.Smoothness = 0;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
