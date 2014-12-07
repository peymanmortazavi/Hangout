
namespace Hangout.Core
{

    /// <summary>
    /// Hangout Claims (for Claims Based Authentication)
    /// </summary>
    public static class HangoutClaims
	{

        /// <summary>
        /// The prefix for claims scheme
        /// </summary>
        const string PREFIX = "http://claims.hangout.com/";

        /// <summary>
        /// The identifier claim scheme
        /// </summary>
        public static readonly string Id = PREFIX + "id";

	}

}