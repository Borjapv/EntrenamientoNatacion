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

            btOk.Click += (o, args) => this.OnSave();
            btCancel.Click += (o, args) => this.OnExit();
            this.Closed += (_, _) => this.OnExit();
            
            this.IsCancelled = true;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        void OnSave()
        {
            this.IsCancelled = false;
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
        public string Notas
        {
            get
            {
                var toret = this.FindControl<TextBox>("Notas").Text ?? "";
                return toret.Trim();
            }
        }

        public bool IsCancelled { get; private set; }
    }
}
