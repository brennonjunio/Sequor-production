using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sequorProduction.DataContext;

namespace OrdersService
{
    public class Order : IOrdersInterface
    {
        private readonly DataBase db;

        public Order(DataBase db)
        {
            this.db = db;
        }

  public async Task<ApiResponse<List<OrderResponse>>> GetOrders()
    {
        ApiResponse<List<OrderResponse>> response;

        try
        {
            var query =
                from order in db.Order
                join product in db.Product on order.productCode equals product.productCode
                select new
                {
                    order.order,
                    order.quantity,
                    product.productCode,
                    product.image,
                    product.cycleTime,
                    product.productDescription
                };

            var result = await query.ToListAsync();

           var orders = result
            .Select(
                item =>
                    new OrderResponse
                    {
                        order = item.order,
                        quantity = item.quantity,
                        productCode = item.productCode,
                        productDescription = item.productDescription, // Adicione esta linha
                        image = item.image, // Adicione esta linha
                        cycleTime = item.cycleTime // Adicione esta linha
                    }
            )
            .ToList();


            response = new ApiResponse<List<OrderResponse>>(
                "200",
                "S",
                "Ordens Listadas com sucesso!",
                orders
                // orders
            );
        }
        catch (Exception ex)
        {
            response = new ApiResponse<List<OrderResponse>>(
                "201",
                "E",
                ex.Message,
                null
            );
        }

        return response;
    }
}
}
