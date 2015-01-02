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
    public class BarrockAI : EntitySystem, IAgentBehaviour
    {
        public float barrarokSpeed = 50;

        public BarrockAI(SceneManager sceneManager) :
            base(sceneManager) 
        {
        }

        public void Update(GameTime gameTime, Entity entity)
        {
            var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
            var velocityComponent = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
            var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 futurePosition = transformComponent.Position;
            Vector2 temp = transformComponent.Position;
            Vector2 v = velocityComponent.Velocity;
            Vector2 tempV = velocityComponent.Velocity;

            tempV.X *= (1 - body.Friction); //Apply friction
            temp.X += tempV.X  * dt;    //Move

            if (!body.OnGround || Collided(entity, temp))
            {
                //Only change position in here. Position is changed in PhysicsSystem
                //Move object back if it is falling or it has collided
                v.X = -v.X; //Turn velocity
                futurePosition.X += v.X * dt; //Move back
                barrarokSpeed = -barrarokSpeed; //Change speed +/-
            }

            v.X += barrarokSpeed;
            velocityComponent.Velocity = v;
            transformComponent.Position = futurePosition;
            
        }
        //Borde ej finnas här.
        public bool Collided(Entity entity, Vector2 position)
        {
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
                        var jackHealth = ComponentManager.Instance.GetEntities<HealthComponent>(SceneManager.CurrentScene.Entities);
                        var healthComponent = ComponentManager.Instance.GetComponentOfType<HealthComponent>(jackHealth[0]);
                        healthComponent.CurrentHP--;
                        SoundManager.Instance.PlaySoundEffect("Punch2");
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
