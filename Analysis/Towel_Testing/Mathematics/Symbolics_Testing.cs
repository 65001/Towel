﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.Mathematics;

namespace Towel_Testing.Mathematics
{
    [TestClass]
    public class Symbolics_Testing
    {
        [TestMethod]
        public void Parse_String_Testing()
        {
            Symbolics.Constant<int> ONE = new Symbolics.Constant<int>(1);

            #region Basic Negate Tests

            {
                var A = Symbolics.Parse<int>("-1", int.TryParse);
                var B = -ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("- 1", int.TryParse);
                var B = - ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }

            #endregion

            #region Basic Add Tests

            {
                var A = Symbolics.Parse<int>("1+1", int.TryParse);
                var B = ONE + ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 + 1", int.TryParse);
                var B = ONE + ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 + (1 + 1)", int.TryParse);
                var B = ONE + (ONE + ONE);
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 + (1 + (1 + 1))", int.TryParse);
                var B = ONE + (ONE + (ONE + ONE));
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }

            #endregion

            #region Basic Subtract Tests

            {
                var A = Symbolics.Parse<int>("1-1", int.TryParse);
                var B = ONE - ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 - 1", int.TryParse);
                var B = ONE - ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 - (1 - 1)", int.TryParse);
                var B = ONE - (ONE - ONE);
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 - (1 - (1 - 1))", int.TryParse);
                var B = ONE - (ONE - (ONE - ONE));
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }

            #endregion

            #region Basic Multiply Tests

            {
                var A = Symbolics.Parse<int>("1*1", int.TryParse);
                var B = ONE * ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 * 1", int.TryParse);
                var B = ONE * ONE;
                Assert.IsTrue(A.Equals(B));
            }
            {
                var A = Symbolics.Parse<int>("1 * (1 * 1)", int.TryParse);
                var B = ONE * (ONE * ONE);
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 * (1 * (1 * 1))", int.TryParse);
                var B = ONE * (ONE * (ONE * ONE));
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }

            #endregion

            #region Basic Divide Tests

            {
                var A = Symbolics.Parse<int>("1/1", int.TryParse);
                var B = ONE / ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 / 1", int.TryParse);
                var B = ONE / ONE;
                Assert.IsTrue(A.Equals(B));
            }
            {
                var A = Symbolics.Parse<int>("1 / (1 / 1)", int.TryParse);
                var B = ONE / (ONE / ONE);
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 / (1 / (1 / 1))", int.TryParse);
                var B = ONE / (ONE / (ONE / ONE));
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }

            #endregion

            #region Basic Factorial Tests

            {
                var A = Symbolics.Parse<int>("1!", int.TryParse);
                var B = new Symbolics.Factorial(ONE);
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 !", int.TryParse);
                var B = new Symbolics.Factorial(ONE);
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }

            #endregion

            #region Order Of Operations (2 operands)

            {
                var A = Symbolics.Parse<int>("1 + -1", int.TryParse);
                var B = ONE + -ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("-1 + 1", int.TryParse);
                var B = -ONE + ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 + 1 - 1", int.TryParse);
                var B = ONE + ONE - ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 - 1 + 1", int.TryParse);
                var B = ONE - ONE + ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 + 1 * 1", int.TryParse);
                var B = ONE + ONE * ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 * 1 + 1", int.TryParse);
                var B = ONE * ONE + ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 - 1 * 1", int.TryParse);
                var B = ONE - ONE * ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 * 1 - 1", int.TryParse);
                var B = ONE * ONE - ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 * 1 / 1", int.TryParse);
                var B = ONE * ONE / ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 / 1 * 1", int.TryParse);
                var B = ONE / ONE * ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 + 1!", int.TryParse);
                var B = ONE + new Symbolics.Factorial(ONE);
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1! + 1", int.TryParse);
                var B = new Symbolics.Factorial(ONE) + ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 - 1!", int.TryParse);
                var B = ONE - new Symbolics.Factorial(ONE);
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1! - 1", int.TryParse);
                var B = new Symbolics.Factorial(ONE) - ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));

            }
            {
                var A = Symbolics.Parse<int>("1 * 1!", int.TryParse);
                var B = ONE * new Symbolics.Factorial(ONE);
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1! * 1", int.TryParse);
                var B = new Symbolics.Factorial(ONE) * ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1 / 1!", int.TryParse);
                var B = ONE / new Symbolics.Factorial(ONE);
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }
            {
                var A = Symbolics.Parse<int>("1! / 1", int.TryParse);
                var B = new Symbolics.Factorial(ONE) / ONE;
                Assert.IsTrue(A.Equals(B));
                Assert.IsTrue(A.ToString().Equals(B.ToString()));
            }

            #endregion
            
            //Assert.Inconclusive("Test Method Not Fully Implemented");
        }
    }
}
