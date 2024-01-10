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
                    status = 200,
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
                    status = 500,
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
                var customValidator = new CustomValidator(db);
                string? validationMessage = await customValidator.ValidateEmailAsync(emailParams);
                if (validationMessage != null)
                {
                    throw new Exception(validationMessage);
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
                        status = 200,
                        type = "S",
                        description = "Produções Listadas Com Sucesso!",
                        productions = result.SelectMany(r => r.productions).ToList()
                    };
                }
                else
                {
                    return new
                    {
                        status = 404,
                        type = "E",
                        description = "Nenhum resultado encontrado.",
                    };
                }
            }
            catch (System.Exception ex)
            {
                return new
                {
                    status = 200,
                    type = "E",
                    description = ex.Message,
                };
            }
        }

        public async Task<object> SetProduction(
            string emailParams,
            string orderParams,
            string productionDateParams,
            string productionTimeParams,
            decimal quantityParams,
            string materialCodeParams,
            decimal cycleTimeParams
        )
        {
            try
            {
                var combinedDateTime = DateTime.Parse(
                    $"{productionDateParams} {productionTimeParams}"
                );
                Console.WriteLine($"Caiuuuuuuuuuuu {productionDateParams}");
                var customValidator = new CustomValidator(db);

                var validationConfigurations = new List<CustomValidator.ValidationConfig>
                {
                    new CustomValidator.ValidationConfig
                    {
                        ValidationType = CustomValidator.ValidationType.Email,
                        Email = emailParams
                    },
                    new CustomValidator.ValidationConfig
                    {
                        ValidationType = CustomValidator.ValidationType.Order,
                        Order = orderParams
                    },
                    new CustomValidator.ValidationConfig
                    {
                        ValidationType = CustomValidator.ValidationType.MaterialInOrder,
                        Order = orderParams,
                        MaterialCode = materialCodeParams
                    },
                    new CustomValidator.ValidationConfig
                    {
                        ValidationType = CustomValidator.ValidationType.Quantity,
                        Order = orderParams,
                        Quantity = quantityParams
                    },
                    new CustomValidator.ValidationConfig
                    {
                        ValidationType = CustomValidator.ValidationType.Date,
                        Email = emailParams,
                        ProductionDate = combinedDateTime
                    },
                };

                foreach (var config in validationConfigurations)
                {
                    var validationMessage = await customValidator.ValidationProduction(config);
                    if (validationMessage != null)
                    {
                        return new
                        {
                            status = 200,
                            type = "E",
                            description = validationMessage
                        };
                    }
                }
                var valid = await customValidator.ValidateCycleTimeAsync(
                    orderParams,
                    cycleTimeParams
                );
                var message = "Produção cadastrada com sucesso!";
                if (valid != null)
                {
                    message = valid;
                }
                var newProduction = new ProductionModel(
                    emailParams,
                    orderParams,
                    combinedDateTime,
                    quantityParams,
                    materialCodeParams,
                    cycleTimeParams
                );
                db.Production.AddRange(newProduction);
                db.SaveChanges();
                return new
                {
                    status = 200,
                    type = "S",
                    description = message,
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    status = 404,
                    type = "E",
                    description = ex.Message,
                };
            }
        }
    }
}
