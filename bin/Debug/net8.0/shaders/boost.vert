#version 440 core

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aUV;

out vec2 passUV;

uniform mat4 transformation, view, projection;

uniform float time;

float rand(vec2 co)
{
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

void main(){
    float x = sin(time + rand(aPos.xy));
    gl_Position = projection * view * transformation * vec4(aPos + vec3(rand(aPos.xy + vec2(time, time)), 0, 0), 1.0);
    passUV = aUV;
}