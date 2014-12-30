using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Components;

namespace GameEngine.Systems
{
    public class RenderSystem : EntitySystem, IRenderableSystem
    {
        private SpriteBatch spriteBatch;

        public RenderSystem(SceneManager sceneManager, SpriteBatch spriteBatch) :
            base(sceneManager)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Draw(GameTime gameTime)
        {
            var layers = SceneManager.CurrentScene.Layers;

            for (int i = 0; i < layers.Count; i++)
            {
                var entities = ComponentManager.Instance.GetEntities<RenderComponent>(layers[i].Entities);
                for (int j = 0; j < entities.Count; j++)
                {
                    if (!entities[j].Visible)
                        continue;

                    var renderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entities[j]);
                    var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entities[j]);

                    spriteBatch.Draw(renderComponent.Texture, 
                                    transformComponent.Position, 
                                    new Rectangle(renderComponent.Frame * renderComponent.Width , 0 , renderComponent.Width, renderComponent.Height),
                                    Color.White, transformComponent.Rotation, transformComponent.RotationOrigin, 
                                    transformComponent.Scale, renderComponent.Effect, 0f);
                }
            }
        }
    }
}
