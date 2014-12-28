using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using GameEngine.Framework;
using GameEngine.Components;

namespace ExampleGame
{
    public class InputSystem : EntitySystem, IUpdatableSystem 
    {
        public InputSystem(SceneManager sceneManager) :
            base (sceneManager)
        {

        }

        public void Update(GameTime gameTime)
        {
            var entities = ComponentManager.Instance.GetEntities<InputComponent>(SceneManager.CurrentScene.Entities);
            if (entities == null)
                return;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < entities.Count; i++)
            {
                var input = ComponentManager.Instance.GetComponentOfType<InputComponent>(entities[i]);
                input.Script.Update(gameTime, entities[i]);
            }
        }
    }
}
