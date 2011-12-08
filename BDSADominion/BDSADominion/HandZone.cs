namespace BDSADominion
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// The class for the Handzone.
    /// </summary>
    public class HandZone : SpriteBatch
    {
        /// <summary>
        /// The list of cards in the hand
        /// </summary>
        private List<Card> hand = new List<Card>();

        /// <summary>
        /// The asset name for the Sprite's Texture
        /// </summary>
        private string assetName = "dominnion-gardens";

        /// <summary>
        /// The texture object used when drawing the sprite.
        /// </summary>
        private Texture2D cardimage;

        /// <summary>
        /// The Size of the Sprite (with scale applied)
        /// </summary>
        private Rectangle size = new Rectangle(300, 330, 400, 150);

        /// <summary>
        /// The current position of the Sprite.
        /// </summary>
        private Vector2 position = new Vector2(360, 330);

        /// <summary>
        /// The amount to increase/decrease the size of the original sprite. 
        /// </summary>
        private float scale = 1.0f;

        /// <summary>
        /// Input of mouse (keys)
        /// </summary>
        private InputState input;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandZone"/> class.
        /// </summary>
        /// <param name="graphicsDevice">
        /// The graphics device.
        /// </param>
        public HandZone(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
        }

        /// <summary>
        /// Gets or sets TouchRect.
        /// </summary>
        public Rectangle TouchRect { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Clicked.
        /// </summary>
        public bool Clicked { get; set; }

        /// <summary>
        /// Add one card to the deck
        /// </summary>
        public void AddCard(Card newCard)
        {
            hand.Add(newCard);
        }

        /// <summary>
        /// Adds a list of cards to hand.
        /// </summary>
        /// <param name="cards">
        /// The cards.
        /// </param>
        public void AddCards(List<Card> cards)
        {
            foreach (Card c in cards)
            {
                hand.Add(c);
            }
        }

        /// <summary>
        /// The loadcontent for the handzone
        /// </summary>
        /// <param name="theContentManager">
        /// The the Content Manager.
        /// </param>
        /// <param name="theAssetName">
        /// The the Asset Name.
        /// </param>
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            input = new InputState();
            TouchRect = new Rectangle(360, 300, 100, 200);
            this.cardimage = theContentManager.Load<Texture2D>("dominion-gardens");
            this.assetName = theAssetName;
            this.size = new Rectangle(
                0, 0, (int)(this.cardimage.Width * this.scale), (int)(this.cardimage.Height * this.scale));
        }

        /// <summary>
        /// Returns the 
        /// </summary>
        /// <param name="mouseX">
        /// The mouse x.
        /// </param>
        /// <param name="mouseY">
        /// The mouse y.
        /// </param>
        public void IsCardClicked(int mouseX, int mouseY)
        {
            Rectangle mouseRect = new Rectangle(mouseX, mouseY, 1, 1);

            if (TouchRect.Intersects(mouseRect))
            {
                Clicked = true;
            }
        }

        /// <summary>
        /// Update method for the handzone
        /// </summary>
        /// <param name="gameTime">
        /// The game time.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        public void Update(GameTime gameTime)
        {
            if (input.CurrentMouseState.LeftButton == ButtonState.Pressed && input.LastMouseState.LeftButton == ButtonState.Released)
            {
                IsCardClicked((int)input.CurrentMouseState.X, (int)input.CurrentMouseState.Y);
            }
        }

        /// <summary>
        /// Draw the handzone spritbatch
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            /*if (hand.Count > 0)
            {
                foreach (Card card in hand)
                {

                }
            }*/
            spriteBatch.Draw(
                this.cardimage,
                this.position,
                new Rectangle(0, 0, this.cardimage.Width, this.cardimage.Height),
                Color.White,
                0.0f,
                Vector2.Zero,
                this.scale,
                SpriteEffects.None,
                0);
        }
    }
}
