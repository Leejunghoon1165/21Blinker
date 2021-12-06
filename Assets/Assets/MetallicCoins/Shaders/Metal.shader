// ------------------------------------------------------------------------------------------------------------
// 							 Metal.shader
//  	Authors: Leonardo M. Lopes <euleoo@gmail.com> - http://about.me/leonardo_lopes
// ------------------------------------------------------------------------------------------------------------

Shader "Custom/Metal" {
Properties {
	_Color ("Main Color", Color) = (.5,.5,.5,.5)
	_MainTex ("Base (RGB) Reflection Str/Gloss (A)", 2D) = "white" {}
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_RimColor ("Rim Color", Color) = (1,1,1,0)
    _RimPower ("Rim Power", Range(0.5,8.0)) = 8.0
    _ReflectColor ("Reflection Color", Color) = (.5,.5,.5,.5)
	_Cube ("Reflection Cubemap", Cube) = "" {}
}
//Shader Model 3 target
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 300
	
CGPROGRAM
	#pragma surface surf BlinnPhong
	#pragma target 3.0
	fixed4 _Color, _ReflectColor, _RimColor;
	sampler2D _MainTex, _BumpMap;
	half _Shininess;
	float _RimPower;
	samplerCUBE _Cube;

	struct Input {
		float2 uv_MainTex;
		float2 uv_BumpMap;
		float3 viewDir;
		float3 worldRefl;
		INTERNAL_DATA
	};

	void surf (Input IN, inout SurfaceOutput o) {					
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = c.rgb * _Color;
		
		o.Gloss = c.a;
		o.Specular = _Shininess;
		
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		
		fixed rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
		fixed rimPowered = pow (rim, _RimPower);
		
		
		float3 worldRefl = WorldReflectionVector (IN, o.Normal);
		fixed4 reflcol = texCUBE (_Cube, worldRefl);
		
		reflcol *= c.a;
		o.Emission = (_RimColor.rgb * rimPowered) + (reflcol.rgb * _ReflectColor.rgb);
		o.Alpha = reflcol.a * _ReflectColor.a;
	}
ENDCG
}

Fallback "Reflective/Bumped Specular"
}
