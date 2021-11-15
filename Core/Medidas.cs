using System.Xml.Linq;

namespace EntrenamientoNatacion.Core
{
    public class Medidas
    {
        private int Peso { get; }
        private int CircAbd { get; }
        private string Notas { get; }

        public Medidas(int peso, int circAbd, string notas)
        {
            this.Peso = peso;
            this.CircAbd = circAbd;
            this.Notas = notas;
        }

        public XElement Guardar()
        {
            var element = new XElement("medidas",
                new XElement("peso", this.Peso),
                new XElement("circAbd", this.CircAbd),
                new XElement("notas", this.Notas));
            return element;
        }

        public static Medidas Cargar(XElement node)
        {
            var toret = new Medidas(
                (int) node.Element("peso")!,
                (int) node.Element("circAbd")!,
                (string) node.Element("notas")! );

            return toret;
        }
    }
}