using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
            
        opExit.Click += (_, _) => this.OnExit();
        opAbout.Click += (_, _) => this.OnAbout();

        opForm.Click += (_, _) => this.OnViewForm();
        btForm.Click += (_, _) => this.OnViewForm();
    }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public void OnExit()
        {
            this.Close();
        }

        public void OnAbout()
        {
            //new AboutWindow().ShowDialog( this );
        }

        public void OnViewForm()
        {
            new Formulario().ShowDialog( this );
        }
    }
}