using System.Text.RegularExpressions;
using CookieAuth.Services;

namespace CookieAuth.Tests;

/// <summary>
///   Contains unit tests for the
///   <see cref="CookieAuthenticationHelper.SetAuthenticationCookieNameWithHostPrefix(string?)"/>
///   method.
/// </summary>
public class CookieAuthenticationHelper_SetAuthenticationCookieNameWithHostPrefix_Should
{
    private const string AsciiAlphabetCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private const string AsciiDigits = "1234567890";
    private const string AsciiSpecialNonSeparatorCharacters = @"!#$%&'*+-.^_`|~";

    /// <summary>
    ///   <para>
    ///     Ensures that the returned cookie-name starts with the
    ///     <see cref="CookieAuthenticationHelper.HostPrefix"/>
    ///   </para>
    ///   <para>
    ///     The valid characters for a HTTP cookie-name are US-ASCII
    ///     characters except for control characters, space, and tab. In
    ///     addition, the cookie-name MUST NOT contain any separator characters
    ///     such as: ( ) <![CDATA[< >]]> @ , ; : \ " / [ ] ? = { }
    ///   </para>
    /// </summary>
    /// <param name="desiredCookieName">
    ///   The input cookie-name as the test data.
    /// </param>
    [InlineData(null)]
    [InlineData($"{WhiteSpaceConstants.CarriageReturn}{WhiteSpaceConstants.LineFeed}")]
    [InlineData(WhiteSpaceConstants.CarriageReturn)]
    [InlineData(WhiteSpaceConstants.CharacterTabulation)]
    [InlineData(WhiteSpaceConstants.Empty)]
    [InlineData(WhiteSpaceConstants.EmQuad)]
    [InlineData(WhiteSpaceConstants.EmSpace)]
    [InlineData(WhiteSpaceConstants.EnQuad)]
    [InlineData(WhiteSpaceConstants.EnSpace)]
    [InlineData(WhiteSpaceConstants.FigureSpace)]
    [InlineData(WhiteSpaceConstants.FormFeed)]
    [InlineData(WhiteSpaceConstants.FourPerEmSpace)]
    [InlineData(WhiteSpaceConstants.HairSpace)]
    [InlineData(WhiteSpaceConstants.IdeographicSpace)]
    [InlineData(WhiteSpaceConstants.LineFeed)]
    [InlineData(WhiteSpaceConstants.LineSeparator)]
    [InlineData(WhiteSpaceConstants.LineTabulation)]
    [InlineData(WhiteSpaceConstants.MediumMathematicalSpace)]
    [InlineData(WhiteSpaceConstants.NarrowNoBreakSpace)]
    [InlineData(WhiteSpaceConstants.NextLine)]
    [InlineData(WhiteSpaceConstants.NoBreakSpace)]
    [InlineData(WhiteSpaceConstants.OGHamSpaceMark)]
    [InlineData(WhiteSpaceConstants.ParagraphSeparator)]
    [InlineData(WhiteSpaceConstants.PunctuationSpace)]
    [InlineData(WhiteSpaceConstants.SixPerEmSpace)]
    [InlineData(WhiteSpaceConstants.Space)]
    [InlineData(WhiteSpaceConstants.ThinSpace)]
    [InlineData(WhiteSpaceConstants.ThreePerEmSpace)]
    [InlineData(AsciiAlphabetCharacters)]
    [InlineData(AsciiSpecialNonSeparatorCharacters)]
    [InlineData(AsciiDigits)]
    [InlineData($"{CookieAuthenticationHelper.HostPrefix}MyCookie")]
    [InlineData("MyCookie")]
    [Theory]
    public void ReturnACookieNameThatStartsWithTheHostPrefix_Given_AnyValidStartingValue(string? desiredCookieName)
    {
        #region Arrange

        const string expectedPrefix = CookieAuthenticationHelper.HostPrefix;

        #endregion

        #region Act

        string actualResult = CookieAuthenticationHelper
            .SetAuthenticationCookieNameWithHostPrefix(desiredCookieName);

        #endregion

        #region Assert

        Assert.StartsWith(expectedPrefix, actualResult);

        #endregion
    }

    /// <summary>
    ///   <para>
    ///     Ensures that the returned cookie-name is a valid cookie-name
    ///     when the <paramref name="desiredCookieName"/> contains valid
    ///     cookie-name characters.
    ///   </para>
    ///   <para>
    ///     The valid characters for a HTTP cookie-name are US-ASCII
    ///     characters except for control characters, space, and tab. In
    ///     addition, the cookie-name MUST NOT contain any separator characters
    ///     such as: ( ) <![CDATA[< >]]> @ , ; : \ " / [ ] ? = { }
    ///   </para>
    /// </summary>
    /// <param name="desiredCookieName">
    ///   The input cookie-name as the test data.
    /// </param>
    [InlineData(AsciiAlphabetCharacters)]
    [InlineData(AsciiDigits)]
    [InlineData(AsciiSpecialNonSeparatorCharacters)]
    [Theory]
    public void ReturnAValidCookieName_Given_AnyValidStartingValue(string? desiredCookieName)
    {
        #region Arrange

        const string Pattern = @"^[A-Za-z\d!#\$%&'\*\+\-\.\^_`\|~]+$";
        RegexOptions options = RegexOptions.Singleline;
        TimeSpan timeout = TimeSpan.FromSeconds(5);
        Regex expectedCookieNamePattern = new(Pattern, options, timeout);

        #endregion

        #region Act

        string actualCookieName = CookieAuthenticationHelper.SetAuthenticationCookieNameWithHostPrefix(desiredCookieName);

        #endregion

        #region Assert

        Assert.Matches(expectedCookieNamePattern, actualCookieName);

        #endregion
    }

