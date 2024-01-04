using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public string productCode { get; set; }
    public string productDescription { get; set; }
    public string image { get; set; }
    public float cycleTime { get; set; }
    public Product(
        string productCode,
        string productDescription,
        string image,
        float cycleTime
    )
    {
        this.productCode = productCode;
        this.productDescription = productDescription;
        this.image = image;
        this.cycleTime = cycleTime;

    }
}
