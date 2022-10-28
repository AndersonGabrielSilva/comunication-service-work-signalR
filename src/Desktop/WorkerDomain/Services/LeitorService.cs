
using ServiceWorker.Core.DTO;
using WorkerDomain.Interfaces;

namespace WorkerDomain.Services
{
    public abstract class LeitorService
    {
        public IWokerComunicationHub Woker { get; }
        protected RequestHubDTO RequestHub { get; }
        protected string GroupReturnWebClient { get; }

        #region Construtor
        protected LeitorService()
        {

        }

        public LeitorService(IWokerComunicationHub woker, RequestHubDTO requestHub)
        {
            Woker = woker;
            RequestHub = requestHub;
            GroupReturnWebClient = RequestHub.GroupReturnWebClient;
        }
        #endregion        

        public abstract Task RealizaLeitura();
    }
}
