using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Components;

namespace ExampleGame
{
    public class TileManager
    {
        public static List<Entity> LoadLevel(Texture2D texture)
        {
            List<Entity> entities = new List<Entity>();

            for (int i = 0; i < 20; i++)
            {
                if (i > 10 && i < 14)
                    continue;

                entities.Add(CreateEntity(i * texture.Width, texture.Height * 12, texture));
            }

            entities.Add(CreateEntity(10 * texture.Width, texture.Height * 8, texture));
            entities.Add(CreateEntity(11 * texture.Width, texture.Height * 8, texture));

            entities.Add(CreateEntity(12 * texture.Width, texture.Height * 9, texture));
            entities.Add(CreateEntity(13 * texture.Width, texture.Height * 10, texture));
            entities.Add(CreateEntity(14 * texture.Width, texture.Height * 10, texture));
            entities.Add(CreateEntity(15 * texture.Width, texture.Height * 11, texture));

            entities.Add(CreateEntity(5 * texture.Width, texture.Height * 10, texture));
            entities.Add(CreateEntity(5 * texture.Width, texture.Height * 11, texture));

            return entities;
        }

        private static Entity CreateEntity(int x, int y, Texture2D texture)
        {
            Entity entity = EntityFactory.CreateEntity(EntityFactory.GenerateID, texture, new Vector2(x, y));

            ComponentManager.Instance.AddComponent<CollisionRectangleComponent>(entity, new CollisionRectangleComponent(new Rectangle(x, y, texture.Width, texture.Height)));

            return entity;
        }
    }
}
