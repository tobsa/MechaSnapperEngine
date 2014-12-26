using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameEngine.Systems;

namespace GameEngine.Framework
{
    public abstract class GameState
    {
        protected MechaSnapperEngine engine;

        protected List<IUpdatableSystem> updateableSystems = new List<IUpdatableSystem>();
        protected List<IRenderableSystem> renderableSystems = new List<IRenderableSystem>();

        public GameState(MechaSnapperEngine engine)
        {
            this.engine = engine;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

        public bool Enabled
        {
            get; set;
        }

        public bool Visible
        {
            get; set;
        }

        public void RegisterSystem(EntitySystem system)
        {
            if (system is IUpdatableSystem)
                updateableSystems.Add(system as IUpdatableSystem);

            if (system is IRenderableSystem)
                renderableSystems.Add(system as IRenderableSystem);
        }

        public void UnregisterSystem(EntitySystem system)
        {
            if (system is IUpdatableSystem)
                updateableSystems.Remove(system as IUpdatableSystem);

            if (system is IRenderableSystem)
                renderableSystems.Remove(system as IRenderableSystem);
        }

        public virtual void StateChanged(object sender, EventArgs args)
        {
            if (engine.State == this)
            {
                Enabled = true;
                Visible = true;
            }
            else
            {
                Enabled = false;
                Visible = false;
            }
        }
    }

}
