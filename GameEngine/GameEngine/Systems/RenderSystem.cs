using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Components;

namespace GameEngine.Systems {
    public class RenderSystem : EntitySystem, IRenderableSystem {
        private SpriteBatch spriteBatch;

        public RenderSystem(SceneManager sceneManager, SpriteBatch spriteBatch) :
            base(sceneManager) {
            this.spriteBatch = spriteBatch;
        }

        public void Draw(GameTime gameTime) {
            var layers = SceneManager.CurrentScene.Layers;

            for (int i = 0; i < layers.Count; i++) {
                DrawTexture(layers[i]);
                DrawString(layers[i]);
            }
        }
        private void DrawTexture(Layer layer) {
            var entities = ComponentManager.Instance.GetEntities<RenderComponent>(layer.Entities);
            for (int j = 0; j < entities.Count; j++) {
                if (!entities[j].Visible)
                    continue;

                var renderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entities[j]);
                var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entities[j]);
                spriteBatch.Draw(renderComponent.Texture,
                                transformComponent.Position,
                                new Rectangle(renderComponent.Frame * renderComponent.Width, 0, renderComponent.Width, renderComponent.Height),
                                Color.White, transformComponent.Rotation, transformComponent.RotationOrigin,
                                transformComponent.Scale, renderComponent.Effect, 0f);
            }
        }
        private void DrawString(Layer layer) {
            var entities = ComponentManager.Instance.GetEntities<StringRenderComponent>(layer.Entities);
            if (entities != null) {
                for (int j = 0; j < entities.Count; j++) {
                    if (!entities[j].Visible)
                        continue;

                    var stringRenderComponent = ComponentManager.Instance.GetComponentOfType<StringRenderComponent>(entities[j]);
                    var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entities[j]);
                    spriteBatch.DrawString(FontManager.Instance.GetFont(stringRenderComponent.Font),
                        stringRenderComponent.Text, transformComponent.Position, Color.White, transformComponent.Rotation,
                        transformComponent.RotationOrigin, transformComponent.Scale, stringRenderComponent.Effect, 0f);
                }
            }
        }
    }
}
