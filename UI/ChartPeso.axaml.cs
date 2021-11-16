using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EntrenamientoNatacion.Core;

namespace EntrenamientoNatacion.UI
{
    public partial class ChartPeso : Window
    {
        public ChartPeso()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var btAnt = this.FindControl<Button>( "BtAnt" );
            var btSig = this.FindControl<Button>( "BtSig" );
            this.Chart = this.FindControl<Chart>( "PesoGrf" );
            var rbBars = this.FindControl<RadioButton>( "PesoBars" );
            var rbLine = this.FindControl<RadioButton>( "PesoLine" );
            
            btAnt.Click += (_, _) => this.AntMes();
            btSig.Click += (_, _) => this.SigMes();

            rbBars.Checked += (_, _) => this.OnChartFormatChanged();
            rbLine.Checked += (_, _) => this.OnChartFormatChanged();
            
            this.Chart.LegendY = "Peso (kg)";
            Fecha = DateTime.Today;
            this.Chart.LegendX = Fecha.ToString("MMMM yyyy", new CultureInfo("es-ES"));
            
            ListaMedidas lista = ListaMedidas.CargarDatos();
            var dict = lista.GetLista();
            
            //buscar los valores del mes actual
            Valores = 
                from x in dict
                where (x.Key.Month.Equals(Fecha.Month) && x.Key.Year.Equals(Fecha.Year))
                select x.Value.Peso;

            this.Chart.Values = Valores.ToArray();
        }

        void OnChartFormatChanged()
        {
            if ( this.Chart.Type == Chart.ChartType.Bars ) {
                this.Chart.Type = Chart.ChartType.Lines;
            } else {
                this.Chart.Type = Chart.ChartType.Bars;
            }
            
            this.Chart.Draw();
        }
        
        public override void Render(DrawingContext context)
        {
            base.Render( context );

            this.Chart.Draw();
        }

        void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void AntMes()
        {
            Fecha = Fecha.AddMonths(-1);
            this.Chart.LegendX = Fecha.ToString("MMMM yyyy", new CultureInfo("es-ES"));
            
            ListaMedidas lista = ListaMedidas.CargarDatos();
            var dict = lista.GetLista();
            
            //buscar los valores del mes anterior
            Valores = 
                from x in dict
                where (x.Key.Month.Equals(Fecha.Month) && x.Key.Year.Equals(Fecha.Year))
                select x.Value.Peso;

            this.Chart.Values = Valores.ToArray();
            this.Chart.Draw();
        }
        public void SigMes()
        {
            Fecha = Fecha.AddMonths(1);
            this.Chart.LegendX = Fecha.ToString("MMMM yyyy", new CultureInfo("es-ES"));
            
            ListaMedidas lista = ListaMedidas.CargarDatos();
            var dict = lista.GetLista();
            
            //buscar los valores del mes siguiente
            Valores = 
                from x in dict
                where (x.Key.Month.Equals(Fecha.Month) && x.Key.Year.Equals(Fecha.Year))
                select x.Value.Peso;

            this.Chart.Values = Valores.ToArray();
            this.Chart.Draw();
        }
        
        private Chart Chart { get; }
        private DateTime Fecha { get; set; }
        private IEnumerable<int> Valores { get; set; }
    }
}
