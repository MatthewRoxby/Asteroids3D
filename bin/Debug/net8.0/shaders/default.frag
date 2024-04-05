#version 440 core

in vec2 passUV;

out vec4 fragColour;

uniform vec4 modulate;
uniform int albedoEnabled;
uniform sampler2D albedo;

void main(){

    if(albedoEnabled == 1){
        fragColour = texture(albedo, passUV) * modulate;
    }
    else{
        fragColour = modulate;
    }
    
}