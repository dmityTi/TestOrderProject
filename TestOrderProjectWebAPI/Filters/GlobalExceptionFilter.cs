using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestOrderProjectWebAPI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"  Message: {context?.Exception?.Message}," + Environment.NewLine +
                              $" Sourse: {context?.Exception?.Source}, " + Environment.NewLine +
                              $"TargetSite: {context?.Exception?.TargetSite}" + Environment.NewLine +
                              $"StackTrace: {context?.Exception?.StackTrace}" + Environment.NewLine + 
                              $"");
        }
    }
}