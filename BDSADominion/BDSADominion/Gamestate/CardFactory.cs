namespace BDSADominion.Gamestate
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using BDSADominion.Gamestate.Card_Types;
    using BDSADominion.Gamestate.Card_Types.Cards;
    using BDSADominion.Gamestate.Card_Types.Cards.Action_Cards;
    using BDSADominion.Gamestate.Card_Types.Cards.Action_Cards.Attack_Cards;
    using BDSADominion.Gamestate.Card_Types.Cards.Action_Cards.Reaction_Cards;
    using BDSADominion.Gamestate.Card_Types.Cards.Treasure_Cards;
    using BDSADominion.Gamestate.Card_Types.Cards.Victory_Cards;

    /// <summary>
    /// Used to produce cards for the Dominion game.
    /// </summary>
    /// <author>
    /// Jakob Melnyk (jmel@itu.dk)
    /// </author>
    public class CardFactory
    {
        /// <summary>
        /// Keeps track of how many of the different cards have been made.
        /// </summary>
        private static readonly Dictionary<CardName, uint> CardsMade = new Dictionary<CardName, uint>();

        /// <summary>
        /// Gets a value indicating whether set-up has already been performed.
        /// </summary>
        public static bool SetUp { get; private set; }

        /// <summary>
        /// Set up the factory for a game of Dominion.
        /// </summary>
        /// <param name="cards">
        /// The cards used in this game of Dominion.
        /// </param>
        public static void SetUpCards(List<Card> cards)
        {
            Contract.Requires(!SetUp);
            Contract.Ensures(SetUp);
            foreach (Card c in cards)
            {
                CardsMade.Add(c.Name, 1);
            }

            SetUp = true;
        }

        /// <summary>
        /// Creates a new card based on how many cards have already been made.
        /// </summary>
        /// <param name="card">
        /// The name of the card to be created.
        /// </param>
        /// <returns>
        /// The new created card.
        /// </returns>
        public static Card CreateCard(CardName card)
        {
            Contract.Ensures(Contract.Result<Card>().Name == card);
            Card c;
            switch (card)
            {
                case CardName.Copper:
                    c = new Copper();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Silver:
                    c = new Silver();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Gold:
                    c = new Gold();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Curse:
                    c = new Curse();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Estate:
                    c = new Estate();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Duchy:
                    c = new Duchy();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Province:
                    c = new Province();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Gardens:
                    c = new Gardens();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Cellar:
                    c = new Cellar();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Chapel:
                    c = new Chapel();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Chancellor:
                    c = new Chancellor();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Village:
                    c = new Village();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Woodcutter:
                    c = new Woodcutter();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Workshop:
                    c = new Workshop();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Feast:
                    c = new Feast();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Moneylender:
                    c = new Moneylender();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Remodel:
                    c = new Remodel();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Smithy:
                    c = new Smithy();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.ThroneRoom:
                    c = new ThroneRoom();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.CouncilRoom:
                    c = new CouncilRoom();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Festival:
                    c = new Festival();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Laboratory:
                    c = new Laboratory();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Library:
                    c = new Library();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Market:
                    c = new Market();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Mine:
                    c = new Mine();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Adventurer:
                    c = new Adventurer();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Bureaucrat:
                    c = new Bureaucrat();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Militia:
                    c = new Militia();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Spy:
                    c = new Spy();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Thief:
                    c = new Thief();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Witch:
                    c = new Witch();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                case CardName.Moat:
                    c = new Moat();
                    c.Initialize(card, CardsMade[card]);
                    CardsMade[card] += 1;
                    return c;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
