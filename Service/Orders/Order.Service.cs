using Microsoft.EntityFrameworkCore;
using sequorProduction.DataContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersService
{
    public class Order : IOrdersInterface
    {
        private readonly DataBase db;

        public Order(DataBase db)
        {
            this.db = db;
        }

        public async Task<ApiResponse<List<OrderModel>>> GetOrders()
        {
            ApiResponse<List<OrderModel>> response;

            try
            {
                List<OrderModel> orders = await db.Order.ToListAsync();
                response = new ApiResponse<List<OrderModel>>("200", "S", "Ordens Listadas com sucesso!", orders);

            }
            catch (System.Exception ex)
            {
                response = new ApiResponse<List<OrderModel>>("201", "E", ex.Message,null);

            }

            return response;
        }
    }
}
