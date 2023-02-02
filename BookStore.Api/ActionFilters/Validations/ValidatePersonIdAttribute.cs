using BookStore.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.Api.ActionFilters.Validations
{
    public class ValidatePersonIdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var PersonId = context.RouteData.Values["PersonId"];
            if(!long.TryParse(PersonId.ToString(), out _))
            {
                BadRequestResponse(context, new Error{Number = (int)ErrorNumber.InvalidId, Message = "PersonId should be a number"});
            }
            base.OnActionExecuting(context);
        }

        private void BadRequestResponse(ActionExecutingContext context, Error error)
        {
            context.Result = new BadRequestObjectResult(error);
        }
    }
}