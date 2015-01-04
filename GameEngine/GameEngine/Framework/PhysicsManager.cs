using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameEngine.Components;

namespace GameEngine.Framework
{
    public class PhysicsManager
    {
        private static PhysicsManager manager;
        private SceneManager sceneManager;
        public static PhysicsManager Instance
        {
            get
            {
                if (manager == null)
                    manager = new PhysicsManager();
                return manager;
            }
        }

        private PhysicsManager()
        {
        }

        PhysicsManager(SceneManager manager) 
        {
            sceneManager = manager;
        }

        //public bool CollidedWithObject()
        //{
        //    var collidableEntities = ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(sceneManager.CurrentScene.Layers[3].Entities);
        //    var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);
        //    var position = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);


        //    CollisionRectangleComponent temp = collision;

        //    temp.Rectangle = new Rectangle((int)position.Position.X + 32, (int)position.Position.Y, collision.Rectangle.Width, collision.Rectangle.Height);

        //    foreach (var collidableEntity in collidableEntities)
        //    {
        //        var otherCollision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(collidableEntity);

        //        if (temp == otherCollision)
        //            continue;

        //        if (temp.Rectangle.Intersects(otherCollision.Rectangle))
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        public bool CollidedWithEnemy(Entity entity, SceneManager sceneManager)
        {
            var collidableEntities = ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(sceneManager.CurrentScene.Layers[3].Entities);
            var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);
            var position = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
            

            CollisionRectangleComponent temp = collision;

            temp.Rectangle = new Rectangle((int)position.Position.X + 32, (int)position.Position.Y, collision.Rectangle.Width, collision.Rectangle.Height);

            foreach (var collidableEntity in collidableEntities)
            {
                var otherCollision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(collidableEntity);

                if (temp == otherCollision)
                    continue;

                if (temp.Rectangle.Intersects(otherCollision.Rectangle))
                {
                    return true;
                }
            }

            return false;
        }

        public void UpdateCollisionBox(CollisionRectangleComponent collision, TransformComponent position)
        {
            collision.Rectangle = new Rectangle((int)position.Position.X + 32, (int)position.Position.Y, collision.Rectangle.Width, collision.Rectangle.Height);
        }

        public void Move(TransformComponent position, Vector2 velocity)
        {
            position.Position += velocity;
            //For the camera, we need to put Jack at a pixel and not in between. Which means that we can't have it at x.xx, need it at x.00
            var p = position.Position;
            p.X = (float)Math.Round(p.X);
            position.Position = p;
        }

        public void ApplyFriction(VelocityComponent velocity, RigidBodyComponent body, float dt)
        {
            Vector2 newVelocity = velocity.Velocity;

            newVelocity.X *= (1 - body.Friction);

            velocity.Velocity = newVelocity;
        }

        public void ApplyGravity(VelocityComponent velocity, RigidBodyComponent body, float dt)
        {
            Vector2 newVelocity = velocity.Velocity;

            newVelocity.Y += body.Gravity;

            velocity.Velocity = newVelocity;
        }
    }
}
