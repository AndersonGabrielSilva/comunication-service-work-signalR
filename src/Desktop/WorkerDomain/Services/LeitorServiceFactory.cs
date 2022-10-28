using ServiceWorker.Core.DTO;
using ServiceWorker.Core.Enum;
using ServiceWorker.Core.Menssages;
using ServiceWorker.Core.Menssages.Commands;
using WorkerDomain.Interfaces;

namespace WorkerDomain.Services
{
    public static class LeitorServiceFactory
    {
        public static LeitorService? Create(RequestHubDTO requestHub, IWokerComunicationHub Woker = null)
        {
            switch (requestHub.Command)
            {
                case CommandConst.REALIZA_LEITURA_BIOMETRIA:
                    return new LeitorBiometricoService(Woker, requestHub);
                case CommandConst.REALIZA_LEITURA_NFC:
                    return new LeitorNFCService(Woker, requestHub);
                default:
                    Woker?.Notify(new WorkerNotification(requestHub.GroupReturnWebClient, "Comando não identificado", NotificationTypeEnum.Erro)).Wait();
                    return null;
            }
        }
    }
}
