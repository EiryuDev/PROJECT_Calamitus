Shader "FFPS_Engine/RetroShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Palette ("Palette", 2D) = "white" {}
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.5
        _NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.1
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
            float _ScanlineIntensity;
            float _NoiseIntensity;
            
            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target {
                // Sample main texture and palette
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 paletteColor = tex2D(_Palette, i.uv);
                
                // Apply color from palette
                fixed4 finalColor = texColor * paletteColor;
                
                // Apply scanlines
                finalColor.rgb -= finalColor.rgb * _ScanlineIntensity * step(0.5, frac(i.uv.y * 10));
                
                // Apply noise
                float noise = tex2D(_MainTex, i.uv * _NoiseIntensity * 10).r;
                finalColor.rgb += noise - _NoiseIntensity * 0.5;
                
                return finalColor;
            }
            ENDCG
        }
    }
}
