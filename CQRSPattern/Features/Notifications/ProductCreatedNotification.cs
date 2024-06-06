using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CQRSPattern.Features.Notifications
{
    public record ProductCreatedNotification(Guid Id) : INotification;
}