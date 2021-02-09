using eyazisma.online.api.framework.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.framework.Classes
{
    /// <summary>
    /// Belgeyi oluşturan tarafa ait bilgidir
    /// </summary>
    public class Olusturan
    {
        public Olusturan() { }

        private Olusturan(object oge)
        {
            Oge = oge;
        }

        /// <summary>
        /// GercekSahis, KurumKurulus, TuzelSahis tipinde değer olmalıdır.
        /// GercekSahis -> Belgeyi oluşturan gerçek şahıs bilgisidir.
        /// KurumKurulus -> Belgeyi oluşturan kurum / kuruluş bilgisidir.
        /// TuzelSahis -> Belgeyi oluşturan tüzel şahıs bilgisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public object Oge { get; set; }

        public sealed class Kilavuz : IOlusturanFluent
        {
            private readonly object _oge;

            private Kilavuz(object oge)
            {
                _oge = oge;
            }

            /// <summary>
            /// Belgeyi oluşturan gerçek şahıs bilgisidir.
            /// </summary>
            /// <param name="oge">Belgeyi oluşturan gerçek şahıs bilgisi değeridir. GercekSahis tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IOlusturanFluentItem OgeAta(GercekSahis oge) => new Kilavuz(oge);

            /// <summary>
            /// Belgeyi oluşturan kurum / kuruluş bilgisidir.
            /// </summary>
            /// <param name="oge">Belgeyi oluşturan kurum / kuruluş bilgisi değeridir. KurumKurulus tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IOlusturanFluentItem OgeAta(KurumKurulus oge) => new Kilavuz(oge);

            /// <summary>
            /// Belgeyi oluşturan tüzel şahıs bilgisidir.
            /// </summary>
            /// <param name="oge">Belgeyi oluşturan tüzel şahıs bilgisi değeridir. TuzelSahis tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IOlusturanFluentItem OgeAta(TuzelSahis oge) => new Kilavuz(oge);

            public Olusturan Olustur()
            {
                return new Olusturan(_oge);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }

    internal static partial class OlusturanExtensions
    {
        public static string GenerateOlusturanAd(this Olusturan olusturan)
        {
            if (olusturan == null || olusturan.Oge == null)
            {
                return null;
            }

            string olusturanAdi = string.Empty;

            if (olusturan.Oge is KurumKurulus kurumKurulus)
            {
                if (kurumKurulus.Ad != null && !string.IsNullOrWhiteSpace(kurumKurulus.Ad.Deger))
                {
                    if (!string.IsNullOrEmpty(kurumKurulus.KKK))
                        olusturanAdi = string.Format("{0}/{1}", kurumKurulus.Ad.Deger, kurumKurulus.KKK);
                    else
                        olusturanAdi = kurumKurulus.Ad.Deger;
                }
                else
                {
                    olusturanAdi = kurumKurulus.KKK;
                }
            }
            else if (olusturan.Oge is TuzelSahis tuzelSahis)
            {
                if (tuzelSahis.Ad != null && !string.IsNullOrWhiteSpace(tuzelSahis.Ad.Deger))
                {
                    olusturanAdi = tuzelSahis.Ad.Deger;
                }
                else if (tuzelSahis.Id != null && !string.IsNullOrWhiteSpace(tuzelSahis.Id.Deger))
                {
                    if (!string.IsNullOrWhiteSpace(tuzelSahis.Id.SemaID))
                        olusturanAdi = tuzelSahis.Id.SemaID + ":";

                    olusturanAdi += tuzelSahis.Id.Deger;
                }
            }
            else if (olusturan.Oge is GercekSahis gercekSahis)
            {
                if (!string.IsNullOrWhiteSpace(gercekSahis.TCKN))
                    olusturanAdi += gercekSahis.TCKN + ",";

                if (gercekSahis.Gorev != null && !string.IsNullOrWhiteSpace(gercekSahis.Gorev.Deger))
                    olusturanAdi += gercekSahis.Gorev.Deger + ",";

                if (gercekSahis.Kisi != null)
                {
                    if (gercekSahis.Kisi.OnEk != null && !string.IsNullOrWhiteSpace(gercekSahis.Kisi.OnEk.Deger))
                        olusturanAdi += gercekSahis.Kisi.OnEk.Deger + " ";

                    if (gercekSahis.Kisi.Unvan != null && !string.IsNullOrWhiteSpace(gercekSahis.Kisi.Unvan.Deger))
                        olusturanAdi += gercekSahis.Kisi.Unvan.Deger + " ";

                    if (gercekSahis.Kisi.IlkAdi != null && !string.IsNullOrWhiteSpace(gercekSahis.Kisi.IlkAdi.Deger))
                        olusturanAdi += gercekSahis.Kisi.IlkAdi.Deger + " ";

                    if (gercekSahis.Kisi.IkinciAdi != null && !string.IsNullOrWhiteSpace(gercekSahis.Kisi.IkinciAdi.Deger))
                        olusturanAdi += gercekSahis.Kisi.IkinciAdi.Deger + " ";

                    if (gercekSahis.Kisi.Soyadi != null && !string.IsNullOrWhiteSpace(gercekSahis.Kisi.Soyadi.Deger))
                        olusturanAdi += gercekSahis.Kisi.Soyadi.Deger;
                }
            }

            return olusturanAdi.Trim();
        }
    }
}
