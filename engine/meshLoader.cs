using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Assimp;
using materials;
using nodes;
using OpenTK.Graphics.OpenGL4;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace engine{

    public class Mesh{
        public Mesh(int drawCount, int vAO, PrimitiveType drawType, bool elements)
        {
            this.drawCount = drawCount;
            VAO = vAO;
            this.drawType = drawType;
            this.elements = elements;
        }

        public int drawCount {get; private set;}
        public int VAO {get; private set;}
        public PrimitiveType drawType {get; set;}
        public bool elements {get; private set;}
    }

    public static class MeshLoader{
        private static List<int> usedBuffers = new List<int>();
        private static List<int> usedArrays = new List<int>();

        public static Mesh LoadMesh(float[] vertices, float[]? uvs = null, float[]? normals = null, uint[]? indices = null, PrimitiveType drawType = PrimitiveType.Triangles){
            int VAO = GL.GenVertexArray();
            usedArrays.Add(VAO);
            GL.BindVertexArray(VAO);

            LoadAttribute(0, 3, false, vertices);

            if(uvs != null){
                LoadAttribute(1, 2, false, uvs);
            }

            if(normals != null){
                LoadAttribute(2, 3, true, normals);
            }

            if(indices == null){
                //elements is false
                return new Mesh(vertices.Length, VAO, drawType, false);
            }
            else{
                int EBO = GL.GenBuffer();
                usedBuffers.Add(EBO);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
                return new Mesh(indices.Length, VAO, drawType, true);
            }
        }

        private static void LoadAttribute(int index, int numItems, bool normalised, float[] data){
            int VBO = GL.GenBuffer();
            usedBuffers.Add(VBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(index, numItems, VertexAttribPointerType.Float, normalised, numItems * sizeof(float), 0);
            GL.EnableVertexAttribArray(index);
        }

        private static float[] toFloatArray(List<Assimp.Vector3D> data, bool useFirstTwo = false){
            List<float> r = new List<float>();
            foreach(Assimp.Vector3D v in data){
                r.Add(v.X);
                r.Add(v.Y);
                if(!useFirstTwo) r.Add(v.Z);
            }

            return r.ToArray();
        }

        public static List<Mesh3D> FromFile(string meshFile){
            List<Mesh3D> result = new List<Mesh3D>();
            
            //do the MAT file first
            Dictionary<string, Material> materials = new Dictionary<string, Material>();

            string currentMatName = null;
            Material currentMatMaterial = null;
            using(StreamReader reader = new StreamReader(meshFile + ".mat")){
                
                while(!reader.EndOfStream){
                    string[] line = reader.ReadLine().Split(' ');
                    switch(line[0]){
                        case "name":
                            if(currentMatName != null && currentMatMaterial != null){
                                currentMatMaterial.GetInitials();
                                materials.Add(currentMatName, currentMatMaterial);
                            }
                            currentMatName = line[1];
                            break;
                        case "type":
                            Type t = Type.GetType("materials." + line[1]);
                            if(t != null && t.IsSubclassOf(typeof(Material))){
                                currentMatMaterial = (Material)Activator.CreateInstance(t);
                                currentMatMaterial.Use();
                            }
                            break;
                        case "f4":
                            Debug.WriteLine($"setting '{line[1]}' of material '{currentMatName}' to ({line[2]},{line[3]},{line[4]},{line[5]})");
                            currentMatMaterial?.Set(line[1], float.Parse(line[2]), float.Parse(line[3]), float.Parse(line[4]), float.Parse(line[5]));
                            break;
                    }
                }
                if(currentMatName != null && currentMatMaterial != null){
                    currentMatMaterial.GetInitials();
                    materials.Add(currentMatName, currentMatMaterial);
                }
            }

            //then OBJ
            List<float> vertexBatch = new List<float>();
            List<float> uvBatch = new List<float>();
            List<float> normalsBatch = new List<float>();
            
            List<float> currentVertices = new List<float>();
            List<float> currentUVs = new List<float>();
            List<float> currentNormals = new List<float>();
            string currentMaterial = null;
            string currentName = null;
            
            using(StreamReader reader = new StreamReader(meshFile + ".obj")){
                while(!reader.EndOfStream){
                    string[] line = reader.ReadLine().Split(" ");
                    
                    switch(line[0]){
                        case "g":
                            //group, for the object name
                            //also this means a new mesh so create the old one and clear the lists
                            if(currentName != null){
                                result.Add(new Mesh3D(currentName + "_" + currentMaterial){mesh = MeshLoader.LoadMesh(currentVertices.ToArray(), currentUVs.ToArray(), currentNormals.ToArray()), material = materials[currentMaterial]});
                                currentVertices.Clear();
                                currentUVs.Clear();
                                currentNormals.Clear();
                                currentMaterial = null;
                            }
                            currentName = line[1];
                            break;
                        case "v":
                            vertexBatch.Add(float.Parse(line[1]));
                            vertexBatch.Add(float.Parse(line[2]));
                            vertexBatch.Add(float.Parse(line[3]));
                            break;
                        case "vt":
                            uvBatch.Add(float.Parse(line[1]));
                            uvBatch.Add(float.Parse(line[2]));
                            break;
                        case "vn":
                            normalsBatch.Add(float.Parse(line[1]));
                            normalsBatch.Add(float.Parse(line[2]));
                            normalsBatch.Add(float.Parse(line[3]));
                            break;
                        case "f":
                            for(int i = 0; i < 3; i++){
                                int[] indices = Array.ConvertAll(line[i + 1].Split('/'), s => int.Parse(s) - 1);
                                currentVertices.Add(vertexBatch[indices[0] * 3 + 0]);
                                currentVertices.Add(vertexBatch[indices[0] * 3 + 1]);
                                currentVertices.Add(vertexBatch[indices[0] * 3 + 2]);

                                currentUVs.Add(uvBatch[indices[1] * 2 + 0]);
                                currentUVs.Add(uvBatch[indices[1] * 2 + 1]);

                                currentNormals.Add(normalsBatch[indices[2] * 3 + 0]);
                                currentNormals.Add(normalsBatch[indices[2] * 3 + 1]);
                                currentNormals.Add(normalsBatch[indices[2] * 3 + 2]);
                            }
                            break;
                        case "usemtl":
                            if(currentMaterial != null){
                                result.Add(new Mesh3D(currentName + "_" + currentMaterial){mesh = MeshLoader.LoadMesh(currentVertices.ToArray(), currentUVs.ToArray(), currentNormals.ToArray()), material = materials[currentMaterial]});
                                currentVertices.Clear();
                                currentUVs.Clear();
                                currentNormals.Clear();
                                currentMaterial = null;
                            }
                            currentMaterial = line[1];
                            break;
                        default:
                            break;
                            
                    }
                }
                //add the final mesh
                result.Add(new Mesh3D(currentName + "_" + currentMaterial){mesh = MeshLoader.LoadMesh(currentVertices.ToArray(), currentUVs.ToArray(), currentNormals.ToArray()), material = materials[currentMaterial]});
            }

            return result;
        }



        public static void CleanUp(){
            GL.DeleteVertexArrays(usedArrays.Count, usedArrays.ToArray());
            GL.DeleteBuffers(usedBuffers.Count, usedBuffers.ToArray());
        }
    }
}