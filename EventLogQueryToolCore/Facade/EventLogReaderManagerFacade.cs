using Autofac;
using EventLogQueryToolCore.Common;
using EventLogQueryToolCore.Services;

namespace EventLogQueryToolCore.Facade
{
    internal class EventLogReaderManagerFacade
    {
        #region Public Methods

        public static IEventLogReaderManager CreateInstance()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EventLogReaderManager>().As<IEventLogReaderManager>();
            builder.RegisterType<EventLogReaderService>().As<IEventLogReaderService>();
            builder.RegisterType<EventLogCriteriaConverter>().As<IEventLogCriteriaConverter>();
            builder.RegisterType<ExceptionManager>().As<IExceptionManager>();
            var container = builder.Build();
            return container.Resolve<IEventLogReaderManager>();
        }

        #endregion Public Methods
    }
}