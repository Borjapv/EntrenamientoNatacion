using System;
using Avalonia;
using EntrenamientoNatacion.Core;

namespace EntrenamientoNatacion.UI
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
        
        /*public static void Main(string[] args)
        {
            Medidas med = Medidas.Cargar("medidas.xml");
            med.Guardar("medidas2.xml");
        }*/
    }
}