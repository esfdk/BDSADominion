// <copyright file="PlayerTest.cs" company="Dominion Dominators">Copyright © Dominion Dominators 2011</copyright>

using System;
using BDSADominion.Gamestate;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BDSADominion.Gamestate
{
    [TestClass]
    [PexClass(typeof(Player))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PlayerTest
    {
        [PexMethod]
        public Player Constructor(uint playerNumber)
        {
            Player target = new Player(playerNumber);
            return target;
            // TODO: add assertions to method PlayerTest.Constructor(UInt32)
        }
    }
}
