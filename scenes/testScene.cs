using engine;
using materials;
using nodes;
using OpenTK.Mathematics;

namespace scenes{
    public class TestScene : Scene{

        float[] vertices = {
            -0.5f,-0.5f, 0.0f,
            0.5f,-0.5f, 0.0f,
            -0.5f,0.5f, 0.0f,
            0.5f,0.5f, 0.0f
        };

        float[] uvs = {
            0.0f, 0.0f,
            1.0f, 0.0f,
            0.0f, 1.0f,
            1.0f, 1.0f
        };

        uint[] indices = {
            0,1,2,
            2,1,3
        };

        public override List<Node> initScene()
        {
            List<Node> result = new List<Node>(){
                new Camera3D("Cam"){position = new Vector3(0,0,100), fov = 45f, euler = new Vector3(0f,180f, 0f)},
                new AssimpMesh3D("models/asteroidStation") {euler = new Vector3(-75f, 20f, 0f)},
                new AssimpMesh3D("models/rocks"){visible = false},
                new AssimpMesh3D("models/spaceship"){euler = new Vector3(-90f, 0f, -90f), position = new Vector3(0f, -10f, 65f)},
                new StarBG(),
                new TextMesh2D(){mat = new TextMaterial(), text = "ABCDEF\nGHIJKL\nMNOPQR\nSTUVWX\nYZ\n012345\n6789", scale = Vector3.One * 0.2f},
            };

            

            return result;
        }
    }
}