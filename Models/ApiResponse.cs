public class ApiResponse<T>
{
    public string Status { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public ApiResponse(
            string Status,
            string Type,
            string Message,
            T Data
    )
    {
        this.Status = Status;
        this.Type = Type;
        this.Message = Message;
        this.Data = Data;
    }
}
