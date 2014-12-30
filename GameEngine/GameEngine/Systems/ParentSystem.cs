﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework;
using GameEngine.Components;

namespace GameEngine.Systems
{
    public class ParentSystem : EntitySystem, IUpdatableSystem
    {
        public ParentSystem(SceneManager sceneManager) :
            base(sceneManager)
        {
        }

        public void Update(GameTime gameTime)
        {
            var entities = ComponentManager.Instance.GetEntities<ParentComponent>(SceneManager.CurrentScene.Entities);

            foreach (var entity in entities)
            {
                var parentComponent = ComponentManager.Instance.GetComponentOfType<ParentComponent>(entity);
                var parentTransformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(parentComponent.Parent);
                var childTransformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);

                childTransformComponent.Position = parentTransformComponent.Position + new Vector2(parentComponent.XoffSet, parentComponent.YoffSet);
            }
        }
    }
}