using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;

namespace GameEngine.Systems
{
    public class EntitySystem
    {
        private SceneManager sceneManager;

        public EntitySystem(SceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        public SceneManager SceneManager
        {
            get { return sceneManager; }
        }
    }
}
