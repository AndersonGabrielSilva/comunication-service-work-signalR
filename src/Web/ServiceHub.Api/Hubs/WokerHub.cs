using Microsoft.AspNetCore.SignalR;
using ServiceWorker.Core.DTO;
using ServiceWorker.Core.Menssages;
using ServiceWorker.Core.SignalR;

namespace ServiceHub.Api.Hubs
{
    public class WokerHub : BaseHub
    {
        #region Gerenciador de grupos
        public override Task JoinGroup(string groupName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public override Task LeaveGroup(string groupName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        #endregion

        #region Mensagens
        //Este método pode ser invocado pelo Client via SignalR 
        public async Task WorkerNotificationDesktop(WorkerNotification workerNotification)
        {
            //Notifica todos os Hubs conectados
            //await Clients.All.SendAsync(groupName + SignalRName.WokerHub, message);
            await Clients.Group(workerNotification.GroupReturnWebClient).SendAsync(SignalRName.WorkerNotificationWeb, workerNotification);
        }

        public Task Teste(ReponseHubDTO reponseHubDTO)
        {
            return Task.CompletedTask;
        }

        public Task RetornoBiometria(ResponseLeituraBiometraHubDTO responseLeituraBiometraHubDTO)
        {
            if (string.IsNullOrEmpty(responseLeituraBiometraHubDTO.GroupReturnWebClient))
                return Task.CompletedTask;

            // Salva no banco de dados
            // Realiza autorização
            // Qualquer regra de negocio

            Clients.Group(responseLeituraBiometraHubDTO.GroupReturnWebClient).SendAsync(SignalRName.WorkerNotificationWeb, responseLeituraBiometraHubDTO);

            return Task.CompletedTask;
        }

        public Task RetornoLeituraNFCDesktop(ResponseLeituraNFCHubDTO responseLeituraNFCHubDTO)
        {
            if (string.IsNullOrEmpty(responseLeituraNFCHubDTO.GroupReturnWebClient))
                return Task.CompletedTask;


            Clients.Group(responseLeituraNFCHubDTO.GroupReturnWebClient).SendAsync(SignalRName.WorkerNotificationWeb, responseLeituraNFCHubDTO);
            return Task.CompletedTask;
        }
        #endregion
    }


}
