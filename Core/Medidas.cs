namespace EntrenamientoNatacion.Core
{
    public class Medidas
    {
        private int peso { get; set; }
        private int circAbd { get; set; }
        private string notas { get; set; }

        public Medidas(int peso, int circAbd, string notas)
        {
            this.peso = peso;
            this.circAbd = circAbd;
            this.notas = notas;
        }
        
        
    }
}