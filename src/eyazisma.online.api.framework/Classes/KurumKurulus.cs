using eyazisma.online.api.framework.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.framework.Classes
{
    /// <summary>
    /// Kurum veya kuruluş bilgisidir.
    /// </summary>
    public sealed class KurumKurulus
    {
        public KurumKurulus() { }

        private KurumKurulus(string kkk, string birimKKK, IsimTip ad, IletisimBilgisi iletisimBilgisi)
        {
            KKK = kkk;
            BirimKKK = birimKKK;
            Ad = ad;
            IletisimBilgisi = iletisimBilgisi;
        }

        /// <summary>
        /// Kurum/kuruluşun DETSİS'te yer alan Türkiye Cumhuriyeti Devlet Teşkilatı Numarasıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string KKK { get; set; }

        /// <summary>
        /// Paketi oluşturan veya muhatap alt birimlerin, DETSİS'te yer alan Türkiye Cumhuriyeti Devlet Teşkilatı Numarasıdır.
        /// </summary>
        /// <remarks>Only for version 2.0</remarks>
        public string BirimKKK { get; set; }

        /// <summary>
        /// Kurum / kuruluşun DTVT'deki Başbakanlık yazışma kodudur.
        /// </summary>
        [Obsolete("Since version 1.2", false)]
        public string BYK { get; set; }

        /// <summary>
        /// Kurum / kuruluşun adıdır.
        /// </summary>
        /// <remarks>Bu alanın kullanılması durumunda DETSİS’teki kurum adı kullanılmalıdır.</remarks>
        public IsimTip Ad { get; set; }

        /// <summary>
        /// Kurum/kuruluşun iletişim bilgisidir.
        /// </summary>
        public IletisimBilgisi IletisimBilgisi { get; set; }

        public sealed class Kilavuz : IKurumKurulusFluent
        {
            private string _kkk, _birimKKK;
            private IsimTip _ad;
            private IletisimBilgisi _iletisimBilgisi;

            private Kilavuz() { }

            public static IKurumKurulusFluentV1X Versiyon1X() => new Kilavuz();
            public static IKurumKurulusFluentV2X Versiyon2X() => new Kilavuz();

            /// <summary>
            /// Kurum / kuruluşun adıdır.
            /// </summary>
            /// <param name="ad">Kurum / kuruluşa ait ad değeridir. IsimTip tipinde olmalıdır.</param>
            /// <remarks>Bu alanın kullanılması durumunda DETSİS’teki kurum adı kullanılmalıdır.</remarks>
            public IKurumKurulusFluentAd AdIle(IsimTip ad)
            {
                _ad = ad;
                return this;
            }

            /// <summary>
            /// Paketi oluşturan veya muhatap alt birimlerin, DETSİS'te yer alan Türkiye Cumhuriyeti Devlet Teşkilatı Numarasıdır.
            /// </summary>
            /// <param name="birimKKK">Devlet Teşkilatı Numarası değeridir.</param>
            /// <remarks>Only for version 2.0</remarks>
            public IKurumKurulusFluentV2XBirimKKK BirimKKKIle(string birimKKK)
            {
                _birimKKK = birimKKK;
                return this;
            }

            /// <summary>
            /// Kurum / kuruluşun iletişim bilgisidir.
            /// </summary>
            /// <param name="iletisimBilgisi">Kurum / kuruluşun iletişim bilgisi değeridir.</param>
            public IKurumKurulusFluentIletisimBilgisi IletisimBilgisiIle(IletisimBilgisi iletisimBilgisi)
            {
                _iletisimBilgisi = iletisimBilgisi;
                return this;
            }

            /// <summary>
            /// Kurum / kuruluşun DETSİS'te yer alan Türkiye Cumhuriyeti Devlet Teşkilatı Numarasıdır.
            /// </summary>
            /// <param name="kkk">Devlet Teşkilatı Numarası değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IKurumKurulusFluentV1XKKK KKKAta(string kkk)
            {
                _kkk = kkk;
                return this;
            }

            /// <summary>
            /// Kurum / kuruluşun DETSİS'te yer alan Türkiye Cumhuriyeti Devlet Teşkilatı Numarasıdır.
            /// </summary>
            /// <param name="kkk">Devlet Teşkilatı Numarası değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            IKurumKurulusFluentV2XKKK IKurumKurulusFluentV2X.KKKAta(string kkk)
            {
                _kkk = kkk;
                return this;
            }

            public KurumKurulus Olustur()
            {
                return new KurumKurulus(_kkk, _birimKKK, _ad, _iletisimBilgisi);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
