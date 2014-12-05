using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Framework
{
    public class SceneManager
    {
        private List<Entity> entities = new List<Entity>();
        private Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();
        private Scene currentScene;

        public Scene CurrentScene
        {
            get { return currentScene; }
        }

        public List<Scene> Scenes
        {
            get { return scenes.Values.ToList(); }
        }

        public void SetCurrentScene(string name)
        {
            currentScene = scenes[name];
        }

        public void AddScene(Scene scene)
        {
            if (!scenes.ContainsKey(scene.Name))
                scenes.Add(scene.Name, scene);
        }

        public void AddEntity(string name, int layerPriority, Entity entity)
        {
            entities.Add(entity);

            if (!scenes.ContainsKey(name))
                AddScene(new Scene(name));

            scenes[name].AddEntity(layerPriority, entity);
        }
    }
}
