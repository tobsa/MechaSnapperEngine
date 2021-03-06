﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using GameEngine.Framework;
using GameEngine.Components;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems{
    public class CameraSystem : EntitySystem, IUpdatableSystem
    {
        //public CameraSystem(SceneManager sceneManager) :
        //    base(sceneManager)
        //{

        //}

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //List<Entity> list = ComponentManager.Instance.GetEntities<CameraComponent>(SceneManager.CurrentScene.Entities);
            List<Entity> list = ComponentManager.Instance.GetEntities<CameraComponent>(SceneManager.Instance.CurrentScene.Entities);

            if (list != null) {
                foreach (Entity entity in list) {
                    TransformComponent transform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                    CameraComponent camera = ComponentManager.Instance.GetComponentOfType<CameraComponent>(entity);

                    // MathHelper.Clamp(camera.Zoom, 0.01f, 10.0f);
                    camera.Transform = Matrix.CreateScale(new Vector3(camera.Zoom, camera.Zoom, 1)) * Matrix.CreateTranslation(-transform.Position.X + camera.XOffset, camera.YOffset, 0);
                }
            }
        }
    }
}
