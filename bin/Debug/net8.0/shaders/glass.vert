#version 440 core

layout(location = 0) in vec3 aPos;

out vec3 gPos;

uniform mat4 transformation, view, projection;

void main(){
    gl_Position = projection * view * transformation * vec4(aPos, 1.0);
    gPos = (transformation * vec4(aPos, 1.0)).xyz;
}