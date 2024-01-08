using Microsoft.EntityFrameworkCore;
using OrdersService;
using sequorProduction.DataContext;

public class CustomValidator
{
    public readonly DataBase db;

    public CustomValidator(DataBase dbContext)
    {
        db = dbContext;
    }

    public async Task<string?> ValidateEmailAsync(string emailParams)
    {
        var validEmail = await db.User.AnyAsync(user => user.email == emailParams);

        if (!validEmail)
        {
            return "Email Não encontrado!";
        }
        return null;
    }

    public async Task<string?> ValidateOrderAsync(string orderParams)
    {
        var validOrder = await db.Order.Where(order => order.order == orderParams).AnyAsync();

        if (!validOrder)
        {
            throw new Exception("Ordem Não encontrada!");
        }
        return null;
    }

    public async Task<string?> ValidateMaterialInOrderAsync(
        string orderParams,
        string materialCodeParams
    )
    {
        var validMaterial = await (
            from order in db.Order
            join product in db.Product on order.productCode equals product.productCode
            join productMaterial in db.ProductMaterial
                on product.productCode equals productMaterial.productCode
            join materials in db.Material
                on productMaterial.materialCode equals materials.materialCode
            orderby order.order
            where order.order == orderParams && productMaterial.materialCode == materialCodeParams
            select new
            {
                order.order,
                order.quantity,
                product.productCode,
                product.cycleTime,
            }
        ).FirstOrDefaultAsync();

        if (validMaterial == null)
        {
            throw new Exception("Material não consta lista de materiais da Ordem ");
        }
        return null;
    }

    public async Task<string?> ValidateQuantityAsync(string orderParams, decimal quantityParams)
    {
        if (quantityParams == 0)
        {
            throw new Exception("Quantidade deve ser maior que 0");
        }
        var validQuantity = await db.Order.Join(
            db.ProductMaterial,
            a => a.productCode,
            b => b.productCode,
            (a, b) => new { a, b }
        )
            .Where(joinResult => joinResult.a.order == orderParams)
            .GroupBy(joinResult => joinResult.a.order)
            .Select(
                groupedResult =>
                    new
                    {
                        Order = groupedResult.Key,
                        Quantity = groupedResult.Sum(result => result.a.quantity)
                    }
            )
            .FirstOrDefaultAsync();

        if (validQuantity != null && quantityParams > validQuantity.Quantity)
        {
            throw new Exception("Quantidade deve ser menor ou igual à quantidade da ordem.");
        }

        return null;
    }

    public async Task<string?> ValidateDateAsync(string emailParams, DateTime productionDateParams)
    {
        var validDate = await db.User.Where(
            user =>
                user.email == emailParams
                && user.initialDate <= productionDateParams
                && user.endDate >= productionDateParams
        )
            .AnyAsync();

        if (!validDate)
        {
            throw new Exception("Data de apontamento diferente da periodo definado para usuario!");
        }
        return null;
    }

    public async Task<string?> ValidateCycleTimeAsync(string orderParams, decimal cycleTimeParams)
    {
        if (cycleTimeParams <= 0)
        {
            throw new Exception("Tempo de ciclo informado deve ser maior que 0");
        }
        var validCycle = await (
            from order in db.Order
            join product in db.Product on order.productCode equals product.productCode
            join productMaterial in db.ProductMaterial
                on product.productCode equals productMaterial.productCode
            join materials in db.Material
                on productMaterial.materialCode equals materials.materialCode
            orderby order.order
            where order.order == orderParams
            select new
            {
                order.order,
                order.quantity,
                product.productCode,
                product.cycleTime,
            }
        ).FirstOrDefaultAsync();

        if (validCycle != null && cycleTimeParams < validCycle.cycleTime)
        {
            
              return  "Sucesso ao gerar, OBS: Tempo de ciclo informado é menor que o cadastrado para usuario";
            
        }
        return null;
    }

    public async Task<string?> ValidationProduction(ValidationConfig config)
    {
        switch (config.ValidationType)
        {
            case ValidationType.Email:
                return await ValidateEmailAsync(config.Email);
            case ValidationType.Order:
                return await ValidateOrderAsync(config.Order);
            case ValidationType.MaterialInOrder:
                return await ValidateMaterialInOrderAsync(config.Order, config.MaterialCode);
            case ValidationType.Quantity:
                return await ValidateQuantityAsync(config.Order, config.Quantity);
            case ValidationType.Date:
                return await ValidateDateAsync(config.Email, config.ProductionDate);
            case ValidationType.CycleTime:
                return await ValidateCycleTimeAsync(config.Order, config.CycleTime);
            default:
                return null;
        }
    }

    public class ValidationConfig
    {
        public ValidationType ValidationType { get; set; }
        public string Email { get; set; }
        public string Order { get; set; }
        public string MaterialCode { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ProductionDate { get; set; }
        public decimal CycleTime { get; set; }
    }

    public enum ValidationType
    {
        Email,
        Order,
        MaterialInOrder,
        Quantity,
        Date,
        CycleTime
    }
}
