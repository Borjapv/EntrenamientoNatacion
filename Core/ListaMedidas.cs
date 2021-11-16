using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace EntrenamientoNatacion.Core
{
    public class ListaMedidas : ObservableCollection<Dictionary<DateTime, Medidas>>
    {
        private Dictionary<DateTime, Medidas> Lista { get; set; }

        public ListaMedidas()
        {
            this.Lista = new Dictionary<DateTime, Medidas>();
        }

        public Dictionary<DateTime, Medidas> GetLista()
        {
            return this.Lista.ToDictionary(entry => entry.Key,
                                           entry => entry.Value);
        }
        
        public void Add(DateTime fecha, Medidas datos)
        {
            if (!Lista.TryAdd(fecha, datos))
            {
                Lista[fecha] = datos;
            }
        }

        public void GuardarDatos()
        {
            foreach (var pair in Lista)
            {
                var element = pair.Value.Guardar();
                var fecha = pair.Key.ToString("dd-MM-yyyy");
                element.Add(new XElement("fecha", fecha));
                element.Save("../../../XML/" + "medidas_" + pair.Key.ToString("dd-MM-yyyy") + ".xml");
            }
        }
        
        public static ListaMedidas CargarDatos()
        {
            var toret = new ListaMedidas();
            string[] files = Directory.GetFiles("../../../XML");
            XElement node;
            foreach (var dir in files)
            {
                node = XElement.Load(dir);
                DateTime fecha = DateTime.ParseExact((string) node.Element("fecha")!, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                Medidas med = Medidas.Cargar(node);
                toret.Add(fecha, med);
            }
            return toret;
        }
    }
}