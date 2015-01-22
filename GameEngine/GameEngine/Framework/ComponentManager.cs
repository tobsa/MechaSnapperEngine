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

        public void AddComponent(Entity entity, IComponent component)
        {
            Type type = component.GetType();

            if (!components.ContainsKey(type))
                components.Add(type, new Dictionary<Entity, IComponent>());

            components[type].Add(entity, component);
        }

        public void RemoveComponent<T>(Entity entity) where T : IComponent
        {
            Type type = typeof(T);

            if (components.ContainsKey(type))
                components[type][entity] = null;
        }

        public void RemoveEntityComponents(Entity entity)
        {
            foreach (var comp in components)
            {

            }
        }

        public List<Entity> GetEntities<T>() where T : IComponent
        {
            if (!components.ContainsKey(typeof(T)))
                return null;

            return components[typeof(T)].Keys.ToList();
        }

        public List<Entity> GetEntities<T>(List<Entity> entities) where T : IComponent
        {
            if(!components.ContainsKey(typeof(T)))
                return null;

            return components[typeof(T)].Where(x => entities.Any(y => y.ID == x.Key.ID)).Select(x => x.Key).ToList();
        }

        public List<Entity> GetEntities(IComponent[] comps)
        {
            List<Entity> entities = new List<Entity>();

            foreach (var component in comps)
            {
                entities.AddRange(components[component.GetType()].Keys);
            }

            return entities;
        }

        public List<T> GetComponentsOfType<T>() where T : IComponent
        {
            if(!components.ContainsKey(typeof(T)))
                return null;

            return components[typeof(T)].Values.Select(x => (T)x).ToList();
        }

        public T GetComponentOfType<T>(Entity entity) where T : IComponent
        {
            if(!components.ContainsKey(typeof(T)))
                return default(T);
            if (!components[typeof(T)].ContainsKey(entity))
                return default(T);

            return (T)components[typeof(T)][entity];
        }
    }
}
