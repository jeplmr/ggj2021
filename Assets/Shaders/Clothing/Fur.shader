Shader "Custom/Fur"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _FurLength("Fur Length", Range(.0002, 1)) = 0.25
        _Cutoff("Alpha Cutoff", Range(0, 1)) = 0.5
        _CutoffEnd("Alpha Cutoff End", Range(0, 1)) = 0.5
        _EdgeFade("Edge Face",  Range(0, 1)) = 0.4
        _Gravity("Gravity Direction", Vector) = (0, 0, 1, 0)
        _GravityStrength("Gravity Strength", Range(0, 1)) = 0.25
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a; 
        }
        ENDCG

        //1
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
        #define FUR_MULTIPLIER 0.05
        #include "FurPass.cginc"
        ENDCG
        //2
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
        #define FUR_MULTIPLIER 0.15
        #include "FurPass.cginc"
        ENDCG
        //3
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
        #define FUR_MULTIPLIER 0.25
        #include "FurPass.cginc"
        ENDCG
        //4
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
        #define FUR_MULTIPLIER 0.35
        #include "FurPass.cginc"
        ENDCG
        //5
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
        #define FUR_MULTIPLIER 0.45
        #include "FurPass.cginc"
        ENDCG
        //6
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
        #define FUR_MULTIPLIER 0.55
        #include "FurPass.cginc"
        ENDCG
        //7
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
        #define FUR_MULTIPLIER 0.65
        #include "FurPass.cginc"
        ENDCG
        //8
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
        #define FUR_MULTIPLIER 0.75
        #include "FurPass.cginc"
        ENDCG
        //9
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
        #define FUR_MULTIPLIER 0.85
        #include "FurPass.cginc"
        ENDCG
        //10
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
        #define FUR_MULTIPLIER 0.95
        #include "FurPass.cginc"
        ENDCG

    }
    FallBack "Diffuse"
}
