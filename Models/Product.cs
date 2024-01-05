using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
