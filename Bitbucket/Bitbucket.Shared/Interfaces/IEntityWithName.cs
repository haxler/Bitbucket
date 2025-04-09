namespace Bitbucket.Shared.Interfaces;

public interface IEntityWithName
{
    string Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}