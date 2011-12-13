﻿namespace BDSADominion.GUI.Zones
 {
     using Microsoft.Xna.Framework;
     using Microsoft.Xna.Framework.Graphics;

     /// <summary>
     /// The class for the Deckzone.
     /// </summary>
     /// <author>
     /// Frederik Lysgaard (frly@itu.dk)
     /// </author>
     internal class DeckZone
     {
         /// <summary>
         /// The list of cards in the deck.
         /// </summary>
         private CardSprite deckzone;

         /// <summary>
         /// The starting position of the deck.
         /// </summary>
         private Vector2 drawPosition = new Vector2(145, 375); //This would be an interesting candidate for GUIConstants

         /// <summary>
         /// Initializes a new instance of the <see cref="HandZone"/> class.
         /// </summary>
         /// <param name="topmostleftlocation">
         /// The topmostleftlocation.
         /// </param>
         internal DeckZone()
         {
         }

         /// <summary>
         /// Add one card to the deck.
         /// </summary>
         /// <param name="newCardSprite">
         /// The new Card.
         /// </param>
         internal void SetFilled()
         {
             deckzone = GameClass.Back;
         }

         internal void SetEmpty()
         {
             deckzone = GameClass.Empty;
         }

         /// <summary>
         /// Draw the deckzone spritbatch
         /// </summary>
         /// <param name="spriteBatch">
         /// The sprite Batch.
         /// </param>
         internal void Draw(SpriteBatch spriteBatch)
         {
             Vector2 currentPosition = drawPosition;

             if (deckzone != null)
             {
                 deckzone.Draw(spriteBatch, currentPosition);
             }
         }
     }
 }