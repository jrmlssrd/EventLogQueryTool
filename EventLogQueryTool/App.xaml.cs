using GalaSoft.MvvmLight.Threading;
using System.Windows;

namespace EventLogQueryTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Public Constructors

        static App()
        {
            DispatcherHelper.Initialize();
            var c = System.Threading.Thread.CurrentThread.CurrentUICulture;
        }

        #endregion Public Constructors
    }
}