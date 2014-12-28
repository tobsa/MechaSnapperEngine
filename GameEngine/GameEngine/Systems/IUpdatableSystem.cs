using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems
{
    public interface IUpdatableSystem
    {
        void Update(GameTime gameTime);
    }
}
