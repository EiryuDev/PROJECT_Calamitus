Shader "FFPS_Engine/MAIN_RETRO_SHADER" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Palette ("Palette", 2D) = "white" {}
        _CustomNoiseTex ("Custom Noise Texture", 2D) = "white" {}
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.5
        _VignetteIntensity ("Vignette Intensity", Range(0, 1)) = 0.5
        _DistortionAmount ("Distortion Amount", Range(0, 1)) = 0.1
        _ChromaticAberration ("Chromatic Aberration", Range(0, 1)) = 0.1
        _CRTGridIntensity ("CRT Grid Intensity", Range(0, 1)) = 0.2
        _Pixelation ("Pixelation", Range(1, 100)) = 10
        _AspectRatio ("Aspect Ratio", Range(0.1, 10)) = 1.0 // Add aspect ratio slider
        _ColorSelector ("Color Selector", Color) = (1, 1, 1, 1)
        _GlitchAmount ("Glitch Amount", Range(0, 1)) = 0
        _AutoPixelation ("Auto Pixelation", Range(0, 1)) = 0 // Checkbox for auto pixelation
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
            sampler2D _Palette;
            sampler2D _CustomNoiseTex;
            float _ScanlineIntensity;
            float _VignetteIntensity;
            float _DistortionAmount;
            float _ChromaticAberration;
            float _CRTGridIntensity;
            float _Pixelation;
            float _AspectRatio; // Aspect ratio property
            fixed4 _ColorSelector;
            float _GlitchAmount;
            float _AutoPixelation; // Checkbox for auto pixelation
            
            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                // Stretch the UV coordinates with proper aspect ratio
                o.uv = float2(v.uv.x * _Pixelation, v.uv.y * _Pixelation * _AspectRatio);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target {
                // Apply pixelation
                float2 pixelUV = floor(i.uv) / _Pixelation;
                
                // Sample main texture and palette
                fixed4 texColor = tex2D(_MainTex, pixelUV);
                fixed4 paletteColor = tex2D(_Palette, pixelUV);
                
                // Apply color from palette and color selector
                fixed4 finalColor = texColor * (paletteColor * _ColorSelector);
                
                // Apply scanlines
                finalColor.rgb -= finalColor.rgb * _ScanlineIntensity * step(0.5, frac(pixelUV.y * 10));
                
                // Apply distortion
                float distortion = _DistortionAmount * tex2D(_MainTex, pixelUV * 10).r;
                pixelUV += distortion * (pixelUV - 0.5);
                
                // Apply vignette
                float vignette = 1.0 - smoothstep(0.0, _VignetteIntensity, length(pixelUV - 0.5));
                finalColor.rgb *= vignette;
                
                // Apply chromatic aberration
                float3 chromaticAberration = tex2D(_MainTex, pixelUV + _ChromaticAberration).rgb;
                finalColor.rgb = lerp(finalColor.rgb, chromaticAberration, _ChromaticAberration);
                
                // Apply CRT grid
                finalColor.rgb -= finalColor.rgb * _CRTGridIntensity * step(0.5, frac(pixelUV.x * 20)) * step(0.5, frac(pixelUV.y * 20));
                
                // Apply glitch effect using custom noise texture
                if (_GlitchAmount > 0) {
                    float noise = tex2D(_CustomNoiseTex, i.uv).r;
                    float glitch = _GlitchAmount * (noise - 0.5);
                    finalColor.rgb += glitch;
                }
                
                // Automatically adjust pixelation if auto pixelation is enabled and the texture's aspect ratio is wider
                if (_AutoPixelation > 0 && _AspectRatio > 1) {
                    _Pixelation = 1; // Adjust this value as needed
                }
                
                return finalColor;
            }
            ENDCG
        }
    }
}
