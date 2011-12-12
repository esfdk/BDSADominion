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
        internal int actions;

        /// <summary>
        /// Number of buys left.
        /// </summary>
        internal int buys;

        /// <summary>
        /// Number of extra coins.
        /// </summary>
        internal int coins;

        /// <summary>
        /// Indicate player turn.
        /// </summary>
        private bool turn;

        /// <summary>
        /// Indicate phase.
        /// </summary>
        private bool phase;

        /// <summary>
        /// indicate the players number.
        /// </summary>
        private int playernum;

        /// <summary>
        /// Texture for the end phase button.
        /// </summary>
        private Texture2D endphasebutton;

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
        /// Indicates if games is over.
        /// </summary>
        private bool endofgame;

        /// <summary>
        /// indicate the winer
        /// </summary>
        private int winernum;

        /// <summary>
        /// The spritefont for WIN.
        /// </summary>
        private SpriteFont fontWin;

        /// <summary>
        /// The spritebatch used.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// The discardsprite.
        /// </summary>
        internal DiscardZone discardZone;

        /// <summary>
        /// The Decksprite.
        /// </summary>
        internal DeckZone deckZone;

        /// <summary>
        /// The ActionZone.
        /// </summary>
        internal ActionZone actionZone;

        /// <summary>
        /// The handzone.
        /// </summary>
        internal HandZone handZone;

        /// <summary>
        /// The supplyzone.
        /// </summary>
        internal SupplyZone supplyZone;

        /// <summary>
        /// The playing table.
        /// </summary>
        ////private Texture2D table;

        /// <summary>
        /// The cursor.
        /// </summary>
        private Texture2D cursor;

        private MouseState currentMouseState;

        private MouseState lastMouseState;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GameClass"/> class.
        /// </summary>
        public GameClass()
        {
            handZone = new HandZone();
            actionZone = new ActionZone();
            discardZone = new DiscardZone();
            deckZone = new DeckZone();
            supplyZone = new SupplyZone();
            graphics = new GraphicsDeviceManager(this) { PreferredBackBufferWidth = 1280, PreferredBackBufferHeight = 600 };
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

            font = Content.Load<SpriteFont>("Fonts\\Arial");
            fontWin = Content.Load<SpriteFont>("Fonts\\Winfont");
            ////table = Content.Load<Texture2D>("Dominiontable");
            cursor = Content.Load<Texture2D>("Pics\\Cursor");
            endphasebutton = Content.Load<Texture2D>("Pics\\EndButton");
            actions = 0;
            buys = 0;
            coins = 0;
            playernum = 0;
            turn = false;
            phase = false;


            foreach (Cardmember cardmem in Enum.GetValues(typeof(Cardmember)))
            {
                string contentLocation = string.Format("Kingdom\\{0}", cardmem);
                Texture2D cardTexture = Content.Load<Texture2D>(contentLocation);
                GUIConstants.cardImages.Add(cardmem, cardTexture);
            }

            foreach (Cardmember cardmem in Enum.GetValues(typeof(Cardmember)))
            {
                if (cardmem != Cardmember.EMPTY & cardmem != Cardmember.BACKSIDE)
                {
                    string contentLocation = string.Format("Supply\\{0}", cardmem);
                    Texture2D cardTexture = Content.Load<Texture2D>(contentLocation);
                    GUIConstants.buttonImages.Add(cardmem, cardTexture);
                }
            }

            discardZone.AddCard(new CardSprite(Cardmember.EMPTY, 1));
            deckZone.AddCard(new CardSprite(Cardmember.BACKSIDE, 1));

            handZone.AddCards(
                new List<CardSprite>() { new CardSprite(Cardmember.MARKET, 1), new CardSprite(Cardmember.GARDENS, 1) });
            supplyZone.AddCards(
                new List<CardSprite>() { new CardSprite(Cardmember.MARKET, 1), new CardSprite(Cardmember.GARDENS, 1) });
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
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            mouseX = this.currentMouseState.X;
            mouseY = this.currentMouseState.Y;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
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
            {
                if (ButtonState.Pressed == currentMouseState.LeftButton && lastMouseState.LeftButton == ButtonState.Released)
                {
                    CardSprite cardx = handZone.FindCardByMouseClick(mouseX, mouseY);
                    if (cardx != null)
                    {
                        actionZone.AddCard(cardx);
                    }
                    if (cardx != null)
                    {
                        handZone.RemoveCard(cardx.CardMember, cardx.Id);
                    }


                }

                if (ButtonState.Pressed == currentMouseState.LeftButton && lastMouseState.LeftButton == ButtonState.Released)
                {
                    CardSprite cardx = actionZone.FindCardByMouseClick(mouseX, mouseY);
                    if (cardx != null)
                    {
                        discardZone.AddCard(cardx);
                    }
                    if (cardx != null)
                    {
                        actionZone.RemoveCard(cardx.CardMember, cardx.Id);
                    }

                }

                if (ButtonState.Pressed == currentMouseState.LeftButton && lastMouseState.LeftButton == ButtonState.Released)
                {
                    CardSprite cardx = supplyZone.FindCardByMouseClick(mouseX, mouseY, spriteBatch, font);
                    if (cardx != null)
                    {
                        discardZone.AddCard(cardx);
                    }
                }

                handZone.Draw(spriteBatch);
                actionZone.Draw(spriteBatch);
                discardZone.Draw(spriteBatch);
                deckZone.Draw(spriteBatch);
                supplyZone.Draw(spriteBatch);
                spriteBatch.Draw(cursor, new Vector2(mouseX, mouseY), Color.White);
                spriteBatch.Draw(endphasebutton, new Vector2(300, 300), Color.White);
                spriteBatch.DrawString(font, "Actions: " + actions.ToString(), new Vector2(400, 15), Color.RoyalBlue);
                spriteBatch.DrawString(font, "Buys: " + buys.ToString(), new Vector2(600, 15), Color.RoyalBlue);
                spriteBatch.DrawString(font, "Coins: " + coins.ToString(), new Vector2(800, 15), Color.RoyalBlue);
                spriteBatch.DrawString(font, "Player" + playernum.ToString(), new Vector2(500, 500), Color.RoyalBlue);
                spriteBatch.DrawString(font, turn == true ? "Your turn   -" : "Not your turn", new Vector2(10, 10), Color.RoyalBlue);

                if (turn == true && phase == false)
                {
                    spriteBatch.DrawString(font, "Action phase", new Vector2(163, 10), Color.RoyalBlue);
                }
                if (turn == true && phase == true)
                {
                    spriteBatch.DrawString(font, "Buy phase", new Vector2(163, 10), Color.RoyalBlue);
                }

                if (endofgame == true && playernum == winernum)
                {
                    spriteBatch.DrawString(fontWin, "YOU ARE THE WINER CONGRATULATIONS", new Vector2(450, 330), Color.Indigo);
                }
                if (endofgame == true && playernum != winernum)
                {
                    spriteBatch.DrawString(fontWin, "YOU LOOSE, NOW TAKE YOUR CRUSED SOUL AND GO AWAY", new Vector2(450, 330), Color.Indigo);
                }

                spriteBatch.End();
                base.Draw(gameTime);
            }
        }
    }
}