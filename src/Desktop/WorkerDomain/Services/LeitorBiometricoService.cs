
using ServiceWorker.Core.DTO;
using WorkerDomain.Interfaces;

namespace WorkerDomain.Services
{
    public class LeitorBiometricoService : LeitorService
    {
        #region Construtor
        public LeitorBiometricoService(IWokerComunicationHub _woker, RequestHubDTO requestHub) : base(_woker, requestHub) { }
        #endregion

        public override async Task RealizaLeitura()
        {
            Console.WriteLine("Realiza Leitura Biometrica");

            await Woker.CommandResponse(new ResponseLeituraBiometraHubDTO(GroupReturnWebClient) { Mensagem = "Leitura da Biometria realizada com sucesso!" });
        }
    }
}
