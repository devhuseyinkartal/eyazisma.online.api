using System;
using System.IO;
using System.Security.Cryptography;

namespace eyazisma.online.api.Enums
{
    /// <summary>
    ///     Özet algoritma türünü belirtir.
    /// </summary>
    public enum OzetAlgoritmaTuru
    {
        /// <summary>
        ///     Yok
        /// </summary>
        YOK = 0,

        /// <summary>
        ///     SHA1
        /// </summary>
        [Obsolete("Since version 2.0", false)] SHA1 = 1,

        /// <summary>
        ///     SHA256
        /// </summary>
        SHA256 = 2,

        /// <summary>
        ///     SHA384
        /// </summary>
        /// <remarks>Only for version 2.0</remarks>
        SHA384 = 3,

        /// <summary>
        ///     SHA512
        /// </summary>
        SHA512 = 4,

        /// <summary>
        ///     RIPEMD160
        /// </summary>
        [Obsolete("Since version 2.0", false)] RIPEMD160 = 5
    }

    internal static class AlgoritmaTuruExtensions
    {
        public static string ToXmlNameSpace(this OzetAlgoritmaTuru algoritmaTuru)
        {
            switch (algoritmaTuru)
            {
                case OzetAlgoritmaTuru.RIPEMD160: return Constants.ALGORITHM_RIPEMD160;
                case OzetAlgoritmaTuru.SHA1: return Constants.ALGORITHM_SHA1;
                case OzetAlgoritmaTuru.SHA256: return Constants.ALGORITHM_SHA256;
                case OzetAlgoritmaTuru.SHA384: return Constants.ALGORITHM_SHA384;
                case OzetAlgoritmaTuru.SHA512: return Constants.ALGORITHM_SHA512;
                default: return "";
            }
        }

        public static OzetAlgoritmaTuru ToOzetAlgoritmaTuru(this string xmlNameSpace)
        {
            switch (xmlNameSpace)
            {
                case Constants.ALGORITHM_RIPEMD160: return OzetAlgoritmaTuru.RIPEMD160;
                case Constants.ALGORITHM_SHA1: return OzetAlgoritmaTuru.SHA1;
                case Constants.ALGORITHM_SHA256: return OzetAlgoritmaTuru.SHA256;
                case Constants.ALGORITHM_SHA384: return OzetAlgoritmaTuru.SHA384;
                case Constants.ALGORITHM_SHA512: return OzetAlgoritmaTuru.SHA512;
                default: return OzetAlgoritmaTuru.YOK;
            }
        }

        public static byte[] CalculateHash(this OzetAlgoritmaTuru algoritmaTuru, Stream value)
        {
            switch (algoritmaTuru)
            {
                case OzetAlgoritmaTuru.RIPEMD160:
                    using (var hashAlgorithm = new RIPEMD160Managed())
                    {
                        return hashAlgorithm.ComputeHash(value);
                    }
                case OzetAlgoritmaTuru.SHA1:
                {
                    using (var hashAlgorithm = new SHA1Managed())
                    {
                        return hashAlgorithm.ComputeHash(value);
                    }
                }
                case OzetAlgoritmaTuru.SHA256:
                    using (var hashAlgorithm = new SHA256Managed())
                    {
                        return hashAlgorithm.ComputeHash(value);
                    }
                case OzetAlgoritmaTuru.SHA384:
                    using (var hashAlgorithm = new SHA384Managed())
                    {
                        return hashAlgorithm.ComputeHash(value);
                    }
                case OzetAlgoritmaTuru.SHA512:
                    using (var hashAlgorithm = new SHA512Managed())
                    {
                        return hashAlgorithm.ComputeHash(value);
                    }
                default:
                    return null;
            }
        }

        public static byte[] CalculateHash(this OzetAlgoritmaTuru algoritmaTuru, byte[] value)
        {
            if (value == null) return null;
            using (var ms = new MemoryStream(value))
            {
                return algoritmaTuru.CalculateHash(ms);
            }
        }
    }
}