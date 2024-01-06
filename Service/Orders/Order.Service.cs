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

        public async Task<object> GetOrders()
        {
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

                var groupedOrders = result
                    .GroupBy(
                        item =>
                            new
                            {
                                item.order,
                                item.productCode,
                                item.productDescription,
                                item.image,
                                item.cycleTime
                            }
                    )
                    .Select(
                        groupedItem =>
                            new ResponseOrder
                            {
                                order = groupedItem.Key.order,
                                quantity = groupedItem.Sum(item => item.quantity),
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

                var response = new
                {
                    status = "200",
                    type = "S",
                    description = "Ordens Listadas com sucesso!",
                    orders = groupedOrders
                };

                return response;
            }
            catch (Exception ex)
            {
                return new
                {
                    status = "201",
                    type = "E",
                    description = ex.Message,
                    orders = new List<ResponseOrder>()
                };
            }
        }

        public async Task<object> GetProduction(string emailParams)
        {
            try
            {
                var validEmail =
                    from user in db.User
                    where user.email == emailParams
                    select new { user.name };
                var resultEmail = await validEmail.ToListAsync();
                if (resultEmail.Count == 0)
                {
                    throw new Exception("AAAAAAAAAAAAAA");
                }

                var query =
                    from p in db.Production
                    where p.email == emailParams
                    orderby p.id
                    select new
                    {
                        productions = new List<ResponseProduction>
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
                if (result.Count > 0)
                {
                    return new
                    {
                        status = "200",
                        type = "S",
                        description = "Produções Listadas Com Sucesso!",
                        productions = result.SelectMany(r => r.productions).ToList()
                    };
                }
                else
                {
                    return new
                    {
                        status = "201",
                        type = "E",
                        description = "Nenhum resultado encontrado.",
                    };
                }
            }
            catch (System.Exception ex)
            {
                return new
                {
                    status = "201",
                    type = "E",
                    description = ex.Message,
                };
            }
        }

        public async Task<object> SetProduction(
            string emailParams,
            string orderParams,
            DateTime productionDateParams,
            decimal quantityParams,
            string materialCodeParams,
            decimal cycleTimeParams
        )
        {
            try
            {
                var customValidator = new CustomValidator(db);

                await customValidator.ValidateEmailAsync(emailParams);
                await customValidator.ValidateOrderAsync(orderParams);
                await customValidator.ValidateMaterialInOrderAsync(orderParams, materialCodeParams);
                await customValidator.ValidateQuantityAsync(orderParams, quantityParams);
                await customValidator.ValidateDateAsync(emailParams, productionDateParams);
                await customValidator.ValidateCycleTimeAsync(orderParams, cycleTimeParams);

                return new
                {
                    status = "200",
                    type = "S",
                    description = "Produção cadastrada com sucesso!",
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    status = "201",
                    type = "E",
                    description = ex.Message,
                };
            }
        }
    }
}
