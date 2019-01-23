using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GlassfullPlugin.UI;

namespace GlassFull.UnitTest
{


    [TestFixture]
    public class UnitTests
    {
        private GlasfullParametrs _parameters;

        [SetUp]
        public void Test()
        {

            _parameters = new GlasfullParametrs(1, 10, 10, 1, 8);
        }

        [Test(Description = "Позитивный тест конструктора класса ")]

        public void TestBearingParametrs_CorrectValue()
        {
            var expectedParameters = new GlasfullParametrs(1, 10, 10, 1, 8);
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

        [TestCase(double.NaN, 10, 10, 1, 8, "WallWidth",
            TestName = "Негативный тест на Nan поля WallWidth")]
        [TestCase(1, double.NaN, 10, 1, 8, "HighDiameter",
            TestName = "Негативный тест на Nan поля HighDiameter")]
        [TestCase(1, 10, double.NaN, 1, 8, "Height",
            TestName = "Негативный тест на Nan поля Height")]
        [TestCase(1, 10, 10, double.NaN, 8, "LowDiameter",
            TestName = "Негативный тест на Nan поля LowDiameter")]
        [TestCase(1, 10, 10, 1, double.NaN, "BottomThickness",
            TestName = "Негативный тест на Nan поля BottomThickness")]

        public void TestGlasFullParamets_NanValue
            (double wallWidth, double highDiameter, double height,
            double lowDiameter, double bottomThickness, string attr)
        {
            Assert.Throws<ArgumentException>(
                () => {
                    var parameters = new GlasfullParametrs
                (wallWidth, highDiameter, height, lowDiameter, bottomThickness);
                },
                "Возникнет исключение если в поле " + attr + " значение double.Nan");
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

        public void TestGlasFullParametrs_ArgumentValue
        (double wallWidth, double highDiameter, double height,
            double lowDiameter, double bottomThickness, string attr)
        {
            Assert.Throws<ArgumentException>(
                () => {
                    var parameters = new GlasfullParametrs
                        (wallWidth, highDiameter, height, lowDiameter, bottomThickness);
                },
                "Должно возникнуть исключение если значение поля "
                + attr + "выходит за диапозон доп-х значений");
        }
    }

}
