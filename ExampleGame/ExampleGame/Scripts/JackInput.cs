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
using Microsoft.Xna.Framework.Input;

namespace ExampleGame
{
    public class JackInput : IScript
    {
        private Entity portalGun;
        private Entity portalBullet;
        private float bulletMaxLiveTime = 4000;
        private float bulletCountTime = 0;
        private int bulletDistanceY = 700;
        private int bulletDistanceX = 700;

        private float maxVelocity = 350;
        private float jumpStrength = 720;
        private IAnimation idleAnim = new JackIdleAnimation();
        private IAnimation runningAnim = new JackRunningAnimation();
        private IAnimation fallingAnim = new JackFallingAnimation();
        private IAnimation jumpingAnim = new JackJumpingAnimation();
        private bool facingRight = true;
        private bool latestFacingRight = true;

        private int playerIndex = 1;

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
            var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(entity);
            var gunRender = ComponentManager.Instance.GetComponentOfType<RenderComponent>(portalGun);
            var gunTransform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(portalGun);
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 newVelocity = velocity.Velocity;
            anim.Animation = idleAnim;

            if (InputManager.Instance.IsKeyDown(playerIndex, Buttons.LeftThumbstickLeft, "Left"))
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

            if (InputManager.Instance.IsKeyDown(playerIndex, Buttons.LeftThumbstickRight, "Right"))
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

            if (InputManager.Instance.WasKeyDown(playerIndex, Buttons.A, "Jump"))
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

            if (InputManager.Instance.IsKeyDown(playerIndex, Buttons.RightThumbstickUp, "Up"))
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

            if (InputManager.Instance.IsKeyDown(playerIndex, Buttons.RightThumbstickDown, "Down"))
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

            if (portalBullet.Visible)
            {
                bulletCountTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (bulletCountTime > bulletMaxLiveTime)
                {
                    bulletCountTime = 0;
                    portalBullet.Visible = false;
                }
            }else
                bulletCountTime = 0;

            if (InputManager.Instance.WasKeyDown(playerIndex, Buttons.X, "Shoot"))
            {
                if (portalBullet.Visible)
                {
                    SoundManager.Instance.PlaySoundEffect("Teleport");
                    portalBullet.Visible = false;

                    var bulletTransform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(portalBullet);
                    transform.Position = bulletTransform.Position - new Vector2(64, 84);
                }
                else
                {
                    SoundManager.Instance.PlaySoundEffect("Gunshot");
                    portalBullet.Visible = true;

                    var bulletTransform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(portalBullet);
                    var teleportComponent = ComponentManager.Instance.GetComponentOfType<TeleportComponent>(portalBullet);
                    var bulletVelocity = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(portalBullet);

                    bulletTransform.Position = transform.Position + new Vector2(64, 84);
                    teleportComponent.Rotation = gunTransform.Rotation;
                    teleportComponent.Velocity = facingRight ? 500 : -500;
                    Vector2 bulletVelocitys = bulletVelocity.Velocity;
                    bulletVelocitys.Y = teleportComponent.Velocity;
                    bulletVelocitys.X = teleportComponent.Velocity;
                    bulletVelocity.Velocity = bulletVelocitys;
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

                Vector2 newPos = bulletTransform.Position;
                newPos.X += vx * dt * bulletVelocity.Velocity.X;
                newPos.Y += vy * dt * bulletVelocity.Velocity.Y;
                int side = PhysicsManager.Instance.SideCollisionDetection(portalBullet, newPos, new List<int>() { Layers.WALKABLE_OBJECTS });

                if (side > 0)
                {
                    Vector2 newBulletVelocity = bulletVelocity.Velocity;
                    if (side == 1)//top collision
                    {
                        newBulletVelocity.Y = -newBulletVelocity.Y;
                    }
                    else if (side == 2)//bottom collision
                    {
                        newBulletVelocity.Y = -newBulletVelocity.Y;
                    }
                    else if (side == 3)//left collision
                    {
                        newBulletVelocity.X = -newBulletVelocity.X;
                    }
                    else //right collision
                    {
                        newBulletVelocity.X = -newBulletVelocity.X;
                    }
                    bulletVelocity.Velocity = newBulletVelocity;
                    teleportComponent.Velocity = -teleportComponent.Velocity;
                }

               // bulletTransform.Position += new Vector2(vx, vy) * dt * teleportComponent.Velocity;
                bulletTransform.Position = newPos;

                if (bulletTransform.Position.X > transform.Position.X + bulletDistanceX || bulletTransform.Position.X < transform.Position.X - bulletDistanceX
                    || bulletTransform.Position.Y > transform.Position.Y + bulletDistanceY || bulletTransform.Position.Y < transform.Position.Y - bulletDistanceY)
                    portalBullet.Visible = false;
            }

            Vector2 collisionVelocity = PhysicsManager.Instance.ApplyFriction(velocity.Velocity, body, dt);
            Vector2 collisionPosition = PhysicsManager.Instance.Move(transform.Position, new Vector2(collisionVelocity.X * dt, 0));

            List<Entity> jackHealth = null;
            HealthComponent healthComponent = null;
            if (PhysicsManager.Instance.Collided(entity, collisionPosition, new List<int>() { Layers.BARRAROK }))
            {
                jackHealth = ComponentManager.Instance.GetEntities<HealthComponent>(SceneManager.Instance.CurrentScene.Entities);
                healthComponent = ComponentManager.Instance.GetComponentOfType<HealthComponent>(jackHealth[0]);

                if (healthComponent.HitClock >= healthComponent.HitCoolDown)
                {
                    healthComponent.CurrentHP--;
                    SoundManager.Instance.PlaySoundEffect("Punch2");
                    healthComponent.HitClock = 0;
                }
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

            

            if (PhysicsManager.Instance.Collided(entity, transform.Position, new List<int>() { Layers.HORSE_SHOE }))
            {
                if (healthComponent != null)
                    healthComponent.HasHorseShoe = true;
                else
                {
                    jackHealth = ComponentManager.Instance.GetEntities<HealthComponent>(SceneManager.Instance.CurrentScene.Entities);
                    healthComponent = ComponentManager.Instance.GetComponentOfType<HealthComponent>(jackHealth[0]);
                    healthComponent.HasHorseShoe = true;
                }
            }

        } 
    }
}
