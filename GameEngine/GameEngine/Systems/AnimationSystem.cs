using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Framework;
using GameEngine.Components;

namespace GameEngine.Systems
{
    public class AnimationSystem : EntitySystem, IRenderableSystem
    {
        private SpriteBatch spriteBatch;

        public AnimationSystem(SceneManager sceneManager, SpriteBatch spriteBatch) :
            base(sceneManager)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Draw(GameTime gameTime)
        {
            var layers = SceneManager.CurrentScene.Layers;

            for (int i = 0; i < layers.Count; i++)
            {
                var entities = ComponentManager.Instance.GetEntities<AnimationComponent>(layers[i].Entities);
                for (int j = 0; j < entities.Count; j++)
                {
                    var animComponent = ComponentManager.Instance.GetComponentOfType<AnimationComponent>(entities[j]);
                    var transformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entities[j]);

                    int currentFrame = animComponent.Animation.GetCurrentFrame(gameTime);

                    // Only works with sprite sheets with 1 row right now
                    spriteBatch.Draw(animComponent.SpriteSheet, 
                                     transformComponent.Position, 
                                     new Rectangle(currentFrame * animComponent.FrameWidth, 0 * animComponent.FrameHeight, animComponent.FrameWidth, animComponent.FrameHeight),
                                     Color.White);
                }
            }
        }
    }
}
