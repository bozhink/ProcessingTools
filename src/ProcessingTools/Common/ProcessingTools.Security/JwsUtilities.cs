// <copyright file="JwsUtilities.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Security
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using Newtonsoft.Json;
    using ProcessingTools.Constants.Security;

    /// <summary>
    /// JWS helper.
    /// </summary>
    /// <remarks>
    /// See https://hdknr.github.io/docs/identity/impl_jws.html#id11
    /// </remarks>
    public static class JwsUtilities
    {
        /// <summary>
        /// Gets the payload as JSON string from JWS token.
        /// </summary>
        /// <param name="jwsToken">JWS token to be processed.</param>
        /// <returns>Payload as JSON string.</returns>
        public static string GetPayloadJsonFromJwsToken(string jwsToken)
        {
            if (string.IsNullOrWhiteSpace(jwsToken))
            {
                throw new ArgumentNullException(nameof(jwsToken));
            }

            string[] jwsTokenParts = jwsToken.Split(new[] { '.' });
            if (jwsTokenParts?.Length != 3)
            {
                throw new InvalidOperationException("JWS token has invalid structure");
            }

            Encoding encoding = Encoding.UTF8;

            string encodedJwsPayload = jwsTokenParts[1];
            string jwsPayload = encoding.GetString(SecurityUtilities.FromBase64Url(encodedJwsPayload));

            return jwsPayload;
        }

        /// <summary>
        /// JWS create RSA token with embedded into the header certificate's public key.
        /// </summary>
        /// <param name="header">Header of the token.</param>
        /// <param name="payload">Payload of the token.</param>
        /// <param name="certificate">Certificate to be used.</param>
        /// <returns>JWS token.</returns>
        public static string JwsCreateRsaTokenWithEmbeddedCertificate(IDictionary<string, object> header, object payload, X509Certificate2 certificate)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }

            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            header[JwsHeaderParameters.JsonWebKey] = Convert.ToBase64String(certificate.GetPublicKey());

            return JwsCreateRsaToken(header, payload, certificate);
        }

        /// <summary>
        /// JWS create RSA token.
        /// </summary>
        /// <param name="header">Header of the token.</param>
        /// <param name="payload">Payload of the token.</param>
        /// <param name="certificate">Certificate to be used.</param>
        /// <returns>JWS token.</returns>
        public static string JwsCreateRsaToken(IDictionary<string, object> header, object payload, X509Certificate2 certificate)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }

            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            if (!header.ContainsKey(JwsHeaderParameters.Algorithm))
            {
                throw new InvalidOperationException($@"""{JwsHeaderParameters.Algorithm}"" header parameter is required");
            }

            if (!certificate.HasPrivateKey)
            {
                throw new InvalidOperationException("Certificate must have private key");
            }

            Encoding encoding = Encoding.UTF8;

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string jwsHeader = JsonConvert.SerializeObject(header, jsonSerializerSettings);
            string jwsPayload = JsonConvert.SerializeObject(payload, jsonSerializerSettings);

            string encodedJwsHeader = SecurityUtilities.ToBase64Url(encoding.GetBytes(jwsHeader));
            string encodedJwsPayload = SecurityUtilities.ToBase64Url(encoding.GetBytes(jwsPayload));

            string jwsSecuredInput = encodedJwsHeader + "." + encodedJwsPayload;

            string algorithm = (string)header[JwsHeaderParameters.Algorithm];

            byte[] jwsSignature = SecurityUtilities.RsaSignHash(encoding.GetBytes(jwsSecuredInput), algorithm, certificate);

            if (jwsSignature == null || jwsSignature.Length < 1)
            {
                return null;
            }

            string encodedJwsSignature = SecurityUtilities.ToBase64Url(jwsSignature);

            return string.Format("{0}.{1}.{2}", encodedJwsHeader, encodedJwsPayload, encodedJwsSignature);
        }

        /// <summary>
        /// JWS verify RSA token with embedded into the header certificate's public key.
        /// </summary>
        /// <param name="jwsToken">JWS token.</param>
        /// <returns>Verification result.</returns>
        public static bool JwsVerifyRsaTokenWithEmbeddedCertificate(string jwsToken)
        {
            if (string.IsNullOrEmpty(jwsToken))
            {
                return false;
            }

            string[] jwsTokenParts = GetJwsTokenParts(jwsToken);
            if (jwsTokenParts?.Length != 3)
            {
                return false;
            }

            Encoding encoding = Encoding.UTF8;

            IDictionary<string, object> header = GetHeaderFromJwsToken(jwsTokenParts, encoding);
            if (header.ContainsKey(JwsHeaderParameters.Algorithm) && header.ContainsKey(JwsHeaderParameters.JsonWebKey))
            {
                string algorithm = (string)header[JwsHeaderParameters.Algorithm];
                if (string.IsNullOrWhiteSpace(algorithm))
                {
                    return false;
                }

                string certificateString = (string)header[JwsHeaderParameters.JsonWebKey];
                if (string.IsNullOrWhiteSpace(certificateString))
                {
                    return false;
                }

                byte[] certificateBytes = Convert.FromBase64String(certificateString);
                if (certificateBytes.Length < 1)
                {
                    return false;
                }

                X509Certificate2 certificate = new X509Certificate2(rawData: certificateBytes);

                return JwsVerifyRsaToken(certificate, jwsTokenParts, encoding, algorithm);
            }

            return false;
        }

        /// <summary>
        /// JWS verify RSA token.
        /// </summary>
        /// <param name="jwsToken">JWS token.</param>
        /// <param name="certificate">Certificate to be used.</param>
        /// <returns>Verification result.</returns>
        public static bool JwsVerifyRsaToken(string jwsToken, X509Certificate2 certificate)
        {
            if (string.IsNullOrEmpty(jwsToken))
            {
                return false;
            }

            string[] jwsTokenParts = GetJwsTokenParts(jwsToken);
            if (jwsTokenParts?.Length != 3)
            {
                return false;
            }

            Encoding encoding = Encoding.UTF8;

            IDictionary<string, object> header = GetHeaderFromJwsToken(jwsTokenParts, encoding);
            if (header.ContainsKey(JwsHeaderParameters.Algorithm))
            {
                string algorithm = (string)header[JwsHeaderParameters.Algorithm];
                if (string.IsNullOrWhiteSpace(algorithm))
                {
                    return false;
                }

                return JwsVerifyRsaToken(certificate, jwsTokenParts, encoding, algorithm);
            }

            return false;
        }

        private static string[] GetJwsTokenParts(string jwsToken)
        {
            return jwsToken.Split(new[] { '.' });
        }

        private static IDictionary<string, object> GetHeaderFromJwsToken(string[] jwsTokenParts, Encoding encoding)
        {
            string jwsHeader = encoding.GetString(SecurityUtilities.FromBase64Url(jwsTokenParts[0]));
            IDictionary<string, object> header = JsonConvert.DeserializeObject<Dictionary<string, object>>(jwsHeader);
            if (!header.ContainsKey(JwsHeaderParameters.Algorithm))
            {
                throw new InvalidOperationException($@"""{JwsHeaderParameters.Algorithm}"" header parameter is required");
            }

            return header;
        }

        private static bool JwsVerifyRsaToken(X509Certificate2 certificate, string[] jwsTokenParts, Encoding encoding, string algorithm)
        {
            if (certificate == null || encoding == null || string.IsNullOrWhiteSpace(algorithm))
            {
                return false;
            }

            byte[] jwsSignature = SecurityUtilities.FromBase64Url(jwsTokenParts[2]);
            string jwsSecuredInput = jwsTokenParts[0] + "." + jwsTokenParts[1];

            bool result = SecurityUtilities.RsaVerifyHash(encoding.GetBytes(jwsSecuredInput), jwsSignature, algorithm, certificate);

            return result;
        }
    }
}
