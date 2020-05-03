using Basil.Interfaces;
using System.Collections.Generic;
using System.Reflection;

namespace Basil.Engine
{
    public class Scene : IUpdatable, IRenderable
    {
        public string name { get; set; }
        public List<Entity> entities { get; private set; } = new List<Entity>();

        static private readonly string className = MethodBase.GetCurrentMethod().DeclaringType.Name;

        public Scene()
        {
            name = className;
        }

        public Scene(string name)
        {
            this.name = name;
        }

        public void AddEntity(Entity entity)
        {
            if (entities.Contains(entity))
                throw new System.ArgumentException("Entity already exists in Scene", "entity");

            // FIXME[0001]: Is this too dangerous? What if someone manually changes the scene without
            // moving it around, or if someone just adds an entity to another scene?
            //
            // Need to clean this all up and restrict access so I can control it better.
            entities.Add(entity);
            entity.scene = this;
        }

        public void Update()
        {
            foreach (Entity entity in entities)
            {
                entity.Update();
            }
        }

        public void Render()
        {
            foreach (Entity entity in entities)
            {
                entity.Render();
            }
        }
    }
}