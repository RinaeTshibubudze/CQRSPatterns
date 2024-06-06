using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CQRSPattern.Features.Notifications
{
    public class StockAssignedHandler(ILogger<StockAssignedHandler> logger) : INotificationHandler<ProductCreatedNotification>
    {
        public Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"handling notification for product creation with id : {notification.Id}. assigning stocks.");
            return Task.CompletedTask;
        }
    }
}