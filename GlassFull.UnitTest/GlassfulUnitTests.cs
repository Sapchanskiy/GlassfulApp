using System;
using NUnit.Framework;
using GlassfullPlugin.UI;

namespace GlassFull.UnitTest
{
    [TestFixture]
    public class UnitTests
    {
        private GlassfulParametrs _parameters;

        [SetUp]
        public void Test()
        {
            _parameters = new GlassfulParametrs(1, 10, 10, 1, 8);
        }

        [Test(Description = "Позитивный тест конструктора класса ")]

        public void TestGlassfulParametrs_CorrectValue()
        {
            var expectedParameters = new GlassfulParametrs(1, 10, 10, 1, 8);
            var actual = _parameters;

            Assert.AreEqual
                (expectedParameters.WallWidth, actual.WallWidth,
                "Некорректное значение WallWidth");
            Assert.AreEqual
                (expectedParameters.HighDiameter, actual.HighDiameter,
                "Некорректное значение HighDiameter");
            Assert.AreEqual
                (expectedParameters.Height, actual.Height,
                "Некорректное значение Height");
            Assert.AreEqual
                (expectedParameters.LowDiameter, actual.LowDiameter,
                "Некорректное значение LowDiameter");
            Assert.AreEqual
                (expectedParameters.BottomThickness, actual.BottomThickness,
                "Некорректное значение BottomThickness");
        }

        [TestCase(double.NaN, 10, 10, 1, 8, "WallWidth", "NaN", 
            TestName = "Негативный тест на Nan поля WallWidth")]
        [TestCase(1, double.NaN, 10, 1, 8, "HighDiameter", "NaN",
            TestName = "Негативный тест на Nan поля HighDiameter")]
        [TestCase(1, 10, double.NaN, 1, 8, "Height", "NaN",
            TestName = "Негативный тест на Nan поля Height")]
        [TestCase(1, 10, 10, double.NaN, 8, "LowDiameter", "NaN",
            TestName = "Негативный тест на Nan поля LowDiameter")]
        [TestCase(1, 10, 10, 1, double.NaN, "BottomThickness", "NaN",
            TestName = "Негативный тест на Nan поля BottomThickness")]

        [TestCase(double.NegativeInfinity, 10, 10, 1, 8, "WallWidth", "NegativeInfinity",
            TestName = "Негативный тест на NegativeInfinity поля WallWidth")]
        [TestCase(1, double.NegativeInfinity, 10, 1, 8, "HighDiameter", "NegativeInfinity",
            TestName = "Негативный тест на NegativeInfinity поля HighDiameter")]
        [TestCase(1, 10, double.NegativeInfinity, 1, 8, "Height", "NegativeInfinity",
            TestName = "Негативный тест на NegativeInfinity поля Height")]
        [TestCase(1, 10, 10, double.NegativeInfinity, 8, "LowDiameter", "NegativeInfinity",
            TestName = "Негативный тест на NegativeInfinity поля LowDiameter")]
        [TestCase(1, 10, 10, 1, double.NegativeInfinity, "BottomThickness", "NegativeInfinity",
            TestName = "Негативный тест на NegativeInfinity поля BottomThickness")]

        [TestCase(double.PositiveInfinity, 10, 10, 1, 8, "WallWidth", "PositiveInfinity",
            TestName = "Негативный тест на PositiveInfinity поля WallWidth")]
        [TestCase(1, double.PositiveInfinity, 10, 1, 8, "HighDiameter", "PositiveInfinity",
            TestName = "Негативный тест на PositiveInfinity поля HighDiameter")]
        [TestCase(1, 10, double.PositiveInfinity, 1, 8, "Height", "PositiveInfinity",
            TestName = "Негативный тест на PositiveInfinity поля Height")]
        [TestCase(1, 10, 10, double.PositiveInfinity, 8, "LowDiameter", "PositiveInfinity",
            TestName = "Негативный тест на PositiveInfinity поля LowDiameter")]
        [TestCase(1, 10, 10, 1, double.PositiveInfinity, "BottomThickness", "PositiveInfinity",
            TestName = "Негативный тест на PositiveInfinity поля BottomThickness")]

        public void TestGlassfulParamets_NanValue
            (double wallWidth, double highDiameter, double height,
            double lowDiameter, double bottomThickness, string attr, string par)
        {
            Assert.Throws<ArgumentException>(
                () => {
                    var parameters = new GlassfulParametrs
                (wallWidth, highDiameter, height, lowDiameter, bottomThickness);
                },
                $"Возникнет исключение если в поле {attr} значение {par}.");
        }

        [TestCase(20, 10, 10, 8, 1, "wallWidth",
            TestName = "Негативный тест поля wallWidth если walLWidth > lowDiameter/2")]
        [TestCase(1, 5, 10, 10, 1, "highDiameter",
            TestName = "Негативный тест поля HighDiameter если highDiameter< LowDiameter")]
        [TestCase(1, 10, 10, 8, 8, "height",
            TestName = "Негативный тест поля height если height/2 < bottomThickness")]
        [TestCase(1, 10, 10, 20, 1, "lowDiameter",
            TestName = "Негативный тест поля lowDiameter если lowDiameter>highDiameter")]
        [TestCase(1, 10, 10, 8, 7, "bottomThickness",
            TestName = "Негативный тест поля internalRadiusInRim если bottomThickness > height/2")]

        public void TestGlassfulParametrs_ArgumentValue
        (double wallWidth, double highDiameter, double height,
            double lowDiameter, double bottomThickness, string attr)
        {
            Assert.Throws<ArgumentException>(
                () => {
                    var parameters = new GlassfulParametrs
                        (wallWidth, highDiameter, height, lowDiameter, bottomThickness);
                },
                "Должно возникнуть исключение если значение поля "
                + attr + "выходит за диапозон доп-х значений");
        }
    }

}
