#version 440 core

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aUV;

out vec2 passUV;

uniform mat4 transformation, view, projection;

void main(){
    gl_Position = projection * view * transformation * vec4(aPos, 1.0);
    passUV = aUV;
}