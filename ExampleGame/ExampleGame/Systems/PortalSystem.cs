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
            var portalEntities = ComponentManager.Instance.GetEntities<PortalComponent>(SceneManager.Instance.CurrentScene.Layers[Layers.PORTALS].Entities); 
            var collidableEntities = ComponentManager.Instance.GetEntities<TransformComponent>(SceneManager.Instance.CurrentScene.Layers[Layers.JACK].Entities);
            collidableEntities.AddRange(ComponentManager.Instance.GetEntities<TransformComponent>(SceneManager.Instance.CurrentScene.Layers[Layers.PORTAL_BULLET].Entities));

            foreach (Entity entity in portalEntities)
            {
                var portalComponent = ComponentManager.Instance.GetComponentOfType<PortalComponent>(entity);
                var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);
                portalComponent.CurrentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                foreach (Entity collidable in collidableEntities)
                {
                    var collidableCollision= ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(collidable);

                    if (collision.Rectangle.Intersects(collidableCollision.Rectangle) && portalComponent.CurrentTime > portalComponent.CoolDownTime)
                    {
                        var collidableTransform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(collidable);
                        var transformComponent2 = ComponentManager.Instance.GetComponentOfType<TransformComponent>(portalComponent.PortalBuddy);
                        //set the collidable entities new position as the buddy portals position
                        Vector2 newPos = transformComponent2.Position;
                        collidableTransform.Position = newPos;
                        //reset time
                        var buddyPortal = ComponentManager.Instance.GetComponentOfType<PortalComponent>(portalComponent.PortalBuddy);
                        buddyPortal.CurrentTime = portalComponent.CurrentTime = 0;
                    }
                }
            }

        }

    }
}
