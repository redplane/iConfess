using System.Web.Http;
using Shared.Interfaces;

namespace iConfess.Admin.Controllers
{
    public class ApiParentController : ApiController
    {
        #region Properties

        /// <summary>
        /// Contains repositories and methods to access to database.
        /// </summary>
        protected readonly IUnitOfWork UniOfWork;

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate controller with dependency injection.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ApiParentController(IUnitOfWork unitOfWork)
        {
            UniOfWork = unitOfWork;
            
        }
        
        #endregion
    }
}