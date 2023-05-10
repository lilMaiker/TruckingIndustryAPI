using Elmah;
using System.Web.Http.Filters;

namespace TruckingIndustryAPI.Exceptions
{
    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            ErrorSignal.FromCurrentContext().Raise(context.Exception);

            // Now generate the result to the client
        }
    }
}
