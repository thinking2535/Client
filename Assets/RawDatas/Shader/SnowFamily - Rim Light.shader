Shader "Snow Family/Rim Light"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
    
        _MainTex ("Base (RGB)", 2D) = "white" {}
        //_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
        
        _RimColor ("Rim Color", Color) = (0.8,0.7,0.6,0.6)
        _RimPower ("Rim Power", Float) = 6
        
        //_SColor ("Shadow Color", Color) = (0.0,0.0,0.0,1)
        //_LColor ("Highlight Color", Color) = (0.5,0.5,0.5,1)
        
    }

    SubShader
    {

        Tags {
        		"Queue" = "Geometry" 
        		"LightMode" = "Always"
        		//"LightMode" = "Always" 이것은 라이트를 사용하지 않겠다라는 놈
        		}
        //LOD 200
        
CGPROGRAM
        #pragma surface surf ToonRamp noambient
        //#pragma surface surf ToonRamp [noambient] <- 요녀석이 앰비언트 라이트를 무시한다는 놈이다. 지워주면 이 섀이더는 앰비언트 컬러에 영향을 받는다.
        
        sampler2D _MainTex;
        //sampler2D _Ramp;
        //float4 _LColor;
        //float4 _SColor;
        fixed4 _Color;
        fixed _RimPower;
        fixed4 _RimColor;
        
        #pragma lighting ToonRamp exclude_path:prepass
        inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
        {
            //#ifndef USING_DIRECTIONAL_LIGHT
            //lightDir = normalize(lightDir);
            //#endif
            
            //lightDir = normalize(lightDir);
            
            //half d = dot (s.Normal, 1.0)*0.5 + 0.5;
            //half3 ramp = tex2D(_Ramp, float2(d,d)).rgb;
            //ramp = lerp(_SColor,_LColor,ramp);
            
            half4 c;			
            //c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
            //c.rgb = s.Albedo  * ramp * (atten * 2);
            c.rgb = s.Albedo * atten ;
            //c.rgb = s.Albedo;
            c.a = 0;
            
            return c;
        }
        
        struct Input
        {
            float2 uv_MainTex : TEXCOORD0;
            float3 pos : POSITION0;
            float3 viewDir;
        };
        
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 MainTexture = tex2D(_MainTex, IN.uv_MainTex);
            
            o.Albedo = MainTexture.rgb * _Color.rgb;
            o.Alpha = MainTexture.a;
            
            //Rim Light
            fixed rim = 1.0f - saturate( dot(normalize(IN.viewDir), o.Normal) );
            o.Emission = (_RimColor.rgb * pow(rim, _RimPower)) * _RimColor.a;
         
        }
        
ENDCG

    
    }

    Fallback "Mobile/VertexLit"
}
