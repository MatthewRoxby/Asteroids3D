using nodes;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;

namespace engine{
    public class Window : GameWindow{
        
        ImGuiController controller;

        bool wireframe = false;
        bool showDebug = true;

        Node? inspectorNode = null;

        public Window(int width, int height, string title, Scene startScene) : base(GameWindowSettings.Default, new NativeWindowSettings(){ClientSize = new Vector2i(width, height), NumberOfSamples = 4, Title = title + (GameManager.EDIT_MODE? "    [EDIT MODE]": "")}){
            Unload += unload;
            RenderFrame += render;
            UpdateFrame += update;
            Load += load;
            Resize += resize;
            TextInput += textInput;
            MouseWheel += mouseWheel;
            controller = new ImGuiController(width, height);
            CenterWindow();
            GameManager.screenHeight = height;
            GameManager.screenWidth = width;
            GameManager.LoadNewScene(startScene);
            Run();
        }

        private void mouseWheel(MouseWheelEventArgs args)
        {
            controller.MouseScroll(args.Offset);
        }

        private void textInput(TextInputEventArgs args)
        {
            controller.PressChar((char)args.Unicode);
        }

        private void resize(ResizeEventArgs args)
        {
            GL.Viewport(0,0,args.Width, args.Height);
            controller.WindowResized(args.Width, args.Height);
            GameManager.screenWidth = args.Width;
            GameManager.screenHeight = args.Height;
        }

        private void setWireframe(bool b){
            wireframe = b;
            if(wireframe){
                GL.Disable(EnableCap.CullFace);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            else{
                GL.Enable(EnableCap.CullFace);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
        }

        private void load()
        {
            GL.ClearColor(Color4.Black);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Multisample);
            //GL.Disable(EnableCap.PolygonSmooth);
            
            float[] f = new float[2];
            GL.GetFloat(GetPName.AliasedLineWidthRange, f);
            Debug.WriteLine($"line min: {f[0]} line max: {f[1]}");
            setWireframe(wireframe);
        }

        private void update(FrameEventArgs args)
        {
            float delta = (float)args.Time;
            controller.Update(this, delta);

            GameManager.Update(delta);
            
            if(GameManager.EDIT_MODE){
                if(IsKeyPressed(Keys.Tab)){
                    setWireframe(!wireframe);
                }

                if(IsKeyPressed(Keys.GraveAccent)){
                    showDebug = !showDebug;
                }

                if(!showDebug) return;

                if(ImGui.Begin("tools")){
                    if(ImGui.Button("toggle wireframe: " + (wireframe? "ON": "OFF"))){
                        setWireframe(!wireframe);
                    }
                }

                if(ImGui.Begin("heirarchy")){
                    
                    if(ImGui.BeginListBox("##nodes")){
                        foreach(Node n in GameManager.GetNodes()){
                            if(ImGui.Selectable(n.name + "##" + n.ID.ToString())) inspectorNode = n;
                        }
                    }
                    
                }

                if(ImGui.Begin("inspector")){
                    inspectorNode?.inspect();
                }
            }
        }

        private void render(FrameEventArgs args)
        {
            float delta = (float)args.Time;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GameManager.Render(delta);

            controller.Render();
            SwapBuffers();
        }

        private void unload()
        {
            MeshLoader.CleanUp();
            TextureLoader.CleanUp();
            ShaderLoader.CleanUp();
            controller.Dispose();
        }
    }
}