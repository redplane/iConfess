using Shared.Enumerations;

namespace Shared.Interfaces.Services
{
    public interface ITemplateService
    {
        /// <summary>
        /// Render full text content from template and related data.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        string Render(string template, object data);
    }
}