using eyazisma.online.api.framework.Enums;
using eyazisma.online.api.framework.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.framework.Classes
{
    /// <summary>
    /// Belgeye eklenmiş eke ilişkin bilgilerdir.
    /// </summary>
    public sealed class Ek
    {
        public Ek()
        {
            ImzaliMi = true;
        }

        private Ek(IdTip id,
                  string belgeNo,
                  EkTuru ekTuru,
                  string dosyaAdi,
                  string mimeTuru,
                  MetinTip ad,
                  int siraNo,
                  MetinTip aciklama,
                  string referans,
                  TanimlayiciTip ozId,
                  bool imzaliMi,
                  Ozet ozet)
        {
            Id = id;
            BelgeNo = belgeNo;
            Tur = ekTuru;
            DosyaAdi = dosyaAdi;
            MimeTuru = mimeTuru;
            Ad = ad;
            SiraNo = siraNo;
            Aciklama = aciklama;
            Referans = referans;
            OzId = ozId;
            ImzaliMi = imzaliMi;
            Ozet = ozet;
        }

        /// <summary>
        /// Ekin paket içerisindeki tekil belirtecidir. Id değeri paketi oluşturan tarafından belirlenir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public IdTip Id { get; set; }

        /// <summary>
        /// Eklenen ek resmi bir belge ise bu alana söz konusu belgenin numarası verilir.
        /// </summary>
        public string BelgeNo { get; set; }

        /// <summary>
        /// Ekin türünü belirtir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public EkTuru Tur { get; set; }

        /// <summary>
        /// Ekin dosya sistemindeki adıdır.
        /// </summary>
        /// <remarks>Ek türünün DED olması durumunda bu alan zorunludur.</remarks>
        public string DosyaAdi { get; set; }

        /// <summary>
        /// Eklenen dosyanın mime türü bilgisidir.
        /// </summary>
        /// <remarks>Ek türünün DED olması durumunda bu alan zorunludur.</remarks>
        public string MimeTuru { get; set; }

        /// <summary>
        /// Ekin adıdır.
        /// </summary>
        public MetinTip Ad { get; set; }

        /// <summary>
        /// Belge üzerinde ek için belirtilen sıra bilgisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public int SiraNo { get; set; }

        /// <summary>
        /// Eke ait açıklamalardır.
        /// </summary>
        public MetinTip Aciklama { get; set; }

        /// <summary>
        /// Ekin kaynağını gösteren URI değeridir.
        /// </summary>
        /// <remarks>Ek türü HRF ise bu alan zorunludur.</remarks>
        public string Referans { get; set; }

        /// <summary>
        /// Ekin üretildiği sistemdeki tekil anahtar değeridir. 
        /// </summary>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        public TanimlayiciTip OzId { get; set; }

        /// <summary>
        /// Ekin paket içerisinde imzalı olarak bulunup bulunmadığı bilgisidir.
        /// </summary>
        public bool ImzaliMi { get; set; }

        /// <summary>
        /// Ekin HRF (Harici referans) olması durumunda referans verilmiş belgenin özet değerini barındırır.
        /// </summary>
        public Ozet Ozet { get; set; }

        public sealed class Kilavuz
        {
            /// <summary>
            /// Eke ilişkin elektronik dosyanın paket içerisine eklenmesiyle oluşturulmuş eklerdir.
            /// </summary>
            public static IEkDEDFluent DahiliElektronikDosya() => new EkDEDFluent();
            /// <summary>
            /// Elektronik olarak ifade edilemeyen eklerdir. Bu tür ekler paket içerisine eklenemez ancak “Üstveri” bileşeninde ek olarak tanımlanırlar.
            /// </summary>
            public static IEkFZKFluent FizikiNesne() => new EkFZKFluent();
            /// <summary>
            /// Bir URI ile ifade edilebilen, paket içerisine elektronik dosya olarak eklenmesi pratik olarak mümkün olmayan veya tercih edilmeyen eklerdir.
            /// </summary>
            public static IEkHRFFluent HariciReferans() => new EkHRFFluent();
        }

        private class EkDEDFluent : IDisposable,
                                     IEkDEDFluent,
                                     IEkDEDFluentId,
                                     IEkDEDFluentBelgeNo,
                                     IEkDEDFluentDosyaAdi,
                                     IEkDEDFluentMimeTuru,
                                     IEkDEDFluentAd,
                                     IEkDEDFluentSiraNo,
                                     IEkDEDFluentAciklama,
                                     IEkDEDFluentOzId,
                                     IEkDEDFluentImzaliMi
        {
            private IdTip _id;
            private MetinTip _ad, _aciklama;
            private string _belgeNo, _dosyaAdi, _mimeTuru;
            private int _siraNo;
            private bool _imzaliMi;
            private TanimlayiciTip _ozId;

            /// <summary>
            /// Ekin paket içerisindeki tekil belirtecidir. Id değeri paketi oluşturan tarafından belirlenir.
            /// </summary>
            /// <param name="id">Ekin paket içerisindeki tekil belirtecinin değeridir. IdTip tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkDEDFluentId IdAta(IdTip id)
            {
                _id = id;
                return this;
            }

            /// <summary>
            /// Eklenen ek resmi bir belge ise bu alana söz konusu belgenin numarası verilir.
            /// </summary>
            /// <param name="belgeNo">Belge numarasının değeridir.</param>
            public IEkDEDFluentBelgeNo BelgeNoIle(string belgeNo)
            {
                _belgeNo = belgeNo;
                return this;
            }

            /// <summary>
            /// Ekin dosya sistemindeki adıdır.
            /// </summary>
            /// <param name="dosyaAdi">Ekin dosya sistemindeki adının değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkDEDFluentDosyaAdi DosyaAdiAta(string dosyaAdi)
            {
                _dosyaAdi = dosyaAdi;
                return this;
            }

            /// <summary>
            /// Eklenen dosyanın mime türü bilgisidir.
            /// </summary>
            /// <param name="mimeTuru">Eklenen dosyanın mime türü bilgisi değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkDEDFluentMimeTuru MimeTuruAta(string mimeTuru)
            {
                _mimeTuru = mimeTuru;
                return this;
            }

            /// <summary>
            /// Ekin adıdır.
            /// </summary>
            /// <param name="ad">Ekin ad değeridir.</param>
            public IEkDEDFluentAd AdIle(MetinTip ad)
            {
                _ad = ad;
                return this;
            }

            /// <summary>
            /// Belge üzerinde ek için belirtilen sıra bilgisidir.
            /// </summary>
            /// <param name="siraNo">Ek için belirtilen sıra bilgisinin değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkDEDFluentSiraNo SiraNoAta(int siraNo)
            {
                _siraNo = siraNo;
                return this;
            }

            /// <summary>
            /// Eke ait açıklamalardır.
            /// </summary>
            /// <param name="aciklama">Eke ait açıklamaların değeridir.</param>
            public IEkDEDFluentAciklama AciklamaIle(MetinTip aciklama)
            {
                _aciklama = aciklama;
                return this;
            }

            /// <summary>
            /// Ekin üretildiği sistemdeki tekil anahtar değeridir. 
            /// </summary>
            /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
            /// <remarks>
            /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
            /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
            /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
            /// </remarks>
            public IEkDEDFluentOzId OzIdIle(TanimlayiciTip ozId)
            {
                _ozId = ozId;
                return this;
            }

            /// <summary>
            /// Ekin paket içerisinde imzalı olarak bulunup bulunmadığı bilgisidir.
            /// </summary>
            /// <param name="imzaliMi">Ekin paket içerisinde imzalı olarak bulunup bulunmadığı bilgisinin değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkDEDFluentImzaliMi ImzaliMiAta(bool imzaliMi)
            {
                _imzaliMi = imzaliMi;
                return this;
            }

            public Ek Olustur()
            {
                return new Ek(_id, _belgeNo, EkTuru.DED, _dosyaAdi, _mimeTuru, _ad, _siraNo, _aciklama, null, _ozId, _imzaliMi, null);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }

        private class EkFZKFluent : IDisposable,
                                     IEkFZKFluent,
                                     IEkFZKFluentId,
                                     IEkFZKFluentBelgeNo,
                                     IEkFZKFluentAd,
                                     IEkFZKFluentSiraNo,
                                     IEkFZKFluentAciklama
        {
            private IdTip _id;
            private MetinTip _ad, _aciklama;
            private string _belgeNo;
            private int _siraNo;

            /// <summary>
            /// Eke ait açıklamalardır.
            /// </summary>
            /// <param name="aciklama">Eke ait açıklamaların değeridir.</param>
            public IEkFZKFluentAciklama AciklamaIle(MetinTip aciklama)
            {
                _aciklama = aciklama;
                return this;
            }

            /// <summary>
            /// Ekin adıdır.
            /// </summary>
            /// <param name="ad">Ekin ad değeridir.</param>
            public IEkFZKFluentAd AdIle(MetinTip ad)
            {
                _ad = ad;
                return this;
            }

            /// <summary>
            /// Eklenen ek resmi bir belge ise bu alana söz konusu belgenin numarası verilir.
            /// </summary>
            /// <param name="belgeNo">Belge numarasının değeridir.</param>
            public IEkFZKFluentBelgeNo BelgeNoIle(string belgeNo)
            {
                _belgeNo = belgeNo;
                return this;
            }

            /// <summary>
            /// Ekin paket içerisindeki tekil belirtecidir. Id değeri paketi oluşturan tarafından belirlenir.
            /// </summary>
            /// <param name="id">Ekin paket içerisindeki tekil belirtecinin değeridir. IdTip tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkFZKFluentId IdAta(IdTip id)
            {
                _id = id;
                return this;
            }

            /// <summary>
            /// Belge üzerinde ek için belirtilen sıra bilgisidir.
            /// </summary>
            /// <param name="siraNo">Ek için belirtilen sıra bilgisinin değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkFZKFluentSiraNo SiraNoAta(int siraNo)
            {
                _siraNo = siraNo;
                return this;
            }

            public Ek Olustur()
            {
                return new Ek(_id, _belgeNo, EkTuru.FZK, null, null, _ad, _siraNo, _aciklama, null, null, false, null);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }

        private class EkHRFFluent : IDisposable,
                                     IEkHRFFluent,
                                     IEkHRFFluentId,
                                     IEkHRFFluentBelgeNo,
                                     IEkHRFFluentAd,
                                     IEkHRFFluentSiraNo,
                                     IEkHRFFluentAciklama,
                                     IEkHRFFluentReferans,
                                     IEkHRFFluentOzId,
                                     IEkHRFFluentOzet
        {
            private IdTip _id;
            private MetinTip _ad, _aciklama;
            private string _belgeNo, _referans;
            private TanimlayiciTip _ozId;
            private Ozet _ozet;
            private int _siraNo;

            /// <summary>
            /// Eke ait açıklamalardır.
            /// </summary>
            /// <param name="aciklama">Eke ait açıklamaların değeridir.</param>
            public IEkHRFFluentAciklama AciklamaIle(MetinTip aciklama)
            {
                _aciklama = aciklama;
                return this;
            }

            /// <summary>
            /// Ekin adıdır.
            /// </summary>
            /// <param name="ad">Ekin ad değeridir.</param>
            public IEkHRFFluentAd AdIle(MetinTip ad)
            {
                _ad = ad;
                return this;
            }

            /// <summary>
            /// Eklenen ek resmi bir belge ise bu alana söz konusu belgenin numarası verilir.
            /// </summary>
            /// <param name="belgeNo">Belge numarasının değeridir.</param>
            public IEkHRFFluentBelgeNo BelgeNoIle(string belgeNo)
            {
                _belgeNo = belgeNo;
                return this;
            }

            /// <summary>
            /// Ekin paket içerisindeki tekil belirtecidir. Id değeri paketi oluşturan tarafından belirlenir.
            /// </summary>
            /// <param name="id">Ekin paket içerisindeki tekil belirtecinin değeridir. IdTip tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkHRFFluentId IdAta(IdTip id)
            {
                _id = id;
                return this;
            }

            /// <summary>
            /// Referans verilmiş belgenin özet değerini barındırır.
            /// </summary>
            /// <param name="ozet">Referans verilmiş belgenin özet değeridir. Ozet tipinde olmalıdır.</param>
            public IEkHRFFluentOzet OzetIle(Ozet ozet)
            {
                _ozet = ozet;
                return this;
            }

            /// <summary>
            /// Ekin üretildiği sistemdeki tekil anahtar değeridir. 
            /// </summary>
            /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
            /// <remarks>
            /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
            /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
            /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
            /// </remarks>
            public IEkHRFFluentOzId OzIdIle(TanimlayiciTip ozId)
            {
                _ozId = ozId;
                return this;
            }

            /// <summary>
            /// Ekin kaynağını gösteren URI değeridir.
            /// </summary>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkHRFFluentReferans ReferansAta(string referans)
            {
                _referans = referans;
                return this;
            }

            /// <summary>
            /// Belge üzerinde ek için belirtilen sıra bilgisidir.
            /// </summary>
            /// <param name="siraNo">Ek için belirtilen sıra bilgisinin değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkHRFFluentSiraNo SiraNoAta(int siraNo)
            {
                _siraNo = siraNo;
                return this;
            }

            public Ek Olustur()
            {
                return new Ek(_id, _belgeNo, EkTuru.HRF, null, null, _ad, _siraNo, _aciklama, _referans, _ozId, false, _ozet);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
