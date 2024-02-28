using Microsoft.AspNetCore.Mvc.Filters;

namespace BreweryAPI.Filters.ActionFilters;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        
        if(context.ModelState.IsValid) return;

        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Status = StatusCodes.Status400BadRequest
        };
        context.Result = new BadRequestObjectResult(problemDetails);
        
    }
}