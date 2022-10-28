
namespace ServiceWorker.Core.Menssages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }

        public Message()
        {
            // Retorna o Nome da Classe que está implementando a Message
            MessageType = GetType().Name;
        }
    }
}
