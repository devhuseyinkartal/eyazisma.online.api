using System;
using System.Collections.Generic;
using eyazisma.online.api.Enums;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Belgeye ilişkin kimlik bilgilerinin tanımlandığı bileşendir.
    /// </summary>
    public sealed class Ustveri
    {
        public Ustveri()
        {
        }

        private Ustveri(Guid belgeId,
            MetinTip konu,
            GuvenlikKoduTuru guvenlikKodu,
            DateTime? guvenlikKoduGecerlilikTarihi,
            TanimlayiciTip ozId,
            List<Dagitim> dagitimlar,
            List<Ek> ekler,
            List<Ilgi> ilgiler,
            string dil,
            Olusturan olusturan,
            List<Ilgili> ilgililer,
            string dosyaAdi,
            DateTime? tarih = null,
            string belgeNo = null,
            SDPBilgisi sdpBilgisi = null,
            List<HEYSK> heysKodlari = null,
            DogrulamaBilgisi dogrulamaBilgisi = null
        )
        {
            BelgeId = belgeId;
            Konu = konu;
            GuvenlikKodu = guvenlikKodu;
            GuvenlikKoduGecerlilikTarihi = guvenlikKoduGecerlilikTarihi;
            OzId = ozId;
            Dagitimlar = dagitimlar;
            Ekler = ekler;
            Ilgiler = ilgiler;
            Dil = dil;
            Olusturan = olusturan;
            Ilgililer = ilgililer;
            DosyaAdi = dosyaAdi;
            if (tarih.HasValue)
                Tarih = tarih.Value;
            BelgeNo = belgeNo;
            SdpBilgisi = sdpBilgisi;
            HEYSKodlari = heysKodlari;
            DogrulamaBilgisi = dogrulamaBilgisi;
        }

        /// <summary>
        ///     Belgenin tekil numarasıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public Guid BelgeId { get; set; }

        /// <summary>
        ///     Belgenin konusudur.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public MetinTip Konu { get; set; }

        /// <summary>
        ///     Belgenin tarihidir.
        /// </summary>
        /// <remarks>
        ///     Zorunlu alandır.
        ///     UTC ofset değeri ile verilmesi tavsiye edilir.
        /// </remarks>
        [Obsolete("Since version 2.0", false)]
        public DateTime Tarih { get; set; }

        /// <summary>
        ///     Belge numarasıdır.
        /// </summary>
        /// <remarks>
        ///     Zorunlu alandır.
        ///     Resmi yazışmalara ilişkin mevzuatta belirtilen biçime uygun olmalıdır.
        /// </remarks>
        [Obsolete("Since version 2.0", false)]
        public string BelgeNo { get; set; }

        /// <summary>
        ///     Belgenin güvenlik derecesini gösterir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public GuvenlikKoduTuru GuvenlikKodu { get; set; }

        /// <summary>
        ///     Belgenin güvenlik seviyesinin ortadan kalktığı tarihtir.
        /// </summary>
        /// <remarks>
        ///     Bu elemanın "null" olarak verilmesi belgenin güvenlik kodu geçerlilik tarihinin süresiz olduğunu belirtir.
        ///     Elemanın bulunmaması ise belgenin güvenlik kodu geçerlilik tarihinin belirsiz olduğu anlamına gelir.
        /// </remarks>
        public DateTime? GuvenlikKoduGecerlilikTarihi { get; set; }

        /// <summary>
        ///     "Üst Yazı" bileşeni olarak pakete eklenmiş dosyanın formatıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        internal string MimeTuru { get; set; }

        /// <summary>
        ///     Belgenin üretildiği sistemdeki tekil anahtar değeridir.
        /// </summary>
        /// <remarks>
        ///     Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        ///     Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        ///     OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        public TanimlayiciTip OzId { get; set; }

        /// <summary>
        ///     Belgenin iletileceği tarafların listelendiği elemandır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public List<Dagitim> Dagitimlar { get; set; }

        /// <summary>
        ///     Belgenin eklerinin listelendiği elemandır.
        /// </summary>
        public List<Ek> Ekler { get; set; }

        /// <summary>
        ///     Belgenin ilgilerinin listelendiği elemandır.
        /// </summary>
        public List<Ilgi> Ilgiler { get; set; }

        /// <summary>
        ///     Belgenin oluşturulduğu dildir.
        /// </summary>
        /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
        public string Dil { get; set; }

        /// <summary>
        ///     Belgeyi oluşturan taraftır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public Olusturan Olusturan { get; set; }

        /// <summary>
        ///     Belgeyle ilgili olarak iletişim kurulacak kurum, kuruluş, gerçek veya tüzel kişi bilgilerinin listelendiği
        ///     elemandır.
        /// </summary>
        public List<Ilgili> Ilgililer { get; set; }

        /// <summary>
        ///     "Üst Yazı" bileşeninin dosya sistemindeki adıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string DosyaAdi { get; set; }

        /// <summary>
        ///     Belgenin standart dosya planı bilgisidir.
        /// </summary>
        /// <remarks>Only for version 2.0</remarks>
        public SDPBilgisi SdpBilgisi { get; set; }

        /// <summary>
        ///     Belgenin Hizmet Envanteri Yönetim Sistemi bilgilerinin listelendiği elemandır.
        /// </summary>
        /// <remarks>Only for version 2.0</remarks>
        public List<HEYSK> HEYSKodlari { get; set; }

        /// <summary>
        ///     Belgenin doğrulama yapılacağı web adresi bilgisidir.
        /// </summary>
        /// <remarks>
        ///     Zorunlu alandır.
        ///     Only for version 2.0
        /// </remarks>
        public DogrulamaBilgisi DogrulamaBilgisi { get; set; }

        public sealed class Kilavuz
        {
            public static IUstveriV1XFluent Versiyon1X()
            {
                return new UstveriV1XFluent();
            }

            public static IUstveriV2XFluent Versiyon2X()
            {
                return new UstveriV2XFluent();
            }
        }

        private class UstveriV1XFluent : IDisposable,
            IUstveriV1XFluent,
            IUstveriV1XFluentBelgeId,
            IUstveriV1XFluentKonu,
            IUstveriV1XFluentTarih,
            IUstveriV1XFluentBelgeNo,
            IUstveriV1XFluentGuvenlikKodu,
            IUstveriV1XFluentGuvenlikKoduGecerlilikTarihi,
            IUstveriV1XFluentOzId,
            IUstveriV1XFluentDagitim,
            IUstveriV1XFluentDagitimlar,
            IUstveriV1XFluentEk,
            IUstveriV1XFluentEkler,
            IUstveriV1XFluentIlgi,
            IUstveriV1XFluentIlgiler,
            IUstveriV1XFluentDil,
            IUstveriV1XFluentOlusturan,
            IUstveriV1XFluentIlgili,
            IUstveriV1XFluentIlgililer,
            IUstveriV1XFluentDosyaAdi
        {
            private Guid _belgeId;
            private string _belgeNo, _dil, _dosyaAdi;
            private List<Dagitim> _dagitimlar;
            private List<Ek> _ekler;
            private GuvenlikKoduTuru _guvenlikKodu;
            private List<Ilgi> _ilgiler;
            private List<Ilgili> _ilgililer;
            private MetinTip _konu;
            private Olusturan _olusturan;
            private TanimlayiciTip _ozId;
            private DateTime? _tarih, _guvenlikKoduGecerlilikTarihi;

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Belgenin tekil numarasıdır.
            /// </summary>
            /// <param name="belgeId">Belgenin tekil numarası değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV1XFluentBelgeId BelgeIdAta(Guid belgeId)
            {
                _belgeId = belgeId;
                return this;
            }

            /// <summary>
            ///     Belgenin konusudur.
            /// </summary>
            /// <param name="konu">Belgenin konu bilgisi değeridir. MetinTip tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV1XFluentKonu KonuAta(MetinTip konu)
            {
                _konu = konu;
                return this;
            }

            /// <summary>
            ///     Belgenin güvenlik derecesini gösterir.
            /// </summary>
            /// <param name="guvenlikKodu">Belgenin güvenlik derecesi değeridir. GuvenlikKoduTuru tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV1XFluentGuvenlikKodu GuvenlikKoduAta(GuvenlikKoduTuru guvenlikKodu)
            {
                _guvenlikKodu = guvenlikKodu;
                return this;
            }

            /// <summary>
            ///     Belgeye eklenecek eke ilişkin bilgidir.
            /// </summary>
            /// <param name="ek">Belgeye eklenecek ekin değeridir. Ek tipinde olmalıdır.</param>
            public IUstveriV1XFluentEk EkIle(Ek ek)
            {
                if (ek != null)
                {
                    if (_ekler == null)
                        _ekler = new List<Ek>();

                    _ekler.Add(ek);
                }

                return this;
            }

            /// <summary>
            ///     Belgeye eklenecek eklere ilişkin bilgilerdir.
            /// </summary>
            /// <param name="ekler">Belgeye eklenecek eklerin değeridir. Ek listesi tipinde olmalıdır.</param>
            public IUstveriV1XFluentEkler EklerIle(List<Ek> ekler)
            {
                if (ekler != null)
                {
                    if (_ekler == null)
                        _ekler = new List<Ek>();

                    _ekler.AddRange(ekler);
                }

                return this;
            }

            /// <summary>
            ///     Belgeye eklenecek ilgiye ilişkin bilgidir.
            /// </summary>
            /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
            public IUstveriV1XFluentIlgi IlgiIle(Ilgi ilgi)
            {
                if (ilgi != null)
                {
                    if (_ilgiler == null)
                        _ilgiler = new List<Ilgi>();

                    _ilgiler.Add(ilgi);
                }

                return this;
            }

            /// <summary>
            ///     Belgeye eklenecek ilgilere ilişkin bilgilerdir.
            /// </summary>
            /// <param name="ilgiler">Belgeye eklenecek ilgilerin değeridir. Ilgi listesi tipinde olmalıdır.</param>
            public IUstveriV1XFluentIlgiler IlgilerIle(List<Ilgi> ilgiler)
            {
                if (ilgiler != null)
                {
                    if (_ilgiler == null)
                        _ilgiler = new List<Ilgi>();

                    _ilgiler.AddRange(ilgiler);
                }

                return this;
            }

            /// <summary>
            ///     Belgenin oluşturulduğu dildir.
            /// </summary>
            /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
            /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
            public IUstveriV1XFluentDil DilIle(string dil)
            {
                _dil = dil;
                return this;
            }

            /// <summary>
            ///     Belgeyi oluşturan taraftır.
            /// </summary>
            /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV1XFluentOlusturan OlusturanAta(Olusturan olusturan)
            {
                _olusturan = olusturan;
                return this;
            }

            public Ustveri Olustur()
            {
                return new(_belgeId,
                    _konu,
                    _guvenlikKodu,
                    _guvenlikKoduGecerlilikTarihi,
                    _ozId,
                    _dagitimlar,
                    _ekler,
                    _ilgiler,
                    _dil,
                    _olusturan,
                    _ilgililer,
                    _dosyaAdi,
                    _tarih,
                    _belgeNo);
            }

            /// <summary>
            ///     Belgenin güvenlik seviyesinin ortadan kalktığı tarihtir.
            /// </summary>
            /// <param name="tarih">Belgenin güvenlik seviyesinin ortadan kalktığı tarih değeridir.</param>
            public IUstveriV1XFluentGuvenlikKoduGecerlilikTarihi GuvenlikKoduGecerlilikTarihiIle(DateTime tarih)
            {
                _guvenlikKoduGecerlilikTarihi = tarih;
                return this;
            }

            /// <summary>
            ///     Belgenin üretildiği sistemdeki tekil anahtar değeridir.
            /// </summary>
            /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
            /// <remarks>
            ///     Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
            ///     Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
            ///     OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
            /// </remarks>
            public IUstveriV1XFluentOzId OzIdIle(TanimlayiciTip ozId)
            {
                _ozId = ozId;
                return this;
            }

            /// <summary>
            ///     Belgenin dağıtımının yapıldığı taraf bilgisidir.
            /// </summary>
            /// <param name="dagitim">Belgenin dağıtımının yapıldığı taraf bilgisi değeridir. Dagitim tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV1XFluentDagitim DagitimAta(Dagitim dagitim)
            {
                if (dagitim != null)
                {
                    if (_dagitimlar == null)
                        _dagitimlar = new List<Dagitim>();

                    _dagitimlar.Add(dagitim);
                }

                return this;
            }

            /// <summary>
            ///     Belgenin dağıtımının yapıldığı taraf bilgileridir.
            /// </summary>
            /// <param name="dagitimlar">Belgenin dağıtımının yapıldığı taraf bilgileri değeridir. Dagitim listesi tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV1XFluentDagitimlar DagitimlarAta(List<Dagitim> dagitimlar)
            {
                if (dagitimlar != null)
                {
                    if (_dagitimlar == null)
                        _dagitimlar = new List<Dagitim>();

                    _dagitimlar.AddRange(dagitimlar);
                }

                return this;
            }

            /// <summary>
            ///     Belgenin tarihidir.
            /// </summary>
            /// <param name="tarih">Belgenin tarih değeridir.</param>
            /// <remarks>
            ///     Zorunlu alandır.
            ///     UTC ofset değeri ile verilmesi tavsiye edilir.
            /// </remarks>
            public IUstveriV1XFluentTarih TarihAta(DateTime tarih)
            {
                _tarih = tarih;
                return this;
            }

            /// <summary>
            ///     Belge ile ilgili iletişim kurulacak tarafa ait bilgidir.
            /// </summary>
            /// <param name="ilgili">Ilgili tipinde olmalıdır.</param>
            public IUstveriV1XFluentIlgili IlgiliIle(Ilgili ilgili)
            {
                if (ilgili != null)
                {
                    if (_ilgililer == null)
                        _ilgililer = new List<Ilgili>();

                    _ilgililer.Add(ilgili);
                }

                return this;
            }

            /// <summary>
            ///     Belge ile ilgili iletişim kurulacak taraflara ait bilgilerdir.
            /// </summary>
            /// <param name="ilgililer">Ilgili listesi tipinde olmalıdır.</param>
            public IUstveriV1XFluentIlgililer IlgililerIle(List<Ilgili> ilgililer)
            {
                if (ilgililer != null)
                {
                    if (_ilgililer == null)
                        _ilgililer = new List<Ilgili>();

                    _ilgililer.AddRange(ilgililer);
                }

                return this;
            }

            /// <summary>
            ///     "Üst Yazı" bileşeninin dosya sistemindeki adıdır.
            /// </summary>
            /// <param name="dosyaAdi">"Üst Yazı" bileşeninin dosya sistemindeki adının değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV1XFluentDosyaAdi DosyaAdiAta(string dosyaAdi)
            {
                _dosyaAdi = dosyaAdi;
                return this;
            }

            /// <summary>
            ///     Belge numarasıdır.
            /// </summary>
            /// <param name="belgeNo">Belge numarası değeridir.</param>
            /// <remarks>
            ///     Zorunlu alandır.
            ///     Resmi yazışmalara ilişkin mevzuatta belirtilen biçime uygun olmalıdır.
            /// </remarks>
            public IUstveriV1XFluentBelgeNo BelgeNoAta(string belgeNo)
            {
                _belgeNo = belgeNo;
                return this;
            }
        }

        private class UstveriV2XFluent : IDisposable,
            IUstveriV2XFluent,
            IUstveriV2XFluentBelgeId,
            IUstveriV2XFluentKonu,
            IUstveriV2XFluentGuvenlikKodu,
            IUstveriV2XFluentGuvenlikKoduGecerlilikTarihi,
            IUstveriV2XFluentOzId,
            IUstveriV2XFluentDagitim,
            IUstveriV2XFluentDagitimlar,
            IUstveriV2XFluentEk,
            IUstveriV2XFluentEkler,
            IUstveriV2XFluentIlgi,
            IUstveriV2XFluentIlgiler,
            IUstveriV2XFluentDil,
            IUstveriV2XFluentOlusturan,
            IUstveriV2XFluentIlgili,
            IUstveriV2XFluentIlgililer,
            IUstveriV2XFluentDosyaAdi,
            IUstveriV2XFluentSdp,
            IUstveriV2XFluentHeysk,
            IUstveriV2XFluentDogrulamaBilgisi
        {
            private Guid _belgeId;
            private List<Dagitim> _dagitimlar;
            private string _dil, _dosyaAdi;
            private DogrulamaBilgisi _dogrulamaBilgisi;
            private List<Ek> _ekler;
            private GuvenlikKoduTuru _guvenlikKodu;
            private DateTime? _guvenlikKoduGecerlilikTarihi;
            private List<HEYSK> _heysKodlari;
            private List<Ilgi> _ilgiler;
            private List<Ilgili> _ilgililer;
            private MetinTip _konu;
            private Olusturan _olusturan;
            private TanimlayiciTip _ozId;
            private SDPBilgisi _sdpBilgisi;

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Belgenin tekil numarasıdır.
            /// </summary>
            /// <param name="belgeId">Belgenin tekil numarası değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV2XFluentBelgeId BelgeIdAta(Guid belgeId)
            {
                _belgeId = belgeId;
                return this;
            }

            /// <summary>
            ///     Belgenin konusudur.
            /// </summary>
            /// <param name="konu">Belgenin konu bilgisi değeridir. MetinTip tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV2XFluentKonu KonuAta(MetinTip konu)
            {
                _konu = konu;
                return this;
            }

            /// <summary>
            ///     Belgeye eklenecek eke ilişkin bilgidir.
            /// </summary>
            /// <param name="ek">Belgeye eklenecek ekin değeridir. Ek tipinde olmalıdır.</param>
            public IUstveriV2XFluentEk EkIle(Ek ek)
            {
                if (ek != null)
                {
                    if (_ekler == null)
                        _ekler = new List<Ek>();

                    _ekler.Add(ek);
                }

                return this;
            }

            /// <summary>
            ///     Belgeye eklenecek eklere ilişkin bilgilerdir.
            /// </summary>
            /// <param name="ekler">Belgeye eklenecek eklerin değeridir. Ek listesi tipinde olmalıdır.</param>
            public IUstveriV2XFluentEkler EklerIle(List<Ek> ekler)
            {
                if (ekler != null)
                {
                    if (_ekler == null)
                        _ekler = new List<Ek>();

                    _ekler.AddRange(ekler);
                }

                return this;
            }

            /// <summary>
            ///     Belgeye eklenecek ilgiye ilişkin bilgidir.
            /// </summary>
            /// <param name="ilgi">Belgeye eklenecek ilginin değeridir. Ilgi tipinde olmalıdır.</param>
            public IUstveriV2XFluentIlgi IlgiIle(Ilgi ilgi)
            {
                if (ilgi != null)
                {
                    if (_ilgiler == null)
                        _ilgiler = new List<Ilgi>();

                    _ilgiler.Add(ilgi);
                }

                return this;
            }

            /// <summary>
            ///     Belgeye eklenecek ilgilere ilişkin bilgilerdir.
            /// </summary>
            /// <param name="ilgiler">Belgeye eklenecek ilgilerin değeridir. Ilgi listesi tipinde olmalıdır.</param>
            public IUstveriV2XFluentIlgiler IlgilerIle(List<Ilgi> ilgiler)
            {
                if (ilgiler != null)
                {
                    if (_ilgiler == null)
                        _ilgiler = new List<Ilgi>();

                    _ilgiler.AddRange(ilgiler);
                }

                return this;
            }

            /// <summary>
            ///     Belgenin oluşturulduğu dildir.
            /// </summary>
            /// <param name="dil">Belgenin oluşturulduğu dilin değeridir.</param>
            /// <remarks>ISO 639-3 standardına uygun dil kodu verilmelidir.</remarks>
            public IUstveriV2XFluentDil DilIle(string dil)
            {
                _dil = dil;
                return this;
            }

            /// <summary>
            ///     Belgeyi oluşturan taraftır.
            /// </summary>
            /// <param name="olusturan">Belgeyi oluşturan tarafın değeridir. Olusturan tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV2XFluentOlusturan OlusturanAta(Olusturan olusturan)
            {
                _olusturan = olusturan;
                return this;
            }

            public Ustveri Olustur()
            {
                return new(_belgeId,
                    _konu,
                    _guvenlikKodu,
                    _guvenlikKoduGecerlilikTarihi,
                    _ozId,
                    _dagitimlar,
                    _ekler,
                    _ilgiler,
                    _dil,
                    _olusturan,
                    _ilgililer,
                    _dosyaAdi,
                    null,
                    null,
                    _sdpBilgisi,
                    _heysKodlari,
                    _dogrulamaBilgisi);
            }

            /// <summary>
            ///     Belgenin Standart Dosya Planı bilgisidir.
            /// </summary>
            /// <param name="sdpBilgisi">Belgenin Standart Dosya Planı bilgisi değeridir. SDPBilgisi tipinde olmalıdır.</param>
            public IUstveriV2XFluentSdp SdpBilgisiIle(SDPBilgisi sdpBilgisi)
            {
                _sdpBilgisi = sdpBilgisi;
                return this;
            }

            /// <summary>
            ///     Belgeye ilişkin Hizmet Envanter Yönetim Sistemi kodu bilgisidir.
            /// </summary>
            /// <param name="heysKodu">HEYSK tipinde olmalıdır.</param>
            public IUstveriV2XFluentHeysk HEYSKoduIle(HEYSK heysKodu)
            {
                if (heysKodu != null)
                {
                    if (_heysKodlari == null)
                        _heysKodlari = new List<HEYSK>();

                    _heysKodlari.Add(heysKodu);
                }

                return this;
            }

            /// <summary>
            ///     Belgenin doğrulama yapılacağı web adresi bilgisidir.
            /// </summary>
            /// <param name="dogrulamaBilgisi">Belgenin doğrulama yapılacağı web adresi bilgisi değeridir.</param>
            /// <remarks>
            ///     Zorunlu alandır.
            /// </remarks>
            public IUstveriV2XFluentDogrulamaBilgisi DogrulamaBilgisiAta(DogrulamaBilgisi dogrulamaBilgisi)
            {
                _dogrulamaBilgisi = dogrulamaBilgisi;
                return this;
            }

            /// <summary>
            ///     Belgenin güvenlik seviyesinin ortadan kalktığı tarihtir.
            /// </summary>
            /// <param name="tarih">Belgenin güvenlik seviyesinin ortadan kalktığı tarih değeridir.</param>
            public IUstveriV2XFluentGuvenlikKoduGecerlilikTarihi GuvenlikKoduGecerlilikTarihiIle(DateTime tarih)
            {
                _guvenlikKoduGecerlilikTarihi = tarih;
                return this;
            }

            /// <summary>
            ///     Belgenin üretildiği sistemdeki tekil anahtar değeridir.
            /// </summary>
            /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
            /// <remarks>
            ///     Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
            ///     Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
            ///     OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
            /// </remarks>
            public IUstveriV2XFluentOzId OzIdIle(TanimlayiciTip ozId)
            {
                _ozId = ozId;
                return this;
            }

            /// <summary>
            ///     Belgenin dağıtımının yapıldığı taraf bilgisidir.
            /// </summary>
            /// <param name="dagitim">Belgenin dağıtımının yapıldığı taraf bilgisi değeridir. Dagitim tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV2XFluentDagitim DagitimAta(Dagitim dagitim)
            {
                if (dagitim != null)
                {
                    if (_dagitimlar == null)
                        _dagitimlar = new List<Dagitim>();

                    _dagitimlar.Add(dagitim);
                }

                return this;
            }

            /// <summary>
            ///     Belgenin dağıtımının yapıldığı taraf bilgileridir.
            /// </summary>
            /// <param name="dagitimlar">Belgenin dağıtımının yapıldığı taraf bilgileri değeridir. Dagitim listesi tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV2XFluentDagitimlar DagitimlarAta(List<Dagitim> dagitimlar)
            {
                if (dagitimlar != null)
                {
                    if (_dagitimlar == null)
                        _dagitimlar = new List<Dagitim>();

                    _dagitimlar.AddRange(dagitimlar);
                }

                return this;
            }

            /// <summary>
            ///     Belgenin güvenlik derecesini gösterir.
            /// </summary>
            /// <param name="guvenlikKodu">Belgenin güvenlik derecesi değeridir. GuvenlikKoduTuru tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV2XFluentGuvenlikKodu GuvenlikKoduAta(GuvenlikKoduTuru guvenlikKodu)
            {
                _guvenlikKodu = guvenlikKodu;
                return this;
            }

            /// <summary>
            ///     Belge ile ilgili iletişim kurulacak tarafa ait bilgidir.
            /// </summary>
            /// <param name="ilgili">Ilgili tipinde olmalıdır.</param>
            public IUstveriV2XFluentIlgili IlgiliIle(Ilgili ilgili)
            {
                if (ilgili != null)
                {
                    if (_ilgililer == null)
                        _ilgililer = new List<Ilgili>();

                    _ilgililer.Add(ilgili);
                }

                return this;
            }

            /// <summary>
            ///     Belge ile ilgili iletişim kurulacak taraflara ait bilgilerdir.
            /// </summary>
            /// <param name="ilgililer">Ilgili listesi tipinde olmalıdır.</param>
            public IUstveriV2XFluentIlgililer IlgililerIle(List<Ilgili> ilgililer)
            {
                if (ilgililer != null)
                {
                    if (_ilgililer == null)
                        _ilgililer = new List<Ilgili>();

                    _ilgililer.AddRange(ilgililer);
                }

                return this;
            }

            /// <summary>
            ///     "Üst Yazı" bileşeninin dosya sistemindeki adıdır.
            /// </summary>
            /// <param name="dosyaAdi">"Üst Yazı" bileşeninin dosya sistemindeki adının değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstveriV2XFluentDosyaAdi DosyaAdiAta(string dosyaAdi)
            {
                _dosyaAdi = dosyaAdi;
                return this;
            }
        }
    }
}