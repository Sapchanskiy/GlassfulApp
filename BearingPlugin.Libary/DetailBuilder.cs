
using GlassfullPlugin.UI;
using Kompas6API5;
using Kompas6Constants3D;

namespace GlassfullPlugin.Libary
{
    /// <summary>
    /// Класс построения детали
    /// </summary>
    public class DetailBuilder
    {
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
        /// Метод выдавливания эскиза
        /// </summary> 
        /// <returns>Ссылка на результат выдавливания 
        private ksEntity MakeExtrude(double depth)
        {
            var entityExtrude = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
            var entityExtrudeDefinition = (ksBaseExtrusionDefinition)entityExtrude.GetDefinition();
            entityExtrudeDefinition.SetSideParam(true, 0, depth, 0, true);
            entityExtrudeDefinition.SetSketch(_entitySketch);
            entityExtrude.Create();
            return entityExtrude;
        }

        /// <summary>
        /// Эскиз стакана
        /// </summary>
        /// <param name="wallWidth"></param>
        /// <param name="highDiameter"></param>
        /// <param name="height"></param>
        /// <param name="bottomThicknes"></param>
        /// <param name="lowDiameter"></param>
        private void GlasfullSketch(double wallWidth, double highDiameter, double height, double bottomThicknes, double lowDiameter)
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
                (highDiameter / 2 - wallWidth, height,lowDiameter / 2 - wallWidth ,bottomThicknes , 1);
            _sketchEdit.ksLineSeg
                ( lowDiameter / 2 - wallWidth, bottomThicknes, origin, bottomThicknes  ,1);
            _sketchEdit.ksLineSeg
                ( origin, bottomThicknes,origin,origin, 1);
              _sketchEdit.ksLineSeg
                (origin, origin, origin, height * 2, 3);
            _sketchDefinition.EndEdit();
            RotateSketch();
        }


        /// <summary>
        /// Построение детали
        /// </summary>
        /// <param name="parameters">Параметры подшипника</param>
        public void CreateDetail(GlasfullParametrs parameters)
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

            GlasfullSketch(wallwidth, highdiameter, height, bottomthickness, lowdiameter);



        }

    }

}
