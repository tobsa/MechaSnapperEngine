using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Framework
{
    public class Entity
    {
        public int ID { get; set; }

        public Entity() { }

        public Entity(int ID)
        {
            this.ID = ID;
        }
    }
}
