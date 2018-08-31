// <copyright file="CertificateValidationConstants.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Constants
{
    using System.Collections.Generic;

    /// <summary>
    /// Certificate validation constants.
    /// </summary>
    public static class CertificateValidationConstants
    {
        /// <summary>
        /// Valid subject key identifiers.
        /// </summary>
        public static readonly IEnumerable<string> ValidSubjectKeyIdentifiers = new[]
        {
            // VeriSign Class 3 Secure Server CA - G2
            "A5EF0B11CEC04103A34A659048B21CE0572D7D47",

            // VeriSign Class 3 Secure Server CA - G3
            "0D445C165344C1827E1D20AB25F40163D8BE79A5",

            // VeriSign Class 3 Public Primary Certification Authority - G5
            "7FD365A7C2DDECBBF03009F34339FA02AF333133",

            // Symantec Class 3 Secure Server CA - G4
            "39A55D933676616E73A761DFA16A7E59CDE66FAD",

            // DigiCert SHA2 High Assurance Server C‎A
            "5168FF90AF0207753CCCD9656462A212B859723B",

            // DigiCert High Assurance EV Root CA
            "B13EC36903F8BF4701D498261A0802EF63642BC3"
        };
    }
}
