Shader "FFPS_Engine/AdvanceColorRetroShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.5
        _VignetteIntensity ("Vignette Intensity", Range(0, 1)) = 0.5
        _DistortionAmount ("Distortion Amount", Range(0, 1)) = 0.1
        _ChromaticAberration ("Chromatic Aberration", Range(0, 1)) = 0.1
        _CRTGridIntensity ("CRT Grid Intensity", Range(0, 1)) = 0.2
        _Saturation ("Saturation", Range(0, 2)) = 1
        _Contrast ("Contrast", Range(0, 2)) = 1
        _Brightness ("Brightness", Range(0, 2)) = 1
    }
    
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float _ScanlineIntensity;
            float _VignetteIntensity;
            float _DistortionAmount;
            float _ChromaticAberration;
            float _CRTGridIntensity;
            float _Saturation;
            float _Contrast;
            float _Brightness;
            
            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target {
                // Sample main texture
                fixed4 texColor = tex2D(_MainTex, i.uv);
                
                // Apply scanlines
                texColor.rgb -= texColor.rgb * _ScanlineIntensity * step(0.5, frac(i.uv.y * 10));
                
                // Apply distortion
                float distortion = _DistortionAmount * tex2D(_MainTex, i.uv * 10).r;
                i.uv += distortion * (i.uv - 0.5);
                
                // Apply vignette
                float vignette = 1.0 - smoothstep(0.0, _VignetteIntensity, length(i.uv - 0.5));
                texColor.rgb *= vignette;
                
                // Apply chromatic aberration
                float3 chromaticAberration = tex2D(_MainTex, i.uv + _ChromaticAberration).rgb;
                texColor.rgb = lerp(texColor.rgb, chromaticAberration, _ChromaticAberration);
                
                // Apply CRT grid
                texColor.rgb -= texColor.rgb * _CRTGridIntensity * step(0.5, frac(i.uv.x * 20)) * step(0.5, frac(i.uv.y * 20));
                
                // Apply color grading
                texColor.rgb = saturate(texColor.rgb * _Saturation);
                texColor.rgb = texColor.rgb * _Contrast + (_Brightness - 1.0);
                
                return texColor;
            }
            ENDCG
        }
    }
}
