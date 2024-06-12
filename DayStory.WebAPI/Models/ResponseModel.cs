using System.Text.Json;

namespace DayStory.WebAPI.Models;

public partial class ResponseModel
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public ResponseModel(string? message = null, int statusCode = 400)
    {
        StatusCode = statusCode;
        if (string.IsNullOrWhiteSpace(message))
        {
            Message = "An error occurred.";
        }
        else
        {
            Message = message;
        }
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }
}

public partial class ResponseModel<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }

    public ResponseModel(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
        Data = default;
    }

    public ResponseModel(T data, int statusCode = 200)
    {
        StatusCode = statusCode;
        Data = data;
        Message = "Success";
    }

    public ResponseModel(string message, int statusCode = 400)
    {
        StatusCode = statusCode;
        Data = default;
        Message = message;
    }
}
