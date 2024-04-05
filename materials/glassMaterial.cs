using engine;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace materials{
    public class GlassMaterial : TransformMaterial3D{

        

        public GlassMaterial() : base("shaders/glass.vert", "shaders/glass.frag"){

        }

    }
}