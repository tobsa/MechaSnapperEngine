using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Components;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Framework;

namespace ExampleGame.Components
{
    public class PortalComponent : IComponent
    {
        public Entity FirstPortal { get; set; }
        public Entity SecondPortal { get; set; }
        public float CoolDownTime { get; set; }
        public float CurrentTime { get; set; }

        public PortalComponent(Entity firstPortal, Entity secondPortal, float portalCoolDownTime)
        {
            FirstPortal = firstPortal;
            SecondPortal = secondPortal;
            CoolDownTime = portalCoolDownTime;

        }
    }
}
