using System;
using System.ComponentModel.DataAnnotations;

public class Order
{
	[Key]
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
