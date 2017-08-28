namespace Administration.Interfaces.Services
{
    public interface IQueueService
    {
        #region Methods

        /// <summary>
        /// Listen to messages 
        /// </summary>
        void InitiateAccountRegistrationQueue();
        
        #endregion
    }
}