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
                    join productMaterial in db.ProductMaterial
                        on product.productCode equals productMaterial.productCode
                    join materials in db.Material
                        on productMaterial.materialCode equals materials.materialCode
                    orderby order.order
                    select new
                    {
                        order.order,
                        order.quantity,
                        product.productCode,
                        product.image,
                        product.cycleTime,
                        product.productDescription,
                        materials.materialCode,
                        materials.materialDescription
                    };

                var result = await query.ToListAsync();

                var orders = result
                    .GroupBy(
                        item =>
                            new
                            {
                                item.order,
                                item.quantity,
                                item.productCode,
                                item.productDescription,
                                item.image,
                                item.cycleTime
                            }
                    )
                    .Select(
                        groupedItem =>
                            new OrderResponse
                            {
                                order = groupedItem.Key.order,
                                quantity = groupedItem.Key.quantity,
                                productCode = groupedItem.Key.productCode,
                                productDescription = groupedItem.Key.productDescription,
                                image = groupedItem.Key.image,
                                cycleTime = groupedItem.Key.cycleTime,
                                materials = groupedItem
                                    .Select(
                                        materialItem =>
                                            new MaterialResponse
                                            {
                                                materialCode = materialItem.materialCode,
                                                materialDescription =
                                                    materialItem.materialDescription
                                            }
                                    )
                                    .ToList()
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
                response = new ApiResponse<List<OrderResponse>>("201", "E", ex.Message, null);
            }

            return response;
        }
    }
}
