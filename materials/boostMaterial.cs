using engine;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace materials{
    public class BoostMaterial : TransformMaterial3D{
        public Color4 modulate = Color4.White;

        public BoostMaterial() : base("shaders/boost.vert", "shaders/default.frag"){

        }

        public override void GetInitials()
        {
            base.GetInitials();
            float[] p = new float[4];
            GL.GetUniform(shader.program, GL.GetUniformLocation(shader.program, "modulate"), p);
            modulate = new Color4(p[0], p[1], p[2], p[3]);
        }

        public override void Use()
        {
            base.Use();
            GL.Uniform1(GL.GetUniformLocation(shader.program, "time"), GameManager.time);
            GL.Uniform4(GL.GetUniformLocation(shader.program, "modulate"), modulate);
        }
    }
}