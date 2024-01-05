using System.ComponentModel.DataAnnotations;

public class ProductMaterial
{
    [Key]
    public string productCode { get; set; }

    [Key]
    public string materialCode { get; set; }

    public ProductMaterial(string productCode, string materialCode)
    {
        this.productCode = productCode;
        this.materialCode = materialCode;
    }
}