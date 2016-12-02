using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Shared.Interfaces.Services;

namespace iConfess.Admin.Controllers
{
    public class ApiParentController : ApiController
    {
        #region Properties

        /// <summary>
        ///     Contains repositories and methods to access to database.
        /// </summary>
        protected readonly IUnitOfWork UnitOfWork;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate controller with dependency injection.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ApiParentController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Find validation messages from modelstate dictionary.
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        protected Dictionary<string, string[]> FindValidationMessage(ModelStateDictionary modelStateDictionary)
        {
            return modelStateDictionary.ToDictionary(x => x.Key,
                x => x.Value.Errors.Select(y => y.ErrorMessage).ToArray());
        }

        #endregion
    }
}