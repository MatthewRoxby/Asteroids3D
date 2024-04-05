#version 440 core


out vec4 fragColour;

uniform vec4 modulate;

void main(){

    fragColour = modulate;
    
}