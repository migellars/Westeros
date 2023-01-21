using MediatR;

namespace SharedKernel.Resources.CQRS;

public interface ILannisterNotificationHandler<T> : INotificationHandler<T> where T :  ILannisterNotification
{
   
}