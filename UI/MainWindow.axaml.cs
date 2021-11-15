using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EntrenamientoNatacion.Core;

namespace EntrenamientoNatacion.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        
        var opExit = this.FindControl<MenuItem>( "OpExit" );
        var opAbout = this.FindControl<MenuItem>( "OpAbout" );
        var opForm = this.FindControl<MenuItem>( "OpForm" );
        var btForm = this.FindControl<Button>( "BtForm" );
        var btSave = this.FindControl<Button>( "BtSave" );
        var btChart = this.FindControl<Button>( "BtChart" );
            
        opExit.Click += (_, _) => this.OnExit();
        opAbout.Click += (_, _) => this.OnAbout();

        opForm.Click += (_, _) => this.OnViewForm();
        btForm.Click += (_, _) => this.OnViewForm();
        btSave.Click += (_, _) => this.OnSave();
        btChart.Click += (_, _) => this.OnViewChart();
        
        this.Closed += (_, _) => this.OnClose();

        this.Lista = ListaMedidas.CargarDatos();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        void OnClose()
        {
            OnSave();
        }

        void OnExit()
        {
            this.OnClose();
            this.Close();
        }

        void OnAbout()
        {
            //new AboutWindow().ShowDialog( this );
        }

        async void OnViewForm()
        {
            var form = new Formulario();
            await form.ShowDialog(this);

            if (!form.IsCancelled ) {
                var med = new Medidas((int)form.Peso, (int)form.CircAbd, form.Notas);
                //Medidas med = Medidas.Cargar("asd");
                //med.Guardar("asd");
                this.Lista.Add(DateTime.Today, med);
            }
        }

        void OnSave()
        {
            Lista.GuardarDatos();
        }
        public void OnViewChart()
        {
            new ChartPeso().Show();
            new ChartCircAbd().Show();
        }
        
        
        ListaMedidas Lista { get; }
    }
}