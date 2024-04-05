using engine;
using ImGuiNET;
using OpenTK.Mathematics;

namespace nodes{
    public class Camera3D : Node3D{
        public float fov; //vertical fov in degrees
        float near = 0.1f, far = 1000f;

        public Camera3D(string name = "Camera3D") : base(name){
            
        }

        public override void update(float delta)
        {
            base.update(delta);
            GameManager.projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), (GameManager.screenWidth / GameManager.screenHeight), near, far);
            //could be either of these
            GameManager.view = Matrix4.LookAt(position, position + front, up);
            //GameManager.view = Matrix4.LookAt(position, position + front, Vector3.UnitY);
        }

        public override void inspect()
        {
            base.inspect();
            ImGui.SliderFloat("fov", ref fov, 1f, 90f);
        }
    }
}