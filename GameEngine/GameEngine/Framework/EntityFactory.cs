using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Components;

namespace GameEngine.Framework
{
    public abstract class EntityFactory
    {
        private static int id = 0;
        public static int GenerateID { get { return id++; } set { } }

        public static Entity CreateEntity(int id, Texture2D texture, Vector2 position)
        {
            Entity entity = new Entity(id);

            var render = new RenderComponent(texture);
            var transform = new TransformComponent(position);

            ComponentManager.Instance.AddComponent(entity, render);
            ComponentManager.Instance.AddComponent(entity, transform);

            return entity;
        }

        public static Entity CreateEmptyEntity(int id, Vector2 position)
        {
            Entity entity = new Entity(id);

            var transform = new TransformComponent(position);

            ComponentManager.Instance.AddComponent(entity, transform);

            return entity;
        }


        
        public static List<Entity> CreateTileWorld(int[,] world, Texture2D spriteSheet, int tileWidth, int tileHeight)
        {
            List<Entity> entities = new List<Entity>();

            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (world[i, j] == 0)
                    {
                        continue;
                    }
                    else if (world[i, j] < 0)
                    {
                        Entity entity = CreateTileEntityWithCollider(GenerateID, spriteSheet, new Vector2(j * tileWidth, i * tileHeight), world[i, j] + 1, tileWidth, tileHeight);
                        entities.Add(entity);
                    }
                    else
                    {
                        Entity entity = CreateTileEntity(GenerateID, spriteSheet, new Vector2(j * tileWidth, i * tileHeight), world[i, j] - 1, tileWidth, tileHeight);
                        entities.Add(entity);
                    }
                }
            }

            return entities;
        }

        private static Entity CreateTileEntity(int id, Texture2D spriteSheet, Vector2 position, int frame, int tileWidth, int tileHeight)
        {
            Entity entity = new Entity(GenerateID);

            var render = new RenderComponent(spriteSheet, tileWidth, tileHeight, frame);
            var transform = new TransformComponent();

            transform.Position = position;

            ComponentManager.Instance.AddComponent(entity, render);
            ComponentManager.Instance.AddComponent(entity, transform);

            return entity;
        }


        private static Entity CreateTileEntityWithCollider(int id, Texture2D spriteSheet, Vector2 position, int frame, int tileWidth, int tileHeight)
        {
            Entity entity = new Entity(GenerateID);

            var render = new RenderComponent(spriteSheet, tileWidth, tileHeight, Math.Abs(frame));
            var transform = new TransformComponent();
            var collisionRec = new CollisionRectangleComponent(new Rectangle((int)position.X,(int)position.Y, tileWidth, tileHeight));

            transform.Position = position;

            ComponentManager.Instance.AddComponent(entity, render);
            ComponentManager.Instance.AddComponent(entity, transform);
            ComponentManager.Instance.AddComponent(entity, collisionRec);

            return entity;
        }
    }
}
