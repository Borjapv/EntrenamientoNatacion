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
        var btForm = this.FindControl<Button>( "BtForm" );
        var btData = this.FindControl<Button>( "BtData" );
        var btChartP = this.FindControl<Button>( "BtChartP" );
        var btChartC = this.FindControl<Button>( "BtChartC" );
            
        opExit.Click += (_, _) => this.OnExit();

        btForm.Click += (_, _) => this.OnViewForm();
        btData.Click += (_, _) => this.OnViewData();
        btChartP.Click += (_, _) => this.OnViewChartPeso();
        btChartC.Click += (_, _) => this.OnViewChartCircAbd();
        
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
        public void OnViewData()
        {
            new DataMedidas().Show();
        }
        public void OnViewChartPeso()
        {
            new ChartPeso().Show();
        }
        public void OnViewChartCircAbd()
        {
            new ChartPeso().Show();
        }
        ListaMedidas Lista { get; }
    }
}