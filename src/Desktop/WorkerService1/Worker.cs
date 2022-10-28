using Microsoft.AspNetCore.SignalR.Client;
using ServiceWorker.Core.DTO;
using ServiceWorker.Core.Enum;
using ServiceWorker.Core.Menssages;
using ServiceWorker.Core.SignalR;
using WorkerDomain.Interfaces;
using WorkerDomain.Services;
using WorkerDomain.Settings;

namespace WorkerService1
{
    public class Worker : BackgroundService, IWokerComunicationHub
    {
        private readonly ILogger<Worker> _logger;

        //SignalR
        private HubConnection hubConnection;
        private const int TENTATIVAS = 20;
        private int _nroTentativasCorrente;

        private readonly WorkerServiceSettings workerSettings;

        #region Construtor
        public Worker(ILogger<Worker> logger,
                      WorkerServiceSettings workerServiceSettings)
        {
            _logger = logger;
            this.workerSettings = workerServiceSettings;

            CreateConnectionHub();
        }
        #endregion

        #region SignalR Settings
        private void CreateConnectionHub()
        {
            var apiUrlHub = workerSettings.ApiServiceHubBase + SignalRName.SignalRRouteWorkerHub;
            hubConnection = new HubConnectionBuilder()
                           .WithUrl(apiUrlHub)
                           .WithAutomaticReconnect()
                           .Build();

            hubConnection.Closed += HubConnection_Closed;
            hubConnection.Reconnecting += HubConnection_Reconnecting;
            hubConnection.Reconnected += HubConnection_Reconnected;

            //Criação dos metodos do client
            CreateRequestHandler();
        }

        private void CreateRequestHandler()
        {
            //Fica escultando o hub com a chave : "WokerHub"            
            hubConnection.On<RequestHubDTO>(SignalRName.WokerHub, async (requestHub) =>
            {
                try
                {
                    if (requestHub is not null)
                    {
                        var leitorService = LeitorServiceFactory.Create(requestHub, this);
                        if (leitorService is not null)
                            await leitorService.RealizaLeitura();

                        throw new Exception("TESTE 123");
                    }
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException runEx)
                {
                    await hubConnection.InvokeAsync<Notification>(SignalRName.WorkerNotificationDesktop, new WorkerNotification(requestHub.GroupReturnWebClient, $"Objeto inválido", NotificationTypeEnum.Erro));
                }
                catch (Exception ex)
                {
                    await hubConnection.InvokeAsync<Notification>(SignalRName.WorkerNotificationDesktop, new WorkerNotification(requestHub.GroupReturnWebClient, ex.Message, NotificationTypeEnum.Erro));
                }
            });
        }

        private async Task<bool> StartConnection()
        {
            var result = false;

            try
            {
                _nroTentativasCorrente++;
                await hubConnection.StartAsync();
                result = true;
            }
            catch
            {
                if (_nroTentativasCorrente != TENTATIVAS)
                {
                    _logger.LogWarning($"Tentativa de conexão: {_nroTentativasCorrente}");
                    await Task.Delay(5000);

                    CreateConnectionHub();
                    await StartConnection();
                }
            }

            _nroTentativasCorrente = 0;

            return result;
        }

        private async Task VincularServicoAoGrupo() =>
            await hubConnection.InvokeAsync(SignalRName.JoinGroup, workerSettings.GroupName.ToUpper());

        #region Eventos da Connexão
        private async Task HubConnection_Reconnected(string? arg)
        {
            _logger.LogInformation(@"Reconectado! \o/");
            await VincularServicoAoGrupo();
        }

        private async Task HubConnection_Reconnecting(Exception? arg) =>
            _logger.LogWarning("Reconectando...");

        private async Task HubConnection_Closed(Exception? arg)
        {
            _logger.LogError("Connexão encerrada!");
            await StartConnection();
            await VincularServicoAoGrupo();
        }
        #endregion

        #endregion

        #region Execulte Work
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartConnection();
            await VincularServicoAoGrupo();
        }
        #endregion

        #region Notify Hub
        public async Task CommandResponse(ResponseLeituraBiometraHubDTO reponseHubDTO) =>
             await hubConnection.InvokeAsync(SignalRName.RetornoBiometria, reponseHubDTO);

        public async Task CommandResponse(ResponseLeituraNFCHubDTO reponseHubDTO) =>
             await hubConnection.InvokeAsync(SignalRName.RetornoLeituraNFCDesktop, reponseHubDTO);

        public async Task CommandResponse(ReponseHubDTO reponseHubDTO) =>
            await hubConnection.InvokeAsync("Teste", reponseHubDTO);

        public async Task Notify(WorkerNotification WorkerNotification) =>
            await hubConnection.InvokeAsync(SignalRName.WorkerNotificationDesktop, WorkerNotification);
        #endregion
    }
}