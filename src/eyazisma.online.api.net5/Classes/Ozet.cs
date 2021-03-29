using System;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Bir dosyaya ilişkin özet bilgisini barındıran elemandır.
    /// </summary>
    public sealed class Ozet
    {
        public Ozet()
        {
        }

        private Ozet(OzetAlgoritmasi ozetAlgoritmasi, byte[] ozetDegeri)
        {
            OzetAlgoritmasi = ozetAlgoritmasi;
            OzetDegeri = ozetDegeri;
        }

        /// <summary>
        ///     Özet alınırken kullanılan algoritma bilgisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public OzetAlgoritmasi OzetAlgoritmasi { get; set; }

        /// <summary>
        ///     Özetin değeridir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public byte[] OzetDegeri { get; set; }

        public sealed class Kilavuz : IOzetFluent
        {
            private readonly OzetAlgoritmasi _ozetAlgoritmasi;
            private byte[] _ozetDegeri;

            private Kilavuz(OzetAlgoritmasi ozetAlgoritmasi)
            {
                _ozetAlgoritmasi = ozetAlgoritmasi;
            }


            /// <summary>
            ///     Özetin değeridir.
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
                return new(_ozetAlgoritmasi, _ozetDegeri);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Özet alınırken kullanılan algoritma bilgisidir.
            /// </summary>
            /// <param name="ozetAlgoritmasi">Algoritma bilgisi değeridir. OzetAlgoritmasi tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IOzetFluentAlgoritma OzetAlgoritmasiAta(OzetAlgoritmasi ozetAlgoritmasi)
            {
                return new Kilavuz(ozetAlgoritmasi);
            }
        }
    }
}