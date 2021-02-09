using eyazisma.online.api.framework.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.framework.Classes
{
    /// <summary>
    /// Tüzel şahıs bilgisidir.
    /// </summary>
    public sealed class TuzelSahis
    {
        public TuzelSahis() { }

        private TuzelSahis(TanimlayiciTip id, IsimTip ad, IletisimBilgisi iletisimBilgisi)
        {
            Id = id;
            Ad = ad;
            IletisimBilgisi = iletisimBilgisi;
        }

        /// <summary>
        /// Tüzel şahsa ait tekil belirteçtir. 
        /// Türkiye'de faaliyet gösteren tüzel şahıslar için Merkezi Sicil Kayıt Sistemi(MERSİS) numarası değeri "schemeID=MERSIS" değeri kullanılarak verilir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public TanimlayiciTip Id { get; set; }

        /// <summary>
        /// Tüzel kişinin adıdır.
        /// </summary>
        public IsimTip Ad { get; set; }

        /// <summary>
        /// Tüzel kişinin iletişim bilgisidir.
        /// </summary>
        public IletisimBilgisi IletisimBilgisi { get; set; }

        public sealed class Kilavuz : ITuzelSahisFluent
        {
            private TanimlayiciTip _id;
            private IsimTip _ad;
            private IletisimBilgisi _iletisimBilgisi;

            private Kilavuz(TanimlayiciTip id)
            {
                _id = id;
            }

            /// <summary>
            /// Tüzel şahsa ait tekil belirteçtir. 
            /// Türkiye'de faaliyet gösteren tüzel şahıslar için Merkezi Sicil Kayıt Sistemi(MERSİS) numarası değeri "schemeID=MERSIS" değeri kullanılarak verilir.
            /// </summary>
            /// <param name="id">Tüzel şahsa ait tekil belirteç değeridir. TanimlayiciTip tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static ITuzelSahisFluentId IdAta(TanimlayiciTip id) => new Kilavuz(id);

            /// <summary>
            /// Tüzel kişinin adıdır.
            /// </summary>
            /// <param name="ad">Tüzel kişinin ad değeridir. IsimTip tipinde olmalıdır.</param>
            public ITuzelSahisFluentAd AdIle(IsimTip ad)
            {
                _ad = ad;
                return this;
            }

            /// <summary>
            /// Tüzel kişinin iletişim bilgisidir.
            /// </summary>
            /// <param name="iletisimBilgisi">Tüzel kişinin iletişim bilgisi değeridir. IletisimBilgisi tipinde olmalıdır.</param>
            public ITuzelSahisFluentIletisimBilgisi IletisimBilgisiIle(IletisimBilgisi iletisimBilgisi)
            {
                _iletisimBilgisi = iletisimBilgisi;
                return this;
            }

            public TuzelSahis Olustur()
            {
                return new TuzelSahis(_id, _ad, _iletisimBilgisi);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
