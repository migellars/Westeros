using MediatR;

namespace SharedKernel.Helpers.CQRS;

public interface ILannisterNotificationHandler<T> : INotificationHandler<T> where T :  ILannisterNotification
{
   
}