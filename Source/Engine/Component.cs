using Basil.Interfaces;
using System.Reflection;

namespace Basil.Engine
{
    public class Component : IUpdatable, IRenderable
    {
        public Entity entity { get; set; }
        public string name { get; set; }

        // TODO: Move to interface
        static private readonly string className = MethodBase.GetCurrentMethod().DeclaringType.Name;

        public Component()
        {
            name = className;
        }

        public Component(string name)
        {
            this.name = name;
        }

        virtual public void Update() { }
        virtual public void Render() { }
    }
}