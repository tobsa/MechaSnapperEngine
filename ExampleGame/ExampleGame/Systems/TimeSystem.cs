using ExampleGame.Components;
using GameEngine.Framework;
using GameEngine.Systems;
using GameEngine.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleGame.Systems {
    
    public class TimeSystem : EntitySystem, IUpdatableSystem
    {
        
        public TimeSystem(SceneManager manager) : base(manager){ }
        
        public void Update(Microsoft.Xna.Framework.GameTime gameTime) {
           
            List<Entity> entities = ComponentManager.Instance.GetEntities<CountdownTimeComponent>(SceneManager.CurrentScene.Entities);
            
            foreach(Entity entity in entities){

                CountdownTimeComponent time = ComponentManager.Instance.GetComponentOfType<CountdownTimeComponent>(entity);
                StringRenderComponent stringRender = ComponentManager.Instance.GetComponentOfType<StringRenderComponent>(entity);
                
                if (time.State == CountdownTimeComponent.Stopped) {
                    time.BeginTimeReal = gameTime.TotalGameTime.Seconds;
                    time.State = CountdownTimeComponent.Running;
                }
                else if (time.State == CountdownTimeComponent.Running) {
                    time.TimeSeconds = time.BeginTime - (gameTime.TotalGameTime.Seconds - time.BeginTimeReal);
                    stringRender.Text = time.TimeSeconds.ToString();
                }

            }

        }
    }
}
