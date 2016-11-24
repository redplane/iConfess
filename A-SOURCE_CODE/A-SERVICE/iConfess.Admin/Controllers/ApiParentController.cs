using System.Web.Mvc;
using Shared.Interfaces;

namespace iConfess.Admin.Controllers
{
    public class ApiParentController : Controller
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