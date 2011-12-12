namespace BDSADominion.Gamestate
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using BDSADominion.Gamestate.Card_Types;

    /// <summary>
    /// Each player is represented by this object that keeps track of their decks, hands, discard piles, etc.
    /// </summary>
    /// <author>
    /// Jakob Melnyk (jmel@itu.dk)
    /// </author>
    public class Player
    {
        // I realise that a lot of my contracts could have used the if(exp1) ? then(exp2) : else(exp3) to make them easier to read, but I decided to follow
        // the StyleCop/Resharper guidelines of 'simplifying' the expressions. I choose to let one of them stay in the Ensures contract for 'RemoveCardFromZone' method
        // to show how I would have done it otherwise.

        /// <summary>
        /// The deck belonging to the player.
        /// </summary>
        private readonly Stack<Card> deck = new Stack<Card>();

        /// <summary>
        /// The discard pile of the player.
        /// </summary>
        private readonly Stack<Card> discard = new Stack<Card>();

        /// <summary>
        /// The cards the player has played this turn.
        /// </summary>
        private readonly List<Card> played = new List<Card>();

        /// <summary>
        /// The cards in the hand of the player.
        /// </summary>
        private readonly List<Card> hand = new List<Card>();

        /// <summary>
        /// The cards in the temporary zone.
        /// </summary>
        private readonly List<Card> temporary = new List<Card>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="playerNumber">
        /// The player number.
        /// </param>
        public Player(uint playerNumber)
        {
            this.PlayerNumber = playerNumber;
        }

        /// <summary>
        /// Gets the Player's number.
        /// </summary>
        public uint PlayerNumber { get; private set; }

        /// <summary>
        /// Gets all the cards belonging to the player.
        /// </summary>
        public Dictionary<Card, bool> AllCards { get; private set; }

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
        public List<Card> Played
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
            Contract.Requires(zone != Zone.Deck | !(DeckSize == 0 & DiscardSize == 0));
            Contract.Requires(zone != Zone.Discard | DiscardSize != 0);

            Contract.Ensures(TempZone.Count == Contract.OldValue(TempZone.Count) + 1);
            Contract.Ensures(zone != Zone.Deck | DeckSize == Contract.OldValue(DeckSize) - 1);
            Contract.Ensures(zone != Zone.Discard | DiscardSize == Contract.OldValue(DiscardSize) - 1);

            switch (zone)
            {
                case Zone.Deck:
                    if (DeckSize == 0)
                    {
                        ShuffleDiscard();
                    }

                    if (DeckSize == 0)
                    {
                        break;
                    }

                    temporary.Add(deck.Pop());
                    break;
                case Zone.Discard:
                    if (DiscardSize == 0)
                    {
                        break;
                    }

                    temporary.Add(discard.Pop());
                    break;
            }
        }

        /// <summary>
        /// Move a card from the temporary zone to another zone.
        /// </summary>
        /// <param name="card">
        /// The card to move from the temporary zone.
        /// </param>
        /// <param name="zone">
        /// The zone to move the card to.
        /// </param>
        public void MoveFromTemporaryToZone(Card card, Zone zone)
        {
            Contract.Requires(zone == Zone.Deck | zone == Zone.Discard | zone == Zone.Hand | zone == Zone.Played);
            Contract.Requires(TempZone.Contains(card));

            Contract.Ensures(TempZone.Count == Contract.OldValue(TempZone.Count) - 1);
            Contract.Ensures(zone != Zone.Deck | DeckSize == Contract.OldValue(DeckSize) + 1);
            Contract.Ensures(zone != Zone.Discard | DiscardSize == Contract.OldValue(DiscardSize) + 1);
            Contract.Ensures(zone != Zone.Hand | (Hand.Count == Contract.OldValue(Hand.Count) + 1) & Hand.Contains(card));
            Contract.Ensures(zone != Zone.Played | (Played.Count == Contract.OldValue(Played.Count) + 1) & Played.Contains(card));

            switch (zone)
            {
                case Zone.Deck:
                    temporary.Remove(card);
                    deck.Push(card);
                    break;
                case Zone.Discard:
                    temporary.Remove(card);
                    discard.Push(card);
                    break;
                case Zone.Hand:
                    temporary.Remove(card);
                    hand.Add(card);
                    break;
                case Zone.Played:
                    temporary.Remove(card);
                    played.Add(card);
                    break;
            }
        }

        /// <summary>
        /// Adds a card to a player's hand.
        /// </summary>
        /// <param name="card">
        /// The card to be added to the zone.
        /// </param>
        /// <param name="zone">
        /// The zone to add the card to.
        /// </param>
        public void AddCardToZone(Card card, Zone zone)
        {
            Contract.Requires(zone == Zone.Deck | zone == Zone.Discard | zone == Zone.Hand | zone == Zone.Played);

            Contract.Ensures(AllCards[card]);

            Contract.Ensures(zone != Zone.Hand | hand.Contains(card));
            Contract.Ensures(zone != Zone.Hand | hand.Count == Contract.OldValue(hand.Count) + 1);

            Contract.Ensures(zone != Zone.Played | played.Contains(card));
            Contract.Ensures(zone != Zone.Played | played.Count == Contract.OldValue(played.Count) + 1);

            Contract.Ensures(zone != Zone.Deck | DeckSize == Contract.OldValue(DeckSize) + 1);
            Contract.Ensures(zone != Zone.Discard | DiscardSize == Contract.OldValue(DiscardSize) + 1);

            AllCards.Add(card, true);

            switch (zone)
            {
                case Zone.Deck:
                    deck.Push(card);
                    break;
                case Zone.Discard:
                    discard.Push(card);
                    break;
                case Zone.Hand:
                    hand.Add(card);
                    break;
                case Zone.Played:
                    played.Add(card);
                    break;
            }
        }

        /// <summary>
        /// Removes a card from the player.
        /// </summary>
        /// <param name="card">
        /// The card to be removed.
        /// </param>
        /// <param name="zone">
        /// The zone the card is currently in.
        /// </param>
        public void RemoveCardFromZone(Card card, Zone zone)
        {
            Contract.Requires(zone == Zone.Deck | zone == Zone.Discard | zone == Zone.Hand | zone == Zone.Played);

            Contract.Requires(zone != Zone.Hand | hand.Contains(card));
            Contract.Requires(zone != Zone.Played | Played.Contains(card));
            Contract.Requires(zone != Zone.Deck | !(DeckSize == 0 & DiscardSize == 0));
            Contract.Requires(zone != Zone.Discard | DiscardSize != 0);

            Contract.Ensures(!AllCards.ContainsKey(card));

            Contract.Ensures(zone == Zone.Hand ? !hand.Contains(card) : true);
            Contract.Ensures(zone != Zone.Hand | hand.Count == Contract.OldValue(hand.Count) - 1);

            Contract.Ensures(zone != Zone.Played | !played.Contains(card));
            Contract.Ensures(zone != Zone.Played | played.Count == Contract.OldValue(played.Count) - 1);

            Contract.Ensures(zone != Zone.Deck | DeckSize == Contract.OldValue(DeckSize) - 1);
            Contract.Ensures(zone != Zone.Discard | DiscardSize == Contract.OldValue(DiscardSize) - 1);

            switch (zone)
            {
                case Zone.Deck:
                    AllCards.Remove(deck.Peek());
                    deck.Pop();
                    break;
                case Zone.Discard:
                    AllCards.Remove(discard.Peek());
                    discard.Pop();
                    break;
                case Zone.Hand:
                    AllCards.Remove(card);
                    hand.Remove(card);
                    break;
                case Zone.Played:
                    AllCards.Remove(card);
                    played.Remove(card);
                    break;
            }
        }

        /// <summary>
        /// Makes the player draw a specific amount of cards.
        /// </summary>
        /// <param name="amount">
        /// The amount of cards to be drawn.
        /// </param>
        public void DrawCards(uint amount)
        {
            for (int i = 1; i < amount; i++)
            {
                DrawCard();
            }
        }

        /// <summary>
        /// Draws a card from the deck into the hand.
        /// </summary>
        public void DrawCard()
        {
            Contract.Requires(!(DeckSize == 0 & DiscardSize == 0));

            Contract.Ensures(hand.Count == Contract.OldValue(hand.Count) + 1);

            if (DeckSize == 0)
            {
                ShuffleDiscard();
            }

            hand.Add(deck.Pop());
        }

        /// <summary>
        /// Takes all the cards in the players hand and in the played zone, and puts them in the discard pile. Then draws five cards.
        /// </summary>
        public void CleanUp()
        {
            foreach (Card card in hand)
            {
                discard.Push(card);
                hand.Remove(card);
            }

            foreach (Card card in played)
            {
                discard.Push(card);
                played.Remove(card);
            }

            DrawCards(5);
        }

        /// <summary>
        /// A card cannot be in the DECK, DISCARD, HAND or PLAYED zones of a player 
        /// if it is not in the 'ALL CARDS'.
        /// </summary>
        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(InvariantHelper());
        }

        /// <summary>
        /// Helper method for the object invariant.
        /// </summary>
        /// <returns>
        /// True, if invariant holds. False, if not.
        /// </returns>
        private bool InvariantHelper()
        {
            foreach (Card card in AllCards.Keys)
            {
                if (!deck.Contains(card) & !discard.Contains(card) & !played.Contains(card) & !hand.Contains(card) & temporary.Contains(card))
                {
                    return false;
                }
            }

            if (deck.Any(card => !AllCards.ContainsKey(card)))
            {
                return false;
            }

            if (discard.Any(card => !AllCards.ContainsKey(card)))
            {
                return false;
            }

            if (played.Any(card => !AllCards.ContainsKey(card)))
            {
                return false;
            }

            if (hand.Any(card => !AllCards.ContainsKey(card)))
            {
                return false;
            }

            if (temporary.Any(card => !AllCards.ContainsKey(card)))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Shuffles the discard pile into the deck.
        /// </summary>
        private void ShuffleDiscard()
        {
            Contract.Requires(DeckSize == 0);

            // TODO: Do better shuffling
            while (discard.Count != 0)
            {
                deck.Push(discard.Pop());
            }
        }
    }
}