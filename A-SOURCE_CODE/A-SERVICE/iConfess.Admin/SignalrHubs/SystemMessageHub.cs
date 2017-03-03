using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Admin.Attributes;
using iConfess.Database.Enumerations;
using Microsoft.AspNet.SignalR.Hubs;

namespace iConfess.Admin.SignalrHubs
{
    [SignalrAuthorize(AccountRole.Admin)]
    [HubName("SystemMessage")]
    public class SystemMessageHub : ParentHub
    {
        /// <summary>
        /// Callback when client disconnects from hub.
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}