// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "Serj Shaders/Splat2-RGB Tex6 LM S Canyon (UnityLM)" {
Properties {
	_Vert ("Vert Main", 2D) = "white" {}
	_VertDetail ("Vert Detail", 2D) = "white" {}
	_Splat1 ("Layer 1", 2D) = "white" {}
	_Splat2 ("Layer 2", 2D) = "white" {}
	_Splat3 ("Layer 3", 2D) = "white" {}
	_Splat4 ("Layer 4", 2D) = "white" {}
	_Flow ("Flow", 2D) = "white" {}
	_FarDetail ("FarDetail", 2D) = "white" {}
	_LM ("LM", 2D) = "white" {}
	_Control1 ("Control 1 (RGB)", 2D) = "white" {}
	_Control2 ("Control 2 (RGB)", 2D) = "white" {}
	//_Control2 ("Control 2 (RGB)", 2D) = "white" {}
	_BumpMap ("Used For LM only", 2D) = "bump" {}
	//_Tilings ("Tiling Vertical", Vector ) = ( 10, 0.01, 10, 0.01 )
}
SubShader {

	//LOD 400

	Tags{
	    "Queue"="Geometry-2"
	    "IgnoreProjector"="False"
	    "RenderType"="Opaque"
	    }
	//Offset 0, 0    
    Pass {
    //Fog {Mode Off}
    //Fog { Color ( 0.5,0.45,0.31 ) }
    Fog { Color ( 0.384,0.433,0.480 ) }
    Lighting Off
    Tags {"LightMode" = "ForwardBase"}
    
   	CGPROGRAM

	//#define SHADER_API_MOBILE
	//#define DIRECTIONAL

	#include "UnityCG.cginc"
	#include "AutoLight.cginc"
	#include "Lighting.cginc"

	#pragma target 2.0
	#pragma glsl
	//#pragma glsl_no_auto_normalization
	//#pragma glsl_no_optimize
	//#pragma debug
	#define GL_FRAGMENT_PRECISION_HIGH 1
	#pragma exclude_renderers xbox360 ps3 flash
	#pragma vertex vert
	#pragma fragment frag
	//#pragma fragmentoption ARB_precision_hint_fastest
	#pragma multi_compile_fwdbase

	sampler2D _Control1, _Vert, _VertDetail, _Splat1, _Splat2, _Splat4, _FarDetail;
	//fixed4 _fogColor_ST;

	struct v2f {
	    float4 pos : SV_POSITION;
	    float2 uv_Control: TEXCOORD0; 
		float2 uv_VertTex0: TEXCOORD1;
		float2 uv_VertTex90: TEXCOORD2;
		float2 uv_VertTexFar0: TEXCOORD3;
		float2 uv_VertTexFar90: TEXCOORD4;
		float2 uv_FarDetail: TEXCOORD5;
		float2 uv_Plain: TEXCOORD6;
	};

	struct v_base {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
		//fixed4 color : COLOR0; 
	};

	v2f vert (v_base v)
	{
	    v2f o;
	    o.pos = UnityObjectToClipPos (v.vertex);
	    //o.pos.y -= 0.1;
	       
	    o.uv_VertTex0.x = v.texcoord.x * 10;		// тайлинг как в громе
	    o.uv_VertTex0.y = v.vertex.y * 0.004;	// тайлинг указываем как в громе / 1000
	    o.uv_VertTex90.x = v.texcoord.y * 10;
	    o.uv_VertTex90.y = v.vertex.y * 0.004;
	    
	    o.uv_VertTexFar0 = o.uv_VertTex0 * 25;
	    o.uv_VertTexFar90 = o.uv_VertTex90 * 25;
	    
	    o.uv_Plain = v.texcoord * 200;
	    //o.uv_Plain.z = o.pos.z;
	    //o.uv_Plain.zw = v.texcoord * 1000;
	    
	    //o.uv_Grass = v.texcoord * 200;  //250
	    o.uv_FarDetail = v.texcoord * 10;
	    
	    o.uv_Control = v.texcoord; 
	    
	    return o;
	}

	fixed4 frag (v2f i) : COLOR
	{
	    //fixed2 lmuv = i.uv_Lightmap.xy * unity_LightmapST.xy + unity_LightmapST.zw;
		
		fixed4 vert0 = tex2D(_Vert, i.uv_VertTex0 );
		fixed4 vert1 = tex2D(_Vert, i.uv_VertTex90 );
		fixed4 vert0_far = tex2D(_VertDetail, i.uv_VertTexFar0 ) + 0.4;
		fixed4 vert1_far = tex2D(_VertDetail, i.uv_VertTexFar90 ) + 0.4;
		
		fixed4 fardetail = tex2D(_FarDetail, i.uv_FarDetail ) + 0.5;
		
		fixed4 masks1 = tex2D (_Control1, i.uv_Control ); 
	
		fixed4 plain = fardetail * tex2D( _Splat1, i.uv_Plain );
		//fixed4 plainDet = tex2D( _Splat2, i.uv_Plain.xy ) + 0.4;
			
		//fixed4 grassDet = tex2D( _Splat4, i.uv_Plain.xy ) + 0.4;
			
		fixed4 rescolor = vert0 * vert0_far * masks1.r + vert1 * vert1_far * masks1.g + plain * masks1.b;
	
	    // -----------------------------------------------
		
		return rescolor;
	}
	ENDCG

    }

// ====================== PASS TWO ======================================

Pass {
    //Fog {Mode Off}
    //Fog { Color ( 0.5,0.45,0.31 ) }
    Fog { Color ( 0.384,0.433,0.480 ) }
    Lighting Off
    Tags {"LightMode" = "ForwardBase"}
    Blend One One

	CGPROGRAM

	//#define SHADER_API_MOBILE
	//#define DIRECTIONAL

	#include "UnityCG.cginc"
	#include "AutoLight.cginc"
	#include "Lighting.cginc"

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

	sampler2D _Control2, _Flow, _FarDetail, _Splat3;
	//fixed4 _fogColor_ST;

	struct v2f {
	    float4 pos : SV_POSITION;
	    float2 uv_Control: TEXCOORD0; 
		float2 uv_Flow: TEXCOORD1;
		float2 uv_FarDetail: TEXCOORD2;
		float2 uv_Plain: TEXCOORD6;
	};

	struct v_base {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
		//fixed4 color : COLOR0; 
	};

	v2f vert (v_base v)
	{
	    v2f o;
	    o.pos = UnityObjectToClipPos (v.vertex);

	    o.uv_Flow = v.texcoord * 350;
	    o.uv_FarDetail = v.texcoord * 10;
	    
	    o.uv_Plain = v.texcoord * 200;
	    
	    
	    o.uv_Control = v.texcoord; 
	    
	    return o;
	}

	fixed4 frag (v2f i) : COLOR
	{
	    //fixed2 lmuv = i.uv_Lightmap.xy * unity_LightmapST.xy + unity_LightmapST.zw;
		
		fixed4 flow = tex2D(_Flow, i.uv_Flow );
		fixed4 fardetail = tex2D(_FarDetail, i.uv_FarDetail ) + 0.5;
		
		//fixed4 roads1 = tex2D(_Concrete, i.uv_Concrete );
		//fixed4 roads1_far = tex2D(_ConcreteDet, i.uv_ConcreteDet ) + 0.5;
		//fixed4 concreteDet2 = tex2D(_ConcreteDet2, i.uv_ConcreteDet2 ) + 0.5;
		//fixed4 roads2 = tex2D(_Roads, i.uv_Roads );
		fixed4 grass = tex2D( _Splat3, i.uv_Plain );
		
		fixed4 masks2 = tex2D (_Control2, i.uv_Control ); 
		
	    // -----------------------------------------------
		fixed4 rescolor = fardetail * (flow * masks2.g + grass * masks2.r);// + roads1_far * ( roads1 * masks2.a + roads2 * masks2.b );
		
		//rescolor *= fardetail;// * masks1.b;
		
		return rescolor;
	}
	ENDCG

    }
 
// ====================== PASS THREE (LIGHT) ======================================

Pass {
    //Fog {Mode Off}
    Fog { Color ( 1.0,1.0,1.0 ) }
    Lighting On
    Tags {"LightMode" = "ForwardBase"}
    //Blend OneMinusDstAlpha DstAlpha
    Blend Zero SrcColor
    //Blend One One

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

	sampler2D _LM;
	// float4 unity_LightmapST;
	//fixed4 _fogColor_ST;

	struct v2f {
	    float4 pos : SV_POSITION;
	    float2 uv_Control: TEXCOORD0; 
		SHADOW_COORDS(1)
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
	    
	    o.uv_Control = v.texcoord; //TRANSFORM_TEX (v.texcoord, _Control1);
    
	    // ---- это туман -----------------
	    //o.uv_Lightmap.x = saturate((3000 - o.pos.z) * 0.0006667 );/// (2500 - 1000));
	    //o.uv_Lightmap.y = 1 - o.uv_Lightmap.x; 
	    //fogColor_ST = ( 1 - o.uv_Lightmap.z ) * fixed4( 1.0f, 0.88f, 0.60, 1.0f );
	    
	    TRANSFER_SHADOW(o);
	    
	    return o;
	}

	fixed4 frag (v2f i) : COLOR
	{
		// ------------ realtime shadows + lightmap -----------
		float shadow = SHADOW_ATTENUATION(i);
		//fixed4 lm = 1.0 * DecodeLM( tex2D ( _LM, i.uv_Control ) );
		fixed4 lmu = 1.0 * DecodeLM( UNITY_SAMPLE_TEX2D ( unity_Lightmap, i.uv_Control ) );
		//fixed4 lm =  min( 0.7 * DecodeLM( tex2D (unity_Lightmap, i.uv_Lightmap.xy) ), 1.0 * DecodeLM( tex2D ( _LM, i.uv_Lightmap.xy) ) );
		fixed4 lm2 = min( lmu, shadow );
		//fixed4 lm5 = min( lm, lm2 );
	    // -----------------------------------------------
	   	return lm2;
	   	// ----------- FOG -------------------------------
		//return (i.uv_Lightmap.x * rescolor + i.uv_Lightmap.y * fixed4( 0.5f, 0.44f, 0.30, 1.0f ));
		//return rescolor;

	}
	ENDCG

    }
    
}

Fallback "VertexLit"
} 