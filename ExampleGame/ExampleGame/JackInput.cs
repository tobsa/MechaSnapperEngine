using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using GameEngine.Components;

namespace ExampleGame
{
    public class JackInput : IScript
    {
        private float maxVelocity = 350;
        private float jumpStrength = 680;

        public void Update(GameTime gameTime, Entity entity)
        {
            var position = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
            var velocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

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
                newVelocity.Y = -jumpStrength;
            }

            velocity.Velocity = newVelocity;
        }
    }
}
