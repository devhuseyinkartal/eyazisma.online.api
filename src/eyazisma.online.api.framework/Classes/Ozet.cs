using eyazisma.online.api.framework.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.framework.Classes
{
    /// <summary>
    /// Bir dosyaya ilişkin özet bilgisini barındıran elemandır.
    /// </summary>
    public sealed class Ozet
    {
        public Ozet() { }

        private Ozet(OzetAlgoritmasi ozetAlgoritmasi, byte[] ozetDegeri)
        {
            OzetAlgoritmasi = ozetAlgoritmasi;
            OzetDegeri = ozetDegeri;
        }

        /// <summary>
        /// Özet alınırken kullanılan algoritma bilgisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public OzetAlgoritmasi OzetAlgoritmasi { get; set; }

        /// <summary>
        /// Özetin değeridir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public byte[] OzetDegeri { get; set; }

        public sealed class Kilavuz : IOzetFluent
        {
            private OzetAlgoritmasi _ozetAlgoritmasi;
            private byte[] _ozetDegeri;

            private Kilavuz(OzetAlgoritmasi ozetAlgoritmasi)
            {
                _ozetAlgoritmasi = ozetAlgoritmasi;
            }

            /// <summary>
            /// Özet alınırken kullanılan algoritma bilgisidir.
            /// </summary>
            /// <param name="ozetAlgoritmasi">Algoritma bilgisi değeridir. OzetAlgoritmasi tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IOzetFluentAlgoritma OzetAlgoritmasiAta(OzetAlgoritmasi ozetAlgoritmasi) => new Kilavuz(ozetAlgoritmasi);


            /// <summary>
            /// Özetin değeridir.
            /// </summary>
            /// <param name="ozetDegeri">ByteArray tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IOzetFluentDeger OzetDegeriAta(byte[] ozetDegeri)
            {
                _ozetDegeri = ozetDegeri;
                return this;
            }

            public Ozet Olustur()
            {
                return new Ozet(_ozetAlgoritmasi, _ozetDegeri);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
