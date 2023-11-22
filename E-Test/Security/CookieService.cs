using Microsoft.AspNetCore.Mvc;

namespace E_Test.Security
{
	public class CookieService : TokenService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CookieService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public void AddCookieToken(string key, string value)
		{
			_httpContextAccessor.HttpContext.Response.Cookies.Append(key, value);
		}
		public void AddCookieTokenExpired(string key, string value)
		{
			if (IsTokenExpired(value))
			{
				DeleteCookie(key);
				_httpContextAccessor.HttpContext.Response.Cookies.Append(key, value);
			}
		}
		public string FindCookies(string key)
		{
			var result = _httpContextAccessor.HttpContext.Request.Cookies[key];
			if (result != null)
				return result;

			return null;
		}
		public void DeleteCookie(string key)
		{
			_httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
		}
		public bool ValidateToken(string token)
		{
			return IsValidToken(token);
		}
		public TokenDecoder TokenValues(string token)
		{
			return DecodeToken(token);
		}
	}

}
