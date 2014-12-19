using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;

namespace GameEngine.Components
{
    public class AgentComponent : IComponent
    {
        public AgentComponent() { }
        public AgentComponent(IAgentBehaviour behaviour)
        {
            Behaviour = behaviour;
        }

        public IAgentBehaviour Behaviour { get; set; }
    }
}
