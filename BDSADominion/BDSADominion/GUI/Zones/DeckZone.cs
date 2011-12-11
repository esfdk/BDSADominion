﻿﻿namespace BDSADominion
 {

     using Microsoft.Xna.Framework;
     using Microsoft.Xna.Framework.Graphics;

     /// <summary>
     /// The class for the Handzone.
     /// </summary>
     public class DeckZone
     {
         /// <summary>
         /// The list of cards in the hand
         /// </summary>
         private CardSprite deckzone;

         /// <summary>
         /// The starting position of the hand
         /// </summary>
         private Vector2 startPosition = new Vector2(145, 375);

         /// <summary>
         /// Initializes a new instance of the <see cref="HandZone"/> class.
         /// </summary>
         /// <param name="topmostleftlocation">
         /// The topmostleftlocation.
         /// </param>
         public DeckZone()
         {

         }

         /// <summary>
         /// Gets or sets TouchRect.
         /// </summary>
         public Rectangle TouchRect { get; private set; }

         /// <summary>
         /// Gets or sets a value indicating whether Clicked.
         /// </summary>
         ////public bool Clicked { get; set; }

         /// <summary>
         /// Add one card to the hand.
         /// </summary>
         /// <param name="newCardSprite">
         /// The new Card.
         /// </param>
         public void AddCard(CardSprite newCardSprite)
         {
             deckzone = (newCardSprite);
         }

         /// <summary>
         /// Draw the handzone spritbatch
         /// </summary>
         /// <param name="spriteBatch">
         /// The sprite Batch.
         /// </param>
         public void Draw(SpriteBatch spriteBatch)
         {
             Vector2 currentPosition = startPosition;

             if (deckzone != null)
             {
                 deckzone.Draw(spriteBatch, currentPosition);
             }
         }
     }
 }