    /// <summary>
    ///  Tests that the value returned is the default cookie-name.
    /// </summary>
    /// <param name="desiredCookieName">
    ///   The whitespace character to test.
    /// </param>
    [InlineData(null)]
    [InlineData($"{WhiteSpaceConstants.CarriageReturn}{WhiteSpaceConstants.LineFeed}")]
    [InlineData(WhiteSpaceConstants.CarriageReturn)]
    [InlineData(WhiteSpaceConstants.CharacterTabulation)]
    [InlineData(WhiteSpaceConstants.Empty)]
    [InlineData(WhiteSpaceConstants.EmQuad)]
    [InlineData(WhiteSpaceConstants.EmSpace)]
    [InlineData(WhiteSpaceConstants.EnQuad)]
    [InlineData(WhiteSpaceConstants.EnSpace)]
    [InlineData(WhiteSpaceConstants.FigureSpace)]
    [InlineData(WhiteSpaceConstants.FormFeed)]
    [InlineData(WhiteSpaceConstants.FourPerEmSpace)]
    [InlineData(WhiteSpaceConstants.HairSpace)]
    [InlineData(WhiteSpaceConstants.IdeographicSpace)]
    [InlineData(WhiteSpaceConstants.LineFeed)]
    [InlineData(WhiteSpaceConstants.LineSeparator)]
    [InlineData(WhiteSpaceConstants.LineTabulation)]
    [InlineData(WhiteSpaceConstants.MediumMathematicalSpace)]
    [InlineData(WhiteSpaceConstants.NarrowNoBreakSpace)]
    [InlineData(WhiteSpaceConstants.NextLine)]
    [InlineData(WhiteSpaceConstants.NoBreakSpace)]
    [InlineData(WhiteSpaceConstants.OGHamSpaceMark)]
    [InlineData(WhiteSpaceConstants.ParagraphSeparator)]
    [InlineData(WhiteSpaceConstants.PunctuationSpace)]
    [InlineData(WhiteSpaceConstants.SixPerEmSpace)]
    [InlineData(WhiteSpaceConstants.Space)]
    [InlineData(WhiteSpaceConstants.ThinSpace)]
    [InlineData(WhiteSpaceConstants.ThreePerEmSpace)]
    [Theory]
    public void ReturnTheDefaultCookieName_Given_TheStartingValueIsNullOrWhiteSpace(string? desiredCookieName)
    {
        #region Arrange

        const string expectedResult = CookieAuthenticationHelper.DefaultCookieName;

        #endregion

        #region Act

        string actualResult = CookieAuthenticationHelper
            .SetAuthenticationCookieNameWithHostPrefix(desiredCookieName);

        #endregion

        #region Assert

        Assert.Equal(expectedResult, actualResult);

        #endregion
    }

    /// <summary>
    ///   Tests that the value returned is the default cookie-name.
    /// </summary>
    [Fact]
    public void ReturnTheDefaultCookieName_Given_TheStartingValueIsOmitted()
    {
        #region Arrange

        const string expectedResult = CookieAuthenticationHelper.DefaultCookieName;

        #endregion

        #region Act

        string actualResult = CookieAuthenticationHelper
            .SetAuthenticationCookieNameWithHostPrefix();

        #endregion

        #region Assert

        Assert.Equal(expectedResult, actualResult);

        #endregion
    }

    /// <summary>
    ///   <para>
    ///     Ensures that an <see cref="ArgumentException"/> is thrown when the
    ///     <paramref name="desiredCookieName"/> contains any characters that
    ///     are not in the valid set of characters for a HTTP cookie-name.
    ///   </para>
    ///   <para>
    ///     The valid characters for a HTTP cookie-name are US-ASCII
    ///     characters except for control characters, space, and tab. In
    ///     addition, the cookie-name MUST NOT contain any separator characters
    ///     such as: ( ) <![CDATA[< >]]> @ , ; : \ " / [ ] ? = { }
    ///   </para>
    /// </summary>
    /// <param name="desiredCookieName">
    ///   The cookie-name with invalid characters to test.
    /// </param>
    [InlineData("(")]
    [InlineData(")")]
    [InlineData("<")]
    [InlineData(">")]
    [InlineData("@")]
    [InlineData(",")]
    [InlineData(";")]
    [InlineData(":")]
    [InlineData(@"\")]
    [InlineData(@"""")]
    [InlineData("/")]
    [InlineData("[")]
    [InlineData("]")]
    [InlineData("?")]
    [InlineData("=")]
    [InlineData("{")]
    [InlineData("}")]
    [Theory]
    public void ThrowAnArgumentException_Given_TheStartingValueContainsAnyInvalidCookieNameCharacter(string? desiredCookieName)
    {
        #region Arrange / Act

        Func<string> actualMethodCall = () =>
            CookieAuthenticationHelper.SetAuthenticationCookieNameWithHostPrefix(desiredCookieName);

        #endregion

        #region Assert

        Assert.Throws<ArgumentException>(actualMethodCall);

        #endregion
    }
}