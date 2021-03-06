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
        //public ParentSystem(SceneManager sceneManager) :
        //    base(sceneManager)
        //{
        //}

        public void Update(GameTime gameTime)
        {
            //var entities = ComponentManager.Instance.GetEntities<ParentComponent>(SceneManager.CurrentScene.Entities);
            var entities = ComponentManager.Instance.GetEntities<ParentComponent>(SceneManager.Instance.CurrentScene.Entities);

            if (entities != null) {
                foreach (var entity in entities) {
                    var parentComponent = ComponentManager.Instance.GetComponentOfType<ParentComponent>(entity);
                    var parentTransformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(parentComponent.Parent);
                    var parentRenderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(parentComponent.Parent);

                    var childTransformComponent = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                    var childRenderComponent = ComponentManager.Instance.GetComponentOfType<RenderComponent>(entity);

                    childTransformComponent.Position = parentTransformComponent.Position + new Vector2(parentComponent.XoffSet, parentComponent.YoffSet);
                    childRenderComponent.Effect = parentRenderComponent.Effect;
                }
            }
        }
    }
}