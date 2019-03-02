using GlassfullPlugin.UI;
using Kompas6API5;
using Kompas6Constants3D;
using System;

namespace GlassfullPlugin.Libary
{
    /// <summary>
    /// Класс построения детали.
    /// </summary>
    public class DetailBuilder
    {
        /// <summary>
        ///  Указатель на экземпляр компас.
        /// </summary>
        private KompasObject _kompas;

        /// <summary>
        ///  Указатель на интерфейс документа.
        /// </summary>
        private ksDocument3D _doc3D;

        /// <summary>
        ///  Указатель на интерфейс компонента.
        /// </summary>
        private ksPart _part;

        /// <summary>
        ///  Указатель на интерфейс сущности.
        /// </summary>
        private ksEntity _entitySketch;

        /// <summary>
        ///  Указатель на интерфейс параметров эскиза.
        /// </summary>
        private ksSketchDefinition _sketchDefinition;

        /// <summary>
        ///  Указатель на эскиз.
        /// </summary>
        private ksDocument2D _sketchEdit;

        #region Constants

        /// <summary>
        /// Начало координат.
        /// </summary>
        private const int origin = 0;

        /// <summary>
        /// Высота обода граненого стакана. 
        /// </summary>
        private const double throat = -1.1;

        #endregion  

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="kompas">Интерфейс API КОМПАС.</param>
        public DetailBuilder(KompasObject kompas)
        {
            _kompas = kompas;
        }

        /// <summary>
        /// Метод, создающий эскиз.
        /// </summary>
        /// <param name="plane">Плоскость, эскиз которой будет создан.</param>
        private void CreateSketch(short plane)
        {
            var currentPlane = (ksEntity)_part.GetDefaultEntity(plane);

            _entitySketch = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            _sketchDefinition = (ksSketchDefinition)_entitySketch.GetDefinition();
            _sketchDefinition.SetPlane(currentPlane);
            _entitySketch.Create();
        }

        /// <summary>
        /// Построение стакана.
        /// </summary>
        /// <param name="parameters">Параметры стакана.</param>
        /// <param name="checkFaceted">Определяем, граненый ли стакан.</param>
        public void CreateDetail(GlassfulParametrs parameters, bool checkFaceted)
        {
            if (_kompas != null)
            {
                _doc3D = (ksDocument3D)_kompas.Document3D();
                _doc3D.Create(false, true);
            }

            var wallwidth = parameters.WallWidth;
            var highdiameter = parameters.HighDiameter;
            var height = parameters.Height;
            var bottomthickness = parameters.BottomThickness;
            var lowdiameter = parameters.LowDiameter;

            _doc3D = (ksDocument3D)_kompas.ActiveDocument3D();
            _part = (ksPart)_doc3D.GetPart((short)Part_Type.pTop_Part);

            if (checkFaceted) // Определяем реализацию, если мы нажали на чекбокс, то идем по вектке граненого стакана
            {
                BuildFaceted(wallwidth, highdiameter, height, bottomthickness, lowdiameter);
            }
            else // Иначе строим обычный стакан
            {
                GlassfulSketch(wallwidth, highdiameter, height, bottomthickness, lowdiameter);
            }
        }

        /// <summary>
        /// Метод для выдавливания вращением осовного эскиза.
        /// </summary>
        /// <return> Возвращает выдавленный эскиз.</return>
        private ksEntity RotateSketch()
        {
            var entityRotated =
                (ksEntity)_part.NewEntity((short)Obj3dType.o3d_baseRotated);
            var entityRotatedDefinition =
                (ksBaseRotatedDefinition)entityRotated.GetDefinition();

            entityRotatedDefinition.directionType = 0;
            entityRotatedDefinition.SetSideParam(true, 360);
            entityRotatedDefinition.SetSketch(_entitySketch);
            entityRotated.Create();

            return entityRotated;
        }

