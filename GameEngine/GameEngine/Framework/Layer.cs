using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Framework
{
    public class Layer
    {
        private List<Entity> entities = new List<Entity>();

        public List<Entity> Entities
        {
            get { return entities; }
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }
    }
}
