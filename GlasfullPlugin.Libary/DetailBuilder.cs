
using GlassfullPlugin.UI;
using Kompas6API5;
using Kompas6Constants3D;
using System;

namespace GlassfullPlugin.Libary
{
    /// <summary>
    /// Класс построения детали
    /// </summary>
    public class DetailBuilder
    {
        /// <summary>
        /// Добавить XML-ки на поля
        /// </summary>
        private KompasObject _kompas;

        private ksDocument3D _doc3D;

        private ksPart _part;

        private ksEntity _entitySketch;

        private ksSketchDefinition _sketchDefinition;

        private ksDocument2D _sketchEdit;

        //Константы

        const int origin = 0; //Начало координат


        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="kompas">Интерфейс API КОМПАС</param>
        public DetailBuilder(KompasObject kompas)
        {
            _kompas = kompas;
        }

        /// <summary>
        /// Метод, создающий эскиз
        /// </summary>
        /// <param name="plane">Плоскость, эскиз которой будет создан</param>
        private void CreateSketch(short plane)
        {
            var currentPlane = (ksEntity)_part.GetDefaultEntity(plane);

            _entitySketch = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            _sketchDefinition = (ksSketchDefinition)_entitySketch.GetDefinition();
            _sketchDefinition.SetPlane(currentPlane);
            _entitySketch.Create();
        }


        /// <summary>
        /// Построение стакана
        /// </summary>
        /// <param name="parameters">Параметры стакана</param>
        public void CreateDetail(GlasfullParametrs parameters, bool checkFaceted)
        {
            if (_kompas != null)
            {
                _doc3D = (ksDocument3D)_kompas.Document3D();
                _doc3D.Create(false, true);
            }

            var wallwidth = parameters.WallWidth * 10;
            var highdiameter = parameters.HighDiameter * 10;
            var height = parameters.Height * 10;
            var bottomthickness = parameters.BottomThickness * 10;
            var lowdiameter = parameters.LowDiameter * 10;


            _doc3D = (ksDocument3D)_kompas.ActiveDocument3D();
            _part = (ksPart)_doc3D.GetPart((short)Part_Type.pTop_Part);
            if (checkFaceted)
            {
                TestSketch(wallwidth, highdiameter, height, bottomthickness, lowdiameter);
            }
            else
            {
                GlasfullSketch(wallwidth, highdiameter, height, bottomthickness, lowdiameter);
            }


        }

        /// <summary>
        /// Метод для выдавливания вращением осовного эскиза
        /// </summary>
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
        /// Выдавливание граненого стакана
        /// </summary>
        /// <param name="width"></param>
        /// <param name="part"></param>
        /// <param name="entitySketch"></param>
        /// <param name="toForward"></param>
        /// <returns></returns>
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
        /// Вычитание 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="part"></param>
        /// <param name="entitySketch"></param>
        /// <param name="toForward"></param>
        /// <returns>
        /// Указать возвращаемые параметры
        /// </returns>

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
        /// Эскиз стакана
        /// </summary>
        /// <param name="wallWidth">Толщина стенки</param>
        /// <param name="highDiameter">Диаметр верхней окружности</param>
        /// <param name="height">Высота</param>
        /// <param name="bottomThicknes">Толщина дна</param>
        /// <param name="lowDiameter">Диаметр нижней окружности</param>
        private void GlasfullSketch(double wallWidth, double highDiameter, double height,
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
        ///Добавить XML-ку 
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="count"></param>
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


        private void TestSketch(double wallWidth, double highDiameter, double height,
            double bottomThickness, double lowDiamter)
        {
            CreateSketch((short)Obj3dType.o3d_planeXOY);
            var fheight = (float)height;
            var degrees = 15.0;
            degrees = Math.Atan(((highDiameter - lowDiamter)/2) / height) * 180 / Math.PI;

            DrawCircle(lowDiamter / 2, 16);
            MakeExtrude(fheight, _part, _entitySketch, degrees, true);
            MakeSketch(_part, bottomThickness);
            DrawCircle((lowDiamter - wallWidth) / 2, 360);
            ExtrusionEntity(_part, fheight, _entitySketch, degrees, false);
            EdgeSketch(_part, 0.0f);
            DrawEdge(highDiameter / 2, wallWidth, height);
        }

        public void MakeSketch(ksPart part, double offset)
        {
            var entityPlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
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

        public void EdgeSketch(ksPart part, float offset)
        {
            var entityPlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
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

        private void DrawEdge(double radius, double wallwidth, double height)
        {
            _sketchEdit = (ksDocument2D)_sketchDefinition.BeginEdit();
            _sketchEdit.ksLineSeg
                (radius, -height, radius - wallwidth, -height, 1);
            _sketchEdit.ksLineSeg
            (radius-wallwidth, -height, radius - wallwidth, -1.1*height, 1);
            _sketchEdit.ksLineSeg
            (radius - wallwidth, -1.1*height, radius, -1.1*height, 1);
            _sketchEdit.ksLineSeg
            (radius, -1.1*height, radius, -height, 1);
            _sketchEdit.ksLineSeg
                (origin, origin, origin, -50 * 2, 3);
            _sketchDefinition.EndEdit();
            RotateSketch();
        }
            
    }

}
