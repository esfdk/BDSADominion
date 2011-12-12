using BDSADominion.Gamestate;

namespace BDSADominion.GUI
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
        internal bool turn;

        /// <summary>
        /// Indicate phase.
        /// </summary>
        internal int phase;

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
        internal bool endOfGame;

        /// <summary>
        /// indicate the winer
        /// </summary>
        internal int winnerNum;

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
        
        public static CardSprite Empty;
        public static CardSprite Back;

        public static Dictionary<CardName, Texture2D> cardImages = new Dictionary<CardName, Texture2D>();
        public static Dictionary<CardName, Texture2D> buttonImages = new Dictionary<CardName, Texture2D>();

        internal event CardSpriteHandler HandCardClicked;
        internal event CardNameHandler SupplyCardClicked;
        internal event ClickHandler EndPhaseClicked;

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

            ////TEST ZONE:
            handZone.NewCards(
                new List<CardSprite> { new CardSprite(CardName.Market, 1), new CardSprite(CardName.Gardens, 1) });
            supplyZone.NewCards(
                new List<CardName> { CardName.Market, CardName.Gardens });
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

            mouseX = currentMouseState.X;
            mouseY = currentMouseState.Y;

            if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
            {
                if (handZone.isClickWithin(mouseX, mouseY))
                {
                    HandCardClicked(handZone.FindCardByMouseClick(mouseX));
                }

                if (supplyZone.IsClickWithin(mouseX, mouseY))
                {
                    SupplyCardClicked(supplyZone.FindCardByMouseClick(mouseY));
                }

                //EndPhaseClicked();

                //TODO EndPhaseButton
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
            //spriteBatch.Draw(table, Vector2.Zero, Color.White);

            /*if (ButtonState.Pressed == currentMouseState.LeftButton && lastMouseState.LeftButton == ButtonState.Released)
            {
                CardSprite cardx = actionZone.FindCardByMouseClick(mouseX, mouseY);
                if (cardx != null)
                {
                    discardZone.AddCard(cardx);
                }
                if (cardx != null)
                {
                    actionZone.RemoveCard(cardx.Card, cardx.Id);
                }

            }

            if (ButtonState.Pressed == currentMouseState.LeftButton && lastMouseState.LeftButton == ButtonState.Released)
            {
                CardName cardx = supplyZone.FindCardByMouseClick(mouseX, mouseY, spriteBatch, font);
                if (cardx != null)
                {
                    discardZone.AddCard(cardx);
                }
            }*/

            handZone.Draw(spriteBatch);
            actionZone.Draw(spriteBatch);
            discardZone.Draw(spriteBatch);
            deckZone.Draw(spriteBatch);
            supplyZone.Draw(spriteBatch);
            spriteBatch.Draw(endphasebutton, new Vector2(185, 340), Color.White);
            spriteBatch.Draw(cursor, new Vector2(mouseX, mouseY), Color.White);
            spriteBatch.DrawString(font, "Actions: " + actions.ToString(), new Vector2(400, 15), Color.RoyalBlue);
            spriteBatch.DrawString(font, "Buys: " + buys.ToString(), new Vector2(600, 15), Color.RoyalBlue);
            spriteBatch.DrawString(font, "Coins: " + coins.ToString(), new Vector2(800, 15), Color.RoyalBlue);
            spriteBatch.DrawString(font, "Player" + playernum.ToString(), new Vector2(10, 32), Color.RoyalBlue);
            spriteBatch.DrawString(font, turn ? "Your turn   -" : "Not your turn", new Vector2(10, 10), Color.RoyalBlue);
            if (turn && phase == 0)
            {
                spriteBatch.DrawString(font, "Action phase", new Vector2(163, 10), Color.RoyalBlue);
            }
            if (turn && phase == 1)
            {
                spriteBatch.DrawString(font, "Buy phase", new Vector2(163, 10), Color.RoyalBlue);
            }

            if (endOfGame && playernum == winnerNum)
            {
                spriteBatch.DrawString(fontWin, "YOU ARE THE WINNER \n CONGRATULATIONS", new Vector2(200, 220), Color.Indigo);
            }

            if (endOfGame && playernum != winnerNum)
            {
                spriteBatch.DrawString(fontWin, "YOU LOSE, NOW TAKE YOUR CURSED SOUL AND GO AWAY", new Vector2(200, 220), Color.Indigo);
            }

            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}