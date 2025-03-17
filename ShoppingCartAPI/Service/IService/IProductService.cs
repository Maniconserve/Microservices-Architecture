using Organic.Services.ShoppingCartAPI.Models.Dto;

namespace ShoppingCartAPI.Service.IService
{
	public interface IProductService
	{
		Task<IEnumerable<ProductDto>> GetProducts();
	}
}
