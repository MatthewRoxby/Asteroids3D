using System.Net;
using ImGuiNET;
using materials;
using nodes;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace engine{
    public static class TextLoader{
        private static Dictionary<char, LineMesh2D> charData = new Dictionary<char, LineMesh2D>(){
            /*
            {'', new LineMesh2D(new Vector2[]{

            })},
            */
            {'A', new LineMesh2D(new Vector2[]{
                new Vector2(-1, -1),
                new Vector2(-1, 1),
                new Vector2(1, 1),
                new Vector2(1, -1),
                new Vector2(1, 0),
                new Vector2(-1, 0)
            })},
            {'B', new LineMesh2D(new Vector2[]{
                new Vector2(-1, 0),
                new Vector2(-1, 1),
                new Vector2(0.9f, 1),
                new Vector2(1, 0.9f),
                new Vector2(1, 0.1f),
                new Vector2(0.9f, 0),
                new Vector2(1, -0.1f),
                new Vector2(1, -0.9f),
                new Vector2(0.9f, -1),
                new Vector2(-1, -1),
                new Vector2(-1, 0),
                new Vector2(0.9f, 0),
            })},
            {'C', new LineMesh2D(new Vector2[]{
                new Vector2(1, -1),
                new Vector2(-1, -1),
                new Vector2(-1, 1),
                new Vector2(1, 1)
            })},
            {'D', new LineMesh2D(new Vector2[]{
                new Vector2(-1, 0),
                new Vector2(-1,1),
                new Vector2(0.8f, 1),
                new Vector2(1, 0.8f),
                new Vector2(1, -0.8f),
                new Vector2(0.8f, -1),
                new Vector2(-1,-1),
                new Vector2(-1, 0)
            })},
            {'E', new LineMesh2D(new Vector2[]{
                new Vector2(1,-1),
                new Vector2(-1,-1),
                new Vector2(-1,0),
                new Vector2(1,0),
                new Vector2(-1,0),
                new Vector2(-1,1),
                new Vector2(1,1)
            })},
            {'F', new LineMesh2D(new Vector2[]{
                new Vector2(-1,-1),
                new Vector2(-1,0),
                new Vector2(1,0),
                new Vector2(-1,0),
                new Vector2(-1,1),
                new Vector2(1,1)
            })},
            {'G', new LineMesh2D(new Vector2[]{
                new Vector2(0,0),
                new Vector2(1,0),
                new Vector2(0.5f,0),
                new Vector2(0.5f,-1),
                new Vector2(-1,-1),
                new Vector2(-1,1),
                new Vector2(0.5f,1)
            })},
            {'H', new LineMesh2D(new Vector2[]{
                new Vector2(-1,-1),
                new Vector2(-1,1),
                new Vector2(-1,0),
                new Vector2(1,0),
                new Vector2(1,-1),
                new Vector2(1,0),
                new Vector2(1,1)
            })},
            {'I', new LineMesh2D(new Vector2[]{
                new Vector2(-1,-1),
                new Vector2(1,-1),
                new Vector2(0,-1),
                new Vector2(0,1),
                new Vector2(1,1),
                new Vector2(0,1),
                new Vector2(-1,1)
            })},
            {'J', new LineMesh2D(new Vector2[]{
                new Vector2(-0.5f,-1),
                new Vector2(0,-1),
                new Vector2(0,1),
                new Vector2(-1,1),
                new Vector2(0,1),
                new Vector2(1,1)
            })},
            {'K', new LineMesh2D(new Vector2[]{
                new Vector2(1,-1),
                new Vector2(-0.9f,0),
                new Vector2(-1, 0),
                new Vector2(-1,-1),
                new Vector2(-1,1),
                new Vector2(-1,0),
                new Vector2(-0.9f, 0),
                new Vector2(1,1)
            })},
            {'L', new LineMesh2D(new Vector2[]{
                new Vector2(-1,1),
                new Vector2(-1,-1),
                new Vector2(0,-1)
            })},
            {'M', new LineMesh2D(new Vector2[]{
                new Vector2(-1,-1),
                new Vector2(-1,1),
                new Vector2(-0.9f, 1),
                new Vector2(0,0.5f),
                new Vector2(0.9f, 1),
                new Vector2(1,1),
                new Vector2(1,-1)
            })},
            {'N', new LineMesh2D(new Vector2[]{
                new Vector2(-1,-1),
                new Vector2(-1,1),
                new Vector2(-0.9f, 1),
                new Vector2(0.9f,-1),
                new Vector2(1,-1),
                new Vector2(1,1)
            })},
            {'O', new LineMesh2D(new Vector2[]{
                new Vector2(-1f,-1),
                new Vector2(-1f,1),
                new Vector2(1f,1),
                new Vector2(1f,-1),
                new Vector2(-1f,-1)
            })},
            {'P', new LineMesh2D(new Vector2[]{
                new Vector2(-1,-1),
                new Vector2(-1,1),
                new Vector2(1,1),
                new Vector2(1,0),
                new Vector2(-1,0)
            })},
            {'Q', new LineMesh2D(new Vector2[]{
                new Vector2(-1f,-1),
                new Vector2(-1f,1),
                new Vector2(0.5f,1),
                new Vector2(0.5f,-1),
                new Vector2(1,-1),
                new Vector2(0.5f,-1),
                new Vector2(0, 0),
                new Vector2(0.5f, -1),
                new Vector2(-1f,-1)
            })},
            {'R', new LineMesh2D(new Vector2[]{
                new Vector2(-1,-1),
                new Vector2(-1,1),
                new Vector2(1,1),
                new Vector2(1,0),
                new Vector2(-1,0),
                new Vector2(-1,-0.1f),
                new Vector2(1,-1)
            })},
            {'S', new LineMesh2D(new Vector2[]{
                new Vector2(-1, -1),
                new Vector2(1, -1),
                new Vector2(1, 0),
                new Vector2(-1, 0),
                new Vector2(-1, 1),
                new Vector2(1, 1)
            })},
            {'T', new LineMesh2D(new Vector2[]{
                new Vector2(0,-1),
                new Vector2(0,1),
                new Vector2(1,1),
                new Vector2(0,1),
                new Vector2(-1,1)
            })},
            {'U', new LineMesh2D(new Vector2[]{
                new Vector2(-1f,1),
                new Vector2(-1f,-1),
                new Vector2(1f,-1),
                new Vector2(1f,1),
            })},
            {'V', new LineMesh2D(new Vector2[]{
                new Vector2(-1f,1),
                new Vector2(-0.2f,-1),
                new Vector2(0.2f,-1),
                new Vector2(1f,1),
            })},
            {'W', new LineMesh2D(new Vector2[]{
                new Vector2(-1,1),
                new Vector2(-1,-1),
                new Vector2(-0.9f, -1),
                new Vector2(0,-0.5f),
                new Vector2(0.9f, -1),
                new Vector2(1,-1),
                new Vector2(1,1)
            })},
            {'X', new LineMesh2D(new Vector2[]{
                new Vector2(-1f,1),
                new Vector2(0f,0f),
                new Vector2(1,1),
                new Vector2(0,0),
                new Vector2(-1,-1),
                new Vector2(0,0),
                new Vector2(1,-1)
            })},
            {'Y', new LineMesh2D(new Vector2[]{
                new Vector2(0,-1),
                new Vector2(0,0.5f),
                new Vector2(1,1),
                new Vector2(0,0.5f),
                new Vector2(-1,1)
            })},
            {'Z', new LineMesh2D(new Vector2[]{
                new Vector2(-1,1),
                new Vector2(1,1),
                new Vector2(1,0.9f),
                new Vector2(-1,-0.9f),
                new Vector2(-1,-1),
                new Vector2(1,-1)
            })},
            {'0', new LineMesh2D(new Vector2[]{
                new Vector2(-0.5f,1),
                new Vector2(-0.5f,-1),
                new Vector2(0.5f, -1),
                new Vector2(0.5f,1),
                new Vector2(-0.5f,1),
                new Vector2(0.5f,-1)
            })},
            {'1', new LineMesh2D(new Vector2[]{
                new Vector2(-0.5f,-1),
                new Vector2(0.5f,-1),
                new Vector2(0f, -1),
                new Vector2(0f,1),
                new Vector2(-0.1f,1),
                new Vector2(-0.5f,0.8f),
            })},
            {'2', new LineMesh2D(new Vector2[]{
                new Vector2(-0.5f,0.7f),
                new Vector2(-0.2f,1f),
                new Vector2(0.2f,1f),
                new Vector2(0.5f,0.7f),
                new Vector2(-0.5f,-0.9f),
                new Vector2(-0.5f,-1),
                new Vector2(0.5f,-1)
            })},
        };

        public static LineMesh2D[] getString(string s){
            List<LineMesh2D> result = new List<LineMesh2D>();
            foreach(char c in s){
                if(charData.ContainsKey(c)){
                    result.Add(charData[c]);
                }
            }
            return result.ToArray();
        }

        public static LineMesh2D? getChar(char c){
            if(charData.ContainsKey(c)){
                return charData[c];
            }

            return null;
        }
    }

    public class TextMesh2D : Node3D{
        public TextMaterial? mat = null;
        public string text = "";

        public float thickness = 0.1f;

        const float Z_POS = -10;

        public float padding = 2.5f;

        public TextMesh2D(string name = "TextMesh2D") : base(name){
            
        }

        public override void update(float delta)
        {
            position.Z = Z_POS;
            base.update(delta);
        }

        public override void render(float delta)
        {
            base.render(delta);
            bool b = GL.IsEnabled(EnableCap.DepthTest);
            GL.Disable(EnableCap.DepthTest);
            if(mat != null && text != String.Empty){
                Vector3 cursorPos = Vector3.Zero;
                foreach(char c in text){
                    if(c == '\n'){
                        cursorPos = new Vector3(0, cursorPos.Y - padding, 0);
                        continue;
                    }
                    
                    LineMesh2D m = TextLoader.getChar(c);
                    
                    if(m != null){
                        if(m.thickness != thickness){
                            m.thickness = thickness;
                            m.Remesh();
                        }
                        mat.transformation = Matrix4.CreateTranslation(cursorPos) * transformation;
                        mat.Use();
                        GL.BindVertexArray(m.mesh.VAO);
                        if(m.mesh.elements){
                            GL.DrawElements(m.mesh.drawType, m.mesh.drawCount, DrawElementsType.UnsignedInt, 0);
                        }
                        else{
                            GL.DrawArrays(m.mesh.drawType, 0, m.mesh.drawCount);
                        }
                    }

                    cursorPos.X += padding;
                }
            }

            if(b) GL.Enable(EnableCap.DepthTest);

            
        }

        public override void inspect()
        {
            base.inspect();
            ImGui.InputTextMultiline("text", ref text, 100, System.Numerics.Vector2.One * 200);
            ImGui.SliderFloat("padding", ref padding, 0f, 5f);
            ImGui.SliderFloat("thickness", ref thickness, 0.1f, 10f);
        }


    }
}