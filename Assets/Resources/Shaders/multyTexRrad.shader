// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/MultiDiffuseRnd" {
Properties {


                                _Tex1  ("Tex1", 2D) = "white" {}
                                _Tex2  ("Tex2", 2D) = "white" {}
                                _Tex3  ("Tex3", 2D) = "white" {}
                                _Tex4  ("Tex4", 2D) = "white" {}
                                _Tex5  ("Base Tex", 2D) = "white" {}
                                _Noise  ("NoiseTex", 2D) = "white" {}
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 200

CGPROGRAM
#pragma surface surf Lambert
 #include "UnityCG.cginc"

                sampler2D _Tex1;
                sampler2D _Tex2;
                sampler2D _Tex3;
                sampler2D _Tex4;
                sampler2D _Tex5;



struct Input {
    float2 uv_Tex1;
};
        float rand2 ( float2 coords ){
            return frac(sin(dot(coords, float2(12.9898,78.233))) * 43758.5453);
        }

void surf (Input IN, inout SurfaceOutput o) {
    // generate random value based on UV mapping
            float x = (round(rand2(floor(IN.uv_Tex1))*9))/1;
            int mask[10] = {0,0,0,0,0,0,0,0,0,0};
            mask[x] = 1;
            if (x>4) mask[4]=1;

    // float g =  rand2( IN.uv_Tex1.xy );
    
     
                                fixed4 tex1 = tex2D (_Tex1, IN.uv_Tex1);
                                
                                fixed4 tex2 = tex2D (_Tex2, IN.uv_Tex1);
                                
                                fixed4 tex3 = tex2D (_Tex3, IN.uv_Tex1);
                                
                               fixed4 tex4 = tex2D (_Tex4, IN.uv_Tex1);
                                fixed4 tex5 = tex2D (_Tex5, IN.uv_Tex1);


                    o.Albedo = float3(0,0,0);
                o.Albedo = lerp(o.Albedo, tex1.rgb, mask[0]);
                o.Albedo = lerp(o.Albedo, tex2.rgb, mask[1]);
                o.Albedo = lerp(o.Albedo, tex3.rgb, mask[2]);
                o.Albedo = lerp(o.Albedo, tex4.rgb, mask[3]);
                o.Albedo = lerp(o.Albedo, tex5.rgb, mask[4]);
            //   o.Albedo = lerp(o.Albedo, float3(0.08,0.08,0.08), mask[0]);
            //     o.Albedo = lerp(o.Albedo, float3(0.18,0.18,0.18), mask[1]);
            //     o.Albedo = lerp(o.Albedo, float3(0.28,0.28,0.28), mask[2]);
            //     o.Albedo = lerp(o.Albedo, float3(0.38,0.38,0.38), mask[3]);
            //     o.Albedo = lerp(o.Albedo, float3(0.98,0.98,0.98), mask[4]);

    o.Alpha = 1;
}
ENDCG
}

Fallback "Legacy Shaders/VertexLit"
}
