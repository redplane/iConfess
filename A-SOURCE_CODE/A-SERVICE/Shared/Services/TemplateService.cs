using Shared.Interfaces.Services;

namespace Shared.Services
{
    public class TemplateService : ITemplateService
    {
        /// <summary>
        ///     From template to render a filled data content.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Render(string template, object data)
        {
            return Nustache.Core.Render.StringToString(template, data);
        }
    }
}