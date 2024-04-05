using Assimp;
using engine;
using ImGuiNET;

namespace nodes{
    public class AssimpMesh3D : Node3D{
        private List<Mesh3D> subMeshes = new List<Mesh3D>();

        public AssimpMesh3D(string meshFile, string name = "AssimpMesh3D") : base(name){
            if(File.Exists(meshFile + ".obj") && File.Exists(meshFile + ".mat")){
                subMeshes = MeshLoader.FromFile(meshFile);
            }
            
        }

        public override void begin()
        {
            base.begin();
            foreach(Mesh3D m in subMeshes){
                m.begin();
            }
        }

        public override void update(float delta)
        {
            base.update(delta);
            foreach(Mesh3D m in subMeshes){
                if(m.state == NodeState.RUNNING) m.update(delta);
                m.transformation =  m.transformation * transformation;
            }
        }

        public override void render(float delta)
        {
            base.render(delta);
            foreach(Mesh3D m in subMeshes){

                if(m.visible) m.render(delta);
            }
        }

        public override void end()
        {
            base.end();
            foreach(Mesh3D m in subMeshes){
                m.end();
            }
        }

        public override void inspect()
        {
            base.inspect();
            foreach(Mesh3D m in subMeshes){
                if(ImGui.TreeNode("Submesh: " + m.name)){
                    m.inspect();
                    ImGui.TreePop();
                }
                
            }
        }


    }
}