using System;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Belgeye eklenmiş ilgiye ilişkin bilgilerdir.
    /// </summary>
    public sealed class Ilgi
    {
        public Ilgi()
        {
        }

        private Ilgi(IdTip id,
            string belgeNo,
            DateTime? tarih,
            char etiket,
            Guid? ekIdDeger,
            MetinTip ad,
            MetinTip aciklama,
            TanimlayiciTip ozId
        )
        {
            Id = id;
            BelgeNo = belgeNo;
            Tarih = tarih;
            Etiket = etiket;
            EkIdDeger = ekIdDeger;
            Ad = ad;
            Aciklama = aciklama;
            OzId = ozId;
        }

        /// <summary>
        ///     İlginin paket içerisindeki tekil belirtecidir. Id değeri paketi oluşturan tarafından belirlenir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public IdTip Id { get; set; }

        /// <summary>
        ///     İlgi, resmi bir yazı ise bu alana söz konusu belgenin numarası verilir.
        /// </summary>
        public string BelgeNo { get; set; }

        /// <summary>
        ///     İlgi yazının tarihidir.
        /// </summary>
        public DateTime? Tarih { get; set; }

        /// <summary>
        ///     Eklenen ek dosyasının etiketidir. (İlgi a ve ilgi b gibi ilgiler için etiket değerleri sırasıyla "a" ve "b"
        ///     olmalıdır.)
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public char Etiket { get; set; }

        /// <summary>
        ///     İlginin paket içerisinde ek olarak eklenmesi durumunda, ilgili ekin tekil anahtarı bu alana verilir.
        /// </summary>
        public Guid? EkIdDeger { get; set; }

        /// <summary>
        ///     İlgi adıdır.
        /// </summary>
        public MetinTip Ad { get; set; }

        /// <summary>
        ///     İlgiye ait açıklamalardır.
        /// </summary>
        public MetinTip Aciklama { get; set; }

        /// <summary>
        ///     İlginin üretildiği sistemdeki tekil anahtar değeridir.
        /// </summary>
        /// <remarks>
        ///     Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        ///     Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        ///     OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        public TanimlayiciTip OzId { get; set; }

        public sealed class Kilavuz : IIlgiFluent
        {
            private MetinTip _ad, _aciklama;
            private string _belgeNo;
            private Guid? _ekIdDeger;
            private char _etiket;
            private readonly IdTip _id;
            private TanimlayiciTip _ozId;
            private DateTime? _tarih;

            private Kilavuz(IdTip id)
            {
                _id = id;
            }

            /// <summary>
            ///     Eklenen ek dosyasının etiketidir. (İlgi a ve ilgi b gibi ilgiler için etiket değerleri sırasıyla "a" ve "b"
            ///     olmalıdır.)
            /// </summary>
            /// <param name="etiket">Eklenen ek dosyasının etiket değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IIlgiFluentEtiket EtiketAta(char etiket)
            {
                _etiket = etiket;
                return this;
            }

            /// <summary>
            ///     İlgiye ait açıklamalardır.
            /// </summary>
            /// <param name="aciklama">İlgiye ait açıklamanın değeridir.</param>
            public IIlgiFluentAciklama AciklamaIle(MetinTip aciklama)
            {
                _aciklama = aciklama;
                return this;
            }

            /// <summary>
            ///     İlgi adıdır.
            /// </summary>
            /// <param name="ad">İlgi adı değeridir.</param>
            public IIlgiFluentAd AdIle(MetinTip ad)
            {
                _ad = ad;
                return this;
            }

            /// <summary>
            ///     İlgi, resmi bir yazı ise bu alana söz konusu belgenin numarası verilir.
            /// </summary>
            /// <param name="belgeNo">Belge numarasının değeridir.</param>
            public IIlgiFluentBelgeNo BelgeNoIle(string belgeNo)
            {
                _belgeNo = belgeNo;
                return this;
            }

            /// <summary>
            ///     İlginin paket içerisinde ek olarak eklenmesi durumunda, ilgili ekin tekil anahtarı değeri bu alana verilir.
            /// </summary>
            /// <param name="ekIdDeger">İlgili ekin tekil anahtar değeridir. Guid tipinde olmalıdır.</param>
            public IIlgiFluentEkIdDeger EkIdDegerIle(Guid ekIdDeger)
            {
                _ekIdDeger = ekIdDeger;
                return this;
            }

            /// <summary>
            ///     İlginin üretildiği sistemdeki tekil anahtar değeridir.
            /// </summary>
            /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
            /// <remarks>
            ///     Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
            ///     Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
            ///     OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
            /// </remarks>
            public IIlgiFluentOzId OzIdIle(TanimlayiciTip ozId)
            {
                _ozId = ozId;
                return this;
            }

            /// <summary>
            ///     İlgi yazının tarihidir.
            /// </summary>
            /// <param name="tarih">İlgi yazının tarih değeridir.</param>
            public IIlgiFluentTarih TarihIle(DateTime tarih)
            {
                _tarih = tarih;
                return this;
            }

            public Ilgi Olustur()
            {
                return new(_id, _belgeNo, _tarih, _etiket, _ekIdDeger, _ad, _aciklama, _ozId);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     İlginin paket içerisindeki tekil belirtecidir. Id değeri paketi oluşturan tarafından belirlenir.
            /// </summary>
            /// <param name="id">İlginin paket içerisindeki tekil belirtecinin değeridir. IdTip tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IIlgiFluentId IdAta(IdTip id)
            {
                return new Kilavuz(id);
            }
        }
    }
}