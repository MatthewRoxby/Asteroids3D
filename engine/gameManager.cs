using nodes;
using OpenTK.Mathematics;

namespace engine{
    public static class GameManager{
        public static int screenWidth, screenHeight;

        public static Matrix4 projection, view;

        private static List<Node> nodes = new List<Node>();
        private static List<Node> nodesToAdd = new List<Node>();

        public static bool EDIT_MODE;

        public static float time;

        public static void LoadNewScene(Scene scene){
            foreach(Node n in nodes){
                n.end();
            }

            nodes.Clear();
            nodes = scene.initScene();

            foreach(Node n in nodes){
                n.begin();
            }
        }

        public static void Update(float delta){
            time += delta;
            foreach(Node n in nodes){
                if(n.state == Node.NodeState.RUNNING){
                    n.update(delta);

                }
            }

            nodes.RemoveAll((Node n) => n.state == Node.NodeState.DEAD);

            foreach(Node n in nodesToAdd){
                nodes.Add(n);
                n.begin();
            }

            nodesToAdd.Clear();
        }

        public static void Render(float delta){
            foreach(Node n in nodes){
                if(n.visible){
                    n.render(delta);
                }
            }
        }

        public static void End(){
            foreach(Node n in nodes){
                n.end();
            }
        }

        public static void Instantiate(Node n){
            nodesToAdd.Add(n);
        }

        public static Node? GetNode(string name){
            return nodes.Find((Node n) => n.name == name);
        }

        public static Node[] GetNodesOfType<T>() where T : Node{
            return nodes.FindAll((Node n) => n is T).ToArray();
        }

        public static Node? GetFirstNodeOfType<T>() where T : Node{
            return nodes.Find((Node n) => n is T);
        }

        public static Node[] GetNodes(){
            return nodes.ToArray();
        }
    }
}