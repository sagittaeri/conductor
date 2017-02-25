// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "RageSpline/Textured" {
	Properties {
		_MainTex ("Texture1 (RGB)", 2D) = "white" {}
		_VecX ("Vector X", Float) = 0
		_VexY ("Vector Y", Float) = 0
		_PosX ("Pos X", Float) = 0
		_PosY ("Pos Y", Float) = 0
		_Val1 ("Val 1", Float) = 0
		_Val2 ("Val 2", Float) = 0
		_Mag ("Magnitude", Float) = 0
	}

	Category {
		Tags {"RenderType"="Transparent" "Queue"="Transparent"}
		Lighting Off
		BindChannels {
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "Texcoord", Texcoord
		}
		
		SubShader {
			Pass {
				ZWrite Off
				Cull off
				Blend SrcAlpha OneMinusSrcAlpha
				

			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile DUMMY PIXELSNAP_ON
				#include "UnityCG.cginc"
				
				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color    : COLOR;
					half2 texcoord  : TEXCOORD0;
					float3 wPos : TEXCOORD1;
				};
				
				sampler2D _MainTex;
				uniform float _VecX;
				uniform float _VecY;
				uniform float _Val1;
				uniform float _Val2;
				uniform float _PosX;
				uniform float _PosY;
				uniform float _Mag;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color;

					float3 worldPos = mul(unity_ObjectToWorld, IN.vertex).xyz;
					float3 objCenter = mul(unity_ObjectToWorld, half4(0,0,0,1));
					
					OUT.wPos = worldPos - objCenter;
					
					return OUT;
				}


				fixed4 frag(v2f IN) : SV_Target
				{
					float distanceFromCenter = length(IN.wPos - float3(_PosX, _PosY, 0));
					float gradientT = clamp(distanceFromCenter / _Mag, 0, 1);
					fixed4 color = IN.color;
					fixed4 tex = tex2D(_MainTex, IN.texcoord);
					float texWeight = lerp(_Val1, _Val2, gradientT);
					fixed4 texMultiplier = 1 - (texWeight * (1 - tex));
					color *= texMultiplier;
					return color;

				}
			ENDCG
			}
		}
	}
}
