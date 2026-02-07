namespace Catalog.API.Product.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResult>;


public record CreateProductResult(Guid Id);

// Use martin for the database, but you can use any ORM or database access method you prefer
internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // create product entity from command object
        // Save to database,
        // Return CreateProductResult
        var product = new Models.Product
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };


        // Save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        // return result
        //return new CreateProductResult(Guid.NewGuid());
        return new CreateProductResult(product.Id);
    }
}
