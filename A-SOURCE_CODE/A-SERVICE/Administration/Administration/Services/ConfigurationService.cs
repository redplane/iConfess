using System.Configuration;
using Administration.Interfaces.Services;

namespace Administration.Services
{
    public class ConfigurationService : IConfigurationService
    {
        #region Properties

        /// <summary>
        /// Time when the token should be expired.
        /// </summary>
        public int TokenExpiration
        {
            get { return _tokenExpiration;}
            set { _tokenExpiration = value; }
        }

        /// <summary>
        /// Forgot password token expiration
        /// </summary>
        public int ForgotPasswordTokenExpiration
        {
            get { return _forgotPasswordTokenExpiration; }

            set { _forgotPasswordTokenExpiration = value; }
        }

        /// <summary>
        /// Time when the token should be expired.
        /// </summary>
        private int _tokenExpiration;

        /// <summary>
        /// Lifetime of forgot password token.
        /// </summary>
        private int _forgotPasswordTokenExpiration;

        #endregion

        #region Methods

        /// <summary>
        /// Load settings from configuration file.
        /// </summary>
        public void LoadConfiguration()
        {
            if (!int.TryParse(ConfigurationManager.AppSettings["tokenDuration"], out _tokenExpiration))
                _tokenExpiration = 3600;

            if (
                !int.TryParse(ConfigurationManager.AppSettings["forgotPasswordTokenDuration"],
                    out _forgotPasswordTokenExpiration))
                _forgotPasswordTokenExpiration = 3600;
        }

        #endregion
    }
}