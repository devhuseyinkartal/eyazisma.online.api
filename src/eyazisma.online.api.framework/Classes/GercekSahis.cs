using eyazisma.online.api.framework.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.framework.Classes
{
    /// <summary>
    /// Gerçek şahıs bilgisidir.
    /// </summary>
    public sealed class GercekSahis
    {
        public GercekSahis() { }

        private GercekSahis(Kisi kisi, string tckn, MetinTip gorev, IletisimBilgisi iletisimBilgisi)
        {
            Kisi = kisi;
            TCKN = tckn;
            Gorev = gorev;
            IletisimBilgisi = iletisimBilgisi;
        }

        /// <summary>
        /// Kişiye ait kimlik bilgileridir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public Kisi Kisi { get; set; }

        /// <summary>
        /// Kişinin T.C. kimlik numarasıdır.
        /// </summary>
        public string TCKN { get; set; }

        /// <summary>
        /// Kişinin görev bilgisidir.
        /// </summary>
        public MetinTip Gorev { get; set; }

        /// <summary>
        /// Kişiye ait iletişim bilgisidir.
        /// </summary>
        public IletisimBilgisi IletisimBilgisi { get; set; }

        public sealed class Kilavuz : IGercekSahisFluent
        {
            private Kisi _kisi;
            private string _tckn;
            private MetinTip _gorev;
            private IletisimBilgisi _iletisim;

            private Kilavuz(Kisi kisi)
            {
                _kisi = kisi;
            }

            /// <summary>
            /// Kişiye ait kimlik bilgisinin atanaması için kullanılır.
            /// </summary>
            /// <param name="kisi">Kişiye ait kimlik bilgisi değeridir. Kisi tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IGercekSahisFluentKisi KisiAta(Kisi kisi) => new Kilavuz(kisi);

            /// <summary>
            /// Kişinin T.C. kimlik numarasının atanması için kullanılır.
            /// </summary>
            /// <param name="tckn">Kişinin T.C. kimlik numarası değeridir. String tipinde olmalıdır.</param>
            public IGercekSahisFluentTCKN TCKNIle(string tckn)
            {
                _tckn = tckn;
                return this;
            }

            /// <summary>
            /// Kişinin görev bilgisi değerinin atanması için kullanılır.
            /// </summary>
            /// <param name="gorev">Kişinin görev bilgisi değeridir. MetinTip tipinde olmalıdır.</param>
            public IGercekSahisFluentGorev GorevIle(MetinTip gorev)
            {
                _gorev = gorev;
                return this;
            }

            /// <summary>
            /// Kişiye ait iletişim bilgisi değerinin atanması için kullanılır.
            /// </summary>
            /// <param name="iletisimBilgisi">Kişiye ait iletişim bilgisi değeridir. IletisimBilgisi tipinde olmalıdır.</param>
            public IGercekSahisFluentIletisim IletisimBilgisiIle(IletisimBilgisi iletisimBilgisi)
            {
                _iletisim = iletisimBilgisi;
                return this;
            }

            public GercekSahis Olustur()
            {
                return new GercekSahis(_kisi, _tckn, _gorev, _iletisim);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
