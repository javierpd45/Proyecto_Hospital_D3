using System.Net;
using Hospital.Api.Errors;
using Hospital.Application.Exceptions;
using Newtonsoft.Json;

namespace Hospital.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger
    )
    {
        this._next = next;
        this._logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this._next(context);
        }
        catch (Exception ex)
        {
            this._logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (ex)
            {
                case NotFoundException notFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;

                case FluentValidation.ValidationException validationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    var errors = validationException.Errors.Select(ers => ers.ErrorMessage).ToArray();
                    var validationJsons = JsonConvert.SerializeObject(errors);
                    result = JsonConvert.SerializeObject(
                        new CodeErrorException(statusCode, errors, validationJsons)
                    );
                    break;

                case BadRequestException badRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(
                    new CodeErrorException(statusCode, 
                    new string[]{ex.Message}, ex.StackTrace));
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(result);
        }
    }
}