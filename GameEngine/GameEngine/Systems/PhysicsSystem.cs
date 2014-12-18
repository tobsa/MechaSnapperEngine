using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameEngine.Framework;
using GameEngine.Components;

namespace GameEngine.Systems
{
    public class PhysicsSystem : EntitySystem, IUpdateableSystem
    {
        public PhysicsSystem(SceneManager sceneManager) :
            base(sceneManager){}

        public void Update(GameTime gameTime)
        {
            var entities = ComponentManager.Instance.GetEntities<RigidBodyComponent>(SceneManager.CurrentScene.Entities);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var entity in entities)
            {
                var vel = ComponentManager.Instance.GetComponentOfType<VelocityComponent>(entity);
                var pos = ComponentManager.Instance.GetComponentOfType<TransformComponent>(entity);
                var body = ComponentManager.Instance.GetComponentOfType<RigidBodyComponent>(entity);

                

                if (pos.Position.Y < 700)
                {
                    
                    vel.Velocity = new Vector2(vel.Velocity.X, vel.Velocity.Y + body.Gravity);
                    
                }
                else
                {
                    pos.Position = new Vector2(pos.Position.X, 700);
                    vel.Velocity = new Vector2(vel.Velocity.X * (1 - body.Friction), 0);
                }
                 

            }
        }
    }
}
