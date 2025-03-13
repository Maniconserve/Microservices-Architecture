using System.Net;
using System.Text;
using Newtonsoft.Json;
using Organic.Web.Models;
using Organic.Web.Service.Iservice;

namespace Organic.Web.Service
{
	public class BaseService : IBaseService
	{
		private IHttpClientFactory _httpClientFactory;
		public BaseService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
		{
			try
			{
				HttpClient client = _httpClientFactory.CreateClient("OrganicAPI");
				HttpRequestMessage message = new();
				message.Headers.Add("Accept", "application/json");
				message.RequestUri = new Uri(requestDto.Url);
				if (requestDto.Data != null)
				{
					message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
				}
				HttpResponseMessage? apiResponse = null;
				switch (requestDto.ApiType)
				{
					case Utility.SD.ApiType.GET:
						message.Method = HttpMethod.Get;
						break;
					case Utility.SD.ApiType.POST:
						message.Method = HttpMethod.Post;
						break;
					case Utility.SD.ApiType.PUT:
						message.Method = HttpMethod.Put;
						break;
					case Utility.SD.ApiType.DELETE:
						message.Method = HttpMethod.Delete;
						break;
				}
				apiResponse = await client.SendAsync(message);
				switch (apiResponse.StatusCode)
				{
					case HttpStatusCode.NotFound:
						return new() { IsSuccess = false, Message = "NotFound" };
					case HttpStatusCode.Forbidden:
						return new() { IsSuccess = false, Message = "Access Denied" };
					case HttpStatusCode.Unauthorized:
						return new() { IsSuccess = false, Message = "UnAuthorized" };
					case HttpStatusCode.InternalServerError:
						return new() { IsSuccess = false, Message = "InternalServerError" };
					default:
						var apiContent = await apiResponse.Content.ReadAsStringAsync();
						var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
						return apiResponseDto;

				}
			}
			catch (Exception ex)
			{
				var dto = new ResponseDto
				{
					Message = ex.Message,
					IsSuccess = false
				};
				return dto;
			}
		}
	}
}
