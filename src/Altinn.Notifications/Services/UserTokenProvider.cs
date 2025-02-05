﻿using System.Diagnostics.CodeAnalysis;

using Altinn.Notifications.Configuration;
using Altinn.Notifications.Integrations;

using AltinnCore.Authentication.Utils;

using Microsoft.Extensions.Options;

namespace Altinn.Notifications.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IUserTokenProvider"/> using the HttpContext to obtain
    /// the JSON Web Token needed for the application to make calls to other services.
    /// </summary>
    /// <remarks>
    /// This class is excluded from code doverage because we have no good way of mocking the HttpContext.
    /// There are also very little code to test as most of the logic are in an imported package.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class UserTokenProvider : IUserTokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _jwtCookieName;

        /// <summary>
        /// Initialize a new instance of the <see cref="UserTokenProvider"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">A service providing access to the http context.</param>
        /// <param name="generalSettings">The application general settings.</param>
        public UserTokenProvider(IHttpContextAccessor httpContextAccessor, IOptions<GeneralSettings> generalSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtCookieName = generalSettings.Value.JwtCookieName;
        }

        /// <summary>
        /// Get the current JSON Web Token found on the HttpContext.
        /// </summary>
        /// <returns>The JSON Web Token of the current user.</returns>
        public string GetUserToken()
        {
            return JwtTokenUtil.GetTokenFromContext(_httpContextAccessor.HttpContext, _jwtCookieName);
        }
    }
}
