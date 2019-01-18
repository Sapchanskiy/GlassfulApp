using System;
using System.Collections.Generic;
using System.Text;

namespace GlassfullPlugin.UI
{
    /// <summary>
    /// Класс, содержащий параметры стакана
    /// </summary>
    public class GlasfullParametrs
    {
        /// <summary>
        /// Толщина стенки стакана
        /// </summary>
        public double WallWidth { get; private set; }

        /// <summary>
        /// Диаметр верхней окружности стакана
        /// </summary>
        public double HighDiameter { get; private set; }

        /// <summary>
        /// Высота стакана
        /// </summary>
        public double Height { get; private set; }

        /// <summary>
        /// Толщина дна стакана
        /// </summary>
        public double BottomThickness { get; private set; }

        /// <summary>
        /// Диаметр нижней окружности стакана
        /// </summary>
        public double LowDiameter { get; private set; }


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="wallWidth">Толщина стенки стакана</param>
        /// <param name="highDiameter">Диаметр верхней окружности стакана</param>
        /// <param name="height">Высота стакана</param>
        /// <param name="bottomThickness">Толщина дна стакана</param>
        /// <param name="lowDiameter">Диаметр дна стакана</param>
        public GlasfullParametrs(double wallWidth, 
            double highDiameter, 
            double height, 
             double bottomThickness, double lowDiameter)
        {
            WallWidth = wallWidth;
            HighDiameter = highDiameter;
            Height = height;
            BottomThickness = bottomThickness;
            LowDiameter = lowDiameter;

            ValueValidation();
            TypeValidation();
        }

        /// <summary>
        /// Валидация параметров по диапазону значения
        /// </summary>
        private void ValueValidation()
        {
            var errorMessage = new List<String>();

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
        /// Валидация параметров по типу данных
        /// </summary>
        private void TypeValidation()
        {
            var errorMessage = new List<String>();

            if (double.IsNaN(WallWidth))
            {
                errorMessage.Add("Толщина стенки стакана должна быть числом\n"); 
            }
            if (double.IsNaN(HighDiameter))
            {
                errorMessage.Add("Диаметр верхней окружности стакана должен быть числом\n");
            }
            if (double.IsNaN(Height))
            {
                errorMessage.Add("Высота стакана должна быть числом\n");
            }
            if (double.IsNaN(BottomThickness))
            {
                errorMessage.Add("Толщина стакана должна быть числом");
            }
            if (double.IsNaN(LowDiameter))
            {
                errorMessage.Add("Диаметр нижней окружности стакана должен быть числом");
            }

            if (errorMessage.Count > 0)
            {
                throw new ArgumentException(string.Join("\n", errorMessage));
            }
        }

    }
}
