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

        public TimeSystem(SceneManager manager) : base(manager) { }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime) {

            List<Entity> entities = ComponentManager.Instance.GetEntities<CountdownTimeComponent>(SceneManager.CurrentScene.Entities);

            if (entities != null) {
                foreach (Entity entity in entities) {
                    CountdownTimeComponent time = ComponentManager.Instance.GetComponentOfType<CountdownTimeComponent>(entity);
                    StringRenderComponent stringRender = ComponentManager.Instance.GetComponentOfType<StringRenderComponent>(entity);

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
                }
            }
        }
    }
}
