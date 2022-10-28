namespace ServiceWorker.Core.Menssages
{
    public class Command : Message
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        //Caso não for valido, uma notificação será enviada
        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
