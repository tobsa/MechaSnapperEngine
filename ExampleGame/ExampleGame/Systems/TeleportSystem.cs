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
    class TeleportSystem : EntitySystem, IUpdatableSystem {
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
                    case TeleportComponent.JustFired:
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
            transform.Position = new Vector2(teleportTransform.Position.X, teleportTransform.Position.Y);
            teleport.State = TeleportComponent.InChamber;
        }
        private void InChamber() {

        }
        private void CalculatePosition(TeleportComponent teleport, TransformComponent transform) {
            float positionX, positionY, deltaTime;

            positionX = transform.Position.X * teleport.ElevationDegreePercent;
        }
    }
}
