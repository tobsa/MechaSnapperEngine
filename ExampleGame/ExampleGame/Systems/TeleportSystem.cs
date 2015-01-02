using ExampleGame.Components;
using GameEngine.Components;
using GameEngine.Framework;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleGame.Systems
{
    class TeleportSystem : EntitySystem, IUpdatableSystem 
    {
        public TeleportSystem(SceneManager manager) : base(manager)
        {

        }

        public void Update(GameTime gameTime) 
        {
            List<Entity> entities = ComponentManager.Instance.GetEntities<TeleportComponent>(SceneManager.CurrentScene.Entities);

            //foreach(Entity entity in entities){
            //    TeleportComponent teleport = ComponentManager.Instance.GetComponentOfType<TeleportComponent>(entity);
            //    TransformComponent transform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
            //    switch (teleport.State) {
            //        case TeleportComponent.OnAir:
            //            break;
            //        default:
            //            break;
            //    }
            //}

            foreach (var entity in entities)
            {

            }
        }

        public void Shoot(Entity shooter)
        {
            List<Entity> entities = ComponentManager.Instance.GetEntities<TeleportComponent>(SceneManager.CurrentScene.Entities);
            var shooterTransform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(shooter);
            foreach (Entity entity in entities) {
                //TeleportComponent teleport = ComponentManager.Instance.GetComponentOfType<TeleportComponent>(entity);
                //TransformComponent transform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                //switch (teleport.State) {
                //    case TeleportComponent.InGun:
                //        transform.Position = shooterTransform.Position;
                //        //teleport.ElevationAngle = 0;
                //        teleport.Velocity = 100;
                //        teleport.State = TeleportComponent.OnAir;
                //        break;
                //    case TeleportComponent.OnFloor:
                //        break;
                //    default:
                //        break;
                //}
            }
        }
    }
}
