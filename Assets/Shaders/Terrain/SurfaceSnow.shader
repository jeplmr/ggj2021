Shader "Custom/Moss"
{
    Properties
    {
        _MainColor ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Bump ("Bump", 2D) =  "bump" {}
        _Moss ("Level of moss", Range(-1, 1)) = 1
        _MossColor ("Color of Moss", Color) = (1.0, 1.0, 1.0, 1.0)
        _MossDirection ("Direction of Moss", Vector) = (0, 1, 0)
        _MossDepth ("Depth of Moss", Range(0, 1))  = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _Bump; 
        float _Moss; 
        float4 _MossColor; 
        float4 _MainColor; 
        float4 _MossDirection; 
        float _MossDepth; 

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_Bump; 
            float3 worldNormal; 
            INTERNAL_DATA
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
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Normal = UnpackNormal (tex2D(_Bump, IN.uv_Bump));

            if (dot(WorldNormalVector(IN, o.Normal), 
                _MossDirection.xyz) >= _Moss)
                o.Albedo = _MossColor.rgb; 
            else 
                o.Albedo = c.rgb * _MainColor;
            o.Alpha = 1; 
        }

        void vert (inout appdata_full v){
            float4 sn = mul(UNITY_MATRIX_IT_MV, _MossDirection);
            if (dot(v.normal, sn.xyz) >= _Moss)
                v.vertex.xyz += (sn.xyz + v.normal) * _MossDepth * _Moss;  
        }

        ENDCG
    }
    FallBack "Diffuse"
}
