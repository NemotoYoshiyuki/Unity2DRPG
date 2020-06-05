Shader "UI/Cunstom/transition"
{
	Properties
	{
		// Set from "source image" in Image component.
		[HideInInspector] _MainTex("Texture", 2D) = "white" {}
		_Edge("Edge", Range(0, 1)) = 0.01
		_Progress("Progress", Range(0, 1)) = 0
	}
		SubShader
		{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
			LOD 100

			Pass
			{
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float4 color : COLOR;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					fixed4 color : COLOR;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				fixed _Edge;
				fixed _Progress;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.color = v.color;

					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);
					fixed alpha = smoothstep(_Progress - _Edge, _Progress, 1 - col.rgb);

					col.rgb = i.color;
					col.a = i.color.a * alpha;
					return col;
				}
				ENDCG
			}
		}
}