using engine;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace materials{
    public class TransformMaterial3D : Material{

        public Matrix4 transformation;

        public TransformMaterial3D(string vertexPath, string fragmentPath) : base(vertexPath, fragmentPath){

        }

        public override void Use()
        {
            base.Use();
            GL.UniformMatrix4(GL.GetUniformLocation(shader.program, "transformation"), false, ref transformation);
            GL.UniformMatrix4(GL.GetUniformLocation(shader.program, "projection"), false, ref GameManager.projection);
            GL.UniformMatrix4(GL.GetUniformLocation(shader.program, "view"), false, ref GameManager.view);
        }


    }

    public class TransformMaterial2D : Material{

        public Matrix4 transformation;

        public TransformMaterial2D(string vertexPath, string fragmentPath) : base(vertexPath, fragmentPath){

        }

        public override void Use()
        {
            base.Use();
            GL.UniformMatrix4(GL.GetUniformLocation(shader.program, "transformation"), false, ref transformation);
            GL.UniformMatrix4(GL.GetUniformLocation(shader.program, "projection"), false, ref GameManager.projection);
        }


    }
}