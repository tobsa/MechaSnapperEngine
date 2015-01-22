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
    public class MoveSystem : EntitySystem, IUpdatableSystem
    {
        private List<Entity> collidableEntities = new List<Entity>();
        private List<Entity> JackAndEnemies = new List<Entity>();

        /*
         * Set collidable entities, ground, blocks, Layers.WALKABLE_ENTITES
         * This is to speed up the performance of the game. Instead of searching for all the collidable entities
         * we have them saved here.
         * Update when a new Level is started
         */
        public void SetCollidableEntities(List<Entity> entities)
        {
            collidableEntities = entities;
        }

        /*
         * Set jack and enemies, so we dont have to search for them. 
         * Reason: To speed up performance
         */
        public void SetJackAndEnemies(List<Entity> entities)
        {
            JackAndEnemies = entities;
        }
        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var entity in JackAndEnemies)
            {
                var position = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                var velocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
                var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);
                var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);

                velocity.Velocity = PhysicsManager.Instance.ApplyFriction(velocity.Velocity, body, dt);
                position.Position = PhysicsManager.Instance.Move(position.Position, new Vector2(velocity.Velocity.X * dt, 0));
                collision.Rectangle = PhysicsManager.Instance.UpdateCollisionBox(collision.Rectangle, position.Position);

                PhysicsManager.Instance.AvoidWallCollisions(JackAndEnemies, collidableEntities, collision, position, velocity);
                if (!body.OnGround)
                {
                    velocity.Velocity = PhysicsManager.Instance.ApplyGravity(velocity.Velocity, body, dt);
                    position.Position = PhysicsManager.Instance.Move(position.Position, new Vector2(0, velocity.Velocity.Y * dt));
                }

                collision.Rectangle = PhysicsManager.Instance.UpdateCollisionBox(collision.Rectangle, position.Position);
                body.OnGround = PhysicsManager.Instance.IsOnGround(entity, collidableEntities);
            }
        }
    }
}
