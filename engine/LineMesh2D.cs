using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace engine{
    public class LineMesh2D{
        public List<Vector2> points = new List<Vector2>();

        public float thickness = 0.1f;

        public int VAO;

        public int VBO;

        public int EBO;

        public Mesh mesh;

        const float PARALLEL_ERROR_MARGIN = 0.1f;

        public LineMesh2D(Vector2[] points){
            this.points.AddRange(points);
            this.VAO = GL.GenVertexArray();
            this.VBO = GL.GenBuffer();
            this.EBO = GL.GenBuffer();
            Remesh();
        }

        public void Remesh(){
            List<float> vertices = new List<float>();
            List<uint> indices = new List<uint>();
            uint currentIndex = 0;

            if(points.Count < 2) return;

            for(int i = 0; i < points.Count; i++){
                Vector2 current = points[i];

                if(i == 0){
                    //start point has no previous to go off of
                    Vector2 next = points[i+1];
                    Vector2 dir = (next - current).Normalized();
                    Vector2 perp = new Vector2(dir.Y, -dir.X) * (thickness / 2.0f);
                    
                    vertices.Add((current - perp).X);
                    vertices.Add((current - perp).Y);
                    vertices.Add(0.0f);
                    vertices.Add((current + perp).X);
                    vertices.Add((current + perp).Y);
                    vertices.Add(0.0f);

                    indices.Add(currentIndex);
                    indices.Add(currentIndex + 1);
                    indices.Add(currentIndex + 2);
                }
                else if(i == points.Count - 1){
                    //end point has no next point to go off of
                    Vector2 previous = points[i-1];
                    Vector2 dir = (current - previous).Normalized();
                    Vector2 perp = new Vector2(dir.Y, -dir.X) * (thickness / 2.0f);
                    
                    vertices.Add((current - perp).X);
                    vertices.Add((current - perp).Y);
                    vertices.Add(0.0f);
                    vertices.Add((current + perp).X);
                    vertices.Add((current + perp).Y);
                    vertices.Add(0.0f);

                    indices.Add(currentIndex);
                    indices.Add(currentIndex - 1);
                    indices.Add(currentIndex + 1);
                }
                else{
                    //middle point has a previous and next point to go off of
                    Vector2 next = points[i+1];
                    Vector2 dir1 = (next - current).Normalized();
                    Vector2 perp1 = new Vector2(dir1.Y, -dir1.X);
                    
                    Vector2 previous = points[i-1];
                    Vector2 dir2 = (current - previous).Normalized();
                    Vector2 perp2 = new Vector2(dir2.Y, -dir2.X);

                    float pd = Vector2.PerpDot(dir1, dir2);
                    float d = Vector2.Dot(dir1, dir2);

                    Vector2 perp;
                    if(d > -1 - PARALLEL_ERROR_MARGIN && d < -1 + PARALLEL_ERROR_MARGIN){
                        Debug.WriteLine("parallel line detected at point: " + i.ToString());
                        perp = perp2 / 2.0f * (thickness * MathF.Abs(d));
                    }
                    else if(d > 1 - PARALLEL_ERROR_MARGIN && d < 1 + PARALLEL_ERROR_MARGIN){
                        Debug.WriteLine("parallel line detected at point: " + i.ToString());
                        perp = perp2 / 2.0f * (thickness * MathF.Abs(d));
                    }
                    else{
                        perp = (perp1 + perp2) / 2.0f * (thickness * MathF.Abs(pd));
                    }

                    Debug.WriteLine(d);
                    
                    
                    
                    

                    
                    vertices.Add((current - perp).X);
                    vertices.Add((current - perp).Y);
                    vertices.Add(0.0f);
                    vertices.Add((current + perp).X);
                    vertices.Add((current + perp).Y);
                    vertices.Add(0.0f);

                    indices.Add(currentIndex);
                    indices.Add(currentIndex - 1);
                    indices.Add(currentIndex + 1);

                    indices.Add(currentIndex);
                    indices.Add(currentIndex + 1);
                    indices.Add(currentIndex + 2);
                }

                currentIndex += 2;
            }

            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * sizeof(float), vertices.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(uint), indices.ToArray(), BufferUsageHint.StaticDraw);
            
            mesh = new Mesh(indices.Count, VAO, PrimitiveType.Triangles, true);
            GL.EnableVertexAttribArray(0);



            
        }


    }
}