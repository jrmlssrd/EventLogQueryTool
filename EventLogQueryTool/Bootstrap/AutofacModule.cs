using Autofac;
using EventLogQueryTool.Services;

namespace EventLogQueryTool.Bootstrap
{
    public class AutofacModule : Module
    {
        #region Protected Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventLogReaderManager>().As<IEventLogReaderManager>();
            builder.RegisterType<EventLogCriteriaConverter>().As<IEventLogCriteriaConverter>();
            builder.RegisterType<EventLogReaderService>().As<IEventLogReaderService>();
            builder.RegisterType<ExceptionManager>().As<IExceptionManager>();
        }

        #endregion Protected Methods
    }
}