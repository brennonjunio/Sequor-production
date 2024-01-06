public class OrderApiResponseModel
{
    public string Order { get; set; }
    public float Quantity { get; set; }
    public string ProductCode { get; set; }
    public string ProductDescription { get; set; }
    public string Image { get; set; }
    public float CycleTime { get; set; }
    public List<MaterialModel> Materials { get; set; }

    public OrderApiResponseModel(
string Order,
float Quantity,
string ProductCode,
string ProductDescription,
string Image,
float CycleTime,
List<MaterialModel> Materials
    )
    {
        this.Order = Order;
        this.Quantity = Quantity;
        this.ProductCode = ProductCode;
        this.ProductDescription = ProductDescription;
        this.Image = Image;
        this.CycleTime = CycleTime;
        this.Materials = Materials;
    }
}
