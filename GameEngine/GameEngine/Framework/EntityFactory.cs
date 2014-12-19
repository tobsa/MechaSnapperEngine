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

            ComponentManager.Instance.AddComponent<RenderComponent>(entity, render);
            ComponentManager.Instance.AddComponent<TransformComponent>(entity, transform);

            return entity;
        }

        public static Entity CreateEmptyEntity(int id, Vector2 position)
        {
            Entity entity = new Entity(id);

            var transform = new TransformComponent(position);

            ComponentManager.Instance.AddComponent<TransformComponent>(entity, transform);

            return entity;
        }
    }
}
