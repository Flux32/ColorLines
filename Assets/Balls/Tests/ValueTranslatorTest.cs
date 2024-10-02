using Balls.Source.View.UI.Elements;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace Balls.Tests
{
    public class ValueTranslatorTests
    {
        [Test]
        public void Translate_OnValue1000_Return1K()
        {
            //Arrange
            const float value = 1000;
        
            //Act
            string actual = ValueTranslator.Translate(value);
        
            //Assert
            Assert.AreEqual("1K", actual);
        }
    }
}