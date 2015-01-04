using GameEngine.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Components {
    public class IsFixedComponent : IComponent{
        public IsFixedComponent(CameraComponent camera) {
            Camera = camera;
        }
        public CameraComponent Camera{ get; set; }
    }
}
