using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Administration.Models
{
    public class JwtSetting
    {
        /// <summary>
        /// Key which is used for signing ontoken.
        /// </summary>
        private SymmetricSecurityKey _issuerSigningKey;

        /// <summary>
        /// Signing credential which is used for signing on token.
        /// </summary>
        private SigningCredentials _signingCredentials;

        /// <summary>
        /// Key which is used for encrypting/decrypting token.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Time when token should be expired (in seconds).
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Key which is used for signing token.
        /// </summary>
        public string SigningKey { get; set; }
        
        /// <summary>
        /// Amount of time when token should be expired (seconds)
        /// </summary>
        public int Expiration { get; set; }

        /// <summary>
        /// Find the symmetric security of Signing key.
        /// </summary>
        public SymmetricSecurityKey IssuerSigningKey
        {
            get
            {
                if (_issuerSigningKey == null)
                    _issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));

                return _issuerSigningKey;
            }
        }

        /// <summary>
        /// Credential which is used for signing on token.
        /// </summary>
        public SigningCredentials SigningCredentials
        {
            get
            {
                if (_signingCredentials == null)
                    _signingCredentials = new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);
                return _signingCredentials;
            }
        }

        public string JwtIdentity
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
    }
}
