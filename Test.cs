using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chua_MExer05
{
    internal class Test
    {

        [TestCase("((1 <= 2) && (3 == 4))", new[] { "1", "2", "<=", "3", "4", "==", "&&" })]
        [TestCase("(3<1) && (4==4)", new[] { "3", "1","<", "4", "4", "==", "&&"})]

        public void ToPostFix_GivenInfix_ComparePostfixResult(string expression, string[] expectedPostfixArray)
        {
            //arrange
            //act
            var postfix = BooleanInfixToPostfixConverter.ToPostfix(expression);

            //assert
            var expectedPostFix = new Queue<string>();
            foreach (string s in expectedPostfixArray)
            {
                expectedPostFix.Enqueue(s);
            }
            Assert.That(postfix, Is.EqualTo(expectedPostFix));

        }


        [TestCase(new[]{ "1", "2", "<=", "4", "4", "==", "&&" }, true)]
        [TestCase(new[]{ "1", "2", "<", "3", "4", ">=", "!", "&&", "5", "5","!=","||" }, true)]
        [TestCase(new[] { "3", "1", "<", "4", "4", "==", "&&" }, false)]

        public void Evaluate_GivenPostfix_ComparePostfixResult(string[] expression, bool expectedResult)
        {


            var postfixQueue = new Queue<string>();
            foreach (string s in expression) postfixQueue.Enqueue(s);

            bool result = BooleanPostfixEvaluator.Evaluate(postfixQueue);

            Assert.That(result, Is.EqualTo(expectedResult));


        }


    }
}
