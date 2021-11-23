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
        var btChart = this.FindControl<Button>( "BtChart" );
            
        opExit.Click += (_, _) => this.OnExit();

        btForm.Click += (_, _) => this.OnViewForm();
        btData.Click += (_, _) => this.OnViewData();
        btChart.Click += (_, _) => this.OnViewCharts();
        
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

            if (!form.IsCancelled )
            {
                var notas = form.Notas;
                var med = new Medidas((int)form.Peso, (int)form.CircAbd, notas);
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
        public void OnViewCharts()
        {
            new ChartsBoth(Lista.GetLista()).Show();
        }
        ListaMedidas Lista { get; }
    }
}