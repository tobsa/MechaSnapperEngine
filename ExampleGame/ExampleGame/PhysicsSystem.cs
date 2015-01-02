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
        public PhysicsSystem(SceneManager sceneManager) :
            base(sceneManager) 
        {
        }

        public void Update(GameTime gameTime)
        {
            var entities = ComponentManager.Instance.GetEntities<VelocityComponent>(SceneManager.CurrentScene.Entities);
            var collidableEntities = ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(SceneManager.CurrentScene.Entities);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var entity in entities)
            {
                var position = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                var velocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
                var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);
                var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);

                ApplyFriction(velocity, body, dt);
                Move(position, new Vector2(velocity.Velocity.X * dt, 0));
                UpdateCollisionBox(collision, position);

                foreach (var collidableEntity in collidableEntities)
                {
                    var otherCollision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(collidableEntity);

                    if (collision == otherCollision)
                        continue;

                    if (collision.Rectangle.Intersects(otherCollision.Rectangle))
                    {
                        if (collision.Rectangle.Left < otherCollision.Rectangle.Left)
                            position.Position = new Vector2(otherCollision.Rectangle.Left - (collision.Rectangle.Width + collision.Rectangle.Width / 2), position.Position.Y);
                        else
                            position.Position = new Vector2(otherCollision.Rectangle.Right - collision.Rectangle.Width / 2, position.Position.Y);

                        velocity.Velocity = new Vector2(0, velocity.Velocity.Y);
                        UpdateCollisionBox(collision, position);
                    }
                }

                if (!body.OnGround)
                {
                    ApplyGravity(velocity, body, dt);
                    Move(position, new Vector2(0, velocity.Velocity.Y * dt));
                    UpdateCollisionBox(collision, position);
                }
                

                bool onGround = false;
                foreach (var collidableEntity in collidableEntities)
                {
                    var otherCollision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(collidableEntity);

                    if (collision == otherCollision)
                        continue;

                    Point bottomLeft = new Point(collision.Rectangle.Left + 1, collision.Rectangle.Bottom + 1);
                    Point bottomRight = new Point(collision.Rectangle.Right - 1, collision.Rectangle.Bottom + 1);

                    if (otherCollision.Rectangle.Contains(bottomLeft) || otherCollision.Rectangle.Contains(bottomRight))
                    {
                        onGround = true;
                        velocity.Velocity = new Vector2(velocity.Velocity.X, 0);
                    }

                    if (collision.Rectangle.Intersects(otherCollision.Rectangle))
                    {
                        if (position.Position.Y < otherCollision.Rectangle.Top)
                        {
                            position.Position = new Vector2(position.Position.X, otherCollision.Rectangle.Top - collision.Rectangle.Height);
                            body.OnGround = true;
                        }
                        else
                            position.Position = new Vector2(position.Position.X, otherCollision.Rectangle.Bottom);

                        UpdateCollisionBox(collision, position);
                        velocity.Velocity = new Vector2(velocity.Velocity.X, 0);
                    }
                }

                body.OnGround = onGround;
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
