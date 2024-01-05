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

        public async Task<List<OrderModel>> GetOrders()
        {
            List<OrderModel> orders = new List<OrderModel>();

            try
            {
                orders = await db.Order.ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orders;
        }
    }
}
