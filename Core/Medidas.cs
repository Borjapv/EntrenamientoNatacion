using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EntrenamientoNatacion.Core
{
    public class Medidas
    {
        private int Peso { get; set; }
        private int CircAbd { get; set; }
        private string Notas { get; set; }
        private DateTime Fecha { get; set; }
        
        public Medidas(int peso, int circAbd, string notas, DateTime fecha)
        {
            this.Peso = peso;
            this.CircAbd = circAbd;
            this.Notas = notas;
            this.Fecha = fecha;
        }

        public void Guardar(string dir)
        {
            var element = new XElement("medidas",
                new XElement("peso", this.Peso),
                new XElement("circAbd", this.CircAbd),
                new XElement("notas", this.Notas),
                new XElement("fecha", this.Fecha));
            element.Save("../../../"+dir+".xml");
        }

        public static Medidas Cargar(string dir)
        {
            var node = XElement.Load("../../../"+dir+".xml");
            var toret = new Medidas(
                (int) node.Element("peso"),
                (int) node.Element("circAbd"),
                (string) node.Element("notas"),
                (DateTime) node.Element("fecha"));

            return toret;
        }
        
        /*
        public static Medidas LeeXml(string dir)
        {
            int peso = 0;
            int circAbd = 0;
            string notas = "";

            XElement raiz = XElement.Load(dir);
            IEnumerable<XElement> medidas =
                from el in raiz.Elements("medidas")
                select el;
            foreach (XElement el in medidas)
            {
                peso = (int) el.Element("peso");
                circAbd = (int) el.Element("circAbd");
                notas = el.Element("notas").ToString();
            }

            return new Medidas(peso, circAbd, notas);
        }
        */
    }
}