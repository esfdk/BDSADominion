namespace BDSADominion.Gamestate
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using BDSADominion.Gamestate.Card_Types;

    /// <summary>
    /// Each player is represented by this object that keeps track of their decks, hands, discard piles, etc.
    /// </summary>
    /// <author>
    /// Jakob Melnyk (jmel@itu.dk)
    /// </author>
    public class Player
    {
        /// <summary>
        /// The deck belonging to the player.
        /// </summary>
        private Stack<Card> deck = new Stack<Card>();

        /// <summary>
        /// The discard pile of the player.
        /// </summary>
        private Stack<Card> discard = new Stack<Card>();

        /// <summary>
        /// The cards the player has played this turn.
        /// </summary>
        private Stack<Card> played = new Stack<Card>();

        /// <summary>
        /// The cards in the hand of the player.
        /// </summary>
        private List<Card> hand = new List<Card>();

        /// <summary>
        /// The cards in the temporary zone.
        /// </summary>
        private List<Card> temporary = new List<Card>();

        /// <summary>
        /// Gets the Player's number.
        /// </summary>
        public uint PlayerNumber { get; private set; }

        /// <summary>
        /// Gets all the cards belonging to the player.
        /// </summary>
        public List<Card> GetAllCards { get; private set; }

        /// <summary>
        /// Gets the size of the deck.
        /// </summary>
        public uint DeckSize
        {
            get { return (uint)deck.Count; }
        }

        /// <summary>
        /// Gets the size of the discard pile.
        /// </summary>
        public uint DiscardSize
        {
            get { return (uint)discard.Count; }
        }

        /// <summary>
        /// Gets the object at the top of the discard pile.
        /// </summary>
        public Card TopOfDiscard
        {
            get { return discard.Peek(); }
        }

        /// <summary>
        /// Gets the Player's hand.
        /// </summary>
        public List<Card> Hand
        {
            get { return hand; }
        }

        /// <summary>
        /// Gets the cards played by the Player.
        /// </summary>
        public Stack<Card> Played
        {
            get { return played; }
        }

        /// <summary>
        /// Gets the card in the temporary zone.
        /// </summary>
        public List<Card> TempZone
        {
            get { return temporary; }
        }

        /// <summary>
        /// Moves a card from the hand to the temporary zone.
        /// </summary>
        /// <param name="card">
        /// The card to be moved.
        /// </param>
        public void MoveFromHandToTemporary(Card card)
        {
            Contract.Ensures(!hand.Contains(card));
            Contract.Ensures(TempZone.Contains(card));
            temporary.Add(hand[hand.IndexOf(card)]);
            hand.Remove(card);
        }

        /// <summary>
        /// Moves a card from either the deck or the discard pile into the temporary zone.
        /// </summary>
        /// <param name="zone">
        /// The zone to move card from.
        /// </param>
        public void MoveFromZoneToTemporary(Zone zone)
        {
            Contract.Requires(zone == Zone.Deck | zone == Zone.Discard);
            Contract.Ensures(TempZone.Count == Contract.OldValue(TempZone.Count) + 1);
            Contract.Ensures(zone == Zone.Deck ? DeckSize == Contract.OldValue(DeckSize) - 1 : true);
            Contract.Ensures(zone == Zone.Discard ? DiscardSize == Contract.OldValue(DiscardSize) - 1 : true);
            switch (zone)
            {
                case Zone.Deck:
                    temporary.Add(deck.Pop());
                    break;
                case Zone.Discard:
                    temporary.Add(discard.Pop());
                    break;
                default:
                    throw new ArgumentOutOfRangeException("zone");
            }
        }
    }
}