        /// <summary>
        /// Выдавливание граненого стакана.
        /// </summary>
        /// <param name="width">Высота.</param>
        /// <param name="part">Указатель на интерфейс компонента.</param>
        /// <param name="entitySketch">Указатель на интерфейс сущности.</param>
        /// <param name="toForward">Направление выдавливания.</param>
        /// <param name="degrees">Угол наклона при выдавливании.</param>
        /// <returns>Выдавленный эскиз.</returns>
        private ksEntity MakeExtrude(float width, ksPart part, ksEntity entitySketch,
            double degrees, bool toForward = true)
        {
            var entityExtrude = (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
            var entityExtrudeDefinition = (ksBaseExtrusionDefinition)entityExtrude.GetDefinition();
            entityExtrudeDefinition.SetSideParam(toForward, 0, width, degrees, false);
            entityExtrudeDefinition.SetSketch(entitySketch);
            entityExtrude.Create();
            return entityExtrude;
        }

        /// <summary>
        /// Выдавливание внутренней полости стакана.
        /// </summary>
        /// <param name="width">Высота.</param>
        /// <param name="part">Указатель на интерфейс запчасти.</param>
        /// <param name="entitySketch">Указатель на интерфейс сущности.</param>
        /// <param name="toForward">Направление выдавливания.</param>
        /// <param name="degrees">Угол выдавливания.</param>
        /// <returns>Возвращает выдавленный эскиз.</returns>
        public ksEntity ExtrusionEntity(ksPart part, float width, object entityForExtrusion,
            double degrees, bool side = false)
        {
            var entityCutExtrusion = (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            var cutExtrusionDefinition = (ksCutExtrusionDefinition)entityCutExtrusion.GetDefinition();
            cutExtrusionDefinition.cut = true;
            cutExtrusionDefinition.SetSideParam(side, 0, width, degrees, false);
            cutExtrusionDefinition.SetSketch(entityForExtrusion);
            entityCutExtrusion.Create();
            return entityCutExtrusion;
        }

        /// <summary>
        /// Эскиз стакана.
        /// </summary>
        /// <param name="wallWidth">Толщина стенки.</param>
        /// <param name="highDiameter">Диаметр верхней окружности.</param>
        /// <param name="height">Высота.</param>
        /// <param name="bottomThicknes">Толщина дна.</param>
        /// <param name="lowDiameter">Диаметр нижней окружности.</param>
        /// <return>Возвращает выдавленный эскиз.</return>
        private void GlassfulSketch(double wallWidth, double highDiameter, double height,
            double bottomThicknes, double lowDiameter)
        {
            CreateSketch((short)Obj3dType.o3d_planeXOY);
            _sketchEdit = (ksDocument2D)_sketchDefinition.BeginEdit();
            _sketchEdit.ksLineSeg
                (origin, origin, origin + lowDiameter / 2, origin, 1);
            _sketchEdit.ksLineSeg
                (origin + lowDiameter / 2, origin, highDiameter / 2, height, 1);
            _sketchEdit.ksLineSeg
                (highDiameter / 2, height, highDiameter / 2 - wallWidth, height, 1);
            _sketchEdit.ksLineSeg
                (highDiameter / 2 - wallWidth, height, lowDiameter / 2 - wallWidth, bottomThicknes, 1);
            _sketchEdit.ksLineSeg
                (lowDiameter / 2 - wallWidth, bottomThicknes, origin, bottomThicknes, 1);
            _sketchEdit.ksLineSeg
                (origin, bottomThicknes, origin, origin, 1);
            _sketchEdit.ksLineSeg
              (origin, origin, origin, height * 2, 3);
            _sketchDefinition.EndEdit();
            RotateSketch();
        }

        /// <summary>
        /// Метод, рисующий равносторонний многоугольник с заданным расстоянием от центра.
        /// </summary>
        /// <param name="radius">Радиус круга.</param>
        /// <param name="count">Количество углов.</param>
        private void DrawCircle(double radius, int count)
        {
            _sketchEdit = (ksDocument2D)_sketchDefinition.BeginEdit();

            var x1 = radius;
            var y1 = 0.0;
            var x2 = 0.0;
            var y2 = 0.0;
            for (int i = 1; i <= count; i++)
            {
                var koef = 360.0 / (double)count * (double)i;

                x2 = Math.Cos((koef / 180.0) * Math.PI) * radius;
                y2 = Math.Sin((koef / 180.0) * Math.PI) * radius;
                _sketchEdit.ksLineSeg
                (x1, y1, x2, y2, 1);
                x1 = x2;
                y1 = y2;
            }
            _sketchDefinition.EndEdit();
        }

        /// <summary>
        /// Метод построения граненого стакана.
        /// </summary>
        /// <param name="wallWidth">Толщина стенки.</param>
        /// <param name="highDiameter">Верхний диаметр.</param>
        /// <param name="height">Высота стакана.</param>
        /// <param name="bottomThickness">Толщина дна.</param>
        /// <param name="lowDiamter">Нижний диаметр.</param>
        private void BuildFaceted(double wallWidth, double highDiameter, double height,
            double bottomThickness, double lowDiamter)
        {
            CreateSketch((short)Obj3dType.o3d_planeXOY);
            var fheight = (float)height;
            var degrees = 15.0;
            // Определяем угол, с отклонением на который нужно произвести выдавливание.
            degrees = Math.Atan(((highDiameter - lowDiamter) / 2) / height) * 180 / Math.PI;

            DrawCircle(lowDiamter / 2, 16);
            MakeExtrude(fheight, _part, _entitySketch, degrees, true);
            MakeSketch(_part, bottomThickness);
            DrawCircle((lowDiamter - wallWidth) / 2, 360);
            ExtrusionEntity(_part, fheight, _entitySketch, degrees, false);
            MakeSketch(_part, 0.0f, Obj3dType.o3d_planeXOZ);
            DrawEdge(highDiameter / 2, wallWidth, height);
        }

        /// <summary>
        /// Создание эскиза для вычитания полости.
        /// </summary>
        /// <param name="part">Указатель на деталь.</param>
        /// <param name="offset">Отстройка плоскости по высоте, равная толщине дна стакана.</param>
        /// <param name="plane">Плоскость.</param>
        public void MakeSketch(ksPart part, double offset, Obj3dType plane = Obj3dType.o3d_planeXOY)
        {
            var entityPlane = (ksEntity)part.GetDefaultEntity((short)plane);
            var entityOffsetPlane = (ksEntity)part.NewEntity((short)Obj3dType.o3d_planeOffset);
            var planeOffsetDefinition = (ksPlaneOffsetDefinition)entityOffsetPlane.GetDefinition();
            planeOffsetDefinition.direction = true;
            planeOffsetDefinition.offset = offset;
            planeOffsetDefinition.SetPlane(entityPlane);
            entityOffsetPlane.Create();

            _entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            _sketchDefinition = (ksSketchDefinition)_entitySketch.GetDefinition();

            _sketchDefinition.SetPlane(planeOffsetDefinition);
            _entitySketch.Create();

        }

        /// <summary>
        /// Метод, рисующий эскиз верхней окружности стакана.
        /// </summary>
        /// <param name="radius">Радиус верхней окружности.</param>
        /// <param name="wallwidth">Толщина стенки.</param>
        /// <param name="height">Высота.</param>
        private void DrawEdge(double radius, double wallwidth, double height)
        {
            _sketchEdit = (ksDocument2D)_sketchDefinition.BeginEdit();
            _sketchEdit.ksLineSeg
                (radius, -height, radius - wallwidth, -height, 1);
            _sketchEdit.ksLineSeg
            (radius - wallwidth, -height, radius - wallwidth, throat * height, 1);
            _sketchEdit.ksLineSeg
            (radius - wallwidth, throat * height, radius, throat * height, 1);
            _sketchEdit.ksLineSeg
            (radius, throat * height, radius, -height, 1);
            _sketchEdit.ksLineSeg
                (origin, origin, origin, -50 * 2, 3);
            _sketchDefinition.EndEdit();
            RotateSketch();
        }
    }
}
