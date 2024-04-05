using System.Diagnostics;
using ImGuiNET;
using OpenTK.Mathematics;

namespace nodes{
    public class Node3D : Node{
        public Vector3 position, scale = Vector3.One, euler;

        public Vector3 front, up, right;

        public Matrix4 transformation;

        public Node3D(string name = "Node3D") : base(name){

        }

        public override void update(float delta)
        {
            base.update(delta);
            Vector3 e = euler * (MathF.PI / 180f);
            transformation = Matrix4.CreateScale(scale);
            transformation *= Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(e));
            transformation *= Matrix4.CreateTranslation(position);

            // First, the front matrix is calculated using some basic trigonometry.
            
            Matrix3 rot = Matrix3.CreateRotationX(e.X) * Matrix3.CreateRotationY(e.Y) * Matrix3.CreateRotationZ(e.Z);

            front = Vector3.UnitZ * rot;
            up = Vector3.UnitY * rot;
            right = Vector3.Cross(front, up);
        }

        

        public override void inspect()
        {
            base.inspect();
            System.Numerics.Vector3 p = new System.Numerics.Vector3(position.X, position.Y, position.Z);
            ImGui.DragFloat3("position", ref p, 0.1f);
            System.Numerics.Vector3 s = new System.Numerics.Vector3(scale.X, scale.Y, scale.Z);
            ImGui.DragFloat3("scale", ref s, 0.1f);
            System.Numerics.Vector3 r = new System.Numerics.Vector3(euler.X, euler.Y, euler.Z);
            ImGui.DragFloat3("euler angles", ref r, 0.1f);

            position = new Vector3(p.X, p.Y, p.Z);
            scale = new Vector3(s.X, s.Y, s.Z);
            euler = new Vector3(r.X, r.Y, r.Z);

            if(ImGui.TreeNode("matrix")){
                ImGui.Text(transformation.ToString());
                ImGui.TreePop();
            }
        }
    }
}