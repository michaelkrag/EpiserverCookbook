using MediatR;
using MovieShop.Business.Handlers.Evidences.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MovieShop.Business.Handlers.Evidences
{
    public class EvidenceCollectorHandler : INotificationHandler<LogEvent>
    {
        public Task Handle(LogEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}