// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/MultiDiffuseNoise" {
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
                sampler2D _Noise;



struct Input {
    float2 uv_Tex1;
    float2 uv_Noise;
};


void surf (Input IN, inout SurfaceOutput o) {
    // generate random value based on UV mapping
            


            half4 noise = tex2D(_Noise,IN.uv_Noise);

                                 float4 tex;

                                if(noise.r<=0.1) tex = tex2D (_Tex1, IN.uv_Tex1);
                                else 
                                if(noise.r<=0.2) tex = tex2D (_Tex2, IN.uv_Tex1);
                                else 
                                if(noise.r<=0.3) tex = tex2D (_Tex3, IN.uv_Tex1);
                                else 
                                if(noise.r<=0.4) tex = tex2D (_Tex4, IN.uv_Tex1);
                                else tex = tex2D (_Tex5, IN.uv_Tex1);
    


                    o.Albedo = tex.rgb;

    o.Alpha = 1;
}
ENDCG
}

Fallback "Legacy Shaders/VertexLit"
}
