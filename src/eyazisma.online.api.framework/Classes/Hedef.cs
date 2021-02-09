using eyazisma.online.api.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    /// Belgenin iletileceği taraf bilgisidir.
    /// </summary>
    public class Hedef
    {
        public Hedef() { }

        private Hedef(object oge)
        {
            Oge = oge;
        }

        /// <summary>
        /// GercekSahis, KurumKurulus, TuzelSahis tipinde değer olmalıdır.
        /// GercekSahis -> Dağıtımın yapılacağı gerçek şahıs bilgisidir.
        /// KurumKurulus -> Dağıtımın yapılacağı kurum / kuruluş bilgisidir.
        /// TuzelSahis -> Dağıtımın yapılacağı tüzel şahıs bilgisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public object Oge { get; set; }

        public sealed class Kilavuz : IHedefFluent
        {
            private readonly object _oge;

            private Kilavuz(object oge)
            {
                _oge = oge;
            }

            /// <summary>
            /// Dağıtımın yapılacağı gerçek şahıs bilgisidir.
            /// </summary>
            /// <param name="oge">Dağıtımın yapılacağı gerçek şahıs bilgisi değeridir. GercekSahis tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IHedefFluentOge OgeAta(GercekSahis oge) => new Kilavuz(oge);

            /// <summary>
            /// Dağıtımın yapılacağı kurum / kuruluş bilgisidir.
            /// </summary>
            /// <param name="oge">Dağıtımın yapılacağı kurum / kuruluş bilgisi değeridir. KurumKurulus tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IHedefFluentOge OgeAta(KurumKurulus oge) => new Kilavuz(oge);

            /// <summary>
            /// Dağıtımın yapılacağı tüzel şahıs bilgisidir.
            /// </summary>
            /// <param name="oge">Dağıtımın yapılacağı tüzel şahıs bilgisi değeridir. TuzelSahis tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IHedefFluentOge OgeAta(TuzelSahis oge) => new Kilavuz(oge);

            public Hedef Olustur()
            {
                return new Hedef(_oge);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
