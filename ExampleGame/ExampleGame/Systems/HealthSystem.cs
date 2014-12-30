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
                if (!health.IsJack || !health.IsAlive || health.CurrentHP == 0) return;

                var cameraComponent = ComponentManager.Instance.GetComponentsOfType<CameraComponent>();
                var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                
                //Position the health
                Vector2 newPosition = transformComponent.Position;
                newPosition.X = -cameraComponent[0].Transform.M41;
                transformComponent.Position = newPosition;
            }

        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            List<Entity> entities = ComponentManager.Instance.GetEntities<HealthComponent>(SceneManager.CurrentScene.Entities);

            foreach (Entity entity in entities)
            {
                HealthComponent health = ComponentManager.Instance.GetComponentOfType<HealthComponent>(entity);
                if (!health.IsJack || !health.IsAlive || health.CurrentHP == 0) return;
                var renderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entity);

                renderComponent.Frame = health.CurrentHP - 1;
            }
        }
    }
}
