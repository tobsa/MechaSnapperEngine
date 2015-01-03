using GameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleGame.Components {
    class CountdownTimeComponent : IComponent{
        public const int Stopped = 0;
        public const int Running = 1;
        public const int Complete = 2;
        public CountdownTimeComponent(int Seconds) {
            State = Stopped;
            BeginTime = Seconds;
        }
        public int BeginTime { get; set; }
        public int BeginTimeReal { get; set; }
        public int TimeSeconds { get; set; }
        public int State { get; set; }
    }
}
