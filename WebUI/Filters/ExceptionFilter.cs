using System;
using System.Collections.Generic;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KhalafTrade.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly Dictionary<Type, Action<ExceptionContext>> _dictionary;

        public ExceptionFilter()
        {
            _dictionary = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(CantAddEntityException), CantAddEntityExceptionHandler },
                { typeof(NotFoundException), NotFoundExceptionHandler },
                { typeof(UnAuthorizedRequest), UnAuthorizedRequestHandler },
                { typeof(CannotParseEnum), CannotChangeStatusHandler },
                { typeof(ArgumentException), ArgumentExceptionHandler },
            };
        }


        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception.GetType();
            if (_dictionary.ContainsKey(exception))
            {
                _dictionary[exception].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                InvalidModelStateExceptionHandler(context);
                return;
            }

            UnknownExceptionHandler(context);
        }

        private void UnAuthorizedRequestHandler(ExceptionContext context)
        {
            context.Result = new UnauthorizedObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }

        private void NotFoundExceptionHandler(ExceptionContext context)
        {
            context.Result = new NotFoundObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }

        private void CantAddEntityExceptionHandler(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }


        private void InvalidModelStateExceptionHandler(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }

        private void UnknownExceptionHandler(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request. please try again"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void CannotChangeStatusHandler(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }

        private void ArgumentExceptionHandler(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }
    }
}