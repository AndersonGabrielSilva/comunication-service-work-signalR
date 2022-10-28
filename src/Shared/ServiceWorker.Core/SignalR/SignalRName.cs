namespace ServiceWorker.Core.SignalR
{
    public static class SignalRName
    {
        #region Rotas
        public const string SignalRRouteWorkerHub = "/wokerhub";
        #endregion

        #region Grupos
        public const string Group = nameof(Group);
        #endregion

        #region Metodos Server Hub
        public const string JoinGroup = nameof(JoinGroup);
        public const string LeaveGroup = nameof(LeaveGroup);
        public const string WorkerNotificationDesktop = nameof(WorkerNotificationDesktop);
        public const string RetornoBiometria = nameof(RetornoBiometria);
        public const string RetornoLeituraNFCDesktop = nameof(RetornoLeituraNFCDesktop);
        #endregion

        #region Metodos Client Desktop
        public const string WokerHub = nameof(WokerHub);
        #endregion

        #region Metodos Client Web
        public const string WorkerNotificationWeb = nameof(WorkerNotificationWeb);
        #endregion

    }
}