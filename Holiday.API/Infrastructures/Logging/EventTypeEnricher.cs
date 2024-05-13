using Serilog.Core;
using Serilog.Events;
using System.Text;

namespace Holiday.API.Infrastructures.Logging
{
    public class EventTypeEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var eventType = propertyFactory.CreateProperty("EventType", logEvent.Level);
            logEvent.AddPropertyIfAbsent(eventType);
        }
    }
}
