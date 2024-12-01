using CludeTestApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ControleGastos.Middleware
{
    public class ErrorHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Log do erro para análise
            _logger.LogError(ex, ex.Message);

            // Define o status code e o ProblemDetails conforme a exceção
            var problemDetails = new ProblemDetails
            {               
                Detail = ex.Message,
                Instance = ""
            };

            switch (ex)
            {
                case NotFoundException notFoundEx:
                    problemDetails.Type = "https://httpstatuses.com/404";
                    problemDetails.Title = "Recurso não encontrado";
                    problemDetails.Status = (int)HttpStatusCode.NotFound;                    
                    break;

                case ServiceException businessEx:
                    problemDetails.Type = "https://httpstatuses.com/400";
                    problemDetails.Title = "Erro de persistencia";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;                    
                    break;

                case RepositoryException dbEx:
                    problemDetails.Type = "https://httpstatuses.com/500";
                    problemDetails.Title = "Erro de acesso a dados";
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;                    
                    break;

                default:
                    problemDetails.Type = "https://httpstatuses.com/500";
                    problemDetails.Title = "Erro Interno no Servidor";
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;                    
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
            var jsonResponse = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(jsonResponse);
        }



    }
}
