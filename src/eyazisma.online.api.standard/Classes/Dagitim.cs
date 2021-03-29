using System;
using System.Collections.Generic;
using eyazisma.online.api.Enums;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Belgenin dağıtımının yapıldığı taraf bilgisidir.
    /// </summary>
    public sealed class Dagitim
    {
        public Dagitim()
        {
        }

        private Dagitim(object oge, IvedilikTuru ivedilikTuru, DagitimTuru dagitimTuru, TimeSpan miat,
            List<KonulmamisEk> konulmamisEkler)
        {
            Oge = oge;
            IvedilikTuru = ivedilikTuru;
            DagitimTuru = dagitimTuru;
            Miat = miat;
            KonulmamisEkler = konulmamisEkler;
        }

        /// <summary>
        ///     GercekSahis, KurumKurulus, TuzelSahis tipinde değer olmalıdır.
        ///     GercekSahis -> Dağıtımın yapılacağı gerçek şahıs bilgisidir.
        ///     KurumKurulus -> Dağıtımın yapılacağı kurum / kuruluş bilgisidir.
        ///     TuzelSahis -> Dağıtımın yapılacağı tüzel şahıs bilgisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public object Oge { get; set; }

        /// <summary>
        ///     Dağıtıma ait ivedilik bilgisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public IvedilikTuru IvedilikTuru { get; set; }

        /// <summary>
        ///     Dağıtımın türüdür.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public DagitimTuru DagitimTuru { get; set; }

        /// <summary>
        ///     "Ivedilik" elemanının "GNL (Günlüdür)" olması durumunda girilmesi zorunlu olan son tarihi ifade eder.
        /// </summary>
        /// <remarks> "Ivedilik" elemanının "GNL (Günlüdür)" olması durumunda bu alan zorunludur.</remarks>
        public TimeSpan Miat { get; set; }

        /// <summary>
        ///     İlgili dağıtım için konulmamış ek bilgileridir.
        /// </summary>
        public List<KonulmamisEk> KonulmamisEkler { get; set; }

        public sealed class Kilavuz : IDagitimFluent
        {
            private DagitimTuru _dagitimTuru;
            private IvedilikTuru _ivedilikTuru;
            private List<KonulmamisEk> _konulmamisEkler;
            private TimeSpan _miat;
            private readonly object _oge;

            private Kilavuz(object oge)
            {
                _oge = oge;
            }

            /// <summary>
            ///     Dağıtıma ait ivedilik bilgisidir.
            /// </summary>
            /// <param name="ivedilikTuru">Dağıtıma ait ivedilik bilgisi değeridir. IvedilikTuru tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IDagitimFluentIvedilikTuru IvedilikTuruAta(IvedilikTuru ivedilikTuru)
            {
                _ivedilikTuru = ivedilikTuru;
                return this;
            }

            /// <summary>
            ///     Dağıtımın türüdür.
            /// </summary>
            /// <param name="dagitimTuru">Dağıtım türü değeridir. DagitimTuru tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IDagitimFluentDagitimTuru DagitimTuruAta(DagitimTuru dagitimTuru)
            {
                _dagitimTuru = dagitimTuru;
                return this;
            }

            /// <summary>
            ///     "Ivedilik" elemanının "GNL (Günlüdür)" olması durumunda girilmesi zorunlu olan son tarihi ifade eder.
            /// </summary>
            /// <param name="miat">Miat değeridir. TimeSpan tipinde olmalıdır.</param>
            /// <remarks> "Ivedilik" elemanının "GNL (Günlüdür)" olması durumunda bu alan zorunludur.</remarks>
            public IDagitimFluentMiat MiatIle(TimeSpan miat)
            {
                _miat = miat;
                return this;
            }

            /// <summary>
            ///     İlgili dağıtım için konulmamış ek bilgisidir.
            /// </summary>
            /// <param name="konulmamisEk">İlgili dağıtım için konulmamış ek bilgisi değeridir. KonulmamisEk tipinde olmalıdır.</param>
            public IDagitimFluentKonulmamisEk KonulmamisEkIle(KonulmamisEk konulmamisEk)
            {
                if (konulmamisEk != null)
                {
                    if (_konulmamisEkler == null)
                        _konulmamisEkler = new List<KonulmamisEk>();

                    _konulmamisEkler.Add(konulmamisEk);
                }

                return this;
            }

            /// <summary>
            ///     İlgili dağıtım için konulmamış ek bilgileridir.
            /// </summary>
            /// <param name="konulmamisEkler">
            ///     İlgili dağıtım için konulmamış ek bilgileri değeridir. KonulmamisEk listesi tipinde
            ///     olmalıdır.
            /// </param>
            public IDagitimFluentKonulmamisEkler KonulmamisEklerIle(List<KonulmamisEk> konulmamisEkler)
            {
                if (konulmamisEkler != null && konulmamisEkler.Count > 0)
                {
                    if (_konulmamisEkler == null)
                        _konulmamisEkler = new List<KonulmamisEk>();

                    _konulmamisEkler.AddRange(konulmamisEkler);
                }

                return this;
            }

            public Dagitim Olustur()
            {
                return new Dagitim(_oge, _ivedilikTuru, _dagitimTuru, _miat, _konulmamisEkler);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Dağıtımın yapılacağı gerçek şahıs bilgisidir.
            /// </summary>
            /// <param name="oge">Dağıtımın yapılacağı gerçek şahıs bilgisi değeridir. GercekSahis tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IDagitimFluentOge OgeAta(GercekSahis oge)
            {
                return new Kilavuz(oge);
            }

            /// <summary>
            ///     Dağıtımın yapılacağı kurum / kuruluş bilgisidir.
            /// </summary>
            /// <param name="oge">Dağıtımın yapılacağı kurum / kuruluş bilgisi değeridir. KurumKurulus tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IDagitimFluentOge OgeAta(KurumKurulus oge)
            {
                return new Kilavuz(oge);
            }

            /// <summary>
            ///     Dağıtımın yapılacağı tüzel şahıs bilgisidir.
            /// </summary>
            /// <param name="oge">Dağıtımın yapılacağı tüzel şahıs bilgisi değeridir. TuzelSahis tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IDagitimFluentOge OgeAta(TuzelSahis oge)
            {
                return new Kilavuz(oge);
            }
        }
    }
}