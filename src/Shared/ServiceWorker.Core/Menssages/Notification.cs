
using ServiceWorker.Core.Enum;

namespace ServiceWorker.Core.Menssages
{
    public class Notification : Message
    {
        //Para o perfeito funcionamento do SignalR sempre deixe as propriedades publicas, tanto get quanto set
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public NotificationTypeEnum NotificationType { get; set; }

        //SignalR
        public Notification()
        {

        }

        public Notification(string message) : this()
        {
            Timestamp = DateTime.Now;
            Message = message;
        }

        public Notification(string message, NotificationTypeEnum notificationType):this()
        {
            Timestamp = DateTime.Now;
            Message = message;
            NotificationType = notificationType;
        }
    }
}
