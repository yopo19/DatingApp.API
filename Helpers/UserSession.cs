using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public class UserSession : IUserSession
    {

        private readonly IHttpContextAccessor _http;

        public UserSession(IHttpContextAccessor http)
        {
            _http = http;


        }
        public string UserId { get { return _http.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value; } }
        public string UserIP { get { return _http.HttpContext.Connection.RemoteIpAddress.ToString(); } }


    }
}