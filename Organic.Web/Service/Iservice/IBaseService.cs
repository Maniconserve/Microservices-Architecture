using Organic.Web.Models;

namespace Organic.Web.Service.Iservice
{
	public interface IBaseService
	{
		Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
	}
}
