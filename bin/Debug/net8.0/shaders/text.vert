#version 440 core

layout(location = 0) in vec3 aPos;


uniform mat4 transformation, projection;

uniform float scramble;

float rand(vec2 co)
{
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

void main(){
    gl_Position = projection * transformation * vec4(aPos + vec3(rand(aPos.xy) - 0.5,rand(aPos.yx) - 0.5,0) * scramble, 1.0);
}