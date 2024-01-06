using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string order { get; set; }
    public decimal quantity { get; set; }
    public string productCode { get; set; }

    [ForeignKey("productCode")]
    public ProductModel product { get; set; }

    public OrderModel() { }

    public OrderModel(string order, decimal quantity, string productCode, ProductModel product)
    {
        this.order = order;
        this.quantity = quantity;
        this.productCode = productCode;
        this.product = product;
    }
}
