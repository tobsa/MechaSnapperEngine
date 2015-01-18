using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Framework;
using GameEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ExampleGame.Animations;
using ExampleGame.Components;
using ExampleGame.Systems;
using GameEngine.Systems;
using ExampleGame.Enemies;

namespace ExampleGame.Levels
{
    public abstract class Level
    {
        protected MechaSnapperEngine engine;
        protected CameraComponent cameraComponent;
        protected EnemySelectSystem enemySelectSystem;
        protected PlayingState playingState;

        public Level(MechaSnapperEngine engine, PlayingState playingState, CameraComponent cameraComponent)
        {
            this.engine = engine;
            this.cameraComponent = cameraComponent;
            this.playingState = playingState;
        }

        public abstract void RestartLevel();

        public virtual void Initialize()
        {
            LoadEnemySelectSystem();
        }

        protected abstract int[,] LoadRocks();

        protected abstract int[,] LoadRocksBG();

        protected Entity CreateBackground(Vector2 position)
        {
            Entity background = EntityFactory.CreateEntity(EntityFactory.GenerateID, engine.Content.Load<Texture2D>("Sky"), position);

            ComponentManager.Instance.AddComponent(background, new IsFixedComponent(cameraComponent));

            return background;
        }

        protected Entity CreateTime(Vector2 position, int countDownSeconds)
        {
            Entity time = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);

            ComponentManager.Instance.AddComponent(time, new StringRenderComponent());
            ComponentManager.Instance.AddComponent(time, new CountdownTimeComponent(countDownSeconds));
            ComponentManager.Instance.AddComponent(time, new IsFixedComponent(cameraComponent));

            return time;
        }

        protected Entity CreateJack(Vector2 position)
        {
            Entity jack = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);

            ComponentManager.Instance.AddComponent(jack, new RenderComponent(engine.Content.Load<Texture2D>("UnluckyJackAnim2"), 128, 128, 0));
            ComponentManager.Instance.AddComponent(jack, new AnimationComponent(new JackIdleAnimation()));
            ComponentManager.Instance.AddComponent(jack, new RigidBodyComponent(32f, 0.3f, 0f));
            ComponentManager.Instance.AddComponent(jack, new CollisionRectangleComponent(new Rectangle(2 * 64 + 32, 1 * 64, 64, 128)));
            ComponentManager.Instance.AddComponent(jack, new VelocityComponent());
            ComponentManager.Instance.AddComponent(jack, cameraComponent);

            return jack;
        }

        protected Entity CreateJackHealth()
        {
            Entity jackHealth = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, Vector2.Zero);

            ComponentManager.Instance.AddComponent(jackHealth, new HealthComponent() { IsJack = true, IsAlive = true, CurrentHP = 3, MaxHP = 3, HitCoolDown = 3000 });
            ComponentManager.Instance.AddComponent(jackHealth, new RenderComponent(engine.Content.Load<Texture2D>("hearts"), 144, 48, 0));
            ComponentManager.Instance.AddComponent(jackHealth, new IsFixedComponent(cameraComponent));

            return jackHealth;
        }

        protected Entity CreateBarrarok(Vector2 position)
        {
            Entity barrarok = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);
            Texture2D bar = engine.Content.Load<Texture2D>("Barrarok");
            ComponentManager.Instance.AddComponent(barrarok, new AnimationComponent(new BarrarokWalkingAnimation()));
            ComponentManager.Instance.AddComponent(barrarok, new RenderComponent(bar, 64, bar.Height, 0));
            ComponentManager.Instance.AddComponent(barrarok, new RigidBodyComponent(32f, 0.3f, 0f));
            ComponentManager.Instance.AddComponent(barrarok, new CollisionRectangleComponent(new Rectangle(0, 0, 32, bar.Height)));
            ComponentManager.Instance.AddComponent(barrarok, new VelocityComponent());
            ComponentManager.Instance.AddComponent(barrarok, new AgentComponent() { Behaviour = new WalkingState() });
            ComponentManager.Instance.AddComponent(barrarok, new EnemySelectComponent());

            return barrarok;
        }

        /*
         * Creates 2 portals at 2 positions.
         * The portals are created with smaller collision rectangles and moved into the center of the portal
         */
        protected Entity[] CreatePortal(Vector2 position, Vector2 position2)
        {
            Entity[] portals = new Entity[2];
            portals[0] = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);
            portals[1] = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position2);
            Texture2D portalTexture = engine.Content.Load<Texture2D>("Portal");
            ComponentManager.Instance.AddComponent(portals[0], new RenderComponent(portalTexture, portalTexture.Width, portalTexture.Height, 0));
            ComponentManager.Instance.AddComponent(portals[0], new CollisionRectangleComponent(new Rectangle((int)(position.X), (int)position.Y, portalTexture.Width, portalTexture.Height)));
            ComponentManager.Instance.AddComponent(portals[0], new PortalComponent(portals[1], 3000));

            ComponentManager.Instance.AddComponent(portals[1], new RenderComponent(portalTexture, portalTexture.Width, portalTexture.Height, 0));
            ComponentManager.Instance.AddComponent(portals[1], new CollisionRectangleComponent(new Rectangle((int)(position2.X), (int)position2.Y, portalTexture.Width, portalTexture.Height)));
            ComponentManager.Instance.AddComponent(portals[1], new PortalComponent(portals[0], 3000));

            return portals;
        }

        protected Entity CreateHorseShoe(Vector2 position)
        {
            Entity horseShoe = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);
            Texture2D shoe = engine.Content.Load<Texture2D>("horseshoe");
            ComponentManager.Instance.AddComponent(horseShoe, new RenderComponent(shoe, shoe.Width, shoe.Height, 0));
            ComponentManager.Instance.AddComponent(horseShoe, new CollisionRectangleComponent(new Rectangle((int)position.X, (int)position.Y, shoe.Width, shoe.Width)));

            return horseShoe;
        }

        private void LoadEnemySelectSystem()
        {
            enemySelectSystem = new EnemySelectSystem(engine.SpriteBatch);
            enemySelectSystem.AddButton("LB", engine.Content.Load<Texture2D>("bumper_left"));
            enemySelectSystem.AddButton("LT", engine.Content.Load<Texture2D>("trigger_left"));
            enemySelectSystem.AddButton("RB", engine.Content.Load<Texture2D>("bumper_right"));
            enemySelectSystem.AddButton("RT", engine.Content.Load<Texture2D>("trigger_right"));
        }
    }
}
