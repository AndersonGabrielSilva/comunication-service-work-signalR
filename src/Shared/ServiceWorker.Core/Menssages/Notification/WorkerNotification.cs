using ServiceWorker.Core.Enum;

namespace ServiceWorker.Core.Menssages
{
    public class WorkerNotification : Notification
    {
        public string? GroupReturnWebClient { get; set; }

        //SignalR
        public WorkerNotification(){ }

        public WorkerNotification(string groupReturnWebClient, string message)
               : base(message)
        {
            GroupReturnWebClient = groupReturnWebClient;
        }

        public WorkerNotification(string groupReturnWebClient, string message, NotificationTypeEnum notificationType)
              : base(message, notificationType)
        {
            GroupReturnWebClient = groupReturnWebClient;
        }
    }
}
