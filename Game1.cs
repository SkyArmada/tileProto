using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace tileProto
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TileMap testMap;
        Player player;
        Vector2 resolution;
        List<Sprite> gameObjectList;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            gameObjectList = new List<Sprite>();
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth += 32;
            graphics.PreferredBackBufferHeight += 32;

            graphics.ApplyChanges();
            resolution = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player.LoadContent("Player", this);
            player._Position = new Vector2(100, 100);
            testMap = new TileMap("Content/testMap.tmx", Content);

            //load 50 bullets

            for (int i = 0; i < 50; i++)
            {
                Silverfish silverfish = new Silverfish(player);
                silverfish.LoadContent("Silverfish", this);
                silverfish._Draw = false;
                silverfish._CurrentState = Sprite.SpriteState.kStateInActive;
                gameObjectList.Add(silverfish);
            }


            createObject(Sprite.SpriteType.kSilverfishType, new Vector2(300, 300));
            createObject(Sprite.SpriteType.kSilverfishType, new Vector2(150, 300));
            createObject(Sprite.SpriteType.kSilverfishType, new Vector2(300, 150));
            createObject(Sprite.SpriteType.kSilverfishType, new Vector2(300, 400));
            createObject(Sprite.SpriteType.kSilverfishType, new Vector2(100, 300));
            createObject(Sprite.SpriteType.kSilverfishType, new Vector2(380, 100));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime, gameObjectList);
            foreach(Sprite sprite in gameObjectList)
            {
                if(sprite._CurrentState == Sprite.SpriteState.kStateActive)
                {
                    sprite.Update(gameTime, gameObjectList);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            testMap.Draw(spriteBatch);
            player.Draw(spriteBatch);
            foreach(Sprite sprite in gameObjectList)
            {
                sprite.Draw(spriteBatch);
            }
            base.Draw(gameTime);
            spriteBatch.End();
        }

        public void createObject(Sprite.SpriteType type)
        {
            foreach(Sprite sprite in gameObjectList)
            {
                if(sprite._Tag == type && sprite._CurrentState == Sprite.SpriteState.kStateInActive)
                {
                    sprite.Activate();
                    return;
                }
            }
            return;
        }

        public void createObject(Sprite.SpriteType type, Vector2 pos)
        {
            foreach (Sprite sprite in gameObjectList)
            {
                if (sprite._Tag == type && sprite._CurrentState == Sprite.SpriteState.kStateInActive)
                {
                    sprite.Activate();
                    sprite._Position = pos;
                    return;
                }
            }
            return;
        }
    }
}
