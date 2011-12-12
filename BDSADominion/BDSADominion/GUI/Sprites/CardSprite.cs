using BDSADominion.Gamestate;
using BDSADominion.Gamestate.Card_Types;

namespace BDSADominion.GUI
{

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This class holds the information for the representation of cards.
    /// </summary>
    public class CardSprite
    {
        ////public CardName Card { get; private set; }

        public Card cardRef { get; private set; }

        /// <summary>
        /// The texture for the front of the card
        /// </summary>
        private Texture2D cardFront;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardSprite"/> class.
        /// </summary>
        public CardSprite(Card card)
        {
            Clicked = false;
            ////position = Vector2.Zero;
            ////rectangle = new Rectangle((int)position.X, (int)position.Y, 1, 1);
            ////CardName = cardenum;
            ////Id = id;
            this.cardFront = GUIConstants.cardImages[card.Name];
        }

        /// <summary>
        /// Gets ImageHeight.
        /// </summary>
        /*public int ImageHeight
        {
            get
            {
                return this.cardFront.Height;
            }
        }

        /// <summary>
        /// Gets ImageWidth.
        /// </summary>
        public int ImageWidth
        {
            get
            {
                return this.cardFront.Width;
            }
        }*/

        /// <summary>
        /// Gets or sets rectangle size which holds the image of the card.
        /// </summary>
        ////public Rectangle Rectangle { get; set; }

        /// <summary>
        /// Gets or sets isclicked
        /// </summary>
        internal bool Clicked { get; set; }

        /// <summary>
        /// the draw method of card
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            /*
            spriteBatch.Draw(
                cardFront,
                position,
                Rectangle.Empty, <---- THE CODE BREAKER!!!!
                Color.White,
                0.0f,
                Vector2.Zero,
                GUIConstants.SCALE,
                SpriteEffects.None,
                0);*/
            spriteBatch.Draw(cardFront, position, Color.White);
        }
    }
}
