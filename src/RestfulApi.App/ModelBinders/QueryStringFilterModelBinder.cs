using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RestfulApi.App.ModelBinders
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class QueryStringFilterModelBinder : Attribute, IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            QueryString queryString = bindingContext.ActionContext.HttpContext.Request.QueryString;
            if (queryString.HasValue)
            {
                Dictionary<string, string> filter = new Dictionary<string, string>();
                var regex = new Regex(@"^filter\[([a-zA-Z]+)\]=([\d,]+)$");
                foreach (var queryStringArgument in queryString.Value.Split('&'))
                {
                    var filters = regex.Match(queryStringArgument);
                    if (filters.Success)
                    {
                        var captures = filters.Captures;
                        var key = captures[1];
                        var value = captures[2];
                        var model = bindingContext.Model;
                        bindingContext.Model =
                    }
                }

                return Task.CompletedTask;
            }
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}