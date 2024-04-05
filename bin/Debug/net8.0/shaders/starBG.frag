#version 440 core

uniform vec4 modulate;

out vec4 FragColour;

void main(){
    vec2 r = gl_PointCoord - vec2(0.5,0.5);
    if(length(r) > 0.5){
        discard;
    }

    FragColour = modulate;
}