using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderResponse
{
    public string order { get; set; }
    public decimal  quantity { get; set; }
    public string productCode { get; set; }
        public string productDescription { get; set; } // Adicione esta linha
        public string image { get; set; } // Adicione esta linha
        public decimal cycleTime { get; set; } // Adicione esta linha


    // Construtor sem parâmetros necessário para o Entity Framework
    public OrderResponse() { }

    public OrderResponse(string order, decimal  quantity, string productCode, ProductModel product)
    {
        this.order = order;
        this.quantity = quantity;
        this.productCode = productCode;
    }
}
