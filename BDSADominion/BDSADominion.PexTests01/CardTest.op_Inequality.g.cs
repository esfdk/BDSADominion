// <copyright file="CardTest.op_Inequality.g.cs" company="Dominion Dominators">Copyright � Dominion Dominators 2011</copyright>
// <auto-generated>
// This file contains automatically generated unit tests.
// Do NOT modify this file manually.
// 
// When Pex is invoked again,
// it might remove or update any previously generated unit tests.
// 
// If the contents of this file becomes outdated, e.g. if it does not
// compile anymore, you may delete this file and invoke Pex again.
// </auto-generated>
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace BDSADominion.Gamestate.Card_Types
{
    public partial class CardTest
    {
[TestMethod]
[PexGeneratedBy(typeof(CardTest))]
public void op_Inequality693()
{
    bool b;
    b = this.op_Inequality((Card)null, (Card)null);
    Assert.AreEqual<bool>(false, b);
}
    }
}