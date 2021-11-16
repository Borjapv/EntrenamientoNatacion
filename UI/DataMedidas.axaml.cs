using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EntrenamientoNatacion.Core;

namespace EntrenamientoNatacion.UI
{
    public class DataMedidas : Window
    {
        public DataMedidas()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif  
            
            ListaMedidas lista = ListaMedidas.CargarDatos();
            var dataMedidas = this.FindControl<DataGrid>( "DataMedidas" );
            dataMedidas.Items = lista.GetLista();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}