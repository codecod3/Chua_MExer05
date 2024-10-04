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
        [TestCase("5>3", new[] { "5","3",">"})]
        [TestCase("!(5 == 5) && (5<=10)", new[] { "5", "5","==", "!", "5", "10", "<=", "&&"})]
        [TestCase("(5!=5) || (5 < 10)", new[] { "5", "5","!=", "5", "10", "<", "||"})]
        [TestCase("!(5!=5) && !(5 < 10)", new[] { "5", "5","!=", "!","5","10","<","!","&&"})]
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



        [TestCase("((1.5 <= 2.1) && (3 == 4))", new[] { "1.5", "2.1", "<=", "3", "4", "==", "&&" })]
        [TestCase("(3.03<1) && (4==4)", new[] { "3.03", "1", "<", "4", "4", "==", "&&" })]
        [TestCase("5>3.5", new[] { "5", "3.5", ">" })]
        [TestCase("!(5 == 5) && (5<=10.1)", new[] { "5", "5", "==", "!", "5", "10.1", "<=", "&&" })]
        [TestCase("(5!=5.5) || (5 < 10)", new[] { "5", "5.5", "!=", "5", "10", "<", "||" })]
        [TestCase("!(5!=5) && !(5 < 10.01)", new[] { "5", "5", "!=", "!", "5", "10.01", "<", "!", "&&" })]
        public void ToPostFix_GivenInfixWithDecimalNumbers_ComparePostfixResult(string expression, string[] expectedPostfixArray)
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
        [TestCase(new[] { "5", "5", "!=", "!", "5", "10.01", "<", "!", "&&" }, false)]
        [TestCase(new[] { "5", "5.5", "!=", "5", "10", "<", "||" }, true)]
        [TestCase(new[] { "5", "5", "==", "!", "5", "10", "<=", "&&" }, false)]
        [TestCase(new[] { "5", "3.5", ">" }, true)]

        public void Evaluate_GivenPostfix_ComparePostfixResult(string[] expression, bool expectedResult)
        {


            var postfixQueue = new Queue<string>();
            foreach (string s in expression) postfixQueue.Enqueue(s);

            bool result = BooleanPostfixEvaluator.Evaluate(postfixQueue);

            Assert.That(result, Is.EqualTo(expectedResult));


        }


    }
}
