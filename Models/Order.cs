using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string order { get; set; }
    public float quantity { get; set; }
    public string productCode { get; set; }

    // Propriedade de navegação
    [ForeignKey("productCode")]
    public ProductModel product { get; set; }

    // Construtor sem parâmetros necessário para o Entity Framework
    public OrderModel() { }

    public OrderModel(string order, float quantity, string productCode, ProductModel product)
    {
        this.order = order;
        this.quantity = quantity;
        this.productCode = productCode;
        this.product = product;
    }
}
