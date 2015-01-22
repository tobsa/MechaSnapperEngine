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
        private HealthComponent healthComponent;
        private RenderComponent renderComponent;

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (healthComponent == null)
            {
                var entities = ComponentManager.Instance.GetEntities<HealthComponent>(SceneManager.Instance.CurrentScene.Entities);
                healthComponent = ComponentManager.Instance.GetComponentOfType<HealthComponent>(entities[0]);
            }

            healthComponent.HitClock += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (healthComponent.CurrentHP <= 0 && healthComponent.IsAlive)
            {
                healthComponent.IsAlive = false;
                SoundManager.Instance.PlaySoundEffect("JackDeath");
            }

        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (renderComponent == null)
            {
                List<Entity> entities = ComponentManager.Instance.GetEntities<HealthComponent>(SceneManager.Instance.CurrentScene.Entities);
                renderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entities[0]);
            }
            if (healthComponent == null)
            {
                var entities = ComponentManager.Instance.GetEntities<HealthComponent>(SceneManager.Instance.CurrentScene.Entities);
                healthComponent = ComponentManager.Instance.GetComponentOfType<HealthComponent>(entities[0]);
            }
            renderComponent.Frame = healthComponent.CurrentHP;
        }
    }
}
