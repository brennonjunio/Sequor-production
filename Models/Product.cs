﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string productCode { get; set; }
    public string productDescription { get; set; }
    public string image { get; set; }
    public decimal cycleTime { get; set; }
    public List<OrderModel> Orders { get; set; }
      public ProductModel() { }
 
}
