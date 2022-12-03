using System.Text.RegularExpressions;

namespace CookieAuth.Services;

/// <summary>
///   Provides helper methods for configuring cookie based authentication
///   schemes.
/// </summary>
public class CookieAuthenticationHelper
{
    /// <summary>
    ///   The default cookie-name for cookie authentication schemes.
    /// </summary>
    public const string DefaultCookieName = $"{HostPrefix}Id";

    /// <summary>
    ///   <para>
    ///     A standard cookie-name prefix defined in the HTTP specification.
    ///   </para>
    ///   <para>
    ///     This prefix means a compliant user agent will only set the cookie
    ///     when the <c>Secure</c> and <c>Path</c> attributes of the
    ///     <c>Set-Cookie</c> HTTP response header are used, and the
    ///     <c>Domain</c> attribute is not used. The value of the <c>Path</c>
    ///     attribute MUST be <c>"/"</c>.
    ///   </para>
    /// </summary>
    public const string HostPrefix = "__Host-";

    /// <summary>
    ///   <para>
    ///     Provides the name of the cookie used for identificaiton in the
    ///     cookie authuthentication scheme being configured. The default name
    ///     of the cookie is <c>__Host-Id</c>. When a
    ///     <paramref name="startingValue"/> is provided, the method ensures
    ///     the <c>__Host-</c> cookie-name prefix either exists or is prepended
    ///     to the <paramref name="startingValue"/>.
    ///   </para>
    ///   <example>
    ///     The default examples each return the string
    ///     <c>"__Host-Id"</c>
    ///     <code>
    ///       SetAuthenticationCookieNameWithHostPrefix();
    ///       SetAuthenticationCookieNameWithHostPrefix("");
    ///       SetAuthenticationCookieNameWithHostPrefix(" ");
    ///       SetAuthenticationCookieNameWithHostPrefix(null);
    ///     </code>
    ///   </example>
    ///   <example>
    ///     The additional examples each return the string
    ///     <c>"__Host-MyCookie"</c>
    ///     <code>
    ///       SetAuthenticationCookieNameWithHostPrefix("MyCookie");
    ///       SetAuthenticationCookieNameWithHostPrefix("__Host-MyCookie");
    ///     </code>
    ///   </example>
    /// </summary>
    /// <param name="startingValue">The desired name of the cookie.</param>
    /// <returns>
    ///   The cookie-name with the <c>__Host-</c> cookie-name prefix.
    /// </returns>
    /// <exception cref="ArgumentException"/>
    public static string SetAuthenticationCookieNameWithHostPrefix(string? startingValue = default)
	{
        if (string.IsNullOrWhiteSpace(startingValue))
            return DefaultCookieName;

        const string ErrorMessage = "Invalid cookie-name detected.";
        const string Pattern = @"^[A-Za-z\d!#\$%&'\*\+\-\.\^_`\|~]+$";
        RegexOptions options = RegexOptions.Singleline;
        Regex validCookieNameCharacters = new(Pattern, options);
        if (!validCookieNameCharacters.IsMatch(startingValue))
            throw new ArgumentException(ErrorMessage, nameof(startingValue));

        string cookieName = startingValue.StartsWith(HostPrefix)
            ? startingValue
            : $"{HostPrefix}{startingValue}";

        return cookieName;
	}
}

