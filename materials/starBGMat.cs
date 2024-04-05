using engine;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace materials{
    public class StarBGMat : TransformMaterial3D{

        public float scale = 10f;

        public Color4 modulate = Color4.White;

        public StarBGMat() : base("shaders/starBG.vert", "shaders/starBG.frag"){

        }

        public override void Use()
        {
            base.Use();
            GL.Uniform1(GL.GetUniformLocation(shader.program, "scale"), scale);

            GL.Uniform2(GL.GetUniformLocation(shader.program, "screenSize"), new Vector2(GameManager.screenWidth, GameManager.screenHeight));
            GL.Uniform4(GL.GetUniformLocation(shader.program, "modulate"), modulate);

        }

        public override void inspect()
        {
            base.inspect();
            ImGui.SliderFloat("scale", ref scale, 0.1f, 10f);
            System.Numerics.Vector4 v = new System.Numerics.Vector4(modulate.R, modulate.G, modulate.B, modulate.A);
            ImGui.ColorEdit4("modulate", ref v);
            modulate = new Color4(v.X, v.Y, v.Z, v.W);
        }


    }
}