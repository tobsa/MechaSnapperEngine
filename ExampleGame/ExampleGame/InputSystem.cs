using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using GameEngine.Framework;
using GameEngine.Components;

namespace ExampleGame
{
    public class InputSystem : EntitySystem, IUpdateableSystem 
    {
        public InputSystem(SceneManager sceneManager) :
            base (sceneManager)
        {

        }

        public void Update(GameTime gameTime)
        {
            var entities = ComponentManager.Instance.GetEntities<InputComponent>(SceneManager.CurrentScene.Entities);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < entities.Count; i++)
            {
                var input = ComponentManager.Instance.GetComponentOfType<InputComponent>(entities[i]);
                input.Script.Update(gameTime, entities[i]);

                //var positionComponent = (TransformComponent)ComponentManager.Instance.GetComponentOfType<TransformComponent>(entities[i]);
                //var velocityComponent = (VelocityComponent)ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entities[i]);

                //var move = new Vector2(velocityComponent.Velocity.X,velocityComponent.Velocity.Y);
                //float jumpSpeed = 700;
                //float moveSpeed = 200;

                
                //if (InputManager.Instance.IsKeyDown("Left"))
                //{
                //    move.X = -moveSpeed;
                //}
                //if (InputManager.Instance.IsKeyDown("Right"))
                //{
                //    move.X = moveSpeed;
                //}

                //if (InputManager.Instance.WasKeyDown("Jump"))
                //{
                //    move.Y -= jumpSpeed;
                //}

                //velocityComponent.Velocity = move;

                //positionComponent.Position += velocityComponent.Velocity * dt;
            }
        }
    }
}
