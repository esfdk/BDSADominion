// <copyright file="CardFactoryTest.cs" company="Dominion Dominators">Copyright © Dominion Dominators 2011</copyright>

using System;
using BDSADominion.Gamestate;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDSADominion.Gamestate.Card_Types;
using System.Collections.Generic;

namespace BDSADominion.Gamestate
{
    [TestClass]
    [PexClass(typeof(CardFactory))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class CardFactoryTest
    {
        [PexMethod, PexAllowedException(typeof(KeyNotFoundException)), PexAllowedException(typeof(NotImplementedException))]
        public Card CreateCard(CardName card)
        {
            Card result = CardFactory.CreateCard(card);
            return result;
            // TODO: add assertions to method CardFactoryTest.CreateCard(CardName)
        }
    }
}
