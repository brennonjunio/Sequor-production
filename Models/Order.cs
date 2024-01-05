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
	public OrderModel(
		string order,
		float quantity,
		string productCode
	)
	{
		this.order = order;
		this.quantity = quantity;
		this.productCode = productCode;
	}
}

