using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Components;

namespace GameEngine.Framework
{


    // useless comment
    public class EntityFactory
    {
        public static Entity CreateEntity(int id, Texture2D texture, Vector2 position)
        {
            Entity entity = new Entity(id);

            var render = new RenderComponent();
            var transform = new TransformComponent();

            render.Texture = texture;
            transform.Position = position;

            ComponentManager.Instance.AddComponent<RenderComponent>(entity, render);
            ComponentManager.Instance.AddComponent<TransformComponent>(entity, transform);

            return entity;
        }

        public static Entity CreateEmptyEntity(int id, Vector2 position)
        {
            Entity entity = new Entity(id);


            var transform = new TransformComponent();

            transform.Position = position;

            ComponentManager.Instance.AddComponent<TransformComponent>(entity, transform);

            return entity;
        }
    }
}
