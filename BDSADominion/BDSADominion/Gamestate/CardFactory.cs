namespace BDSADominion.Gamestate
{
    using System.Collections.Generic;
    using BDSADominion.Gamestate.Card_Types.Cards.Treasure_Cards;

    /// <summary>
    /// Used to produce cards for the Dominion game.
    /// </summary>
    public class CardFactory
    {
        /// <summary>
        /// Keeps track of how many of the different cards have been made.
        /// </summary>
        private static Dictionary<CardName, int> cardsMade = new Dictionary<CardName, int>();

        /// <summary>
        /// 
        /// </summary>
        private static bool setUp = false;

        /// <summary>
        /// </summary>
        /// <param name="cards">
        /// The cards.
        /// </param>
        public static void SetUpCards(List<Card> cards)
        {

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
            switch (card)
            {
                case CardName.Copper:
                    return new Copper(card, cardsMade[card]);
                case CardName.Silver:
                    return new Silver(card, cardsMade[card]);
                case CardName.Gold:
                    return new Gold(card, cardsMade[card]);
                case CardName.Curse:

                case CardName.Estate:

                case CardName.Duchy:

                case CardName.Province:

                case CardName.Gardens:

                case CardName.Cellar:

                case CardName.Chapel:

                case CardName.Chancellor:

                case CardName.Village:

                case CardName.Woodcutter:

                case CardName.Workshop:

                case CardName.Feast:

                case CardName.Moneylender:

                case CardName.Remodel:

                case CardName.Smithy:

                case CardName.ThroneRoom:

                case CardName.CouncilRoom:

                case CardName.Festival:

                case CardName.Laboratory:

                case CardName.Library:

                case CardName.Market:

                case CardName.Mine:

                case CardName.Adventurer:

                case CardName.Bureaucrat:

                case CardName.Militia:

                case CardName.Spy:

                case CardName.Thief:

                case CardName.Witch:

                case CardName.Moat:

                default:

            }
        }
    }
}
