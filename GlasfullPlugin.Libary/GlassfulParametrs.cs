using System;
using System.Collections.Generic;
using System.Text;

namespace GlassfullPlugin.UI
{
    /// <summary>
    /// Класс, содержащий параметры стакана.
    /// </summary>
    public class GlassfulParametrs
    {
        /// <summary>
        /// Толщина стенки стакана.
        /// </summary>
        public double WallWidth { get; private set; }

        /// <summary>
        /// Диаметр верхней окружности стакана.
        /// </summary>
        public double HighDiameter { get; private set; }

        /// <summary>
        /// Высота стакана.
        /// </summary>
        public double Height { get; private set; }

        /// <summary>
        /// Толщина дна стакана.
        /// </summary>
        public double BottomThickness { get; private set; }

        /// <summary>
        /// Диаметр нижней окружности стакана.
        /// </summary>
        public double LowDiameter { get; private set; }

        /// <summary>
        /// Константа для перевода в сантиметры
        /// </summary>
        private const double _tomm = 10.0;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="wallWidth">Толщина стенки стакана.</param>
        /// <param name="highDiameter">Диаметр верхней окружности стакана.</param>
        /// <param name="height">Высота стакана.</param>
        /// <param name="bottomThickness">Толщина дна стакана.</param>
        /// <param name="lowDiameter">Диаметр дна стакана.</param>
        public GlassfulParametrs(double wallWidth, 
            double highDiameter, 
            double height, 
             double bottomThickness, double lowDiameter)
        {
            WallWidth = wallWidth * _tomm;
            HighDiameter = highDiameter * _tomm;
            Height = height * _tomm;
            BottomThickness = bottomThickness * _tomm;
            LowDiameter = lowDiameter * _tomm;

            ValueValidation();
            TypeValidation();
        }

        /// <summary>
        /// Валидация параметров по диапазону значения.
        /// </summary>
        private void ValueValidation()
        {
            var errorMessage = new List<string>();

            if (WallWidth > (LowDiameter / 2 ))               
            {
                errorMessage.Add("Толщина стенки стакана " +
                                 "не должна быть менее половины диаметра нижней окружности стакана ");
            }

            if (HighDiameter < (LowDiameter))
            {
                errorMessage.Add("Диаметр внешней окружности стакана " +
                                 "должен быть не меньше нижнего диаметра окружности стакана");
            }

            if (Height < (BottomThickness * 2))
            {
                errorMessage.Add("Высота стакана должна быть " +
                                 "не менее двух размеров толщины дна стакана  ");
            }

            if (LowDiameter > HighDiameter)
            {
                errorMessage.Add("Диаметр нижней окружности стакана"
                + "Не может быть больше диаметра верхней окружности стакана");
            }

            if (errorMessage.Count > 0)
            {
                throw new ArgumentException(string.Join("\n", errorMessage));
            }
        }

        /// <summary>
        /// Проверка значения на NaN и Infinity.
        /// </summary>
        /// <param name="errorList">Список ошибок.</param>
        /// <param name="value">Значение.</param>
        /// <param name="name">Название параметра.</param>
        private void CheckValue(List<string> errorList, double value, string name)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                errorList.Add($"{name} не должно быть бесконечным или несуществующим.\n");
            }
        }

        /// <summary>
        /// Валидация параметров по типу данных.
        /// </summary>
        private void TypeValidation()
        {
            var errorMessage = new List<string>();

            CheckValue(errorMessage, WallWidth, "Толщина стенки");
            CheckValue(errorMessage, HighDiameter, "Диаметр верхней окружности стакана");
            CheckValue(errorMessage, Height, "Высота стакана");
            CheckValue(errorMessage, BottomThickness, "Толщина дна стакана");
            CheckValue(errorMessage, LowDiameter, "Диаметр нижней окружности");

            if (errorMessage.Count > 0)
            {
                throw new ArgumentException(string.Join("\n", errorMessage));
            }
        }

    }
}
