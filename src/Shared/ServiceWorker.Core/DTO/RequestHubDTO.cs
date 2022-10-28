
namespace ServiceWorker.Core.DTO
{
    public class RequestHubDTO
    {
        public string Command { get; set; }

        //Para qual metodo o Command Response deve retornar
        public string GroupReturnWebClient { get; set; }

        //Necessario para o SignalR
        public RequestHubDTO() { }

        public RequestHubDTO(string command, string methodReturnWebClient = "")
        {
            Command = command;
            GroupReturnWebClient = methodReturnWebClient;
        }
    }
}
