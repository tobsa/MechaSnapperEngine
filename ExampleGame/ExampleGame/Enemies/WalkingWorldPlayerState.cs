using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using ExampleGame.Components;

namespace ExampleGame.Enemies
{
    public class WalkingWorldPlayerState : EntitySystem, IAgentBehaviour
    {
        public float barrarokSpeed = 75;

        public WalkingWorldPlayerState(SceneManager sceneManager) :
            base(sceneManager) 
        {
        }

        public void Update(GameTime gameTime, Entity entity)
        {

            var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
            var velocityComponent = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
            var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 temp = transformComponent.Position;
            Vector2 v = velocityComponent.Velocity;
            Vector2 tempV = velocityComponent.Velocity;

            bool applyFriction = false;
            if (InputManager.Instance.IsKeyDown("Right2"))
            {
                tempV.X += barrarokSpeed;
                applyFriction = true;
            }
            if (InputManager.Instance.IsKeyDown("Left2"))
            {
                tempV.X -= barrarokSpeed;
                applyFriction = true;
            }
            v.X = tempV.X;

            if (applyFriction)
            {
                tempV.X *= (1 - body.Friction); //Apply friction
                temp.X += tempV.X * dt;    //Move
            }

            if (!body.OnGround || Collided(entity, temp, (float)gameTime.ElapsedGameTime.TotalMilliseconds))
            {
                v.X = -(v.X*2); //Turn velocity or he will fall
            }

            velocityComponent.Velocity = v;
        }

        private float hitCD = 750;
        private float hitClock = 0;
        public bool Collided(Entity entity, Vector2 position, float totalMilliSeconds)
        {
            hitClock += totalMilliSeconds;


            var collidableEntities = ComponentManager.Instance.GetEntities<CollisionRectangleComponent>(SceneManager.CurrentScene.Entities);
            var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);
            CollisionRectangleComponent temp = collision;

            temp.Rectangle = new Rectangle((int)position.X + 32, (int)position.Y, collision.Rectangle.Width, collision.Rectangle.Height);

            foreach (var collidableEntity in collidableEntities)
            {
                var otherCollision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(collidableEntity);

                if (temp == otherCollision)
                    continue;

                if (temp.Rectangle.Intersects(otherCollision.Rectangle))
                {
                    try
                    {
                        //Prevent spamming hits
                        if (hitClock >= hitCD)
                        {
                            var healthComponent = ComponentManager.Instance.GetComponentOfType<HealthComponent>(collidableEntity);
                            healthComponent.CurrentHP--;
                            SoundManager.Instance.PlaySoundEffect("Punch2");
                            hitClock = 0;
                        }
                    }
                    catch (Exception)
                    {
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
