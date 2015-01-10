using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameEngine.Framework;
using ExampleGame.Components;
using GameEngine.Components;
using Microsoft.Xna.Framework.Input;
using ExampleGame.States;

namespace ExampleGame
{
    public class PlayingState : GameState
    {
        SpriteFont font;

        CameraComponent cameraComponent = null;

        public PlayingState(MechaSnapperEngine engine) :
            base(engine)
        {
            InputManager.Instance.AddKeyBinding("MainMenu", Microsoft.Xna.Framework.Input.Keys.Escape);
            InputManager.Instance.AddKeyBinding("Paused", Microsoft.Xna.Framework.Input.Keys.Enter);
            font = engine.Content.Load<SpriteFont>("Font");
        }

        public override void Update(GameTime gameTime)
        {
            SetCameraToRendering();
            //if (InputManager.Instance.WasKeyDown(0, Buttons.Start,"ChangeScene1"))
            //{
            //    //engine.SceneManager.SetCurrentScene("World1.Level1.Room1");
            //    SceneManager.Instance.SetCurrentScene("World1.Level1.Room1");
            //}
            //if (InputManager.Instance.WasKeyDown(0, Buttons.Start,"ChangeScene2"))
            //{
            //    //engine.SceneManager.SetCurrentScene("World1.Level1.Room2");
            //    SceneManager.Instance.SetCurrentScene("World1.Level1.Room2");
            //}
            //if (InputManager.Instance.WasKeyDown(0, Buttons.Start,"ChangeScene3"))
            //{
            //   // engine.SceneManager.SetCurrentScene("World1.Level2.Room1");
            //    SceneManager.Instance.SetCurrentScene("World1.Level2.Room1");
            //}

            if (InputManager.Instance.WasKeyDown(0, Buttons.Start, "MainMenu"))
            {
                SetCameraToNotRendering();
                engine.PopState();
            }
            else if (InputManager.Instance.WasKeyDown(0, Buttons.Start,"Paused"))
            {
                engine.PushState<PausedState>();
            }

            var healthComponent = ComponentManager.Instance.GetComponentsOfType<HealthComponent>();
            if (healthComponent[0].CurrentHP == 0)
            {
                engine.PushState<GameOverState>();
            }
            if (healthComponent[0].HasHorseShoe)
            {
                engine.PushState<GameWonState>();
            }

            for (int i = 0; i < updateableSystems.Count; i++)
                updateableSystems[i].Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //if (IsCameraRendering())
            //{
                for (int i = 0; i < renderableSystems.Count; i++)
                    renderableSystems[i].Draw(gameTime);

                engine.SpriteBatch.DrawString(font, "Playing\n Press Escape to go back to main menu\n Press Enter to pause/unpause the game", Vector2.Zero, Color.White);

            //}
        }

        public override void StateChanged(object sender, EventArgs e)
        {
            base.StateChanged(sender, e);
            
            if (engine.State == engine.GetState<PausedState>())
            {
                Visible = true;
            }
        }

        private void SetCameraToNotRendering()
        {
            if (cameraComponent != null)
                cameraComponent.IsRendering = false;
        }

        private void SetCameraToRendering()
        {
            if (cameraComponent != null)
                cameraComponent.IsRendering = true;
        }

        private bool IsCameraRendering()
        {
            if (cameraComponent != null)
                return cameraComponent.IsRendering;
            return false;
        }

        public void RegisterCamera(CameraComponent camera)
        {
            cameraComponent = camera;
        }
    }
}
