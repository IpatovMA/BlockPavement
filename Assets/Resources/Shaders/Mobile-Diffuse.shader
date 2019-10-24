// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Mobile/qqq" {
             Properties 
                 {
                _MainTex ("Base (RGB)", 2D) = "white" {}
                                _Tex1  ("Detail (RGB)", 2D) = "white" {}
                                _Tex2  ("Detail (RGB)", 2D) = "white" {}
                                _Tex3  ("Detail (RGB)", 2D) = "white" {}
                                _Tex4  ("Detail (RGB)", 2D) = "white" {}
                                _Tex5  ("Detail (RGB)", 2D) = "white" {}
                                _Tex6  ("Detail (RGB)", 2D) = "white" {}
         }

        SubShader {
                Lighting Off
                ZWrite Off
                ZTest Less  
                Blend SrcAlpha OneMinusSrcAlpha
                Tags {"Queue"="Transparent"}
        
        CGPROGRAM
        #pragma surface surf NoLighting alpha

        sampler2D _MainTex;
                sampler2D _Tex1;
                sampler2D _Tex2;
                sampler2D _Tex3;
                sampler2D _Tex4;
                sampler2D _Tex5;
                sampler2D _Tex6;

        struct Input 
                {
                float2 uv_MainTex;
                                float3 color: Color; // Vertex color
        };

        void surf (Input IN, inout SurfaceOutput o) 
                {
                        
                                float4 tex;
                                if(IN.color.x>0.51) tex = tex2D (_Tex6, IN.uv_MainTex);
                                else 
                                if(IN.color.x>0.41) tex = tex2D (_Tex5, IN.uv_MainTex);
                                else 
                                if(IN.color.x>0.31) tex = tex2D (_Tex4, IN.uv_MainTex);
                                else 
                                if(IN.color.x>0.21) tex = tex2D (_Tex3, IN.uv_MainTex);
                                else 
                                if(IN.color.x>0.11) tex = tex2D (_Tex2, IN.uv_MainTex);
                                else 
                                if(IN.color.x>0  ) tex = tex2D (_Tex1, IN.uv_MainTex);
                                else tex = tex2D (_MainTex, IN.uv_MainTex);

                o.Albedo = tex.rgb;
                o.Alpha = tex.a;
        }

                fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
                {
                        fixed4 c;
                        c.rgb = s.Albedo; 
                        c.a = s.Alpha;
                        return c;
                }

        ENDCG
        }
}