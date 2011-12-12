﻿namespace BDSADominion.GUI
 {

     using Microsoft.Xna.Framework;
     using Microsoft.Xna.Framework.Graphics;

     /// <summary>
     /// The class for the Handzone.
     /// </summary>
     internal class DiscardZone
     {
         /// <summary>
         /// The list of cards in the hand
         /// </summary>
         private CardSprite discardzone;

         /// <summary>
         /// The starting position of the hand
         /// </summary>
         private Vector2 drawPosition = new Vector2(10, 375); //TODO Move to GUIConstants

         /// <summary>
         /// Initializes a new instance of the <see cref="HandZone"/> class.
         /// </summary>
         /// <param name="topmostleftlocation">
         /// The topmostleftlocation.
         /// </param>
         internal DiscardZone()
         {

         }

         /// <summary>
         /// Gets or sets TouchRect.
         /// </summary>
         ////public Rectangle TouchRect { get; private set; }

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
             discardzone = newCardSprite;
         }

         internal void SetEmpty()
         {
             discardzone = GUIConstants.Empty;
         }

         /// <summary>
         /// Draw the handzone spritbatch
         /// </summary>
         /// <param name="spriteBatch">
         /// The sprite Batch.
         /// </param>
         public void Draw(SpriteBatch spriteBatch)
         {
             Vector2 currentPosition = drawPosition;

             if (discardzone != null)
             {
                 discardzone.Draw(spriteBatch, currentPosition);
             }
         }
     }
 }
