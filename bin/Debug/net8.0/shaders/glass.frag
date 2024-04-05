#version 440 core

in vec3 gPos;

out vec4 fragColour;

void main(){

    float fac = sin(gPos.x + gPos.y + gPos.z);

    if(fac > -0.95){
        fragColour = vec4(0,0,0,1);
    }
    else{
        fragColour = vec4(1,1,1,1);
    }
    
}