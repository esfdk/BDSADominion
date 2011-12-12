﻿namespace BDSADominion.GUI
 {
     using System;
     using System.Collections.Generic;

     using Microsoft.Xna.Framework;
     using Microsoft.Xna.Framework.Graphics;

     /// <summary>
     /// The class for the Handzone.
     /// </summary>
     public class SupplyZone
     {
         /// <summary>
         /// The list of cards in the hand
         /// </summary>
         private List<ButtonSprite> supply = new List<ButtonSprite>();

         /// <summary>
         /// next card x-coor
         /// </summary>
         private Vector2 offset = new Vector2(0, 35);

         /// <summary>
         /// touchrectangle.
         /// </summary>
         ////private Rectangle touchRect;

         /// <summary>
         /// The starting position of the hand
         /// </summary>
         private Vector2 startPosition = new Vector2(1150, 50);

         /// <summary>
         /// Initializes a new instance of the <see cref="HandZone"/> class.
         /// </summary>
         /// <param name="topmostleftlocation">
         /// The topmostleftlocation.
         /// </param>
         public SupplyZone()
         {
             TouchRect = new Rectangle(
                 (int)this.startPosition.X, (int)this.startPosition.Y, 100, (int)(this.offset.Y * 16));
         }

         /// <summary>
         /// Gets or sets TouchRect.
         /// </summary>
         public Rectangle TouchRect { get; private set; }

         /// <summary>
         /// Gets or sets a value indicating whether Clicked.
         /// </summary>
         ////public bool Clicked { get; set; }

         /*public CardSprite RemoveCard(Cardmember cardmember, int id)
         {
             foreach (ButtonSprite card in supply)
             {
                 if (card.Id == id & card.CardMember == cardmember)
                 {
                     bool success = supply.Remove(card);
                     if (success)
                     {
                         return card;
                     }
                 }
             }
             return null;
         }*/

         /// <summary>
         /// Add one card to the hand.
         /// </summary>
         /// <param name="newButtonSprite">
         /// The new Card.
         /// </param>
         public void AddCard(ButtonSprite newButtonSprite)
         {
             supply.Add(newButtonSprite);
         }

         /// <summary>
         /// Adds a list of cards to hand.
         /// </summary>
         /// <param name="buttons">
         /// The cards.
         /// </param>
         public void AddCards(List<CardSprite> cards)
         {
             foreach (CardSprite card in cards)
             {
                 supply.Add(new ButtonSprite(card.Card));
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
             if (supply.Count > 0)
             {
                 Vector2 currentPosition = startPosition;

                 foreach (ButtonSprite card in supply)
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
         public CardSprite FindCardByMouseClick(int mouseX, int mouseY, SpriteBatch spriteBatch, SpriteFont font)
         {
             //Rectangle mouseRect = new Rectangle(mouseX, mouseY, 1, 1);
             if (TouchRect.Contains(mouseX, mouseY))
             {

                 int mouseCardY = mouseY - (int)this.startPosition.Y;
                 float clickedValue = mouseCardY / this.offset.Y;
                 float clickedInto = (mouseCardY % this.offset.Y) / offset.Y;

                 int clickedIndex = (int)Math.Round(clickedValue - clickedInto);
                 int count = 0;
                 foreach (ButtonSprite card in supply)
                 {
                     if (clickedIndex == count)
                     {
                         return new CardSprite(card.CardMember, 10);
                     }
                     count++;
                 }
             }

             return null; //TODO
         }
     }
 }