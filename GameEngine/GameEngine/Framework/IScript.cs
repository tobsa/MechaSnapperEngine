using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.Framework
{
    public interface IScript
    {
        void Update(GameTime gameTime, Entity entity);
    }
}
