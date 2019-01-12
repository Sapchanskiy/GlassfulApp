using System;
using BearingPlugin.UI;
using NUnit.Framework;

namespace BearingPlugin.UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        private BearingParametrs _parameters;

        [SetUp]
        public void Test()
        {
            _parameters = new BearingParametrs(4.5, 2.7, 1.3, 3, true);
        }

        [Test(Description = "Позитивный тест коструктора класса ")]

        public void TestBearingParametrs_CorrectValue()
        {
            var expectedParameters = new BearingParametrs(4.5, 2.7, 1.3, 3, true);
            var actual = _parameters;

            Assert.AreEqual
                (expectedParameters.ExternalRadiusOutRim, actual.ExternalRadiusOutRim,
                "Некорректное значение ExternalDiametrOutRim");
            Assert.AreEqual
                (expectedParameters.ExternalRadiusInRim, actual.ExternalRadiusInRim,
                "Некорректное значение ExternalDiametrInRim");
            Assert.AreEqual
                (expectedParameters.InternalRadiusInRim, actual.InternalRadiusInRim,
                "Некорректное значение InternalDiametrInRim");
            Assert.AreEqual
                (expectedParameters.SupportShuft, actual.SupportShuft,
                "Некорректное значение ExternalDiametrOutRim");
            Assert.AreEqual
                (expectedParameters.WidthBearing, actual.WidthBearing,
                "Некорректное значение ExternalDiametrOutRim");
        }

        [TestCase(double.NaN, 2.1, 3.2, 2.2, "externalRadiusOutRim",
            TestName = "Негативный тест на Nan поля externalRadiusOutRim")]
        [TestCase(4.2, double.NaN, 3.2, 2.2, "externalRadiusInRim",
            TestName = "Негативный тест на Nan поля externalRadiusInRim")]
        [TestCase(4.2, 2.1, double.NaN, 2.2, "internalRadiusInRim",
            TestName = "Негативный тест на Nan поля internalRadiusInRim")]
        [TestCase(4.2, 2.1, 3.2, double.NaN, "widthBearing",
            TestName = "Негативный тест на Nan поля widthBearing")]

        public void TestBearingParamets_NanValue
            (double externalRadiusOutRim, double externalRadiusInRim, double internalRadiusInRim,
            double widthBearing, string attr)
        {
            Assert.Throws<ArgumentException>(
                () => {
                    var parameters = new BearingParametrs
                (externalRadiusOutRim, externalRadiusInRim, internalRadiusInRim, widthBearing, true);
                },
                "Возникнет исключение если в поле " + attr + " значение double.Nan");
        }

        [TestCase(5.1, 2.3, 1.1, 3, true, "externalRadiusOutRim",
            TestName = "Негативный тест поля externalRadiusOutRim если > 5")]
        [TestCase(2, 1.3, 1.1, 3, true, "externalRadiusOutRim",
            TestName = "Негативный тест поля externalRadiusOutRim если < externalRadiusInRim + 1.1")]
        [TestCase(4.5, 2.2, 1.1, 1.2, true, "externalRadiusInRim",
            TestName = "Негативный тест поля externalRadiusInRim если < internalRadiusInRim + 0.2")]
        [TestCase(4.5, 2.3, 1.2, 3, true, "internalRadiusInRim",
            TestName = "Негативный тест поля internalRadiusInRim если > externalRadiusInRim / 2")]
        [TestCase(4.5, 2.3, 0.4, 2, true, "internalRadiusInRim",
            TestName = "Негативный тест поля internalRadiusInRim если =< 0.5")]
        [TestCase(4.5, 2.3, 1.1, 0.9, true, "widthBearing",
            TestName = "Негативный тест поля widthBearing если =< 1")]
        [TestCase(4.5, 2.3, 1.1, 7, true, "widthBearing",
            TestName = "Негативный тест поля widthBearing если > 6")]
      

        public void TestBearingParametrs_ArgumentValue
        (double externalRadiusOutRim, double externalRadiusInRim, double internalRadiusInRim,
            double widthBearing, bool supportShuft, string attr)
        {
            Assert.Throws<ArgumentException>(
                () => {
                    var parameters = new BearingParametrs
                        (externalRadiusOutRim, externalRadiusInRim, internalRadiusInRim, widthBearing, true);
                },
                "Должно возникнуть исключение если значение поля "
                + attr + "выходит за диапозон доп-х значений");
        }
    }
}

