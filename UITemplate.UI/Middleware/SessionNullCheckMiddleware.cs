using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UITemplate.UI.Middleware
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class SessionNullCheckMiddleware
	{
		private readonly RequestDelegate _next;

		public SessionNullCheckMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			
			var path = httpContext.Request.Path.Value;
			var protectedPaths = new List<string>
		{
			"/admin/anasayfa", "/admin/users"
		};

			if (protectedPaths.Any(p => path.Contains(p)))
			{
				if (httpContext.Session.GetString("Id") == null)
				{
					httpContext.Response.Redirect("/admin/login");
					return;
				}
			}

			await _next(httpContext);
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class SessionNullCheckMiddlewareExtensions
	{
		public static IApplicationBuilder UseSessionNullCheckMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<SessionNullCheckMiddleware>();
		}
	}
}
