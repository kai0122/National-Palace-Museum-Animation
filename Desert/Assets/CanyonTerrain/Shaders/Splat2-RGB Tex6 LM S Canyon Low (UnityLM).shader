// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "Serj Shaders/Splat2-RGB Tex6 LM S Canyon Low (UnityLM)" {
Properties {
	_Baked ("Baked Main", 2D) = "white" {}
	_Vert ("Vert Main", 2D) = "white" {}
	_VertDetail ("Vert Detail", 2D) = "white" {}
	_LM ("LM", 2D) = "white" {}
	_Control1 ("Control 1 (RGB)", 2D) = "white" {}

}
// ================= Simplified Shader ================

SubShader {

	//LOD 200

	Tags{
	    "Queue"="Geometry"
	    "IgnoreProjector"="True"
	    "RenderType"="Opaque"
	    }
	//Offset 0, 0    
    Pass {
    //Fog {Mode Off}
    //Fog { Color ( 0.5,0.45,0.31 ) }
    //Fog { Color ( 0.384,0.433,0.480 ) }
    Lighting Off
    //Tags {"LightMode" = "ForwardBase"}
    
    CGPROGRAM

	//#define SHADER_API_MOBILE
	//#define DIRECTIONAL

	#include "UnityCG.cginc"
	#include "AutoLight.cginc"
	//#include "Lighting.cginc"

	#pragma target 2.0
	#pragma glsl
	//#pragma glsl_no_auto_normalization
	//#pragma glsl_no_optimize
	//#pragma debug
	#pragma exclude_renderers xbox360 ps3 flash
	#pragma vertex vert
	#pragma fragment frag
	//#pragma fragmentoption ARB_precision_hint_nicest
	#pragma multi_compile_fwdbase

	sampler2D _Control1, _Baked, _LM, _Vert, _VertDetail, unity_Lightmap;
	// float4 unity_LightmapST;
	//fixed4 _fogColor_ST;

	struct v2f {
	    float4 pos : SV_POSITION;
	    float2 uv_Control: TEXCOORD0; 
		float2 uv_VertTex0: TEXCOORD1;
		float2 uv_VertTex90: TEXCOORD2;
		//float2 uv_VertTexFar0: TEXCOORD3;
		//float2 uv_VertTexFar90: TEXCOORD4; 
	};

	struct v_base {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
		//fixed4 color : COLOR0; 
	};

	// Decodes lightmaps:
	// - doubleLDR encoded on GLES
	// - RGBM encoded with range [0;8] on other platforms using surface shaders
	inline fixed4 DecodeLM( fixed4 color )
	{
	#if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) && defined(SHADER_API_MOBILE)
		return 2.0 * color;
	#else
		// potentially faster to do the scalar multiplication
		// in parenthesis for scalar GPUs
		return (8.0 * color.a) * color;
	#endif
	}

	v2f vert (v_base v)
	{
	    v2f o;
	    o.pos = UnityObjectToClipPos (v.vertex);   
	    
	    o.uv_VertTex0.x = v.texcoord.x * 10;		// тайлинг как в громе
	    o.uv_VertTex0.y = v.vertex.y * 0.004;	// тайлинг указываем как в громе / 1000
	    o.uv_VertTex90.x = v.texcoord.y * 10;
	    o.uv_VertTex90.y = v.vertex.y * 0.004;
	    
	    //o.uv_VertTexFar0 = o.uv_VertTex0 * 25;
	    //o.uv_VertTexFar90 = o.uv_VertTex90 * 25;
	    
	    o.uv_Control = v.texcoord; //TRANSFORM_TEX (v.texcoord, _Control1);
	    
	    return o;
	}

	fixed4 frag (v2f i) : COLOR
	{
		fixed4 masks1 = tex2D (_Control1, i.uv_Control ); 
		
		fixed4 vert0 = tex2D(_Vert, i.uv_VertTex0 ) - fixed4( 0.0, 0.04, 0.03, 0 );
		fixed4 vert1 = tex2D(_Vert, i.uv_VertTex90 ) - fixed4( 0.0, 0.04, 0.03, 0 );
		//fixed4 vert0_far = tex2D(_VertDetail, i.uv_VertTexFar0 ) + 0.4;
		//fixed4 vert1_far = tex2D(_VertDetail, i.uv_VertTexFar90 ) + 0.4;
		// ------------ lightmap -----------
		fixed4 lm = 1.0 * DecodeLM( UNITY_SAMPLE_TEX2D ( unity_Lightmap, i.uv_Control ) );
		fixed4 tex = 1.25 * tex2D ( _Baked, i.uv_Control ) + fixed4( 0, 0.04, 0.04, 0 );
		fixed bakedMask = 1.00 - masks1.r - masks1.g;
		
	    // -----------------------------------------------
	   	fixed4 rescolor = vert0 * masks1.r + vert1 * masks1.g + tex * bakedMask;
	   	rescolor *= lm;
	   	// ----------- FOG -------------------------------
		//return (i.uv_Lightmap.x * rescolor + i.uv_Lightmap.y * fixed4( 0.5f, 0.44f, 0.30, 1.0f ));
		return rescolor;

	}
	ENDCG
    
    }
}
Fallback "VertexLit"
} 