using MediatR;

namespace SharedKernel.Resources.CQRS;

public interface IWesterosNotificationHandler<T> : INotificationHandler<T> where T :  IWesterosNotification
{
   
}