using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Components;

namespace GameEngine.Framework
{
    public class AgentComponent : IComponent
    {
        public IAgentBehaviour Behaviour { get; set; }
    }
}
