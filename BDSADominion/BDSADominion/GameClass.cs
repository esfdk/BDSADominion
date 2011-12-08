namespace BDSADominion
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameClass : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// The Graphicsmanager for the game.
        /// </summary>
        private readonly GraphicsDeviceManager graphics;

        /// <summary>
        /// The cards on the table.
        /// </summary>
        private readonly List<Card> allCards = new List<Card>();

        /// <summary>
        /// Number of actions left.
        /// </summary>
        private int actions;

        /// <summary>
        /// Number of buys left.
        /// </summary>
        private int buys;

        /// <summary>
        /// Number of extra coins.
        /// </summary>
        private int coins;

        /// <summary>
        /// The spritefont.
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// The spritebatch used.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// The asset name for the Sprite's Texture.
        /// </summary>
        private string assetName;

        /// <summary>
        /// The discardsprite.
        /// </summary>
        private Discard discard;

        /// <summary>
        /// The Decksprite.
        /// </summary>
        private Deck deck;

        /// <summary>
        /// The handzone.
        /// </summary>
        private HandZone handzone;

        /// <summary>
        /// The playing table.
        /// </summary>
        private Texture2D table;

        /// <summary>
        /// Input of mouse (keys).
        /// </summary>
        private InputState input;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameClass"/> class.
        /// </summary>
        public GameClass()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 600;
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
            this.IsMouseVisible = true;
            deck = new Deck(GraphicsDevice);
            discard = new Discard(GraphicsDevice);
            handzone = new HandZone(GraphicsDevice);

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

            input = new InputState();
            font = Content.Load<SpriteFont>("Arial");
            table = Content.Load<Texture2D>("Dominiontable");
            actions = 0;
            buys = 0;
            coins = 0;
            deck.LoadContent(Content, assetName);
            discard.LoadContent(Content, assetName);
            this.handzone.LoadContent(Content, assetName);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            if (input.CurrentMouseState.LeftButton == ButtonState.Pressed && input.LastMouseState.LeftButton == ButtonState.Released)
            {

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.BlanchedAlmond);
            spriteBatch.Begin();
            //spriteBatch.Draw(table, Vector2.Zero, Color.White);

            this.discard.Draw(this.spriteBatch);
            this.deck.Draw(this.spriteBatch);
            this.handzone.Draw(this.spriteBatch);


            foreach (Card card in allCards)
            {
                card.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, "Actions: " + actions.ToString(), new Vector2(400, 15), Color.RoyalBlue);
            spriteBatch.DrawString(font, "Buys: " + buys.ToString(), new Vector2(600, 15), Color.RoyalBlue);
            spriteBatch.DrawString(font, "Coins: " + coins.ToString(), new Vector2(800, 15), Color.RoyalBlue);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void CardClick(int x, int y)
        {

        }
    }
}
