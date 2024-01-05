using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MaterialModel
{
    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string materialCode { get; set; }

    public string materialDescription { get; set; }

    public MaterialModel(string materialCode, string materialDescription)
    {
        this.materialCode = materialCode;
        this.materialDescription = materialDescription;
    }
}
