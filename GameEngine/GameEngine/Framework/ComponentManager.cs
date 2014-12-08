using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Components;

namespace GameEngine.Framework
{
    public class ComponentManager
    {
        private static ComponentManager componentManager;
        private Dictionary<Type, Dictionary<Entity, IComponent>> components = new Dictionary<Type, Dictionary<Entity, IComponent>>();

        public static ComponentManager Instance
        {
            get
            {
                if (componentManager == null)
                    componentManager = new ComponentManager();

                return componentManager;
            }
        }

        public void AddComponent<T>(Entity entity, IComponent component)
        {
            Type type = typeof(T);

            if (!components.ContainsKey(type))
                components.Add(type, new Dictionary<Entity, IComponent>());

            components[type].Add(entity, component);
        }

        public void RemoveComponent<T>(Entity entity)
        {
            Type type = typeof(T);

            if (components.ContainsKey(type))
                components[type][entity] = null;
        }

        public List<Entity> GetEntities<T>()
        {
            return components[typeof(T)].Keys.ToList();
        }

        public List<Entity> GetEntities<T>(List<Entity> entities)
        {
            return components[typeof(T)].Where(x => entities.Any(y => y.ID == x.Key.ID)).Select(x => x.Key).ToList();
        }

        public List<T> GetComponentsOfType<T>() where T : IComponent
        {
            return components[typeof(T)].Values.Select(x => (T)x).ToList();
        }

        public T GetComponentOfType<T>(Entity entity) where T : IComponent
        {
            return (T)components[typeof(T)][entity];
        }
    }
}
