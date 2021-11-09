using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EntrenamientoNatacion.Core;

namespace EntrenamientoNatacion.UI
{
    public class Formulario : Window
    {
        public Formulario()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var btOk = this.FindControl<Button>( "BtOk" );
            var btCancel = this.FindControl<Button>( "BtCancel" );
            var peso = this.FindControl<NumericUpDown>("Peso");
            var circAbd = this.FindControl<NumericUpDown>("CircAbd");
            var notas = this.FindControl<TextBox>("Notas");

            btOk.Click += (o, args) => this.OnSave((int)peso.Value, (int)circAbd.Value, notas.Text);
            btCancel.Click += (o, args) => this.OnClose();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnClose()
        {
            this.Close();
        }
        
        private void OnSave(int peso, int circAbd, string notas)
        {
            //Medidas med = Medidas.Cargar("cargar");
            //med.Guardar("guardar");
            new Medidas(peso, circAbd, notas, DateTime.Today).Guardar("guardar");
            this.Close();
        }
    }
}
