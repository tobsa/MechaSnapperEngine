using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.Framework
{
    public interface IAgentBehaviour
    {
        void Update(GameTime gameTime, Entity entity);
    }
}
