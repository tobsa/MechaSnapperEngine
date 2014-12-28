using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;

namespace GameEngine.Components
{
    public class ParentComponent : IComponent
    {
        public Entity Parent { set; get; }
        public int XoffSet { set; get; }
        public int YoffSet { set; get; }

        public ParentComponent(Entity Parent)
        {
            this.Parent = Parent;
            this.XoffSet = 0;
            this.YoffSet = 0;
        }

        public ParentComponent(Entity Parent, int XoffSet, int YoffSet)
        {
            this.Parent = Parent;
            this.XoffSet = XoffSet;
            this.YoffSet = YoffSet;
        }
    }
}
