using BreweryAPI.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BreweryAPI.Filters.ExceptionFilters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        base.OnException(context);

        ProblemDetails problemDetails;
        var exceptionMessage = context.Exception.Message;
        
        if (context.Exception.GetType().IsGenericType && 
            context.Exception.GetType().GetGenericTypeDefinition() == typeof(EntityIdNotFoundException<>))
        {
            problemDetails = new()
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Wrong ID",
                Detail = exceptionMessage
            };
        }
        else
        {
            problemDetails = new()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unknown problem occured",
                Detail = exceptionMessage
            };
        }
        
        context.Result = new NotFoundObjectResult(problemDetails);
    }
}