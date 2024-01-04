using System.ComponentModel.DataAnnotations;

public class Material
{
    [Key]
    public string materialCode { get; set; }

    public string materialDescription { get; set; }

    public Material(string materialCode, string materialDescription)
    {
        this.materialCode = materialCode;
        this.materialDescription = materialDescription;
    }
}
