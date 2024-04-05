#version 440 core
layout(location = 0) in vec3 aPos;

uniform float scale;

uniform mat4 transformation, view, projection;

uniform vec2 screenSize;

void main(){
    vec4 eyePos = view * transformation * vec4(aPos, 1.0);
    vec4 projVoxel = projection * vec4(scale,scale,eyePos.z,eyePos.w);
    vec2 projSize = screenSize * projVoxel.xy / projVoxel.w;
    gl_PointSize = 0.25 * (projSize.x+projSize.y);
    gl_Position = projection * eyePos;
}