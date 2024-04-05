using engine;
using ImGuiNET;
using materials;
using OpenTK.Graphics.OpenGL4;

namespace nodes{
    public class Mesh3D : Node3D{
        public Material? material;
        public Mesh? mesh;

        public Mesh3D(string name = "MeshNode") : base(name){

        }

        public override void render(float delta)
        {
            base.render(delta);
            if(mesh != null && material != null){
                if(material is DefaultMeshMaterial){
                    ((DefaultMeshMaterial)material).transformation = transformation;
                }
                if(material is GlassMaterial){
                    ((GlassMaterial)material).transformation = transformation;
                }
                if(material is BoostMaterial){
                    ((BoostMaterial)material).transformation = transformation;
                }
                material.Use();
                GL.BindVertexArray(mesh.VAO);
                if(mesh.elements){
                    GL.DrawElements(mesh.drawType, mesh.drawCount, DrawElementsType.UnsignedInt, 0);
                }
                else{
                    GL.DrawArrays(mesh.drawType, 0, mesh.drawCount);
                }
            }
        }

        public override void inspect()
        {
            base.inspect();
            if(ImGui.TreeNode("material: " + material?.GetType().Name)){
                material?.inspect();
                ImGui.TreePop();
            }
        }
    }
}