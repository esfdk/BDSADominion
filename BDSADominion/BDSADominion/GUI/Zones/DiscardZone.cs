﻿namespace BDSADominion.GUI.Zones
 {
     using BDSADominion.GUI.Sprites;

     using Microsoft.Xna.Framework;
     using Microsoft.Xna.Framework.Graphics;

     /// <summary>
     /// The class for the Discardzone.
     /// </summary>
     /// <author>
     /// Frederik Lysgaard (frly@itu.dk)
     /// </author>
     internal class DiscardZone
     {
         /// <summary>
         /// The list of cards in the discardzone.
         /// </summary>
         private CardSprite discardzone;

         /// <summary>
         /// The starting position of the discardzone.
         /// </summary>
         private readonly Vector2 drawPosition = new Vector2(10, 375); //This would be an interesting candidate for GUIConstants

         /// <summary>
         /// Add one card to the discardzone.
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
             discardzone = GameClass.Empty;
         }

         /// <summary>
         /// Draw the discardzone spritbatch
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
