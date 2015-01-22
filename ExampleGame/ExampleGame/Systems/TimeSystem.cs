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

    public class TimeSystem : EntitySystem, IUpdatableSystem {

        private CountdownTimeComponent time;
        private StringRenderComponent stringRender;
        public TimeSystem() { }
        //public TimeSystem(SceneManager manager) : base(manager) { }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime) 
        {
            if (time == null || stringRender == null)
            {
                List<Entity> entities = ComponentManager.Instance.GetEntities<CountdownTimeComponent>(SceneManager.Instance.CurrentScene.Entities);
                time = ComponentManager.Instance.GetComponentOfType<CountdownTimeComponent>(entities[0]);
                stringRender = ComponentManager.Instance.GetComponentOfType<StringRenderComponent>(entities[0]);
            }

            //if (entities != null) {
            //    foreach (Entity entity in entities) {
            //        CountdownTimeComponent time = ComponentManager.Instance.GetComponentOfType<CountdownTimeComponent>(entity);
            //        StringRenderComponent stringRender = ComponentManager.Instance.GetComponentOfType<StringRenderComponent>(entity);

                    if (time.State == CountdownTimeComponent.Stopped) {
                        time.BeginTimeReal = gameTime.TotalGameTime.Seconds;
                        time.State = CountdownTimeComponent.Running;
                    }
                    else if (time.State == CountdownTimeComponent.Running) {
                        time.TimeSeconds = time.BeginTime - ((int)gameTime.TotalGameTime.TotalSeconds - time.BeginTimeReal);
                        //if (time.TimeSeconds < 0)
                        //    ;//Döda/starta om spelet
                        stringRender.Text = time.TimeSeconds.ToString();
                    }
            //    }
            //}
        }
    }
}
