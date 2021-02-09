using eyazisma.online.api.framework.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.framework.Classes
{
    /// <summary>
    /// Dağıtım listesindeki bir dağıtım için konulmamış ek bilgisidir.
    /// </summary>
    public sealed class KonulmamisEk
    {
        public KonulmamisEk() { }

        private KonulmamisEk(Guid ekIdDeger)
        {
            EkIdDeger = ekIdDeger;
        }

        /// <summary>
        /// Ekin paket içerisindeki Id değeridir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public Guid EkIdDeger { get; set; }

        public sealed class Kilavuz : IKonulmamisEkFluent
        {
            private Guid _ekIdDeger;

            private Kilavuz(Guid ekIdDeger)
            {
                _ekIdDeger = ekIdDeger;
            }

            /// <summary>
            /// Ekin paket içerisindeki Id değeridir.
            /// </summary>
            /// <param name="ekIdDeger">Ekin paket içerisindeki Id değeridir. Guid tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IKonulmamisEkFluentEkIdDeger EkIdDegerAta(Guid ekIdDeger) => new Kilavuz(ekIdDeger);

            public KonulmamisEk Olustur()
            {
                return new KonulmamisEk(_ekIdDeger);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
