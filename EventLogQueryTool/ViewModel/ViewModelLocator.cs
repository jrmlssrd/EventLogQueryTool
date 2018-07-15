/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:EventLogQueryTool.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using EventLogQueryTool.Bootstrap;

namespace EventLogQueryTool.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        #region Public Constructors

        static ViewModelLocator()
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                RegisterServices(registerFakes: true);
            }
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }

        public static void RegisterServices(bool registerFakes = false)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            builder.RegisterType<MainViewModel>().AsSelf().InstancePerLifetimeScope();
            var container = builder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }

        #endregion Public Methods
    }
}