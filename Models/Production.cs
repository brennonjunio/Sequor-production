using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Production
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string email { get; set; }
    public string order { get; set; }
    public string date { get; set; }
    public int quantity { get; set; }
    public string materialCode { get; set; }
    public string cycleTime { get; set; }

    public Production(

        int id,
        string email,
        string order,
        string date,
        int quantity,
        string materialCode,
        string cycleTime
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
