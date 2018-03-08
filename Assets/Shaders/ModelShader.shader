// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/ModelShader" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    _MeshRadius ("Mesh Radius", Float) = 1
    _CutHeight ("Cut height", Range(0,1)) = 0
    _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    _FadeHeight("Fade height", Range(0,1)) = 0
    _FadeColor("Fade color", Color) = (0,0,0,0)
}

SubShader {
    Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
    LOD 200

CGPROGRAM
#pragma surface surf Lambert alphatest:_Cutoff

sampler2D _MainTex;
fixed4 _Color;
half _CutHeight;
uniform float _MeshRadius;
half _FadeHeight;
fixed4 _FadeColor;

struct Input {
    float2 uv_MainTex;
    float3 worldPos;
};

void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
    if(IN.worldPos.y < (_MeshRadius - _MeshRadius * _CutHeight)) {
        o.Alpha = 1;

        if(IN.worldPos.y > (_MeshRadius - _MeshRadius * _CutHeight) - _FadeHeight) {
            c = _FadeColor * 3;
        }
    } else {
        o.Alpha = 0;
    }
    o.Albedo = c.rgb;
}
ENDCG
}

Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}
