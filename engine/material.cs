using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace engine{
    public abstract class Material{
        protected Shader shader;

        public Material(string vertexPath, string fragmentPath){
            shader = ShaderLoader.LoadShader(vertexPath, fragmentPath);
        }

        public virtual void GetInitials(){

        }

        public virtual void Use(){
            GL.UseProgram(shader.program);
        }

        public virtual void inspect(){
            
        }

        public void Set(string name, float f1){
            GL.Uniform1(GL.GetUniformLocation(shader.program, name), f1);
        }

        public void Set(string name, int i1){
            GL.Uniform1(GL.GetUniformLocation(shader.program, name), i1);
        }

        public void Set(string name, float f1, float f2){
            GL.Uniform2(GL.GetUniformLocation(shader.program, name), f1, f2);
        }

        public void Set(string name, float f1, float f2, float f3){
            GL.Uniform3(GL.GetUniformLocation(shader.program, name), f1, f2, f3);
        }

        public void Set(string name, float f1, float f2, float f3, float f4){
            GL.Uniform4(GL.GetUniformLocation(shader.program, name), f1, f2, f3, f4);
        }
    }
}