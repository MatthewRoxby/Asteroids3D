using System.Reflection;
using ImGuiNET;

namespace nodes{
    public class Node{
        public Guid ID {get; private set;}
        public string name;

        public bool visible = true;

        public enum NodeState{
            BORN = 0,
            RUNNING = 1,
            PAUSED = 2,
            DEAD = 3
        }

        public NodeState state {get; private set;}

        public Node(string name = "Node"){
            this.name = name;
            this.ID = Guid.NewGuid();
            state = NodeState.BORN;
        }

        public virtual void begin(){
            state = NodeState.RUNNING;
        }

        public virtual void update(float delta){
            
        }

        public virtual void render(float delta){

        }

        public virtual void end(){
            state = NodeState.DEAD;
        }

        public void setPaused(bool b){
            if(state == NodeState.RUNNING || state == NodeState.PAUSED){
                state = b? NodeState.PAUSED : NodeState.RUNNING;
            }
        }

        public virtual void inspect(){
            ImGui.Text("type: " + this.GetType().Name + "\nID: " + ID.ToString());
            ImGui.InputText("name", ref name, 20);
            ImGui.Text("state: " + state.ToString());
            bool b = state == NodeState.PAUSED;
            ImGui.Checkbox("paused" , ref b);
            setPaused(b);

            ImGui.Checkbox("visible", ref visible);
        }
    }
}