using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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

            btOk.Click += (o, args) => this.OnExit();
            btCancel.Click += (o, args) => this.OnCancel();
            
            this.IsCancelled = false;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        void OnCancel()
        {
            this.IsCancelled = true;
            this.OnExit();
        }

        void OnExit()
        {
            this.Close();
        }

        public double Peso {
            get => this.FindControl<NumericUpDown>( "Peso" ).Value;
        }
        public double CircAbd {
            get => this.FindControl<NumericUpDown>( "CircAbd" ).Value;
        }
        public string Notas {
            get => this.FindControl<TextBox>( "Notas" ).Text.Trim();
        }
        public bool IsCancelled { get; private set; }
    }
}
