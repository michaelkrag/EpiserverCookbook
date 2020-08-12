using MediatR;
using System;

namespace MovieShop.Business.Handlers.Evidences.Models
{
    public class LogEvent : INotification
    {
        public string UserId { get; }
        public string ContentId { get; }
        public string Event { get; }
        public DateTime Date { get; }
        public string SessionId { get; }

        private LogEvent(string userId, string contentId, string @event, string sessionId, DateTime date)
        {
            UserId = userId;
            ContentId = contentId;
            Event = @event;
            Date = date;
            SessionId = sessionId;
        }

        public static LogEvent Create(string userId, string contentId, string @event, string sessionId, DateTime date)
        {
            return new LogEvent(userId, contentId, @event, sessionId, date);
        }

        public static LogEvent Create(string userId, string contentId, string sessionId, string @event)
        {
            return new LogEvent(userId, contentId, @event, sessionId, DateTime.UtcNow);
        }
    }
}