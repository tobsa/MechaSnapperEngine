﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleGame
{/*
  * Different layers of the game in one place. Can be used for when searching for entities or components
  */
    public class Layers
    {
        public static int BACKGROUND = 0;
        public static int BACKGROUND_OBJECTS = 1;
        public static int WALKABLE_OBJECTS = 2;
        public static int BARRAROK = 3;
        public static int JACK = 7;
        public static int JACK_ITEMS = 8;
        public static int PORTAL_BULLET = 4;
        public static int TIME_AND_HEALTH = 5;
        public static int PORTALS = 5;
        public static int HORSE_SHOE = 6;
    }
}
