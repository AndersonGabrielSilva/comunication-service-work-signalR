
namespace ServiceWorker.Core.DTO
{
    public class ReponseHubDTO
    {
        public DateTime Timestamp { get; private set; }

        public string Mensagem { get; set; }
        public string GroupReturnWebClient { get; set; }

        /// <summary>
        /// Para poder ser utilizado pelo SignalR deve conter sempre um construtor vazio
        /// </summary>
        public ReponseHubDTO()
        {
            Timestamp = DateTime.Now;
        }

        public ReponseHubDTO(string groupReturnWebClient) : this()
        {

        }

        public ReponseHubDTO(string mensagem, string groupReturnWebClient) : this(groupReturnWebClient)
        {
            Mensagem = mensagem;
            GroupReturnWebClient = groupReturnWebClient;
        }
    }
}
