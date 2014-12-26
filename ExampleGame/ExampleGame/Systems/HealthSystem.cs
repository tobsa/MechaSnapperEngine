using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Systems;
using GameEngine.Framework;
using ExampleGame.Components;
using GameEngine.Components;

namespace ExampleGame.Systems
{
    public class HealthSystem : EntitySystem, IUpdatableSystem, IRenderableSystem
    {
        public HealthSystem(SceneManager manager)
            : base(manager) {

        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            List<Entity> entities = ComponentManager.Instance.GetEntities<HealthComponent>(SceneManager.CurrentScene.Entities);

            foreach (Entity entity in entities)
            {
                //Playa sound om död
            }

        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {

            List<Entity> entities = ComponentManager.Instance.GetEntities<HealthComponent>(SceneManager.CurrentScene.Entities);

            foreach (Entity entity in entities)
            {
                HealthComponent health = ComponentManager.Instance.GetComponentOfType<HealthComponent>(entity);
                if (!health.IsJack) return;

                var renderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entity);


            }

        }
    }
}
