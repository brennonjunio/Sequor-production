public class ApiResponse<T>
{
    public string Status { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public T? Data { get; set; }
    public ApiResponse(
            string Status,
            string Type,
            string Description,
            T Data
    )
    {
        this.Status = Status;
        this.Type = Type;
        this.Description = Description;
        this.Data = Data;
    }
}
