using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using UITemplate.Model.ExceptionHelper;
using UITemplate.Model.Result;

namespace UITemplate.UI.Services
{
	public class BaseService<TEntity>
	{
		private readonly ResponseChecker _responseChecker;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public BaseService(ResponseChecker responseChecker, IHttpContextAccessor httpContextAccessor)
		{
			_responseChecker = responseChecker;
			_httpContextAccessor = httpContextAccessor;
		}

		private RestRequest CreateRequest(string urlTag, TEntity entity,bool includeToken)
		{
			var url = "https://localhost:7272/api/" + urlTag;
			var request = new RestRequest(url, Method.Post);
			request.AddHeader("Content-Type", "application/json");

			if (includeToken)
			{
				var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
				request.AddHeader("Authorization", "Bearer " + token);
			}

			var body = JsonConvert.SerializeObject(entity);
			request.AddBody(body, "application/json");

			return request;
		}

		public async Task<ApiResponse<TEntity>> PostAsync(string urlTag, TEntity entity,bool includeToken = true)
		{
			var client = new RestClient();
			var request = CreateRequest(urlTag, entity,includeToken);

			RestResponse response = await client.ExecuteAsync(request);

			if (response.StatusCode != HttpStatusCode.OK)
			{
				_responseChecker.Checker((int)response.StatusCode);
			}

			var responseObject = JsonConvert.DeserializeObject<ApiResponse<TEntity>>(response.Content);

			return responseObject;
		}

		public async Task<ApiResponse<List<TEntity>>> PostAsyncList(string urlTag, TEntity entity,bool includeToken = true)
		{
			var client = new RestClient();
			var request = CreateRequest(urlTag, entity,includeToken);


			RestResponse response = await client.ExecuteAsync(request);

			if (response.StatusCode != HttpStatusCode.OK)
			{
				_responseChecker.Checker((int)response.StatusCode);
			}

			var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<TEntity>>>(response.Content);


			return responseObject;
		}

	}
}

