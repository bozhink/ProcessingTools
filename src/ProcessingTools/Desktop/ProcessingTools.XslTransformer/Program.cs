// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.XslTransformer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Xsl;

    /// <summary>
    /// Main program class.
    /// </summary>
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                throw new InvalidOperationException("Number of arguments must be at least 2");
            }

            string xsltFileName = args[0];
            string inputFileName = args[1];
            string outFileName = args.Last();

            if (outFileName == inputFileName)
            {
                outFileName = $"{Path.GetFileNameWithoutExtension(inputFileName)}.{DateTime.Now:yyyyMMddHHmmssffffff}.{Path.GetExtension(inputFileName).Trim('.')}";
            }

            XslCompiledTransform xsl = new XslCompiledTransform();
            xsl.Load(xsltFileName);
            xsl.Transform(inputFileName, outFileName);
        }
    }
}
