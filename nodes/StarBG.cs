using engine;
using materials;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace nodes{
    public class StarBG : Mesh3D{

        const int numStars = 1000;

        const int maxRadius = 900;

        
        public StarBG(string name = "StarBG") : base(name){
            GL.Enable(EnableCap.PointSprite);
            GL.Enable(EnableCap.ProgramPointSize);
            //GL.PointSize(10f);
            this.material = new StarBGMat(){scale = 1f};
            List<float> points = new List<float>();
            Random r = new Random();
            for(int i = 0; i < numStars * 3; i++){
                points.Add(r.Next(-maxRadius, maxRadius));
            }

            mesh = MeshLoader.LoadMesh(points.ToArray(), null, null, null, PrimitiveType.Points);
        }

        public override void render(float delta)
        {
            ((StarBGMat)material).transformation = transformation;
            base.render(delta);
        }


    }
}