using System;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Belgeye ilişkin belge doğrulama web adresi bilgisidir.
    /// </summary>
    /// <remarks>Only for version 2.0</remarks>
    public sealed class DogrulamaBilgisi
    {
        public DogrulamaBilgisi()
        {
        }

        private DogrulamaBilgisi(string dogrulamaAdresi)
        {
            DogrulamaAdresi = dogrulamaAdresi;
        }

        /// <summary>
        ///     Belgenin doğrulama yapılacağı web adresi bilgisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string DogrulamaAdresi { get; set; }

        public sealed class Kilavuz : IDogrulamaBilgisiFluent
        {
            private readonly string _dogrulamaAdresi;

            private Kilavuz(string dogrulamaAdresi)
            {
                _dogrulamaAdresi = dogrulamaAdresi;
            }

            public DogrulamaBilgisi Olustur()
            {
                return new DogrulamaBilgisi(_dogrulamaAdresi);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Belgenin doğrulama yapılacağı web adresi bilgisidir.
            /// </summary>
            /// <param name="dogrulamaAdresi">Belgenin doğrulama yapılacağı web adresi bilgisi değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IDogrulamaBilgisiFluentAdres AdresAta(string dogrulamaAdresi)
            {
                return new Kilavuz(dogrulamaAdresi);
            }
        }
    }
}