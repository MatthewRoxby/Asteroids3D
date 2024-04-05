using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;

namespace engine{
    public class Shader{
        public int program {get; private set;}

        public Shader(int program){
            this.program = program;
        }
    }

    public static class ShaderLoader{
        private static Dictionary<string, Shader> shaderCache = new Dictionary<string, Shader>();

        public static Shader LoadShader(string vertexPath, string fragmentPath){
            string key = vertexPath + "_" + fragmentPath;
            if(shaderCache.ContainsKey(key)){
                return shaderCache[key];
            }

            int vertex, fragment, program;
            string vertexSource, fragmentSource, infoLog;

            //vertex
            vertex = GL.CreateShader(ShaderType.VertexShader);

            using(StreamReader reader = new StreamReader(vertexPath)){
                vertexSource = reader.ReadToEnd();
            }

            GL.ShaderSource(vertex, vertexSource);
            GL.CompileShader(vertex);

            infoLog = GL.GetShaderInfoLog(vertex);

            if(infoLog != String.Empty){
                Debug.WriteLine(infoLog);
            }

            //fragment
            fragment = GL.CreateShader(ShaderType.FragmentShader);

            using(StreamReader reader = new StreamReader(fragmentPath)){
                fragmentSource = reader.ReadToEnd();
            }

            GL.ShaderSource(fragment, fragmentSource);
            GL.CompileShader(fragment);

            infoLog = GL.GetShaderInfoLog(fragment);

            if(infoLog != String.Empty){
                Debug.WriteLine(infoLog);
            }

            //program
            program = GL.CreateProgram();

            GL.AttachShader(program, vertex);
            GL.AttachShader(program, fragment);
            GL.LinkProgram(program);

            infoLog = GL.GetProgramInfoLog(program);
            
            if(infoLog != String.Empty){
                Debug.WriteLine(infoLog);
            }

            Shader s = new Shader(program);

            shaderCache.Add(key, s);
            
            GL.DeleteShader(vertex);
            GL.DeleteShader(fragment);

            return s;
        }

        public static void CleanUp(){
            foreach(Shader s in shaderCache.Values){
                GL.DeleteProgram(s.program);
            }
        }
    }
}