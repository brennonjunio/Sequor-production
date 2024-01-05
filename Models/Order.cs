using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string order { get; set; }
	public int quantity { get; set; }
	public string productCode { get; set; }
	public Order(
		string order,
		int quantity,
		string productCode
	)
	{
		this.order = order;
		this.quantity = quantity;
		this.productCode = productCode;
	}
}
