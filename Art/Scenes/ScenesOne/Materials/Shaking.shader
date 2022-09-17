// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "RenaderPass/Shaking"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_OffsetX1("Offset X1", Range(-0.5, 0.5)) = 0.0
		_OffsetX2("Offset X2", Range(-0.5, 0.5)) = 0.0
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "RenderPipeline" = "UniversalRenderPipeline"}

		Pass
		{
		HLSLPROGRAM
	    #pragma vertex vert
	    #pragma fragment frag

	    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

		//sampler2D _MainTex;
		

		CBUFFER_START(UnityPerMaterial)
			float _OffsetX1;
		    float _OffsetX2;
		CBUFFER_END

		TEXTURE2D(_MainTex);
		SAMPLER(sampler_MainTex);

		struct appdata
		{
			float4 vertex : POSITION;
			half2 texcoord : TEXCOORD0;
		};

		struct v2f
		{
			float4 pos : SV_POSITION;
			half2 uv : TEXCOORD0;
		};

		v2f vert(appdata v)
		{
			v2f o = (v2f)0;
			o.pos = TransformObjectToHClip(v.vertex.xyz);
			o.uv = v.texcoord;
			return o;
		} 

		half4 frag(v2f i) : SV_Target
		{
		  _OffsetX1 = sin(_Time.y * 20) / 20;
		  _OffsetX2 = sin(_Time.y * 10) / 100;
		  half4 col = half4(1, 1, 1, 1);
		  half4 scol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(_OffsetX2, 0));
		  float2 uv = i.uv + float2(_OffsetX1, 0);


		  col.rgb = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv).rgb;


		  return col * 0.9 + scol * 0.5;
		}
		ENDHLSL
		}
	}
}


