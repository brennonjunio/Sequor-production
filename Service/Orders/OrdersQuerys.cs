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

    public async Task ValidateEmailAsync(string emailParams)
    {
        var validEmail = await db.User.Where(user => user.email == emailParams).AnyAsync();

        if (!validEmail)
        {
            throw new Exception("Email Não encontrado!");
        }
    }

    public async Task ValidateOrderAsync(string orderParams)
    {
        var validOrder = await db.Order.Where(order => order.order == orderParams).AnyAsync();

        if (!validOrder)
        {
            throw new Exception("Ordem Não encontrada!");
        }
    }

    public async Task ValidateMaterialInOrderAsync(string orderParams, string materialCodeParams)
    {
        var validMaterial = await db.Order.Where(
            order => order.order == orderParams && order.productCode == materialCodeParams
        )
            .AnyAsync();

        if (!validMaterial)
        {
            throw new Exception("Material não consta na ordem!");
        }
    }

    public async Task ValidateQuantityAsync(string orderParams, decimal quantityParams)
    {
        var validQuantity = await db.Order.Where(
            order =>
                order.order == orderParams && quantityParams > 0 && quantityParams <= order.quantity
        )
            .AnyAsync();

        if (!validQuantity)
        {
            throw new Exception("Quantidade diferente do esperado");
        }
    }

    public async Task ValidateDateAsync(string emailParams, DateTime productionDateParams)
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
            throw new Exception("Data de apontamento diferente da data!");
        }
    }

    public async Task ValidateCycleTimeAsync(string orderParams, decimal cycleTimeParams)
    {
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
            Console.WriteLine(
                "Aviso: Tempo de ciclo informado é menor que o tempo cadastrado no produto."
            );
        }
    }
}
