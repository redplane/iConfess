using iConfess.Admin.Attributes;
using iConfess.Database.Enumerations;
using iConfess.Database.Models.Tables;
using Microsoft.AspNet.SignalR.Hubs;

namespace iConfess.Admin.SignalrHubs
{
    [SignalrAuthorize(AccountRole.Admin)]
    [HubName("SystemMessage")]
    public class SystemMessageHub : ParentHub
    {
        /// <summary>
        /// Send account registration message to specific accounts.
        /// </summary>
        /// <param name="connectionIndexes"></param>
        /// <param name="account"></param>
        public void SendAccountRegistrationMessage(string[] connectionIndexes, Account account)
        {
            // Broadcast notification to all specific connection indexes.
            Clients.Clients(connectionIndexes).obtainAccountCreationMessage(account);
        }
    }
}