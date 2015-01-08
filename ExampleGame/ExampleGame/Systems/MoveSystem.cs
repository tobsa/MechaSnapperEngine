using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Systems;
using GameEngine.Framework;
using GameEngine.Components;
using Microsoft.Xna.Framework;

namespace ExampleGame.Systems
{
    public class MoveSystem : EntitySystem, IUpdatableSystem
    {


        public void Update(GameTime gameTime)
        {
            var entities = ComponentManager.Instance.GetEntities<VelocityComponent>(SceneManager.Instance.CurrentScene.Layers[3].Entities);
            entities.AddRange(ComponentManager.Instance.GetEntities<VelocityComponent>(SceneManager.Instance.CurrentScene.Layers[4].Entities));
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var entity in entities)
            {
                var position = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                var velocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
                var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);

                velocity.Velocity = PhysicsManager.Instance.ApplyFriction(velocity.Velocity, body, dt);
                position.Position = PhysicsManager.Instance.Move(position.Position, new Vector2(velocity.Velocity.X * dt, 0));

                if (!body.OnGround)
                {
                    velocity.Velocity = PhysicsManager.Instance.ApplyGravity(velocity.Velocity, body, dt);
                    position.Position = PhysicsManager.Instance.Move(position.Position, new Vector2(0, velocity.Velocity.Y * dt));
                }

                body.OnGround = PhysicsManager.Instance.IsOnGround(entity);
            }

        }
    }
}
