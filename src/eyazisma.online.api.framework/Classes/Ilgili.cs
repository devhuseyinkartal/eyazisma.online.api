using System;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Belge ile ilgili iletişim kurulacak tarafa ait bilgidir.
    /// </summary>
    public class Ilgili
    {
        public Ilgili()
        {
        }

        private Ilgili(object oge)
        {
            Oge = oge;
        }

        /// <summary>
        ///     GercekSahis, KurumKurulus, TuzelSahis tipinde değer olmalıdır.
        ///     GercekSahis -> İletişim kurulacak gerçek şahıs bilgisidir.
        ///     KurumKurulus -> İletişim kurulacak kurum / kuruluş bilgisidir.
        ///     TuzelSahis -> İletişim kurulacak tüzel şahıs bilgisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public object Oge { get; set; }

        public sealed class Kilavuz : IIlgiliFluent
        {
            private readonly object _oge;

            private Kilavuz(object oge)
            {
                _oge = oge;
            }

            public Ilgili Olustur()
            {
                return new Ilgili(_oge);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     İletişim kurulacak gerçek şahıs bilgisidir.
            /// </summary>
            /// <param name="oge">İletişim kurulacak gerçek şahıs bilgisi değeridir. GercekSahis tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IIlgiliFluentItem OgeAta(GercekSahis oge)
            {
                return new Kilavuz(oge);
            }

            /// <summary>
            ///     İletişim kurulacak kurum / kuruluş bilgisidir.
            /// </summary>
            /// <param name="oge">İletişim kurulacak kurum / kuruluş bilgisi değeridir. KurumKurulus tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IIlgiliFluentItem OgeAta(KurumKurulus oge)
            {
                return new Kilavuz(oge);
            }

            /// <summary>
            ///     İletişim kurulacak tüzel şahıs bilgisidir.
            /// </summary>
            /// <param name="oge">İletişim kurulacak tüzel şahıs bilgisi değeridir. TuzelSahis tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IIlgiliFluentItem OgeAta(TuzelSahis oge)
            {
                return new Kilavuz(oge);
            }
        }
    }
}