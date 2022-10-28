using ServiceWorker.Core.DTO;
using ServiceWorker.Core.Menssages;

namespace WorkerDomain.Interfaces
{
    //Toda comunicação do servico -→ hub será realizado atravez destá interface
    public interface IWokerComunicationHub
    {
        //Respostas dos Comandos realizados pela API
        Task CommandResponse(ResponseLeituraBiometraHubDTO reponseHubDTO);
        Task CommandResponse(ResponseLeituraNFCHubDTO reponseHubDTO);
        Task CommandResponse(ReponseHubDTO reponseHubDTO);

        /// <summary>
        /// Responsavel por enviar notificações para o cliente
        /// </summary>
        /// <param name="WorkerNotification"></param>
        /// <returns></returns>
        Task Notify(WorkerNotification WorkerNotification);
    }
}
