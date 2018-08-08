using Autofac;
using EventLogQueryTool.Services;
using EventLogQueryToolCore.Common;
using EventLogQueryToolCore.Services;

namespace EventLogQueryTool.Bootstrap
{
    public class AutofacModule : Module
    {

        #region Protected Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventLogReaderManager>().As<IEventLogReaderManager>();
            builder.RegisterType<EventLogReaderService>().As<IEventLogReaderService>();
            builder.RegisterType<ServerConfigurationXMLConverter>().As<IServerConfigurationConverter>();
            builder.RegisterType<ServerConfigurationManager>().As<IServerConfigurationManager>();
            builder.RegisterType<EventLogCriteriaConverter>().As<IEventLogCriteriaConverter>();
            builder.RegisterType<UIExceptionManager>().As<IExceptionManager>();
        }

        #endregion Protected Methods

    }
}