using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using ExampleGame.Animations;

namespace ExampleGame
{
    public class JackInput : IScript
    {
        private float maxVelocity = 350;
        private float jumpStrength = 720;
        private IAnimation idleAnim = new JackIdleAnimation();
        private IAnimation runningAnim = new JackRunningAnimation();
        private IAnimation fallingAnim = new JackFallingAnimation();
        private IAnimation jumpingAnim = new JackJumpingAnimation();
        private bool facingRight = true;
        

        public void Update(GameTime gameTime, Entity entity)
        {
            // FIX : set transform component in order to get flip to work
            var transform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
            var velocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
            var anim = ComponentManager.Instance.GetComponentOfType<AnimationComponent>(entity);
            
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 newVelocity = velocity.Velocity;
            anim.Animation = idleAnim;

            if (InputManager.Instance.IsKeyDown("Left"))
            {
                if (facingRight)
                {
                    Vector2 newScale = flip(transform.Scale);
                    transform.Scale = newScale;
                }

                newVelocity.X = -maxVelocity;
                anim.Animation = runningAnim;
            }
            if (InputManager.Instance.IsKeyDown("Right"))
            {
                if (!facingRight)
                {
                    Vector2 newScale = flip(transform.Scale);
                    transform.Scale = newScale;
                }
                newVelocity.X = +maxVelocity;
                anim.Animation = runningAnim;
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

            // set animaiton based on movement
            if (velocity.Velocity.Y > 5)
            {
                //anim.Animation = fallingAnim;
            }
            else if (velocity.Velocity.Y < 0)
            {
                anim.Animation = jumpingAnim;
            }


        }

        private Vector2 flip(Vector2 scale)
        {
            facingRight = !facingRight;
            Vector2 newScale = scale;
            newScale.X *= -1;
            return newScale;
        }

    }
}
