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

        /*
         * layers: a list of layers to collide with
         * 
         * returns true if collided with anything in the layers
         * false if not collided
         */
        public bool Collided(Entity entity,Vector2 position, List<int> layers)
        {
            List<Entity> collidableEntities = new List<Entity>();
            if (layers == null)
                collidableEntities = ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(SceneManager.Instance.CurrentScene.Entities);
            else
            {
                foreach (int layer in layers)
                    collidableEntities.AddRange(ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(SceneManager.Instance.CurrentScene.Layers[layer].Entities));
            }
            var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);

            CollisionRectangleComponent temp = collision;
            temp.Rectangle = new Rectangle((int)position.X, (int)position.Y, collision.Rectangle.Width, collision.Rectangle.Height);

            foreach (var collidableEntity in collidableEntities)
            {
                var otherCollision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(collidableEntity);

                if (temp == otherCollision)
                    continue;

                if (temp.Rectangle.Intersects(otherCollision.Rectangle))
                    return true;
            }

            return false;
        }

        /*
         * layers: a list of layers to collide with
         * 
         * Returns an Integer which tells which side the collision was on
         * 0 = no collision, 1 = top, 2 = bottom, 3 = left, 4 = right
         */
        public int SideCollisionDetection(Entity entity, Vector2 position, List<int> layers)
        {
            List<Entity> collidableEntities = new List<Entity>();
            if (layers == null)
                collidableEntities = ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(SceneManager.Instance.CurrentScene.Entities);
            else
            {
                foreach (int layer in layers)
                    collidableEntities.AddRange(ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(SceneManager.Instance.CurrentScene.Layers[layer].Entities));
            }
            var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);
            var transformComp = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);

            CollisionRectangleComponent temp = collision;
            temp.Rectangle = new Rectangle((int)position.X, (int)position.Y, collision.Rectangle.Width, collision.Rectangle.Height);

            foreach (var collidableEntity in collidableEntities)
            {
                var otherCollision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(collidableEntity);

                if (temp == otherCollision)
                    continue;

                if (temp.Rectangle.Intersects(otherCollision.Rectangle))
                {
                    int top, bottom, left, right;
                    top = bottom = left = right = 0;

                    top = temp.Rectangle.Bottom - otherCollision.Rectangle.Top;
                    bottom = otherCollision.Rectangle.Bottom - temp.Rectangle.Top;
                    left = otherCollision.Rectangle.Right - temp.Rectangle.Left;
                    right = temp.Rectangle.Right - otherCollision.Rectangle.Left;

                    int side = 1;
                    int min = Math.Min(int.MaxValue, top);
                    min = Math.Min(min, bottom);
                    if (min == bottom) side = 2;
                    min = Math.Min(min, left);
                    if (min == left) side = 3;
                    min = Math.Min(min, right);
                    if (min == right) side = 4;

                    return side;
                }
            }

            return 0;
        }

        /*
         * Stops the entity walkin into a wall. Sets the entities velocity to 0 if its walking into a wall
         */
        public void AvoidWallCollisions(List<Entity> entities, List<Entity> collidableEntities, CollisionRectangleComponent collision, TransformComponent position, VelocityComponent velocity)
        {
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
                }
            }
        }

        /*
         * Evaluates if the entity is horizontally on one of the collidableEntities. ei, is it standing on one of them?
         * 
         */
        public bool IsOnGround(Entity entity, List<Entity> collidableEntities)
        {
            var position = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
            var velocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
            var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);
            var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);

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
                    if (position.Position.Y <= otherCollision.Rectangle.Top)
                    {
                        position.Position = new Vector2(position.Position.X, otherCollision.Rectangle.Top - collision.Rectangle.Height);
                        onGround = true;
                        return onGround;
                    }
                    else if(!onGround)
                        position.Position = new Vector2(position.Position.X, otherCollision.Rectangle.Bottom); 

                    velocity.Velocity = new Vector2(velocity.Velocity.X, 0);
                }
            }
            collision.Rectangle = PhysicsManager.Instance.UpdateCollisionBox(collision.Rectangle, position.Position);
            //if (collision.Rectangle.Width == 32 && onGround == false)
            //{
            //    int a = 0;
            //}
            return onGround;
        }

        public Rectangle UpdateCollisionBox(Rectangle collision, Vector2 position)
        {
            return new Rectangle((int)position.X + 32, (int)position.Y, collision.Width, collision.Height);
        }

        public Vector2 Move(Vector2 position, Vector2 velocity)
        {
            position += velocity;
            //For the camera, we need to put Jack at a pixel and not in between. Which means that we can't have it at x.xx, need it at x.00
            position.X = (float)Math.Round(position.X);
            return position;
        }

        public Vector2 ApplyFriction(Vector2 velocity, RigidBodyComponent body, float dt)
        {
            Vector2 newVelocity = velocity;

            newVelocity.X *= (1 - body.Friction);

            return newVelocity;
        }

        public Vector2 ApplyGravity(Vector2 velocity, RigidBodyComponent body, float dt)
        {
            Vector2 newVelocity = velocity;

            newVelocity.Y += body.Gravity;

            return newVelocity;
        }
    }
}
