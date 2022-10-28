
using ServiceWorker.Core.DTO;
using WorkerDomain.Interfaces;

namespace WorkerDomain.Services
{
    public class LeitorNFCService : LeitorService
    {
        #region Contrutor
        public LeitorNFCService(IWokerComunicationHub _woker, RequestHubDTO requestHub) : base(_woker, requestHub)
        {

        }
        #endregion

        public override async Task RealizaLeitura()
        {
            Console.WriteLine("Realiza Leitura NFC");
            await Woker.CommandResponse(new ResponseLeituraNFCHubDTO(GroupReturnWebClient) { Mensagem = "Leitura do NFC realizado com sucesso!" });

        }
    }
}
