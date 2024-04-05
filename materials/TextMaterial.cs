using engine;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace materials{
    public class TextMaterial : TransformMaterial2D{

        public Color4 modulate = Color4.White;

        public TextMaterial() : base("shaders/text.vert", "shaders/text.frag"){

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
            GL.Uniform4(GL.GetUniformLocation(shader.program, "modulate"), modulate);
        }

        public override void inspect()
        {
            base.inspect();
            System.Numerics.Vector4 v = new System.Numerics.Vector4(modulate.R, modulate.G, modulate.B, modulate.A);
            ImGui.ColorEdit4("modulate", ref v);
            modulate = new Color4(v.X, v.Y, v.Z, v.W);
        }
    }
}