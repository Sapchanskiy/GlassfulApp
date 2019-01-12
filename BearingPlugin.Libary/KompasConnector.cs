using System;
using Kompas6API5;

namespace GlassfullPlugin.Libary
{
    /// <summary>
    /// Класс для подключения к Компас 3D
    /// </summary>
    public class KompasConnector
    {

        /// <summary>
        /// Интерфейс API КОМПАС 3D
        /// </summary>
        public KompasObject Kompas { get; set; }
        

        /// <summary>
        /// Запуск Компас 3D
        /// </summary>
        public void OpenKompas()
        {
            if (Kompas == null)
            {
                var type = Type.GetTypeFromProgID("KOMPAS.Application.5");
                Kompas = (KompasObject)Activator.CreateInstance(type);
            }

            if (Kompas != null)
            {
                Kompas.Visible = true;
                Kompas.ActivateControllerAPI();
            }
        }

        /// <summary>
        /// Закрыть Компас 3D
        /// </summary>
        public void CloseKompas()
        {
            try
            {
                Kompas.Quit();
                Kompas = null;
            }
            catch
            {
                Kompas = null;
            }

        }
    }
}

