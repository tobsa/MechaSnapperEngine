using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Systems;
using GameEngine.Framework;
using ExampleGame.Components;
using GameEngine.Components;
using Microsoft.Xna.Framework;

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
                HealthComponent health = ComponentManager.Instance.GetComponentOfType<HealthComponent>(entity);
                if (!health.IsJack) return;

                if (health.CurrentHP <= 0 && health.IsAlive)
                {
                    health.IsAlive = false;
                    SoundManager.Instance.PlaySoundEffect("JackDeath");
                }
            }

        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            List<Entity> entities = ComponentManager.Instance.GetEntities<HealthComponent>(SceneManager.CurrentScene.Entities);

            foreach (Entity entity in entities)
            {
                HealthComponent health = ComponentManager.Instance.GetComponentOfType<HealthComponent>(entity);
                if (!health.IsJack) return;
                if (health.CurrentHP < 0) health.CurrentHP = 0;
                var renderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entity);

                renderComponent.Frame = health.CurrentHP;
            }
        }
    }
}
