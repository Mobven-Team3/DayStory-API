using System.Text.Json;

namespace DayStory.WebAPI.Models;

public partial class ResponseModel
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public ResponseModel(string message = null)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            Success = true;
        }
        else
        {
            Success = false;
            Message = message;
        }
    }
    public bool Success { get; set; }
    public string Message { get; set; }
}

public partial class ResponseModel<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Response { get; set; }

    public ResponseModel(bool isSuccess)
    {
        Success = isSuccess;
        Response = default;
        Message = isSuccess ? "Success" : "Error";
    }
    public ResponseModel(T data)
    {
        Success = true;
        Response = data;
        Message = "Success";
    }
    public ResponseModel(string message)
    {
        Success = false;
        Response = default;
        Message = message;
    }
}
