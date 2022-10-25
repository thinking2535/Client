Shader "Snow Family/Unlit - Transparent Colored Twinkle"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Detail ("Detail (RGB)", 2D) = "white" {}
	}
	
	Category
	{
		Tags
		{
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent"
		}
		
		Blend SrcAlpha One
		AlphaTest Greater .01
		ColorMask RGB
		
		Cull off Lighting Off ZWrite Off Fog { Mode Off }
		
		BindChannels
		{
			Bind "Color", color
		}
		
		
		SubShader
		{
			UsePass "Unlit/Transparent Colored Test/BASE"
			Pass
			{
				Name "BASE"
				SetTexture [_MainTex] 
				{ 
					constantColor [_TintColor]
					combine constant * primary
				}
				
				SetTexture [_MainTex]
				{
					combine texture * previous QUAD
				}
				
				SetTexture [_Detail] 
				{ 
					combine previous * texture
				}
			}
		}
	} 
}
