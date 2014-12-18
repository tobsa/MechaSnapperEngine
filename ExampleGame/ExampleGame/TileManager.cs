using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ExampleGame
{
    public class TileManager
    {
        public static List<Entity> LoadLevel(Texture2D texture)
        {
            List<Entity> entities = new List<Entity>();

            int size = texture.Width;

            for (int i = 0; i < 25; i++)
            {

                if (i > 10 && i < 14)
                    continue;

                var entity = EntityFactory.CreateEntity(i, texture, new Vector2(i * size, size * 12));
                entities.Add(entity);

                
            }

            for (int i = 0; i < 5; i++)
            {
                var entity = EntityFactory.CreateEntity(i, texture, new Vector2(size * 5 + i * size, size * 4));
                entities.Add(entity);
            }

            for (int i = 0; i < 3; i++)
            {
                var entity = EntityFactory.CreateEntity(i, texture, new Vector2(size * 12 + i * size, size * 9));
                entities.Add(entity);
            }

            return entities;
        }
    }
}
