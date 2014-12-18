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
    public class AnimationSystem : EntitySystem, IUpdateableSystem
    {
        private SpriteBatch spriteBatch;

        public AnimationSystem(SceneManager sceneManager, SpriteBatch spriteBatch) :
            base(sceneManager)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Update(GameTime gameTime)
        {
            var entities = ComponentManager.Instance.GetEntities<AnimationComponent>(SceneManager.CurrentScene.Entities);

            foreach (var entity in entities)
            {
                var animationComponent = ComponentManager.Instance.GetComponentOfType<AnimationComponent>(entity);
                animationComponent.Animation.Update(gameTime, entity);
            }

        }
    }
}
