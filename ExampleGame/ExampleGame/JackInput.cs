using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using ExampleGame.Components;

namespace ExampleGame
{
    public class JackInput : IScript
    {
        private float maxVelocity = 350;
        private float jumpStrength = 720;

        public void Update(GameTime gameTime, Entity entity)
        {
            var position = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
            var velocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
            
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var p = position.Position;
            p.X = (float)Math.Round(p.X);
            position.Position = p;

            Vector2 newVelocity = velocity.Velocity;

            if (InputManager.Instance.IsKeyDown("Left"))
            {
                newVelocity.X = -maxVelocity; 
            }
            if (InputManager.Instance.IsKeyDown("Right"))
            {
                newVelocity.X = +maxVelocity;  
            }

            if (InputManager.Instance.WasKeyDown("Jump"))
            {
                var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);
                //if (body.OnGround)
                {
                    newVelocity.Y = -jumpStrength;
                    body.OnGround = false;
                }
            }

            velocity.Velocity = newVelocity;
        }
    }
}
