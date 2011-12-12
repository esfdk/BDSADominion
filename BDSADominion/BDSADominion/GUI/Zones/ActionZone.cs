﻿namespace BDSADominion.GUI
 {
     using System;
     using System.Collections.Generic;

     using Microsoft.Xna.Framework;
     using Microsoft.Xna.Framework.Graphics;

     /// <summary>
     /// The class for the Handzone.
     /// </summary>
     public class ActionZone
     {
         /// <summary>
         /// The list of cards in the hand
         /// </summary>
         private List<CardSprite> action = new List<CardSprite>();

         /// <summary>
         /// next card x-coor
         /// </summary>
         private Vector2 offset = new Vector2(135, 0);

         /// <summary>
         /// touchrectangle.
         /// </summary>
         ////private Rectangle touchRect;

         /// <summary>
         /// The starting position of the hand
         /// </summary>
         private Vector2 startPosition = new Vector2(10, 50);

         /// <summary>
         /// Initializes a new instance of the <see cref="BDSADominion.ActionZone"/> class.
         /// </summary>
         /// <param name="topmostleftlocation">
         /// The topmostleftlocation.
         /// </param>
         public ActionZone()
         {
             TouchRect = new Rectangle(
                 (int)this.startPosition.X, (int)this.startPosition.Y, (int)(this.offset.X * 10), 218);
         }

         /// <summary>
         /// Gets or sets TouchRect.
         /// </summary>
         public Rectangle TouchRect { get; private set; }

         /// <summary>
         /// Gets or sets a value indicating whether Clicked.
         /// </summary>
         ////public bool Clicked { get; set; }

         public CardSprite RemoveCard(Cardmember cardmember, int id)
         {
             foreach (CardSprite card in action)
             {
                 if (card.Id == id & card.Card == cardmember)
                 {
                     bool success = action.Remove(card);
                     if (success)
                     {
                         return card;
                     }
                 }
             }
             return null;
         }


         /// <summary>
         /// Add one card to the hand.
         /// </summary>
         /// <param name="newCardSprite">
         /// The new Card.
         /// </param>
         public void AddCard(CardSprite newCardSprite)
         {
             action.Add(newCardSprite);
         }

         /// <summary>
         /// Adds a list of cards to hand.
         /// </summary>
         /// <param name="cards">
         /// The cards.
         /// </param>
         public void AddCards(List<CardSprite> cards)
         {
             foreach (CardSprite card in cards)
             {
                 action.Add(card);
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
             if (action.Count > 0)
             {
                 Vector2 currentPosition = startPosition;

                 foreach (CardSprite card in action)
                 {
                     if (card != null)
                     {
                         card.Draw(spriteBatch, currentPosition);
                         currentPosition += offset;
                     }
                 }
             }
         }

         /// <summary>
         /// Returns the actual card clicked on 
         /// </summary>
         /// <param name="mouseX">
         /// The mouse X.
         /// </param>
         /// <param name="mouseY">
         /// The mouse Y.
         /// </param>
         /// <returns>
         /// The find card by mouse click.
         /// </returns>
         public CardSprite FindCardByMouseClick(int mouseX, int mouseY)
         {
             //Rectangle mouseRect = new Rectangle(mouseX, mouseY, 1, 1);
             if (TouchRect.Contains(mouseX, mouseY))
             {
                 int mouseCardX = mouseX - (int)this.startPosition.X;
                 float clickedValue = mouseCardX / this.offset.X;
                 float clickedInto = (mouseCardX % this.offset.X) / offset.X;

                 int clickedIndex = (int)Math.Round(clickedValue - clickedInto);
                 int count = 0;
                 foreach (CardSprite card in action)
                 {
                     if (clickedIndex == count)
                     {
                         return card;
                     }
                     count++;
                 }
             }

             return null; //TODO
         }
     }
 }
