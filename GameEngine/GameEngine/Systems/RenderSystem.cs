﻿using System;
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

        public RenderSystem(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        List<double> averages = new List<double>();
        List<int> times = new List<int>(100);
        public void Draw(GameTime gameTime)
        {
            var layers = SceneManager.Instance.CurrentScene.Layers;

            if (times.Count >= 100)
            {
                averages.Add(times.Average());
                times = new List<int>(100);
                SoundManager.Instance.PlaySoundEffect("punch2");
            }

            DateTime now = DateTime.Now;
            int milli = now.Millisecond;
            for (int i = 0; i < layers.Count; i++)
            {
                DrawTexture(layers[i]);
                DrawString(layers[i]);
            }
            now = DateTime.Now;
            int timeItTook = now.Millisecond - milli;
            times.Add(timeItTook);
        }

        private void DrawTexture(Layer layer)
        {
            var entities = ComponentManager.Instance.GetEntities<RenderComponent>(layer.Entities);
            Vector2 position = Vector2.Zero;

            for (int j = 0; j < entities.Count; j++)
            {
                if (!entities[j].Visible)
                    continue;

                var renderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entities[j]);
                var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entities[j]);
                var isFixed = ComponentManager.Instance.GetComponentOfType<IsFixedComponent>(entities[j]);
                if (isFixed != null) {
                    position.X = Matrix.Invert(isFixed.Camera.Transform).Translation.X + transformComponent.Position.X;
                    position.Y = Matrix.Invert(isFixed.Camera.Transform).Translation.Y + transformComponent.Position.Y;
                }
                else {
                    position = transformComponent.Position;
                }
                spriteBatch.Draw(renderComponent.Texture,
                                position,
                                new Rectangle(renderComponent.Frame * renderComponent.Width, 0, renderComponent.Width, renderComponent.Height),
                                Color.White,
                                transformComponent.Rotation,
                                transformComponent.Origin,
                                transformComponent.Scale,
                                renderComponent.SpriteEffect,
                                0);
            }
        }

        private void DrawString(Layer layer) 
        {
            var entities = ComponentManager.Instance.GetEntities<StringRenderComponent>(layer.Entities);
            if (entities != null)
            {
                Vector2 position = Vector2.Zero;
                for (int j = 0; j < entities.Count; j++) 
                {
                    if (!entities[j].Visible)
                        continue;
 
                    var renderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entities[j]);
                    var stringRenderComponent = ComponentManager.Instance.GetComponentOfType<StringRenderComponent>(entities[j]);
                    var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entities[j]);
                    var isFixed = ComponentManager.Instance.GetComponentOfType<IsFixedComponent>(entities[j]);
                    if (isFixed != null) {
                        position.X = Matrix.Invert(isFixed.Camera.Transform).Translation.X + transformComponent.Position.X;
                        position.Y = Matrix.Invert(isFixed.Camera.Transform).Translation.Y + transformComponent.Position.Y;
                    }
                    else {
                        position = transformComponent.Position;
                    }
                    spriteBatch.DrawString(FontManager.Instance.GetFont(stringRenderComponent.Font),
                        stringRenderComponent.Text, position, Color.White, transformComponent.Rotation,
                        transformComponent.RotationOrigin, transformComponent.Scale, stringRenderComponent.Effect, 0f);
                }
            }
        }
    }
}
