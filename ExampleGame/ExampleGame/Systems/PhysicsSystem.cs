using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Systems;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using GameEngine.Components;

namespace ExampleGame
{
    public class PhysicsSystem : EntitySystem, IUpdatableSystem
    {
        //public PhysicsSystem(SceneManager sceneManager) :
        //    base(sceneManager) 
        //{
        //}

        public void Update(GameTime gameTime)
        {
            //var entities = ComponentManager.Instance.GetEntities<VelocityComponent>(SceneManager.CurrentScene.Entities);
            //var collidableEntities = ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(SceneManager.CurrentScene.Entities);

            var entities = ComponentManager.Instance.GetEntities<VelocityComponent>(SceneManager.Instance.CurrentScene.Layers[Layers.BARRAROK].Entities);
            entities.AddRange(ComponentManager.Instance.GetEntities<VelocityComponent>(SceneManager.Instance.CurrentScene.Layers[Layers.JACK].Entities));
            var collidableEntities = ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(SceneManager.Instance.CurrentScene.Layers[Layers.WALKABLE_OBJECTS].Entities);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var entity in entities)
            {
                var position = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                var velocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
                var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);
                var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);

                velocity.Velocity = PhysicsManager.Instance.ApplyFriction(velocity.Velocity, body, dt);
                position.Position = PhysicsManager.Instance.Move(position.Position, new Vector2(velocity.Velocity.X * dt, 0));
                collision.Rectangle = PhysicsManager.Instance.UpdateCollisionBox(collision.Rectangle, position.Position);

                //foreach (var collidableEntity in collidableEntities)
                //{
                //    var otherCollision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(collidableEntity);

                //    if (collision == otherCollision)
                //        continue;

                //    if (collision.Rectangle.Intersects(otherCollision.Rectangle))
                //    {
                //        if (collision.Rectangle.Left < otherCollision.Rectangle.Left)
                //            position.Position = new Vector2(otherCollision.Rectangle.Left - (collision.Rectangle.Width + collision.Rectangle.Width / 2), position.Position.Y);
                //        else
                //            position.Position = new Vector2(otherCollision.Rectangle.Right - collision.Rectangle.Width / 2, position.Position.Y); 

                //        velocity.Velocity = new Vector2(0, velocity.Velocity.Y);
                //    }
                //}
                PhysicsManager.Instance.AvoidWallCollisions(entities, collidableEntities, collision, position, velocity);
                if (!body.OnGround)
                {
                    velocity.Velocity = PhysicsManager.Instance.ApplyGravity(velocity.Velocity, body, dt);
                    position.Position = PhysicsManager.Instance.Move(position.Position, new Vector2(0, velocity.Velocity.Y * dt));
                }

                collision.Rectangle = PhysicsManager.Instance.UpdateCollisionBox(collision.Rectangle, position.Position);
                body.OnGround = PhysicsManager.Instance.IsOnGround(entity);
            }
        }

        private void Move(TransformComponent position, Vector2 velocity)
        {
            position.Position += velocity;
            //For the camera, we need to put Jack at a pixel and not in between. Which means that we can't have it at x.xx, need it at x.00
            var p = position.Position;
            p.X = (float)Math.Round(p.X);
            position.Position = p;
        }

        private void UpdateCollisionBox(CollisionRectangleComponent collision, TransformComponent position)
        {
            collision.Rectangle = new Rectangle((int)position.Position.X + 32, (int)position.Position.Y, collision.Rectangle.Width, collision.Rectangle.Height);
        }

        private void ApplyFriction(VelocityComponent velocity, RigidBodyComponent body, float dt)
        {
            Vector2 newVelocity = velocity.Velocity;

            newVelocity.X *= (1 - body.Friction);

            velocity.Velocity = newVelocity;
        }

        private void ApplyGravity(VelocityComponent velocity, RigidBodyComponent body, float dt)
        {
            Vector2 newVelocity = velocity.Velocity;

            newVelocity.Y += body.Gravity;

            velocity.Velocity = newVelocity;
        }
    }
}
