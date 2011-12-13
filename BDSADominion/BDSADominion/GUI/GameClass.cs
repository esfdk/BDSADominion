namespace BDSADominion.GUI
{
    using System;
    using System.Collections.Generic;
    using Gamestate;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Zones;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// <author>
    /// Frederik Lysgaard (frly@itu.dk)
    /// </author>
    public class GameClass : Game
    {
        #region // Fields\\

        /// <summary>
        /// Indicate player turn.
        /// </summary>
        internal bool turn;

        /// <summary>
        /// Indicate phase.
        /// </summary>
        internal int phase;

        /// <summary>
        /// Indicates if games is over.
        /// </summary>
        internal bool endOfGame;

        /// <summary>
        /// indicate the winer
        /// </summary>
        internal int winnerNum;

        /// <summary>
        /// The Discardzone.
        /// </summary>
        internal DiscardZone discardZone;

        /// <summary>
        /// The Deckzone.
        /// </summary>
        internal DeckZone deckZone;

        /// <summary>
        /// The ActionZone.
        /// </summary>
        internal ActionZone actionZone;

        /// <summary>
        /// The Handzone.
        /// </summary>
        internal HandZone handZone;

        /// <summary>
        /// The Supplyzone.
        /// </summary>
        internal SupplyZone supplyZone;

        /// <summary>
        /// emptysprite used to show an emptyspot.
        /// </summary>
        public static CardSprite Empty;

        /// <summary>
        /// backsprite used to show backside of card.
        /// </summary>
        public static CardSprite Back;

        /// <summary>
        /// A dictionary containing a cardmember and the corresponding cardtexture.
        /// </summary>
        public static Dictionary<CardName, Texture2D> cardImages = new Dictionary<CardName, Texture2D>();

        /// <summary>
        /// A dictionary containing a cardmember and the corresponding buttontexture.
        /// </summary>
        public static Dictionary<CardName, Texture2D> buttonImages = new Dictionary<CardName, Texture2D>();

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
        /// The Graphicsmanager for the game.
        /// </summary>
        private readonly GraphicsDeviceManager graphics;

        /// <summary>
        /// The playing table.
        /// </summary>
        private Texture2D table;

        /// <summary>
        /// indicate the players number.
        /// </summary>
        internal int playernum;

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
        /// The spritefont for writing standart text outputs.
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// The spritefont for the endgame text "slidly" bigger than normal text.
        /// </summary>
        private SpriteFont fontWin;

        /// <summary>
        /// The spritebatch used.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// The cursor.
        /// </summary>
        private Texture2D cursor;

        /// <summary>
        /// The current state of the mouse.
        /// </summary>
        private MouseState currentMouseState;

        /// <summary>
        /// last state of the mouse.
        /// </summary>
        private MouseState lastMouseState;

        private bool StartUp = false;

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
        /// event for when a card is clicked in the handzone.
        /// </summary>
        internal event CardSpriteHandler HandCardClicked;

        /// <summary>
        /// event for when a button is clicked in the supplyzone.
        /// </summary>
        internal event CardNameHandler SupplyCardClicked;

        /// <summary>
        /// event for when the endphase button is clicked.
        /// </summary>
        internal event ClickHandler EndPhaseClicked;

        internal event ClickHandler StartUpdate;

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
            table = Content.Load<Texture2D>("Pics\\Dominiontable");
            cursor = Content.Load<Texture2D>("Pics\\Cursor");
            endphasebutton = Content.Load<Texture2D>("Pics\\EndButton");
            actions = 0;
            buys = 0;
            coins = 0;
            playernum = 0;
            winnerNum = 0;
            turn = false;
            phase = 0;
            endOfGame = false;

            foreach (CardName card in Enum.GetValues(typeof(CardName)))
            {
                string content = card.ToString().ToUpper();
                string contentLocation = string.Format("Kingdom\\{0}", content);
                Texture2D cardTexture = Content.Load<Texture2D>(contentLocation);
                cardImages.Add(card, cardTexture);
            }

            foreach (CardName card in Enum.GetValues(typeof(CardName)))
            {
                if (card != CardName.Empty & card != CardName.Backside)
                {
                    string contentLocation = string.Format("Supply\\{0}", card);
                    Texture2D cardTexture = Content.Load<Texture2D>(contentLocation);
                    buttonImages.Add(card, cardTexture);
                }
            }

            Empty = new CardSprite(CardName.Empty, -1);
            Back = new CardSprite(CardName.Backside, -1);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (StartUpdate != null & StartUp == false)
            {
                StartUp = true;
                StartUpdate();
            }

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            mouseX = currentMouseState.X;
            mouseY = currentMouseState.Y;

            if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
            {
                if (handZone.IsClickWithin(mouseX, mouseY))
                {
                    HandCardClicked(handZone.FindCardByMouseClick(mouseX, mouseY));
                }

                if (supplyZone.IsClickWithin(mouseX, mouseY))
                {
                    SupplyCardClicked(supplyZone.FindCardByMouseClick(mouseX, mouseY));
                }

                if ((mouseX < 285 && mouseX > 185) && (mouseY < 370 && mouseY > 340))
                {
                    EndPhaseClicked();
                }
            }

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
            spriteBatch.Draw(table, Vector2.Zero, Color.White);

            handZone.Draw(spriteBatch);
            actionZone.Draw(spriteBatch);
            discardZone.Draw(spriteBatch);
            deckZone.Draw(spriteBatch);
            supplyZone.Draw(spriteBatch);
            spriteBatch.Draw(endphasebutton, new Vector2(165, 340), Color.White);
            spriteBatch.Draw(cursor, new Vector2(mouseX, mouseY), Color.White);
            spriteBatch.DrawString(font, "Actions: " + actions.ToString(), new Vector2(400, 15), Color.RoyalBlue);
            spriteBatch.DrawString(font, "Buys: " + buys.ToString(), new Vector2(600, 15), Color.RoyalBlue);
            spriteBatch.DrawString(font, "Coins: " + coins.ToString(), new Vector2(800, 15), Color.RoyalBlue);
            spriteBatch.DrawString(font, "Player" + playernum.ToString(), new Vector2(10, 32), Color.RoyalBlue);
            spriteBatch.DrawString(font, turn ? "Your turn   -" : "Not your turn", new Vector2(10, 10), Color.RoyalBlue);
            if (phase == 0)
            {
                spriteBatch.DrawString(font, "Action phase", new Vector2(163, 10), Color.RoyalBlue);
            }
            if (phase == 1)
            {
                spriteBatch.DrawString(font, "Buy phase", new Vector2(163, 10), Color.RoyalBlue);
            }

            if (endOfGame && playernum == winnerNum)
            {
                spriteBatch.DrawString(fontWin, "YOU ARE THE WINNER \n CONGRATULATIONS", new Vector2(200, 220), Color.Indigo);
            }

            if (endOfGame && playernum != winnerNum)
            {
                spriteBatch.DrawString(fontWin, "YOU LOSE\n NOW TAKE YOUR\n CURSED SOUL AND GO AWAY", new Vector2(200, 220), Color.Indigo);
            }

            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}