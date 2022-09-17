Shader "Custom/URP_BASE"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BaseColor ("BaseColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { 
            "RenderPipeline"="UniversalRenderPipline"
            "RenderType"="Opaque" 
        }
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        
        CBUFFER_START(UnityMaterial)
        float4 _MainTex_ST;
        float4 _BaseColor;
        CBUFFER_END
 
        TEXTURE2D (_MainTex);
        SAMPLER(sampler_MainTex);

        float4 MainTex (float2 uv)
        {
            return SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,uv);
        }
 
 
 
 
 
        struct appdata
        {
            float4 vertex:POSITION;
            float3 normal:NORMAL;
            float2 uv : TEXCOORD0;
        };
 
        struct v2f
        {
            float4 vertex:SV_POSITION;
            float3 normal:NORMAL;
            float2 uv:TEXCOORD0;
        };
 
 
        ENDHLSL
        pass{
 
            HLSLPROGRAM
            #pragma vertex VERT
            #pragma fragment FRAG
 
            v2f VERT( appdata i)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(i.vertex.xyz);
                o.uv = TRANSFORM_TEX(i.uv, _MainTex);
                return o;
            }
 
            float4 FRAG(v2f i):SV_TARGET
            {

                float4 Var_MainTex =  MainTex(i.uv);
                float4 Albedo = Var_MainTex * _BaseColor;
                return Albedo;

            }
 
            ENDHLSL 
        }
    }
    //    FallBack "Diffuse"
}
