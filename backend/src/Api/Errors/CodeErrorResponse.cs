using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Hospital.Api.Errors;

public class CodeErrorResponse
{
    [JsonProperty(PropertyName = "statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string[] Message { get; set; }

    public CodeErrorResponse(int statusCode, string[]? message = null)
    {
        this.StatusCode = statusCode;
        if (message is null)
        {
            this.Message = new string[0];
            var text = GetDefaultMessageStatusCode(statusCode);
            this.Message[0] = text;
        }
        else
        {
            this.Message = message;
        }
    }

    private string GetDefaultMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "El Request enviado tiene errores",
            401 => "No tienes authorization para este recurso",
            404 => "No se encontro el recurso solicitado",
            500 => "Se produjeron errores en el servidor",
            _ => string.Empty
        };
    }
}