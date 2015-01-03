using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using ExampleGame.Animations;
using Microsoft.Xna.Framework.Graphics;
using ExampleGame.Components;

namespace ExampleGame
{
    public class JackInput : IScript
    {
        private Entity portalGun;
        private Entity portalBullet;

        private float maxVelocity = 350;
        private float jumpStrength = 720;
        private IAnimation idleAnim = new JackIdleAnimation();
        private IAnimation runningAnim = new JackRunningAnimation();
        private IAnimation fallingAnim = new JackFallingAnimation();
        private IAnimation jumpingAnim = new JackJumpingAnimation();
        private bool facingRight = true;
        private bool latestFacingRight = true;

        public JackInput(Entity portalGun, Entity portalBullet)
        {
            this.portalGun = portalGun;
            this.portalBullet = portalBullet;
        }

        public void Update(GameTime gameTime, Entity entity)
        {
            var transform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
            var velocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
            var anim = ComponentManager.Instance.GetComponentOfType<AnimationComponent>(entity);
            var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);
            var render = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entity);
            var gunRender = ComponentManager.Instance.GetComponentOfType<RenderComponent>(portalGun);
            var gunTransform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(portalGun);
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 newVelocity = velocity.Velocity;
            anim.Animation = idleAnim;

            if (InputManager.Instance.IsKeyDown("Left"))
            {
                render.SpriteEffect = SpriteEffects.FlipHorizontally;
                gunRender.Effect = SpriteEffects.FlipHorizontally;
                gunRender.SpriteEffect = SpriteEffects.FlipHorizontally;
                transform.Position = transform.Position;
                facingRight = false;
                render.Effect = SpriteEffects.FlipHorizontally;
                newVelocity.X = -maxVelocity;
                anim.Animation = runningAnim;
            }

            if (InputManager.Instance.IsKeyDown("Right"))
            {
                facingRight = true;
                transform.Position = transform.Position;
                render.Effect = SpriteEffects.None;
                render.SpriteEffect = SpriteEffects.None;
                gunRender.Effect = SpriteEffects.None;
                gunRender.SpriteEffect = SpriteEffects.None;
                newVelocity.X = +maxVelocity;
                anim.Animation = runningAnim;
            }

            if (InputManager.Instance.WasKeyDown("Jump"))
            {
                if (body.OnGround)
                {
                    newVelocity.Y = -jumpStrength;
                    body.OnGround = false;
                    anim.Animation = jumpingAnim;
                    SoundManager.Instance.PlaySoundEffect("JackJump");
                }
            }

            float rightLowLimit = 0.8f;
            float rightHighLimit = -0.8f;
            float leftLowLimit = -0.8f;
            float leftHighLimit = 0.8f;

            if (InputManager.Instance.IsKeyDown("Up"))
            {
                if (facingRight)
                {
                    if (gunTransform.Rotation > rightHighLimit)
                        gunTransform.Rotation -= 0.1f;
                }
                else
                {
                    if (gunTransform.Rotation < leftHighLimit)
                        gunTransform.Rotation += 0.1f;
                }
            }

            if (InputManager.Instance.IsKeyDown("Down"))
            {
                if (facingRight)
                {
                    if (gunTransform.Rotation < rightLowLimit)
                        gunTransform.Rotation += 0.1f;
                }
                else
                {
                    if (gunTransform.Rotation > leftLowLimit)
                        gunTransform.Rotation -= 0.1f;
                }
            }

            if (InputManager.Instance.WasKeyDown("Shoot"))
            {
                if (portalBullet.Visible)
                {
                    portalBullet.Visible = false;

                    var bulletTransform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(portalBullet);
                    transform.Position = bulletTransform.Position - new Vector2(64, 84);
                }
                else
                {
                    portalBullet.Visible = true;

                    var bulletTransform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(portalBullet);
                    var teleportComponent = ComponentManager.Instance.GetComponentOfType<TeleportComponent>(portalBullet);

                    bulletTransform.Position = transform.Position + new Vector2(64, 84);
                    teleportComponent.Rotation = gunTransform.Rotation;
                    teleportComponent.Velocity = facingRight ? 600 : -600;
                }
            }

            if (portalBullet.Visible)
            {
                var bulletTransform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(portalBullet);
                var bulletVelocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(portalBullet);
                var teleportComponent = ComponentManager.Instance.GetComponentOfType<TeleportComponent>(portalBullet);

                float rotation = teleportComponent.Rotation * 65;

                float vx = (float)Math.Cos(MathHelper.ToRadians(rotation));
                float vy = (float)Math.Sin(MathHelper.ToRadians(rotation));

                bulletTransform.Position += new Vector2(vx, vy) * dt * teleportComponent.Velocity;
            }

            velocity.Velocity = newVelocity;

            if (velocity.Velocity.Y < 0)
            {
                anim.Animation = jumpingAnim;
            }
            else if (body.OnGround == false)
            {
                anim.Animation = fallingAnim;
            }

            if (latestFacingRight != facingRight)
            {
                gunTransform.Rotation = -gunTransform.Rotation;
            }

            latestFacingRight = facingRight;
        }
    }
}
