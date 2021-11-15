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
    public partial class ChartCircAbd : Window
    {
        public ChartCircAbd()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            this.Chart = this.FindControl<Chart>( "CircGrf" );
            var rbBars = this.FindControl<RadioButton>( "CircBars" );
            var rbLine = this.FindControl<RadioButton>( "CircLine" );

            rbBars.Checked += (_, _) => this.OnChartFormatChanged();
            rbLine.Checked += (_, _) => this.OnChartFormatChanged();
            
            this.Chart.LegendY = "Circ abdominal";
            var fecha = DateTime.Today;
            this.Chart.LegendX = fecha.ToString("MMMM", new CultureInfo("es-ES"));
            
            ListaMedidas lista = ListaMedidas.CargarDatos();
            var dict = lista.GetLista();
            
            //buscar los valores del mes actual
            IEnumerable<int> valores = 
                from x in dict
                where x.Key.Month.Equals(fecha.Month)
                select x.Value.CircAbd;

            this.Chart.Values = valores.ToArray();
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
        
        Chart Chart { get; }
    }
}