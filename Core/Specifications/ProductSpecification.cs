using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams specParams) : base (
        x => (!specParams.Brands.Any() || specParams.Brands.Contains(x.Brand)) &&
             (!specParams.Types.Any() || specParams.Types.Contains(x.Type))
    )
    {

        ApplyPaging ((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        switch (specParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }
}