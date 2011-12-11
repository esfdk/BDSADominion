namespace BDSADominion.Gamestate
{
    using System.Collections.Generic;

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
        /// All the cards belonging to the player.
        /// </summary>
        private List<Card> allCards = new List<Card>();

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
        /// Used to get the list that holds all the cards belonging to the player.
        /// </summary>
        /// <returns>
        /// Returns the list of all the cards belonging to the player.
        /// </returns>
        public List<Card> GetAllCards()
        {
            return allCards;
        }
    }
}