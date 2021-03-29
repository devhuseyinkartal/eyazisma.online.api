using System;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Herhangi bir tarafa ait iletişim bilgisidir.
    /// </summary>
    public sealed class IletisimBilgisi
    {
        public IletisimBilgisi()
        {
        }

        private IletisimBilgisi(string telefon,
            string telefonDiger,
            string faks,
            string ePosta,
            string kepAdresi,
            string webAdresi,
            MetinTip adres,
            IsimTip ilce,
            IsimTip il,
            IsimTip ulke
        )
        {
            Telefon = telefon;
            TelefonDiger = telefonDiger;
            Faks = faks;
            EPosta = ePosta;
            KepAdresi = kepAdresi;
            WebAdresi = webAdresi;
            Adres = adres;
            Ilce = ilce;
            Il = il;
            Ulke = ulke;
        }

        /// <summary>
        ///     Telefon numarasıdır.
        /// </summary>
        public string Telefon { get; set; }

        /// <summary>
        ///     Diğer telefon bilgisidir.
        /// </summary>
        public string TelefonDiger { get; set; }

        /// <summary>
        ///     e-Posta bilgisidir.
        /// </summary>
        public string EPosta { get; set; }

        /// <summary>
        ///     KEP adresi bilgisidir.
        /// </summary>
        /// <remarks>Only for version 2.0</remarks>
        public string KepAdresi { get; set; }

        /// <summary>
        ///     Faks numarasıdır.
        /// </summary>
        public string Faks { get; set; }

        /// <summary>
        ///     İnternet adresi bilgisidir.
        /// </summary>
        public string WebAdresi { get; set; }

        /// <summary>
        ///     Açık adres bilgisidir.
        /// </summary>
        public MetinTip Adres { get; set; }

        /// <summary>
        ///     İl bilgisidir.
        /// </summary>
        public IsimTip Il { get; set; }

        /// <summary>
        ///     İlçe bilgisidir.
        /// </summary>
        public IsimTip Ilce { get; set; }

        /// <summary>
        ///     Ülke bilgisidir.
        /// </summary>
        public IsimTip Ulke { get; set; }

        public sealed class Kilavuz : IIletisimBilgisiFluent
        {
            private MetinTip _adres;
            private IsimTip _il, _ilce, _ulke;
            private string _telefon, _telefonDiger, _ePosta, _kepAdresi, _faks, _webAdresi;

            private Kilavuz()
            {
            }

            /// <summary>
            ///     Açık adres bilgisidir.
            /// </summary>
            /// <param name="adres">Açık adres bilgisi değeridir. MetinTip tipinde olmalıdır.</param>
            public IIletisimBilgisiFluentV1X AdresIle(MetinTip adres)
            {
                _adres = adres;
                return this;
            }

            /// <summary>
            ///     e-Posta bilgisidir.
            /// </summary>
            /// <param name="ePosta">e-Posta bilgisi değeridir. String tipinde olmalıdır.</param>
            public IIletisimBilgisiFluentV1X EPostaIle(string ePosta)
            {
                _ePosta = ePosta;
                return this;
            }

            /// <summary>
            ///     Faks numarasıdır.
            /// </summary>
            /// <param name="faks">Faks numarası değeridir. String tipinde olmalıdır.</param>
            public IIletisimBilgisiFluentV1X FaksIle(string faks)
            {
                _faks = faks;
                return this;
            }

            /// <summary>
            ///     İl bilgisidir.
            /// </summary>
            /// <param name="il">İl bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
            public IIletisimBilgisiFluentV1X IlIle(IsimTip il)
            {
                _il = il;
                return this;
            }

            /// <summary>
            ///     İlçe bilgisidir.
            /// </summary>
            /// <param name="ilce">İlçe bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
            public IIletisimBilgisiFluentV1X IlceIle(IsimTip ilce)
            {
                _ilce = ilce;
                return this;
            }

            /// <summary>
            ///     Telefon numarasıdır.
            /// </summary>
            /// <param name="telefon">Telefon numarası değeridir. String tipinde olmalıdır.</param>
            public IIletisimBilgisiFluentV1X TelefonIle(string telefon)
            {
                _telefon = telefon;
                return this;
            }

            /// <summary>
            ///     Diğer telefon bilgisidir.
            /// </summary>
            /// <param name="telefonDiger">Diğer telefon bilgisi değeridir. String tipinde olmalıdır.</param>
            public IIletisimBilgisiFluentV1X TelefonDigerIle(string telefonDiger)
            {
                _telefonDiger = telefonDiger;
                return this;
            }

            /// <summary>
            ///     Ülke bilgisidir.
            /// </summary>
            /// <param name="ulke">Ülke bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
            public IIletisimBilgisiFluentV1X UlkeIle(IsimTip ulke)
            {
                _ulke = ulke;
                return this;
            }

            /// <summary>
            ///     İnternet adresi bilgisidir.
            /// </summary>
            /// <param name="webAdresi">İnternet adresi bilgisi değeridir. String tipinde olmalıdır.</param>
            public IIletisimBilgisiFluentV1X WebAdresiIle(string webAdresi)
            {
                _webAdresi = webAdresi;
                return this;
            }

            /// <summary>
            ///     Açık adres bilgisidir.
            /// </summary>
            /// <param name="adres">Açık adres bilgisi değeridir. MetinTip tipinde olmalıdır.</param>
            IIletisimBilgisiFluentV2X IIletisimBilgisiFluentV2X.AdresIle(MetinTip adres)
            {
                _adres = adres;
                return this;
            }

            /// <summary>
            ///     e-Posta bilgisidir.
            /// </summary>
            /// <param name="ePosta">e-Posta bilgisi değeridir. String tipinde olmalıdır.</param>
            IIletisimBilgisiFluentV2X IIletisimBilgisiFluentV2X.EPostaIle(string ePosta)
            {
                _ePosta = ePosta;
                return this;
            }

            /// <summary>
            ///     Faks numarasıdır.
            /// </summary>
            /// <param name="faks">Faks numarası değeridir. String tipinde olmalıdır.</param>
            IIletisimBilgisiFluentV2X IIletisimBilgisiFluentV2X.FaksIle(string faks)
            {
                _faks = faks;
                return this;
            }

            /// <summary>
            ///     İl bilgisidir.
            /// </summary>
            /// <param name="il">İl bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
            IIletisimBilgisiFluentV2X IIletisimBilgisiFluentV2X.IlIle(IsimTip il)
            {
                _il = il;
                return this;
            }

            /// <summary>
            ///     İlçe bilgisidir.
            /// </summary>
            /// <param name="ilce">İlçe bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
            IIletisimBilgisiFluentV2X IIletisimBilgisiFluentV2X.IlceIle(IsimTip ilce)
            {
                _ilce = ilce;
                return this;
            }

            /// <summary>
            ///     KEP adresi bilgisidir.
            /// </summary>
            /// <param name="kepAdresi">KEP adresi bilgisi değeridir. String tipinde olmalıdır.</param>
            public IIletisimBilgisiFluentV2X KepAdresiIle(string kepAdresi)
            {
                _kepAdresi = kepAdresi;
                return this;
            }

            /// <summary>
            ///     Telefon numarasıdır.
            /// </summary>
            /// <param name="telefon">Telefon numarası değeridir. String tipinde olmalıdır.</param>
            IIletisimBilgisiFluentV2X IIletisimBilgisiFluentV2X.TelefonIle(string telefon)
            {
                _telefon = telefon;
                return this;
            }

            /// <summary>
            ///     Diğer telefon bilgisidir.
            /// </summary>
            /// <param name="telefonDiger">Diğer telefon bilgisi değeridir. String tipinde olmalıdır.</param>
            IIletisimBilgisiFluentV2X IIletisimBilgisiFluentV2X.TelefonDigerIle(string telefonDiger)
            {
                _telefonDiger = telefonDiger;
                return this;
            }

            /// <summary>
            ///     Ülke bilgisidir.
            /// </summary>
            /// <param name="ulke">Ülke bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
            IIletisimBilgisiFluentV2X IIletisimBilgisiFluentV2X.UlkeIle(IsimTip ulke)
            {
                _ulke = ulke;
                return this;
            }

            /// <summary>
            ///     İnternet adresi bilgisidir.
            /// </summary>
            /// <param name="webAdresi">İnternet adresi bilgisi değeridir. String tipinde olmalıdır.</param>
            IIletisimBilgisiFluentV2X IIletisimBilgisiFluentV2X.WebAdresiIle(string webAdresi)
            {
                _webAdresi = webAdresi;
                return this;
            }

            public IletisimBilgisi Olustur()
            {
                return new IletisimBilgisi(_telefon, _telefonDiger, _faks, _ePosta, _kepAdresi, _webAdresi, _adres,
                    _ilce, _il, _ulke);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            public static IIletisimBilgisiFluentV1X Versiyon1X()
            {
                return new Kilavuz();
            }

            public static IIletisimBilgisiFluentV2X Versiyon2X()
            {
                return new Kilavuz();
            }
        }
    }
}