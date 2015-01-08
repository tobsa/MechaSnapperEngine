using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using GameEngine.Systems;
using ExampleGame.Components;

namespace ExampleGame.Enemies
{
    public class WalkingState :  IAgentBehaviour
    {
        public float barrarokSpeed = 50;

        public WalkingState() { }

        //public WalkingState(SceneManager sceneManager) :
        //    base(sceneManager) 
        //{
        //}

        public void Update(GameTime gameTime, Entity entity)
        {
            var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
            var velocityComponent = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
            var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);

            Vector2 futurePosition = transformComponent.Position;
            Vector2 tempPosition = transformComponent.Position;
            Vector2 futureVelocity = velocityComponent.Velocity;

            if (futureVelocity.X < 0) tempPosition.X -= 1;
            else tempPosition.X += 1;

            Vector2 collisionVelocity = PhysicsManager.Instance.ApplyFriction(velocityComponent.Velocity, body, (float)gameTime.ElapsedGameTime.TotalSeconds);
            Vector2 collisionPosition = PhysicsManager.Instance.Move(tempPosition, new Vector2(collisionVelocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, 0));

            if (!body.OnGround || PhysicsManager.Instance.Collided(entity, collisionPosition, new List<int>() { 2,3,4 }))
            {
                if (PhysicsManager.Instance.Collided(entity, collisionPosition, new List<int>() { 4 }))
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

                //Only change position in here. Position is changed in PhysicsSystem. We only want to move the object back
                //Move object back if it is falling or it has collided
                futureVelocity.X = -futureVelocity.X; //Turn velocity
                futurePosition.X += futureVelocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds; //Move back
                barrarokSpeed = -barrarokSpeed; //Change speed +/-


            }
           
            futureVelocity.X += barrarokSpeed;
            velocityComponent.Velocity = futureVelocity;
            transformComponent.Position = futurePosition;
            
        }
    }
}
