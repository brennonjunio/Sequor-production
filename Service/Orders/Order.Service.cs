using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
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
                                Orders = groupedItem
                                    .Select(
                                        orderItem =>
                                            new ResponseOrder
                                            {
                                                order = orderItem.order,
                                                quantity = orderItem.quantity,
                                                productCode = orderItem.productCode,
                                                productDescription = orderItem.productDescription,
                                                image = orderItem.image,
                                                cycleTime = orderItem.cycleTime,
                                                materials = groupedItem
                                                    .Select(
                                                        materialItem =>
                                                            new MaterialResponse
                                                            {
                                                                materialCode =
                                                                    materialItem.materialCode,
                                                                materialDescription =
                                                                    materialItem.materialDescription
                                                            }
                                                    )
                                                    .ToList()
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
                );
            }
            catch (Exception ex)
            {
                response = new ApiResponse<List<OrderResponse>>("201", "E", ex.Message, null);
            }

            return response;
        }

        public async Task<ApiResponse<List<ProductionResponse>>> GetProduction(string emailParams)
        {
            ApiResponse<List<ProductionResponse>> response;

            try
            {
                var validEmail =
                    from user in db.User
                    where user.email == emailParams
                    select new { user.name };
                var resultEmail = await validEmail.ToListAsync();
                if (resultEmail.Count == 0)
                {
                    response = new ApiResponse<List<ProductionResponse>>(
                        "201",
                        "E",
                        "E-mail não encontrado",
                        null
                    );
                    return response;
                }

                var query =
                    from p in db.Production
                    where p.email == emailParams
                    orderby p.id
                    select new ProductionResponse
                    {
                        Productions = new List<ResponseProduction>
                        {
                            new ResponseProduction
                            {
                                order = p.order,
                                date = p.date,
                                quantity = p.quantity,
                                materialCode = p.materialCode,
                                cycleTime = p.cycleTime
                            }
                        }
                    };

                var result = await query.ToListAsync();
                Console.WriteLine(result);
                response = new ApiResponse<List<ProductionResponse>>(
                    "200",
                    "S",
                    "Produções Listadas Com Sucesso!",
                    result
                );
            }
            catch (System.Exception ex)
            {
                response = new ApiResponse<List<ProductionResponse>>("201", "E", ex.Message, null);
            }

            return response;
        }
    }
}
