namespace BDSADominion
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameClass : Game
    {
        #region // Fields\\
        /// <summary>
        /// The Graphicsmanager for the game.
        /// </summary>
        private readonly GraphicsDeviceManager graphics;

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
        /// mouse x coordinat.
        /// </summary>
        private int mouseX;

        /// <summary>
        /// mouse y coordinat.
        /// </summary>
        private int mouseY;

        /// <summary>
        /// The spritefont.
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// The spritebatch used.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// The discardsprite.
        /// </summary>
        private DiscardZone discardZone;

        /// <summary>
        /// The Decksprite.
        /// </summary>
        private DeckZone deckZone;

        /// <summary>
        /// The ActionZone.
        /// </summary>
        private ActionZone actionZone;

        /// <summary>
        /// The handzone.
        /// </summary>
        private HandZone handZone;

        /// <summary>
        /// The playing table.
        /// </summary>
        ////private Texture2D table;

        /// <summary>
        /// The cursor.
        /// </summary>
        private Texture2D cursor;

        /// <summary>
        /// button to open supplytab
        /// </summary>
        private Texture2D supbutton;

        /// <summary>
        /// the mouseover button for supplytab
        /// </summary>
        private Texture2D supover;

        /// <summary>
        /// Input of mouse (keys).
        /// </summary>
        ////private InputState input;

        private MouseState mouseState;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GameClass"/> class.
        /// </summary>
        public GameClass()
        {
            handZone = new HandZone();
            actionZone = new ActionZone();
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
            this.deckZone = new DeckZone(GraphicsDevice);
            this.discardZone = new DiscardZone(GraphicsDevice);

            ////this.InitializeCards();

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

            ////input = new InputState();
            font = Content.Load<SpriteFont>("Arial");
            ////table = Content.Load<Texture2D>("Dominiontable");
            cursor = Content.Load<Texture2D>("Cursor");
            supbutton = Content.Load<Texture2D>("Buttonsupply");
            supover = Content.Load<Texture2D>("Buttonover");
            actions = 0;
            buys = 0;
            coins = 0;
            ////deck.LoadContent(Content, assetName);
            ////discard.LoadContent(Content, assetName);

            foreach (Cardmember cardmem in Enum.GetValues(typeof(Cardmember)))
            {
                string contentLocation = string.Format("Kingdom\\{0}", cardmem);
                Texture2D cardTexture = Content.Load<Texture2D>(contentLocation);
                GUIConstants.cardimage.Add(cardmem, cardTexture);
            }

            handZone.AddCards(new List<CardSprite>() { new CardSprite(Cardmember.MARKET, 1), new CardSprite(Cardmember.MARKET, 2) });
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

            mouseState = Mouse.GetState();

            mouseX = mouseState.X;
            mouseY = mouseState.Y;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            /*if (input.CurrentMouseState.LeftButton == ButtonState.Pressed && input.LastMouseState.LeftButton == ButtonState.Released)
            {
                for (int x = 0; x <= 10; x++)
                {
                    handzone.FindCardByMouseClick((int)input.CurrentMouseState.X, (int)input.CurrentMouseState.Y);


                }

            }*/
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
            spriteBatch.Draw(supbutton, new Vector2(1150, 300), Color.White);
            //this.discard.Draw(this.spriteBatch);
            //this.deck.Draw(this.spriteBatch);


            if ((mouseX < 1350 && mouseX > 1050) && (mouseY > 280 && mouseY < 320))
            {
                spriteBatch.Draw(supover, new Vector2(1150, 300), Color.White);
                if (ButtonState.Pressed == mouseState.LeftButton)
                {
                    spriteBatch.DrawString(font, "You click OK", new Vector2(100.0f, 50.0f), Color.YellowGreen);

                    actionZone.AddCard(handZone.RemoveCard(Cardmember.MARKET, 1));
                }
            }

            handZone.Draw(spriteBatch);
            actionZone.Draw(spriteBatch);

            /*foreach (Card card in handcards)
            {
                card.Draw();
            }

            foreach (Card card in actioncards)
            {
                card.Draw();
            }*/

            spriteBatch.DrawString(font, "Actions: " + actions.ToString(), new Vector2(400, 15), Color.RoyalBlue);
            spriteBatch.DrawString(font, "Buys: " + buys.ToString(), new Vector2(600, 15), Color.RoyalBlue);
            spriteBatch.DrawString(font, "Coins: " + coins.ToString(), new Vector2(800, 15), Color.RoyalBlue);
            spriteBatch.Draw(cursor, new Vector2(mouseX, mouseY), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
