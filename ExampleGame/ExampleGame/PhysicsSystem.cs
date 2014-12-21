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
    public class PhysicsSystem : EntitySystem, IUpdateableSystem
    {
        public PhysicsSystem(SceneManager sceneManager) :
            base(sceneManager) { }

        public void Update(GameTime gameTime)
        {
            var entities = ComponentManager.Instance.GetEntities<VelocityComponent>(SceneManager.CurrentScene.Entities);
            var collisionEntities = ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(SceneManager.CurrentScene.Entities);


            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var entity in entities)
            {
                var position = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                var velocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
                var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);
                var box1 = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);

                ApplyFriction(velocity, body, dt);
                ApplyGravity(velocity, body, dt);

                position.Position += new Vector2(velocity.Velocity.X * dt, 0);
                UpdateCollisionBox(box1, position.Position + new Vector2(0, 0), box1.Rectangle.Width, box1.Rectangle.Height);
                foreach (var moveableEntity in collisionEntities)
                {
                    var box2 = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(moveableEntity);

                    if (box1 == box2)
                        continue;

                    if (box1.Rectangle.Intersects(box2.Rectangle))
                    {
                        if(position.Position.X < box2.Rectangle.Left)
                            position.Position = new Vector2(box2.Rectangle.Left - box1.Rectangle.Width, position.Position.Y);
                        else
                            position.Position = new Vector2(box2.Rectangle.Right, position.Position.Y);

                        velocity.Velocity = new Vector2(0, velocity.Velocity.Y);
                        UpdateCollisionBox(box1, position.Position, box1.Rectangle.Width, box1.Rectangle.Height);
                    }
                }

                position.Position += new Vector2(0, velocity.Velocity.Y * dt);
                UpdateCollisionBox(box1, position.Position, box1.Rectangle.Width, box1.Rectangle.Height);

                foreach (var moveableEntity in collisionEntities)
                {
                    var box2 = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(moveableEntity);

                    if (box1 == box2)
                        continue;

                    if (box1.Rectangle.Intersects(box2.Rectangle))
                    {
                        if(position.Position.Y < box2.Rectangle.Top)
                            position.Position = new Vector2(position.Position.X, box2.Rectangle.Top - box1.Rectangle.Height);
                        else
                            position.Position = new Vector2(position.Position.X, box2.Rectangle.Bottom);

                        velocity.Velocity = new Vector2(velocity.Velocity.X, 0);
                        UpdateCollisionBox(box1, position.Position, box1.Rectangle.Width, box1.Rectangle.Height);
                    }                    
                } 
            }
        }

        private void UpdateCollisionBox(CollisionRectangleComponent box, Vector2 position, int width, int height)
        {
            box.Rectangle = new Rectangle((int)position.X, (int)position.Y, box.Rectangle.Width, box.Rectangle.Height);
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
