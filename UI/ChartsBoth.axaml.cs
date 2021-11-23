using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using EntrenamientoNatacion.Core;

namespace EntrenamientoNatacion.UI
{
    public class ChartsBoth : Window
    {
        public ChartsBoth()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        public ChartsBoth(Dictionary<DateTime,Medidas> dict) : this()
        {
            this.Dict = dict;
            var btAnt = this.FindControl<Button>( "BtAnt" );
            var btSig = this.FindControl<Button>( "BtSig" );
            this.ChartPeso = this.FindControl<Chart>( "ChartPeso" );
            this.ChartCirc = this.FindControl<Chart>( "ChartCirc" );
            var rbBars = this.FindControl<RadioButton>( "Bars" );
            var rbLine = this.FindControl<RadioButton>( "Lines" );
            
            btAnt.Click += (_, _) => this.AntMes();
            btSig.Click += (_, _) => this.SigMes();

            rbBars.Checked += (_, _) => this.OnChartFormatChanged();
            rbLine.Checked += (_, _) => this.OnChartFormatChanged();
            
            this.ChartPeso.LegendY = "Peso (kg)";
            this.ChartCirc.LegendY = "Circ abdominal (cm)";
            Fecha = DateTime.Today;
            this.ChartPeso.LegendX = Fecha.ToString("MMMM yyyy", new CultureInfo("es-ES"));
            this.ChartCirc.LegendX = Fecha.ToString("MMMM yyyy", new CultureInfo("es-ES"));
            
            RecalcularValores();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        void OnChartFormatChanged()
        {
            if ( this.ChartPeso.Type == Chart.ChartType.Bars ) {
                this.ChartPeso.Type = Chart.ChartType.Lines;
                this.ChartCirc.Type = Chart.ChartType.Lines;
            } else {
                this.ChartPeso.Type = Chart.ChartType.Bars;
                this.ChartCirc.Type = Chart.ChartType.Bars;
            }
            
            this.ChartPeso.Draw();
            this.ChartCirc.Draw();
        }
        
        public override void Render(DrawingContext context)
        {
            base.Render( context );

            this.ChartPeso.Draw();
            this.ChartCirc.Draw();
        }

        public void AntMes()
        {
            Fecha = Fecha.AddMonths(-1);
            this.ChartPeso.LegendX = Fecha.ToString("MMMM yyyy", new CultureInfo("es-ES"));
            this.ChartCirc.LegendX = Fecha.ToString("MMMM yyyy", new CultureInfo("es-ES"));
            
            ListaMedidas lista = ListaMedidas.CargarDatos();
            var dict = lista.GetLista();
            
            RecalcularValores();
            this.ChartPeso.Draw();
            this.ChartCirc.Draw();
        }
        public void SigMes()
        {
            Fecha = Fecha.AddMonths(1);
            this.ChartPeso.LegendX = Fecha.ToString("MMMM yyyy", new CultureInfo("es-ES"));
            this.ChartCirc.LegendX = Fecha.ToString("MMMM yyyy", new CultureInfo("es-ES"));

            RecalcularValores();
            this.ChartPeso.Draw();
            this.ChartCirc.Draw();
        }

        private void RecalcularValores()
        {
            ValoresPeso = 
                from x in Dict
                where (x.Key.Month.Equals(Fecha.Month) && x.Key.Year.Equals(Fecha.Year))
                select x.Value.Peso;
            
            ValoresCirc = 
                from x in Dict
                where (x.Key.Month.Equals(Fecha.Month) && x.Key.Year.Equals(Fecha.Year))
                select x.Value.CircAbd;

            this.ChartPeso.Values = ValoresPeso.ToArray();
            this.ChartCirc.Values = ValoresCirc.ToArray();
        }

        private Chart ChartPeso { get; }
        private Chart ChartCirc { get; }
        private DateTime Fecha { get; set; }
        private IEnumerable<int> ValoresPeso { get; set; }
        private IEnumerable<int> ValoresCirc { get; set; }
        private Dictionary<DateTime,Medidas> Dict { get; set; }
    }
}