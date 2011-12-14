// <copyright file="CardTest.cs" company="Dominion Dominators">Copyright © Dominion Dominators 2011</copyright>

using System;
using BDSADominion.Gamestate.Card_Types;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDSADominion.Gamestate;

namespace BDSADominion.Gamestate.Card_Types
{
    [TestClass]
    [PexClass(typeof(Card))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class CardTest
    {
        [PexMethod]
        public bool op_Inequality(Card left, Card right)
        {
            bool result = left != right;
            return result;
            // TODO: add assertions to method CardTest.op_Inequality(Card, Card)
        }
        [PexMethod]
        public bool op_Equality(Card left, Card right)
        {
            bool result = left == right;
            return result;
            // TODO: add assertions to method CardTest.op_Equality(Card, Card)
        }
        [PexMethod]
        public void Initialize(
            [PexAssumeNotNull]Card target,
            CardName name,
            uint number
        )
        {
            target.Initialize(name, number);
            // TODO: add assertions to method CardTest.Initialize(Card, CardName, UInt32)
        }
        [PexMethod]
        public int GetHashCode01([PexAssumeNotNull]Card target)
        {
            int result = target.GetHashCode();
            return result;
            // TODO: add assertions to method CardTest.GetHashCode01(Card)
        }
        [PexMethod]
        public bool Equals02([PexAssumeNotNull]Card target, Card other)
        {
            bool result = target.Equals(other);
            return result;
            // TODO: add assertions to method CardTest.Equals02(Card, Card)
        }
        [PexMethod]
        public bool Equals01([PexAssumeNotNull]Card target, object obj)
        {
            bool result = target.Equals(obj);
            return result;
            // TODO: add assertions to method CardTest.Equals01(Card, Object)
        }
    }
}
