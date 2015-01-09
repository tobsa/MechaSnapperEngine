using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using GameEngine.Framework;
using ExampleGame.Components;
using GameEngine.Components;

namespace ExampleGame.Systems
{
    public class PortalSystem : EntitySystem, IUpdatableSystem
    {


        public void Update(GameTime gameTime)
        {
            var entities = ComponentManager.Instance.GetEntities<PortalComponent>(SceneManager.Instance.CurrentScene.Entities); //Get Jack
            var jackEntity = ComponentManager.Instance.GetEntities<TransformComponent>(SceneManager.Instance.CurrentScene.Layers[Layers.JACK].Entities); //Get Jack
            var jackTransform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(jackEntity[0]);
            var jackCollision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(jackEntity[0]);

            foreach (Entity entity in entities)
            {
                var portalComponent = ComponentManager.Instance.GetComponentOfType<PortalComponent>(entity);
                var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(portalComponent.FirstPortal);
                portalComponent.CurrentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (collision.Rectangle.Intersects(jackCollision.Rectangle) && portalComponent.CurrentTime > portalComponent.CoolDownTime)
                {
                    var transformComponent2 = ComponentManager.Instance.GetComponentOfType<TransformComponent>(portalComponent.SecondPortal);
                    jackTransform.Position = transformComponent2.Position;
                    var portalComponent2 = ComponentManager.Instance.GetComponentOfType<PortalComponent>(portalComponent.SecondPortal);
                    portalComponent2.CurrentTime = portalComponent.CurrentTime = 0;
                }
            }

        }

    }
}
