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

        public WalkingWorldPlayerState() { }

        //public WalkingWorldPlayerState(SceneManager sceneManager) :
        //    base(sceneManager) 
        //{
        //}

        public void Update(GameTime gameTime, Entity entity)
        {

            var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
            var velocityComponent = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
            var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);

            Vector2 tempV = velocityComponent.Velocity;

            if (InputManager.Instance.IsKeyDown("Right2"))
            {
                tempV.X += barrarokSpeed;
            }
            if (InputManager.Instance.IsKeyDown("Left2"))
            {
                tempV.X -= barrarokSpeed;
            }

            Vector2 collisionVelocity = PhysicsManager.Instance.ApplyFriction(tempV, body, (float)gameTime.ElapsedGameTime.TotalSeconds);
            Vector2 collisionPosition = PhysicsManager.Instance.Move(transformComponent.Position, new Vector2(collisionVelocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, 0));

            if (!body.OnGround || PhysicsManager.Instance.Collided(entity, collisionPosition))
            {
                if (PhysicsManager.Instance.CollidedWithJack(entity, collisionPosition))
                {
                    var jackHealth = ComponentManager.Instance.GetEntities<HealthComponent>(SceneManager.Instance.CurrentScene.Entities);
                    var healthComponent = ComponentManager.Instance.GetComponentOfType<HealthComponent>(jackHealth[0]);

                    if (healthComponent.hitClock >= healthComponent.hitCoolDown)
                    {
                        healthComponent.CurrentHP--;
                        SoundManager.Instance.PlaySoundEffect("Punch2");
                        healthComponent.hitClock = 0;
                    }
                }
                tempV.X = -(tempV.X); //Turn velocity or he will fall
            }

            velocityComponent.Velocity = tempV;
        }
    }
}
