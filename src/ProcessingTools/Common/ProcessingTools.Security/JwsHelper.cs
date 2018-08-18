// <copyright file="JwsHelper.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Security
{
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// JWS helper.
    /// </summary>
    /// <remarks>
    /// See https://hdknr.github.io/docs/identity/impl_jws.html#id11
    /// </remarks>
    public static class JwsHelper
    {
        /// <summary>
        /// JWS create RSA token.
        /// </summary>
        /// <param name="header">Header of the token.</param>
        /// <param name="payload">Payload of the token.</param>
        /// <param name="secret">Secret of the token.</param>
        /// <param name="certificate">Certificate to be used.</param>
        /// <returns>JWS token.</returns>
        public static string JwsCreateRsaToken(IDictionary<string, object> header, object payload, string secret, X509Certificate2 certificate)
        {
            if (header == null || payload == null || string.IsNullOrEmpty(secret) || certificate == null || !certificate.HasPrivateKey)
            {
                return null;
            }

            Encoding encoding = Encoding.UTF8;

            string jwsHeader = JsonConvert.SerializeObject(header);
            string jwsPayload = JsonConvert.SerializeObject(payload);

            string encodedJwsHeader = Utils.ToBase64Url(encoding.GetBytes(jwsHeader));
            string encodedJwsPayload = Utils.ToBase64Url(encoding.GetBytes(jwsPayload));

            string jwsSecuredInput = encodedJwsHeader + "." + encodedJwsPayload;

            byte[] jwsSignature = Utils.RsaSign(encoding.GetBytes(jwsSecuredInput), (string)header["alg"], certificate);

            if (jwsSignature == null || jwsSignature.Length < 1)
            {
                return null;
            }

            string encodedJwsSignature = Utils.ToBase64Url(jwsSignature);

            return string.Format("{0}.{1}.{2}", encodedJwsHeader, encodedJwsPayload, encodedJwsSignature);
        }

        /// <summary>
        /// JWS verify RSA token.
        /// </summary>
        /// <param name="jwsToken">JWS token.</param>
        /// <param name="secret">Secret of the token.</param>
        /// <param name="certificate">Certificate to be used.</param>
        /// <returns>Verification result.</returns>
        public static bool JwsVerifyRsaToken(string jwsToken, string secret, X509Certificate2 certificate)
        {
            if (string.IsNullOrEmpty(jwsToken) || string.IsNullOrEmpty(secret) || certificate == null)
            {
                return false;
            }

            string[] jwsTokenParts = jwsToken.Split(new[] { '.' });
            if (jwsTokenParts?.Length != 3)
            {
                return false;
            }

            Encoding encoding = Encoding.UTF8;

            string jwsHeader = encoding.GetString(Utils.FromBase64Url(jwsTokenParts[0]));
            byte[] jwsSignature = Utils.FromBase64Url(jwsTokenParts[2]);

            string jwsSecuredInput = jwsTokenParts[0] + "." + jwsTokenParts[1];

            IDictionary<string, object> header = JsonConvert.DeserializeObject<Dictionary<string, object>>(jwsHeader);

            bool ret = Utils.RsaVerify(encoding.GetBytes(jwsSecuredInput), jwsSignature, (string)header["alg"], certificate);

            return ret;
        }
    }
}
