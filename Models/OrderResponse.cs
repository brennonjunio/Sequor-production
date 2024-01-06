using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderResponse
{
    public List<ResponseOrder> Orders { get; set; }

    public OrderResponse() { }
}

public class ResponseOrder
{
    public string order { get; set; }
    public decimal quantity { get; set; }
    public string productCode { get; set; }
    public string productDescription { get; set; }
    public string image { get; set; }
    public decimal cycleTime { get; set; }
    public List<MaterialResponse> materials { get; set; }
}

public class MaterialResponse
{
    public string materialCode { get; set; }
    public string materialDescription { get; set; }
}
