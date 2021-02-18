using eyazisma.online.api.Enums;
using eyazisma.online.api.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    /// Özet alınırken kullanılan algoritma bilgisidir.
    /// </summary>
    public sealed class OzetAlgoritmasi
    {
        public OzetAlgoritmasi() { }

        private OzetAlgoritmasi(OzetAlgoritmaTuru algoritma, System.Xml.XmlNode[] any)
        {
            Algoritma = algoritma;
            Any = any;
        }

        public System.Xml.XmlNode[] Any { get; set; }

        /// <summary>
        /// Algoritma türü değeridir.
        /// </summary>
        ///<remarks>Zorunlu alandır.</remarks>
        public OzetAlgoritmaTuru Algoritma { get; set; }

        public sealed class Kilavuz : IOzetAlgoritmasiFluent
        {
            private System.Xml.XmlNode[] _any;
            private OzetAlgoritmaTuru _algoritma;

            private Kilavuz(OzetAlgoritmaTuru algoritma)
            {
                _algoritma = algoritma;
            }

            /// <summary>
            /// Algoritma türü değeridir.
            /// </summary>
            /// <param name="algoritma">OzetAlgoritmaTuru tipinde olmalıdır.</param>
            ///<remarks>Zorunlu alandır.</remarks>
            public static IOzetAlgoritmasiFluentAlgoritma AlgoritmaAta(OzetAlgoritmaTuru algoritma) => new Kilavuz(algoritma);

            public IOzetAlgoritmasiFluentAny AnyIle(System.Xml.XmlNode[] any)
            {
                _any = any;
                return this;
            }

            public OzetAlgoritmasi Olustur()
            {
                return new OzetAlgoritmasi(_algoritma, _any);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
