using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameEngine.Framework;
using GameEngine.Components;

namespace GameEngine.Systems
{
    public class AISystem : EntitySystem, IUpdateableSystem
    {
        public AISystem(SceneManager sceneManager) :
            base(sceneManager)
        {
        }

        public void Update(GameTime gameTime)
        {
            var entities = ComponentManager.Instance.GetEntities<AgentComponent>(SceneManager.CurrentScene.Entities);

            foreach (var entity in entities)
            {
                var agentComponent = ComponentManager.Instance.GetComponentOfType<AgentComponent>(entity);
                agentComponent.Behaviour.Update(gameTime, entity);
            }

            //var components = ComponentManager.Instance.GetComponentsOfType<AgentComponent>();
            //components.ForEach(x => x.Behaviour.Update(gameTime));
        }
    }
}
