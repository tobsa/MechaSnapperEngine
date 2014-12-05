using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Framework
{
    public class Scene
    {
        private string name;
        private SortedList<int, Layer> layers = new SortedList<int, Layer>();
        private List<Entity> entities = new List<Entity>();

        public Scene(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }


        public List<Layer> Layers
        {
            get { return layers.Values.ToList(); }
        }

        public List<Entity> Entities
        {
            get { return entities; }
        }

        public void AddLayer(int layerPriority, Layer layer)
        {
            layers.Add(layerPriority, layer);
        }

        public void RemoveLayer(int layerPriority)
        {
            layers.Remove(layerPriority);
        }

        public void AddEntity(int layerPriority, Entity entity)
        {
            entities.Add(entity);

            if (!layers.ContainsKey(layerPriority))
                AddLayer(layerPriority, new Layer());

            layers[layerPriority].AddEntity(entity);
        }
    }
}
