using ExampleGame.Components;
using GameEngine.Components;
using GameEngine.Framework;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleGame.Systems {
    class TeleportSystem : EntitySystem, IUpdateableSystem {
        public TeleportSystem(SceneManager manager)
            : base(manager) {

        }
        public void Update(Microsoft.Xna.Framework.GameTime gameTime) {
            List<Entity> entities = ComponentManager.Instance.GetEntities<TeleportComponent>(SceneManager.CurrentScene.Entities);
            foreach (Entity entity in entities) {
                TransformComponent transform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                TeleportComponent teleport = ComponentManager.Instance.GetComponentOfType<TeleportComponent>(entity);
                switch (teleport.State) {
                    case TeleportComponent.InChamber:
                        break;
                    case TeleportComponent.OnAir:
                        CalculatePosition(teleport, transform);
                        break;
                    case TeleportComponent.OnGround:
                        break;
                    default:
                        break;
                }
            }
        }
        public void Teleport(TeleportComponent teleport, TransformComponent teleportTransform, TransformComponent transform) {
            teleport.State = TeleportComponent.InChamber;
        }
        private void InChamber() {

        }
        private void CalculatePosition(TeleportComponent teleport, TransformComponent transform) {
            float positionX, positionY, deltaTime;
            teleport.VelocityX = teleport.VelocityX * (float)Math.Cos(teleport.ElevationDegree);
            teleport.VelocityY = teleport.VelocityY * (float)Math.Sin(teleport.ElevationDegree);
            positionX = transform.Position.X + teleport.VelocityX * deltaTime;
            positionX = transform.Position.X + teleport.VelocityX * deltaTime;
            transform.Position = new Vector2();
        }
    }
}
