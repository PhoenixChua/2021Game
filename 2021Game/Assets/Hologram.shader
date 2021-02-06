Shader "Custom/Hologram"

{

	Properties

	{

		// General

		_Brightness("Brightness", Range(0.1, 6.0)) = 3.0

		_Alpha ("Alpha", Range (0.0, 1.0)) = 1.0

		_MainTex ("MainTexture", 2D) = "white" {}

		_MainColor ("MainColor", Color) = (1,1,1,1)

		// Rim/Fresnel

		_RimColor ("Rim Color", Color) = (1,1,1,1)

		_RimPower ("Rim Power", Range(0.1, 10)) = 5.0

		// Scanline

		_ScanTiling ("Scan Tiling", Range(0.01, 10.0)) = 0.05

		_ScanSpeed ("Scan Speed", Range(-2.0, 2.0)) = 1.0

		// Settings

		[HideInInspector] _Fold("__fld", Float) = 1.0

	}

	SubShader

	{

		Tags { "Queue"="Geometry" "RenderType"="Opaque" "DisableBatching"="True"}

		Blend SrcAlpha One

		LOD 100

		ColorMask RGB

        Cull Back



		Pass

		{

			CGPROGRAM

			#pragma vertex vert

			#pragma fragment frag

			

			#include "UnityCG.cginc"



			struct appdata

			{

				float4 vertex : POSITION;

				float3 normal : NORMAL;

				float2 uv : TEXCOORD0;

			};



			struct v2f

			{

				float4 vertex : SV_POSITION;

				float2 uv : TEXCOORD0;

				float4 worldVertex : TEXCOORD1;

				float3 viewDir : TEXCOORD2;

				float3 worldNormal : NORMAL;

			};



			sampler2D _MainTex;

			sampler2D _FlickerTex;

			float4 _MainTex_ST;

			float4 _MainColor;

			float4 _RimColor;

			float _Rotation;

			float _RimPower;

			float _ScanTiling;

			float _ScanSpeed;

			float _Brightness;

			float _Alpha;

			v2f vert (appdata v)

			{

				v2f o;
				
				o.vertex = UnityObjectToClipPos(v.vertex);

				
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				o.worldVertex = mul(unity_ObjectToWorld, v.vertex);

				o.worldNormal = UnityObjectToWorldNormal(v.normal);

				o.viewDir = normalize(UnityWorldSpaceViewDir(o.worldVertex.xyz));



				return o;

			}



			

			fixed4 frag (v2f i) : SV_Target

			{

				fixed4 texColor = tex2D(_MainTex, i.uv);

				float scan = 0.0;

				#ifdef _SCAN_ON

					scan = step(frac(i.worldVertex.y * _ScanTiling + _Time.w * _ScanSpeed), 0.5) * 0.65;

				#endif

				// Rim Light

				half rim = 1.0-saturate(dot(i.viewDir, i.worldNormal));

				fixed4 rimColor = _RimColor * pow (rim, _RimPower);



				fixed4 col = texColor * _MainColor + (0.35 * _MainColor) + rimColor;

				col.a = texColor.a * _Alpha * (scan + rim);



				col.rgb *= _Brightness;



				return col;

			}

			ENDCG

		}

	}

}