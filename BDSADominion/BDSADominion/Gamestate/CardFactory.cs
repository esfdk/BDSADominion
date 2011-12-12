namespace BDSADominion.Gamestate
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

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
        /// All the cards created by the factory.
        /// </summary>
        private static Dictionary<Card, bool> createdCards = new Dictionary<Card, bool>();

        /// <summary>
        /// Gets a value indicating whether set-up has already been performed.
        /// </summary>
        public static bool SetUp { get; private set; }

        /// <summary>
        /// Gets CreatedCards.
        /// </summary>
        public static Dictionary<Card, bool> CreatedCards
        {
            get { return createdCards; }
        }

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
            Contract.Requires(card != CardName.Backside & card != CardName.Empty);
            Contract.Ensures(Contract.Result<Card>().Name == card);
            Card c;
            switch (card)
            {
                case CardName.Copper:
                    c = new Copper();
                    break;
                case CardName.Silver:
                    c = new Silver();
                    break;
                case CardName.Gold:
                    c = new Gold();
                    break;
                case CardName.Curse:
                    c = new Curse();
                    break;
                case CardName.Estate:
                    c = new Estate();
                    break;
                case CardName.Duchy:
                    c = new Duchy();
                    break;
                case CardName.Province:
                    c = new Province();
                    break;
                case CardName.Gardens:
                    c = new Gardens();
                    break;
                case CardName.Cellar:
                    c = new Cellar();
                    break;
                case CardName.Chapel:
                    c = new Chapel();
                    break;
                case CardName.Chancellor:
                    c = new Chancellor();
                    break;
                case CardName.Village:
                    c = new Village();
                    break;
                case CardName.Woodcutter:
                    c = new Woodcutter();
                    break;
                case CardName.Workshop:
                    c = new Workshop();
                    break;
                case CardName.Feast:
                    c = new Feast();
                    break;
                case CardName.Moneylender:
                    c = new Moneylender();
                    break;
                case CardName.Remodel:
                    c = new Remodel();
                    break;
                case CardName.Smithy:
                    c = new Smithy();
                    break;
                case CardName.ThroneRoom:
                    c = new ThroneRoom();
                    break;
                case CardName.CouncilRoom:
                    c = new CouncilRoom();
                    break;
                case CardName.Festival:
                    c = new Festival();
                    break;
                case CardName.Laboratory:
                    c = new Laboratory();
                    break;
                case CardName.Library:
                    c = new Library();
                    break;
                case CardName.Market:
                    c = new Market();
                    break;
                case CardName.Mine:
                    c = new Mine();
                    break;
                case CardName.Adventurer:
                    c = new Adventurer();
                    break;
                case CardName.Bureaucrat:
                    c = new Bureaucrat();
                    break;
                case CardName.Militia:
                    c = new Militia();
                    break;
                case CardName.Spy:
                    c = new Spy();
                    break;
                case CardName.Thief:
                    c = new Thief();
                    break;
                case CardName.Witch:
                    c = new Witch();
                    break;
                case CardName.Moat:
                    c = new Moat();
                    break;
                default:
                    throw new NotImplementedException();
            }

            c.Initialize(card, CardsMade[card]);
            CardsMade[card] += 1;
            createdCards.Add(c, true);
            return c;
        }

        /// <summary>
        /// Makes sure a card cannot exist 
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
        private static bool InvariantHelper()
        {
            if (CreatedCards.Keys.Any(card => CardsMade[card.Name] < card.Number))
            {
                return false;
            }

            // LINQ helped reduce this.
            return !CardsMade.Keys.Any(cn => CreatedCards.Keys.Count(card => card.Name == cn) != CardsMade[cn]);
        }
    }
}
