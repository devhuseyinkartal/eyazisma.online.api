using System;
using System.Xml;
using eyazisma.online.api.Enums;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Özet alınırken kullanılan algoritma bilgisidir.
    /// </summary>
    public sealed class OzetAlgoritmasi
    {
        public OzetAlgoritmasi()
        {
        }

        private OzetAlgoritmasi(OzetAlgoritmaTuru algoritma, XmlNode[] any)
        {
            Algoritma = algoritma;
            Any = any;
        }

        public XmlNode[] Any { get; set; }

        /// <summary>
        ///     Algoritma türü değeridir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public OzetAlgoritmaTuru Algoritma { get; set; }

        public sealed class Kilavuz : IOzetAlgoritmasiFluent
        {
            private readonly OzetAlgoritmaTuru _algoritma;
            private XmlNode[] _any;

            private Kilavuz(OzetAlgoritmaTuru algoritma)
            {
                _algoritma = algoritma;
            }

            public IOzetAlgoritmasiFluentAny AnyIle(XmlNode[] any)
            {
                _any = any;
                return this;
            }

            public OzetAlgoritmasi Olustur()
            {
                return new(_algoritma, _any);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Algoritma türü değeridir.
            /// </summary>
            /// <param name="algoritma">OzetAlgoritmaTuru tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IOzetAlgoritmasiFluentAlgoritma AlgoritmaAta(OzetAlgoritmaTuru algoritma)
            {
                return new Kilavuz(algoritma);
            }
        }
    }
}