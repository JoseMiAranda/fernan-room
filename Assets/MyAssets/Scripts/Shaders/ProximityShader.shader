Shader "Custom/Proximity" {
    Properties {
        // Regular object texture 
        _MainTex ("Base (RGB)", 2D) = "white" {} 
        // The location of the player - will be set by script
        _PlayerPosition ("Player Position", vector) = (0,0,0,0)
        // How close does the player have to be to make object visible
        _VisibleDistance ("Visibility Distance", float) = 3.0
        // Used to add an outline around visible area a la Mario Galaxy - http://www.youtube.com/watch?v=91raP59am9U
        _OutlineWidth ("Outline Width", float) = 0.3 
        // Colour of the outline
        _OutlineColour ("Outline Colour", color) = (1.0,1.0,0.0,1.0) 
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            LOD 200
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // Access the shaderlab properties
            sampler2D _MainTex;
            float4 _PlayerPosition;
            float _VisibleDistance;
            float _OutlineWidth;
            fixed4 _OutlineColour;
            
            // Input to vertex shader
            struct vertexInput {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };
            // Input to fragment shader
            struct vertexOutput {
                float4 pos : SV_POSITION;
                float4 position_in_world_space : TEXCOORD0;
                float2 tex : TEXCOORD1;
            };
             
            // VERTEX SHADER
            vertexOutput vert(vertexInput input) 
            {
                vertexOutput output; 
                output.pos = UnityObjectToClipPos(input.vertex);
                output.position_in_world_space = mul(unity_ObjectToWorld, input.vertex);
                output.tex = input.texcoord;
                return output;
            }
     
            // FRAGMENT SHADER
            float4 frag(vertexOutput input) : SV_Target 
            {
                // Calculate distance to player position
                float dist = distance(input.position_in_world_space.xyz, _PlayerPosition.xyz);
     
                // Return appropriate colour
                if (dist < _VisibleDistance) {
                    return tex2D(_MainTex, input.tex); // Visible
                }
                else if (dist < _VisibleDistance + _OutlineWidth) {
                    return _OutlineColour; // Edge of visible range
                }
                else {
                    float4 tex = tex2D(_MainTex, input.tex); // Outside visible range
                    tex.a = 0;
                    return tex;
                }
            }

            ENDCG
        }
    } 
    //FallBack "Diffuse"
}
