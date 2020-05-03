using Basil.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace Basil.Engine
{
    public class Entity : IUpdatable, IRenderable
    {
        public bool snap = true;
        public string name { get; set; } = "";

        public Scene scene { get; set; }
        public Transform transform { get; private set; } = new Transform();
        public Transform gridTransform
        {
            get
            {
                Transform snappedTransform = new Transform();
                Vector2 position = snappedTransform.position;

                // TODO: Move magic numbers to variable
                position.X = (int)Math.Floor(transform.position.X / 8) * 8;
                position.Y = (int)Math.Floor(transform.position.Y / 8) * 8;

                return snappedTransform;
            }
        }
        public List<Component> components { get; private set; } = new List<Component>();

        // TODO: Move to interface
        static private readonly string className = MethodBase.GetCurrentMethod().DeclaringType.Name;


        public Entity()
        {
            name = className;
        }

        public Entity(string name)
        {
            this.name = name;
        }

        public void AddComponent(Component component)
        {
            if (components.Contains(component))
                throw new ArgumentException("Component already exists in Entity", "component");

            // Fixme: See Fixme[0001]
            components.Add(component);
            component.entity = this;
        }

        public void Update()
        {
            foreach (Component component in components) component.Update();
        }

        public void Render()
        {
            foreach (Component component in components) component.Render();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}