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
        public string LevelName { get; set; }

        protected static MechaSnapperEngine Engine;
        protected static CameraComponent CameraComponent;
        protected static EnemySelectSystem EnemySelectSystem;
        protected static PlayingState PlayingState;

        protected static List<Entity> Barraroks = new List<Entity>();
        protected static List<Entity> Portals = new List<Entity>();
        protected static Entity Jack;
        protected static Entity JackHealth;
        protected static Entity HorseShoe;
        protected static Entity PortalGun;
        protected static Entity PortalBullet;
        protected static Entity Time;

        private static bool StuffLoaded = false;
        public Level(MechaSnapperEngine engine, PlayingState playingState, CameraComponent cameraComponent)
        {
            if (Engine == null)
                Engine = engine;
            if(CameraComponent == null)
                CameraComponent = cameraComponent;
            if(PlayingState == null)
                PlayingState = playingState;
        }

        public abstract void Initialize();

        public abstract void RestartLevel();

        /*
         * Loades non static entities who are used in all levels.
         * 
         */
        protected void LoadStuff()
        {
            if (!StuffLoaded)
            {
                Jack = CreateJack(new Vector2(2 * 64, 4 * 80));
                JackHealth = CreateJackHealth();
 
                Time = CreateTime(new Vector2(1200, 0), 0);
                HorseShoe = CreateHorseShoe(new Vector2(0, 0));

                PortalGun = EntityFactory.CreateEntity(EntityFactory.GenerateID, Engine.Content.Load<Texture2D>("PortalGun"), new Vector2(2 * 64, 4 * 64));
                PortalBullet = EntityFactory.CreateEntity(EntityFactory.GenerateID, Engine.Content.Load<Texture2D>("Projectile"), new Vector2(2 * 64, 4 * 64));
                PortalBullet.Visible = false;

                ComponentManager.Instance.AddComponent(Jack, new InputComponent(new JackInput(Jack, PortalGun, PortalBullet)));
                ComponentManager.Instance.AddComponent(PortalGun, new ParentComponent(Jack, 55, 70));

                ComponentManager.Instance.AddComponent(PortalBullet, new TeleportComponent());
                ComponentManager.Instance.AddComponent(PortalBullet, new CollisionRectangleComponent());
                ComponentManager.Instance.AddComponent(PortalBullet, new VelocityComponent());
                StuffLoaded = true;
            }
        }

        protected abstract int[,] LoadRocks();

        protected abstract int[,] LoadRocksBG();

        protected Entity CreateBackground(Vector2 position)
        {
            Entity background = EntityFactory.CreateEntity(EntityFactory.GenerateID, Engine.Content.Load<Texture2D>("Sky"), position);

            ComponentManager.Instance.AddComponent(background, new IsFixedComponent(CameraComponent));

            return background;
        }

        private Entity CreateTime(Vector2 position, int countDownSeconds)
        {
            Entity time = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);

            ComponentManager.Instance.AddComponent(time, new StringRenderComponent());
            ComponentManager.Instance.AddComponent(time, new CountdownTimeComponent(countDownSeconds));
            ComponentManager.Instance.AddComponent(time, new IsFixedComponent(CameraComponent));

            return time;
        }

        private Entity CreateJack(Vector2 position)
        {
            Entity jack = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);

            ComponentManager.Instance.AddComponent(jack, new RenderComponent(Engine.Content.Load<Texture2D>("UnluckyJackAnim2"), 128, 128, 0));
            ComponentManager.Instance.AddComponent(jack, new AnimationComponent(new JackIdleAnimation()));
            ComponentManager.Instance.AddComponent(jack, new RigidBodyComponent(32f, 0.3f, 0f));
            ComponentManager.Instance.AddComponent(jack, new CollisionRectangleComponent(new Rectangle(2 * 64 + 32, 1 * 64, 64, 128)));
            ComponentManager.Instance.AddComponent(jack, new VelocityComponent());
            ComponentManager.Instance.AddComponent(jack, CameraComponent);

            return jack;
        }

        private Entity CreateJackHealth()
        {
            Entity jackHealth = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, Vector2.Zero);

            ComponentManager.Instance.AddComponent(jackHealth, new HealthComponent() { IsJack = true, IsAlive = true, CurrentHP = 3, MaxHP = 3, HitCoolDown = 3000 });
            ComponentManager.Instance.AddComponent(jackHealth, new RenderComponent(Engine.Content.Load<Texture2D>("hearts"), 144, 48, 0));
            ComponentManager.Instance.AddComponent(jackHealth, new IsFixedComponent(CameraComponent));

            return jackHealth;
        }

        private Entity CreateBarrarok(Vector2 position)
        {
            Entity barrarok = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);
            Texture2D bar = Engine.Content.Load<Texture2D>("Barrarok");
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
        private Entity[] CreatePortal(Vector2 position, Vector2 position2)
        {
            Entity[] portals = new Entity[2];
            portals[0] = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);
            portals[1] = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position2);
            Texture2D portalTexture = Engine.Content.Load<Texture2D>("Portal");
            ComponentManager.Instance.AddComponent(portals[0], new RenderComponent(portalTexture, portalTexture.Width, portalTexture.Height, 0));
            ComponentManager.Instance.AddComponent(portals[0], new CollisionRectangleComponent(new Rectangle((int)(position.X), (int)position.Y, portalTexture.Width, portalTexture.Height)));
            ComponentManager.Instance.AddComponent(portals[0], new PortalComponent(portals[1], 3000));

            ComponentManager.Instance.AddComponent(portals[1], new RenderComponent(portalTexture, portalTexture.Width, portalTexture.Height, 0));
            ComponentManager.Instance.AddComponent(portals[1], new CollisionRectangleComponent(new Rectangle((int)(position2.X), (int)position2.Y, portalTexture.Width, portalTexture.Height)));
            ComponentManager.Instance.AddComponent(portals[1], new PortalComponent(portals[0], 3000));

            return portals;
        }

        private Entity CreateHorseShoe(Vector2 position)
        {
            Entity horseShoe = EntityFactory.CreateEmptyEntity(EntityFactory.GenerateID, position);
            Texture2D shoe = Engine.Content.Load<Texture2D>("horseshoe");
            ComponentManager.Instance.AddComponent(horseShoe, new RenderComponent(shoe, shoe.Width, shoe.Height, 0));
            ComponentManager.Instance.AddComponent(horseShoe, new CollisionRectangleComponent(new Rectangle((int)position.X, (int)position.Y, shoe.Width, shoe.Width)));

            return horseShoe;
        }

        /*
         * numPortals : Number of portals to create. Should be dividable with 2, aka one portal has one buddy
         * positions : positions of the portals in order. portal1 position [0], portal2 position [1]. portal1 and portal2 are buddys
         * 
         * This method avoids creating new portals, instead it reuses portals
         */
        protected void CreatePortals(int numPortals, List<Vector2> positions)
        {
            if (Portals.Count >= numPortals)
            {
                for (int i = 0; i < numPortals; i++)
                {
                    var portal = ComponentManager.Instance.GetComponentOfType<TransformComponent>(Portals[i]);
                    portal.Position = positions[i];
                    var portalCollision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(Portals[i]);
                    portalCollision.Rectangle = PhysicsManager.Instance.UpdateCollisionBox(portalCollision.Rectangle, portal.Position);
                }
            }
            else
            {
                while(Portals.Count < numPortals)
                {
                    Entity[] portals = CreatePortal(Vector2.Zero, Vector2.Zero);
                    Portals.Add(portals[0]);
                    Portals.Add(portals[1]);
                }
                CreatePortals(numPortals, positions);
            }
        }

        /*
         * numBarraroks : Number of barraroks to create
         * positions : The barraroks positions
         * 
         * This method avoids creating new barraroks, instead it reuses barraroks
         */
        protected void CreateBarraroks(int numBarraroks, List<Vector2> positions)
        {
            if (Barraroks.Count >= numBarraroks)
            {
                for (int i = 0; i < numBarraroks; i++)
                {
                    var barrarok = ComponentManager.Instance.GetComponentOfType<TransformComponent>(Barraroks[i]);
                    barrarok.Position = positions[i];
                }
            }
            else
            {
                while (Barraroks.Count < numBarraroks)
                    Barraroks.Add(CreateBarrarok(Vector2.Zero));
                CreateBarraroks(numBarraroks, positions);
            }

        }

        protected void UpdateHorseShoe(Vector2 position)
        {
            var transform = ComponentManager.Instance.GetComponentOfType<TransformComponent>(HorseShoe);
            transform.Position = position;
            var collision = ComponentManager.Instance.GetComponentOfType<CollisionRectangleComponent>(HorseShoe);
            collision.Rectangle = PhysicsManager.Instance.UpdateCollisionBox(collision.Rectangle, transform.Position);
        }

        protected void ResetHealth()
        {
            var jackHealth = ComponentManager.Instance.GetComponentOfType<HealthComponent>(JackHealth);
            jackHealth.CurrentHP = 3;
            jackHealth.IsAlive = true;
            jackHealth.HasHorseShoe = false;
        }

        
    }
}
