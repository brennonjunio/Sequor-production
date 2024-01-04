using System.ComponentModel.DataAnnotations;

public class ProductMaterial
{

    [Key]
    public string ProductCode { get; set; }

    [Key]
    public string MaterialCode { get; set; }

    public ProductMaterial(string ProductCode,
string MaterialCode)

    {
        this.ProductCode = ProductCode;
        this.MaterialCode = MaterialCode;
    }
}
