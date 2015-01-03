using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Systems;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using ExampleGame.Components;
using GameEngine.Components;
using Microsoft.Xna.Framework.Graphics;
using ExampleGame.Enemies;

namespace ExampleGame.Systems
{
    public class EnemySelectSystem : EntitySystem, IRenderableSystem, IUpdatableSystem
    {
        private List<Button> availableButton;
        private int xOffset = 20;
        private SpriteBatch spriteBatch;
        public EnemySelectSystem(SceneManager sceneManager, SpriteBatch spriteBatch) :
            base(sceneManager)
        {
            this.spriteBatch = spriteBatch;
            
            availableButton = new List<Button>();
        }

        public void AddButton(String buttonName, Texture2D buttonTexture)
        {
            availableButton.Add(new Button { buttonName = buttonName, buttonTexture = buttonTexture, available = true });
        }

        public void Update(GameTime gameTime)
        {
            List<Entity> list = ComponentManager.Instance.GetEntities<EnemySelectComponent>(SceneManager.CurrentScene.Entities);
            List<Entity> camera = ComponentManager.Instance.GetEntities<CameraComponent>(SceneManager.CurrentScene.Entities);

            var jackTransform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(camera[0]);
            CameraComponent camComp = ComponentManager.Instance.GetComponentOfType<CameraComponent>(camera[0]);

            Vector2 left = Vector2.Zero;
            Vector2 right = Vector2.Zero;
            
            left.X = jackTransform.Position.X - camComp.Viewport.Width / 2 - xOffset;
            right.X = jackTransform.Position.X + camComp.Viewport.Width / 2 + xOffset;

            foreach (Entity entity in list)
            {
                EnemySelectComponent selectComponent = ComponentManager.Instance.GetComponentOfType<EnemySelectComponent>(entity);
                TransformComponent transform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);

                //Check if the entity is within the bounds of the camera and that it is not tagged. If true, set a button to it
                if (transform.Position.X > left.X && transform.Position.X < right.X && !selectComponent.buttonTagged) 
                {
                    Button b = GetAvailableButton();
                    if (b == null) continue;
                    selectComponent.buttonName = b.buttonName;
                    selectComponent.buttonTexture = b.buttonTexture;
                    selectComponent.buttonTagged = true;
                    b.available = false;

                }
                    //Remove tagged entity if it is outside the bounds
                else if ((transform.Position.X < left.X || transform.Position.X > right.X) && selectComponent.buttonTagged)
                {
                    SetAvailableButton(selectComponent.buttonName);
                    selectComponent.buttonName = "";
                    selectComponent.buttonTagged = false;
                }

            }
            Select(list);
        }

        private void Select(List<Entity> selectComponentEntities)
        {
            string pressed = "";
            if (InputManager.Instance.IsKeyDown("LB"))
            {
                pressed = "LB";
            }
            if (InputManager.Instance.IsKeyDown("LT"))
            {
                pressed = "LT";
            }

            

            if (pressed.Equals("")) return;
            foreach (Entity entity in selectComponentEntities)
            {
                EnemySelectComponent selectComponent = ComponentManager.Instance.GetComponentOfType<EnemySelectComponent>(entity);

                if (selectComponent.buttonTagged && !selectComponent.playerTagged && selectComponent.buttonName.Equals(pressed))
                {
                    RemoveTagged(selectComponentEntities);
                    selectComponent.playerTagged = true;
                    AgentComponent agentComp = ComponentManager.Instance.GetComponentOfType<AgentComponent>(entity);
                    agentComp.Behaviour = new WalkingWorldPlayerState(SceneManager);
                }
            }
        }

        private void RemoveTagged(List<Entity> selectComponentEntities)
        {
            foreach (Entity entity in selectComponentEntities)
            {
                EnemySelectComponent selectComponent = ComponentManager.Instance.GetComponentOfType<EnemySelectComponent>(entity);

                if (selectComponent.playerTagged)
                {
                    selectComponent.playerTagged = false;
                    AgentComponent agentComp = ComponentManager.Instance.GetComponentOfType<AgentComponent>(entity);
                    agentComp.Behaviour = new WalkingState(SceneManager);
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            List<Entity> list = ComponentManager.Instance.GetEntities<EnemySelectComponent>(SceneManager.CurrentScene.Entities);
            foreach (Entity entity in list)
            {
                EnemySelectComponent selectComponent = ComponentManager.Instance.GetComponentOfType<EnemySelectComponent>(entity);
                if (!selectComponent.buttonTagged) continue;

                var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                var pos = transformComponent.Position;//set the position above the object for this button

                pos.Y -= selectComponent.buttonTexture.Height + 5;
                spriteBatch.Draw(selectComponent.buttonTexture,
                                pos,
                                new Rectangle(0, 0, selectComponent.buttonTexture.Width, selectComponent.buttonTexture.Height),
                                Color.White, transformComponent.Rotation, transformComponent.RotationOrigin,
                                transformComponent.Scale, SpriteEffects.None, 0f);
            }

        }
        

        private Button GetAvailableButton()
        {
            try
            {
                return availableButton.Find(x => x.available == true);
            }
            catch (Exception)
            {
            }
            return null;
        }

        private void SetAvailableButton(string buttonName)
        {
            Button b = availableButton.Find(x => x.buttonName.Equals(buttonName));
            b.available = true;
        }

        

        class Button
        {
            public Texture2D buttonTexture { get; set; }
            public string buttonName { get; set; }
            public bool available { get; set; }
        };
    }
}
