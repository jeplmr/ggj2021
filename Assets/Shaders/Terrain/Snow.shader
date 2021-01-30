Shader "Custom/Snow"
{
    Properties
    {
        _Tess ("Tessellation", Range(1,256)) = 128
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _DispColor ("Displaced Color", Color) = (1,1,1,1)
        _DispTex ("Displaced Texture (RGB)", 2D) = "white" {}
        _Splat("Splat Texture", 2D) = "black" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0
        _GlossTex("Gloss Map (RGB)", 2D) = "black" {}
        _SpecularTex ("Specular", 2D) = "white" {}
        _NormalTex ("Normal", 2D) = "bump" {}
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Displacement ("Displacement", Range(0, 1.0)) = 0.3
        _Distance("Tesselation Distance", Range (25, 200)) = 25
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:disp tessellate:tessDistance

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 4.6

        #include "Tessellation.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            float _Tess;
            half _Distance; 

            float4 tessDistance (appdata v0, appdata v1, appdata v2) {
                float minDist = 10.0;
                float maxDist = _Distance;
                return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, minDist, maxDist, _Tess);
            }

            sampler2D _Splat;
            float _Displacement;

            void disp (inout appdata v)
            {
                float d = tex2Dlod(_Splat, float4(v.texcoord.xy,0,0)).r * _Displacement;
                v.vertex.xyz -= v.normal * d;
                v.vertex.xyz += v.normal * _Displacement; 
            }

        sampler2D _MainTex;
        fixed4 _Color;
        sampler2D _DispTex;
        fixed4 _DispColor;
        sampler2D _GlossTex; 
        sampler2D _SpecularTex;
        sampler2D _NormalTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_DispTex;
            float2 uv_Splat; 
            float2 uv_GlossTex; 
            float2 uv_SpecularTex; 
        };

        half _Glossiness;
        half _Metallic;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            half amount = tex2Dlod(_Splat, float4(IN.uv_Splat,0,0)).r; 
            fixed4 c = lerp(tex2D (_MainTex, IN.uv_MainTex) * _Color, tex2D (_DispTex, IN.uv_DispTex) * _DispColor, amount);
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = tex2D (_GlossTex, IN.uv_GlossTex) * _Glossiness;
            //o.Specular = (tex2D (_SpecularTex, IN.uv_SpecularTex));
            //o.Normal = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
