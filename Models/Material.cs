using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Material
{
    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string materialCode { get; set; }

    public string materialDescription { get; set; }

    public Material(string materialCode, string materialDescription)
    {
        this.materialCode = materialCode;
        this.materialDescription = materialDescription;
    }
}
