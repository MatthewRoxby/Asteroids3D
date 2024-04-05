#version 440 core

layout(location = 0) in vec3 aPos;


uniform mat4 transformation, projection;

void main(){
    gl_Position = projection * transformation * vec4(aPos, 1.0);
}