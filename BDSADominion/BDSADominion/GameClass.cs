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
        #region // Fields\\
        /// <summary>
        /// The Graphicsmanager for the game.
        /// </summary>
        private readonly GraphicsDeviceManager graphics;

        /// <summary>
        /// The cards in the handzone.
        /// </summary>
        private readonly List<Card> handcards = new List<Card>();

        /// <summary>
        /// The cards in the actionzone.
        /// </summary>
        private readonly List<Card> actioncards = new List<Card>();

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
        /// The index.
        /// </summary>
        private int cardIndex;

        /// <summary>
        /// The spritebatch used.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// The asset name for the Sprite's Texture.
        /// </summary>
        private string assetName;

        /// <summary>
        /// The ActionZone sprite.
        /// </summary>
        private ActionZone actionZone;

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
        private InputState input;

        /// <summary>
        /// The mousestate.
        /// </summary>
        private MouseState mouseState;

        /// <summary>
        /// location of the next card.
        /// </summary>
        private const int Cardoffset = 80;

        /// <summary>
        /// initial location
        /// </summary>
        private int LocationX;

        #endregion

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
            deck = new Deck(GraphicsDevice);
            discard = new Discard(GraphicsDevice);
            this.InitializeCards();

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
            cursor = Content.Load<Texture2D>("Cursor");
            supbutton = Content.Load<Texture2D>("Buttonsupply");
            supover = Content.Load<Texture2D>("Buttonover");
            actions = 0;
            buys = 0;
            coins = 0;
            deck.LoadContent(Content, assetName);
            discard.LoadContent(Content, assetName);

            Card.LoadCardTextures();
        }

        /// <summary>
        /// Initialize all cards.
        /// </summary>
        protected void InitializeCards()
        {
            // the hand zone.
            LocationX = 400;
            cardIndex = 0;
            for (int x = 0; x <= 10; x++)
            {
                handcards.Add(new Card(new Vector2(LocationX, 500)));
                handcards[cardIndex].CardHeight = Card.ImageHeight;
                handcards[cardIndex].CardWidth = Card.ImageWidth;
                handcards[cardIndex].OffsetHeight = 0;
                handcards[cardIndex].OffsetWidth = 20;
                handcards[cardIndex].Index = x;

                for (int i = 0; i <= x; i++)
                {
                    handcards.Add();
                }

                LocationX += Cardoffset;
                cardIndex += 1;
            }

            LocationX = 50;
            for (int x = 10; x <= 20; x++)
            {
                actioncards.Add(new ActionZone(new Vector2(LocationX, 50)));
                actioncards[cardIndex].CardHeight = Card.ImageHeight;
                actioncards[cardIndex].CardWidth = Card.ImageWidth;
                actioncards[cardIndex].OffsetHeight = 0;
                actioncards[cardIndex].OffsetWidth = 20;
                actioncards[cardIndex].Index = x;

                LocationX += Cardoffset;
            }
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

            if (input.CurrentMouseState.LeftButton == ButtonState.Pressed && input.LastMouseState.LeftButton == ButtonState.Released)
            {
                for (int x = 0; x <= 10; x++)
                {
                    handzone.FindCardByMouseClick((int)input.CurrentMouseState.X, (int)input.CurrentMouseState.Y);


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
            graphics.GraphicsDevice.Clear(Color.BlanchedAlmond);
            spriteBatch.Begin();
            //spriteBatch.Draw(table, Vector2.Zero, Color.White);
            spriteBatch.Draw(supbutton, new Vector2(1150, 300), Color.White);
            this.discard.Draw(this.spriteBatch);
            this.deck.Draw(this.spriteBatch);

            if ((mouseX < 1350 && mouseX > 1050) && (mouseY > 280 && mouseY < 320))
            {
                spriteBatch.Draw(supover, new Vector2(1150, 300), Color.White);
                if (ButtonState.Pressed == mouseState.LeftButton)
                {
                    spriteBatch.DrawString(font, "You click OK", new Vector2(100.0f, 50.0f), Color.YellowGreen);
                }
            }

            handzone.Draw(spriteBatch);
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
