﻿namespace Organic.Web.Service.Iservice
{
	public interface ITokenProvider
	{
     	void SetToken(string token);
		string? GetToken();
		void ClearToken();
	}
}
