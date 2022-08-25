using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyLib.Mvc
{
    public class FilterHelper : Controller, IActionFilter
    {
        public dynamic? GetCurrentPageResultModel(dynamic? context)
        {
            try
            {
                return context?.Result?.Model ?? null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public dynamic? GetAllPageResultModel(dynamic? context)
        {
            try
            {
                return context.Controller.ItemList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public dynamic? LoadContextContent(dynamic context)
        {
            if (context.HttpContext.User.Identity == null) { return null; }
            if (context.HttpContext.User.Identity.Name == null) { return null; }
            dynamic contextResult = new System.Dynamic.ExpandoObject();
            contextResult.UserId = Convert.ToInt32(context.HttpContext.User.Identity.Name);
            contextResult.ActionPath = context.HttpContext.Request.Path.Value;
            contextResult.Model = GetAllPageResultModel(context);
            return contextResult;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            return;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }
    }
}
