using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductionModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string email { get; set; }
    public string order { get; set; }
    public DateTime date { get; set; }
    public decimal quantity { get; set; }
    public string materialCode { get; set; }
    public decimal cycleTime { get; set; }

    public ProductionModel(
        int id,
        string email,
        string order,
        DateTime date,
        decimal quantity,
        string materialCode,
        decimal cycleTime
    )
    {
        this.id = id;
        this.email = email;
        this.order = order;
        this.date = date;
        this.quantity = quantity;
        this.materialCode = materialCode;
        this.cycleTime = cycleTime;
    }
}
