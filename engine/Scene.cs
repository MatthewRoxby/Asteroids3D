using nodes;

namespace engine{
    public abstract class Scene{
        public virtual List<Node> initScene(){
            return new List<Node>();
        }
    }
}