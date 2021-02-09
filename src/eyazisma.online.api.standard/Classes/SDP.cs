using eyazisma.online.api.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    /// Standart Dosya Planı bilgisidir.
    /// </summary>
    /// <remarks>Only for version 2.0</remarks>
    public sealed class SDP
    {
        public SDP() { }

        private SDP(string kod, string ad, string aciklama)
        {
            Kod = kod;
            Ad = ad;
            Aciklama = aciklama;
        }

        /// <summary>
        /// Standart Dosya Planı kodudur.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string Kod { get; set; }

        /// <summary>
        /// Standart Dosya Planı adıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string Ad { get; set; }

        /// <summary>
        /// Standart Dosya Planı açıklamasıdır.
        /// </summary>
        public string Aciklama { get; set; }

        public sealed class Kilavuz : ISDPFluent
        {
            private string _kod, _ad, _aciklama;

            private Kilavuz(string kod)
            {
                _kod = kod;
            }

            /// <summary>
            /// Standart Dosya Planı kodudur.
            /// </summary>
            /// <param name="ad">SDP kod değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static ISDPFluentKod KodAta(string kod) => new Kilavuz(kod);

            /// <summary>
            /// Standart Dosya Planı adıdır.
            /// </summary>
            /// <param name="ad">SDP adı değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public ISDPFluentAd AdAta(string ad)
            {
                _ad = ad;
                return this;
            }

            /// <summary>
            /// Standart Dosya Planı açıklamasıdır.
            /// </summary>
            /// <param name="aciklama">SDP açıklaması değeridir.</param>
            public ISDPFluentAciklama AciklamaIle(string aciklama)
            {
                _aciklama = aciklama;
                return this;
            }

            public SDP Olustur()
            {
                return new SDP(_kod, _ad, _aciklama);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
