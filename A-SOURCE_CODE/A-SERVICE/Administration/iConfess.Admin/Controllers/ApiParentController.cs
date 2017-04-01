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
        ///     Search validation messages from modelstate dictionary.
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        protected Dictionary<string, string[]> FindValidationMessage(ModelStateDictionary modelStateDictionary,
            string parameterName)
        {
            // Parameter prefix.
            var parameterPrefix = $"{parameterName}.";

            // Parameter prefix length.
            var parameterPrefixLength = parameterPrefix.Length;

            return
                modelStateDictionary.ToDictionary(
                    x => x.Key.StartsWith(parameterPrefix) ? x.Key.Substring(parameterPrefixLength) : x.Key,
                    x => x.Value.Errors.Select(y => y.ErrorMessage).ToArray());
        }

        #endregion
    }
}