using eyazisma.online.api.Classes;
using eyazisma.online.api.Enums;
using eyazisma.online.api.Extensions;
using eyazisma.online.api.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Serialization;

namespace eyazisma.online.api
{
    /// <summary>
    /// E-Yazışma Teknik Rehberi Sürümlerine uygun şekilde şifreli paket işlemlerinin gerçekleştirilmesi için kullanılır.
    /// </summary>
    public sealed class SifreliPaket : ISifreliPaket, IDisposable
    {
        readonly Stream _stream;
        readonly PaketVersiyonTuru _paketVersiyon;

        private SifreliPaket(Stream stream, PaketModuTuru paketModu)
        {
            _stream = stream;

            if (paketModu != PaketModuTuru.Olustur)
            {
                _paketVersiyon = SifreliPaketVersiyonuAl(stream);
                if (_paketVersiyon == PaketVersiyonTuru.TespitEdilemedi)
                    throw new ApplicationException("SifreliPaket versiyonu tespit edilememiştir.");
            }
        }

        /// <summary>
        /// Yeni bir şifreli paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketStream">Şifreli pakete ilişkin STREAM objesidir.</param>
        public static ISifreliPaketOlustur Olustur(Stream sifreliPaketStream)
        {
            if (sifreliPaketStream == null)
                sifreliPaketStream = new MemoryStream();

            return new SifreliPaket(sifreliPaketStream, PaketModuTuru.Olustur);
        }

        /// <summary>
        /// Yeni bir şifreli paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketDosyaYolu">Şifreli pakete ilişkin dosya yoludur.</param>
        /// <exception cref="ArgumentNullException">sifreliPaketDosyaYolu boş olduğu durumlarda fırlatılır.</exception>
        public static ISifreliPaketOlustur Olustur(string sifreliPaketDosyaYolu)
        {
            if (string.IsNullOrWhiteSpace(sifreliPaketDosyaYolu))
                throw new ArgumentNullException(nameof(sifreliPaketDosyaYolu), nameof(sifreliPaketDosyaYolu) + " boş olmamalıdır.");

            if (File.Exists(sifreliPaketDosyaYolu))
            {
                try { File.Delete(sifreliPaketDosyaYolu); } catch { }
            }
            return new SifreliPaket(File.Open(sifreliPaketDosyaYolu, FileMode.OpenOrCreate, FileAccess.ReadWrite), PaketModuTuru.Olustur);
        }

        public ISifreliPaketV1XOlustur Versiyon1X()
        {
            return SifreliPaketV1X.Olustur(_stream);
        }

        public ISifreliPaketV2XOlustur Versiyon2X()
        {
            return SifreliPaketV2X.Olustur(_stream);
        }

        /// <summary>
        /// Var olan bir şifreli paketi okumak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketStream">Şifreli pakete ilişkin STREAM objesidir.</param>
        /// <exception cref="ArgumentNullException">sifreliPaketStream boş olduğu durumlarda fırlatılır.</exception>
        public static ISifreliPaketOku Oku(Stream sifreliPaketStream)
        {
            if (sifreliPaketStream == null || sifreliPaketStream.Length == 0)
                throw new ArgumentNullException(nameof(sifreliPaketStream), nameof(sifreliPaketStream) + " boş olmamalıdır.");
            return new SifreliPaket(sifreliPaketStream, PaketModuTuru.Oku);
        }

        /// <summary>
        /// Var olan bir şifreli paketi okumak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketDosyaYolu">Şifreli pakete ilişkin dosya yoludur.</param>
        /// <exception cref="ArgumentNullException">sifreliPaketDosyaYolu boş olduğu durumlarda fırlatılır.</exception>
        /// <exception cref="FileNotFoundException">Verilen dosya yolunun geçersiz olduğu ya da dosyaya erişim yetkisinin bulunmadığı durumlarda fırlatılır.</exception>
        public static ISifreliPaketOku Oku(string sifreliPaketDosyaYolu)
        {
            if (string.IsNullOrWhiteSpace(sifreliPaketDosyaYolu))
                throw new ArgumentNullException(nameof(sifreliPaketDosyaYolu), nameof(sifreliPaketDosyaYolu) + " boş olmamalıdır.");
            else if (!File.Exists(sifreliPaketDosyaYolu))
                throw new FileNotFoundException(nameof(sifreliPaketDosyaYolu), "Verilen dosya yolu geçersizdir ya da dosyaya erişim yetkisi bulunmamaktadır.");

            return new SifreliPaket(File.Open(sifreliPaketDosyaYolu, FileMode.Open, FileAccess.Read), PaketModuTuru.Oku);
        }

        /// <summary>
        /// SifreliPaket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// SifreliPaket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// ISifreliPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Şifreli pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        public ISifreliPaketOkuAction Versiyon1XIse(Action<bool, ISifreliPaketV1XOkuBilesen, List<DogrulamaHatasi>> bilesenAction)
        {
            if (_paketVersiyon == PaketVersiyonTuru.Versiyon1X)
                SifreliPaketV1X.Oku(_stream).BilesenleriAl(bilesenAction).Kapat();
            return this;
        }

        /// <summary>
        /// SifreliPaket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// SifreliPaket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// ISifreliPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Şifreli pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        public ISifreliPaketOkuAction Versiyon2XIse(Action<bool, ISifreliPaketV2XOkuBilesen, List<DogrulamaHatasi>> bilesenAction)
        {
            if (_paketVersiyon == PaketVersiyonTuru.Versiyon2X)
                SifreliPaketV2X.Oku(_stream).BilesenleriAl(bilesenAction).Kapat();
            return this;
        }

        /// <summary>
        /// Varolan bir şifreli paketin versiyon bilgisini almak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketStream">Şifreli pakete ilişkin STREAM objesidir.</param>
        /// <returns>Şifreli paket versiyon bilgisidir.</returns>
        /// <exception cref="ArgumentNullException">sifreliPaketStream boş olduğu durumlarda fırlatılır.</exception>
        public static PaketVersiyonTuru SifreliPaketVersiyonuAl(Stream sifreliPaketStream)
        {
            if (sifreliPaketStream == null || sifreliPaketStream.Length == 0)
                throw new ArgumentNullException(nameof(sifreliPaketStream), nameof(sifreliPaketStream) + " boş olmamalıdır.");
            sifreliPaketStream.Position = 0;
            return Package.Open(sifreliPaketStream, FileMode.Open, FileAccess.Read).GetPaketVersiyon();
        }

        /// <summary>
        /// Varolan bir şifreli paketin versiyon bilgisini almak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketDosyaYolu">Şifreli pakete ilişkin dosya yoludur.</param>
        /// <returns>Şifreli paket versiyon bilgisidir.</returns>
        /// <exception cref="ArgumentNullException">sifreliPaketDosyaYolu boş olduğu durumlarda fırlatılır.</exception>
        /// <exception cref="FileNotFoundException">Verilen dosya yolunun geçersiz olduğu ya da dosyaya erişim yetkisinin bulunmadığı durumlarda fırlatılır.</exception>
        public static PaketVersiyonTuru SifreliPaketVersiyonuAl(string sifreliPaketDosyaYolu)
        {
            if (string.IsNullOrWhiteSpace(sifreliPaketDosyaYolu))
                throw new ArgumentNullException(nameof(sifreliPaketDosyaYolu), nameof(sifreliPaketDosyaYolu) + " boş olmamalıdır.");
            else if (!File.Exists(sifreliPaketDosyaYolu))
                throw new FileNotFoundException(nameof(sifreliPaketDosyaYolu), "Verilen dosya yolu geçersizdir ya da dosyaya erişim yetkisi bulunmamaktadır.");

            return SifreliPaketVersiyonuAl(File.Open(sifreliPaketDosyaYolu, FileMode.Open, FileAccess.Read));
        }

        /// <summary>
        /// Varolan bir şifreli pakete ait şifreli içerik bilgisini almak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketStream">Şifreli pakete ilişkin STREAM objesidir.</param>
        /// <returns>Şifreli içerik bilgisidir.</returns>
        /// <exception cref="ArgumentNullException">sifreliPaketStream boş olduğu durumlarda fırlatılır.</exception>
        /// <exception cref="InvalidOperationException">SifreliIcerik bileşeninin olmadığı durumlarda fırlatılır.</exception>
        public static Stream SifreliIcerikAl(Stream sifreliPaketStream)
        {
            if (sifreliPaketStream == null || sifreliPaketStream.Length == 0)
                throw new ArgumentNullException(nameof(sifreliPaketStream), nameof(sifreliPaketStream) + " boş olmamalıdır.");
            sifreliPaketStream.Position = 0;
            return Package.Open(sifreliPaketStream, FileMode.Open, FileAccess.Read).GetSifreliIcerikStream();
        }

        /// <summary>
        /// Varolan bir şifreli pakete ait şifreli içerik bilgisini almak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketDosyaYolu">Şifreli pakete ilişkin dosya yoludur.</param>
        /// <returns>Şifreli içerik bilgisidir.</returns>
        /// <exception cref="ArgumentNullException">sifreliPaketDosyaYolu boş olduğu durumlarda fırlatılır.</exception>
        /// <exception cref="FileNotFoundException">Verilen dosya yolunun geçersiz olduğu ya da dosyaya erişim yetkisinin bulunmadığı durumlarda fırlatılır.</exception>
        /// <exception cref="InvalidOperationException">SifreliIcerik bileşeninin olmadığı durumlarda fırlatılır.</exception>
        public static Stream SifreliIcerikAl(string sifreliPaketDosyaYolu)
        {
            if (string.IsNullOrWhiteSpace(sifreliPaketDosyaYolu))
                throw new ArgumentNullException(nameof(sifreliPaketDosyaYolu), nameof(sifreliPaketDosyaYolu) + " boş olmamalıdır.");
            else if (!File.Exists(sifreliPaketDosyaYolu))
                throw new FileNotFoundException(nameof(sifreliPaketDosyaYolu), "Verilen dosya yolu geçersizdir ya da dosyaya erişim yetkisi bulunmamaktadır.");

            return SifreliIcerikAl(File.Open(sifreliPaketDosyaYolu, FileMode.Open, FileAccess.Read));
        }

        public void Kapat() => Dispose();

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// E-Yazışma Teknik Rehberi Sürüm 1.3 uygun şekilde şifreli paket işlemlerinin gerçekleştirilmesi için kullanılır.
    /// </summary>
    public sealed class SifreliPaketV1X : ISifreliPaketV1X
    {
        readonly Package _package;
        readonly Stream _stream;
        readonly PaketModuTuru _paketModu;
        readonly List<DogrulamaHatasi> _dogrulamaHatalari;

        /// <summary>
        /// Şifreli paketin iletileceği hedefleri barındıran nesneye ulaşılır.
        /// </summary>
        public BelgeHedef BelgeHedef { get; set; }

        /// <summary>
        /// Şifreli paket içerisindeki BelgeHedef bileşenini STREAM olarak verir.
        /// </summary>
        public Stream BelgeHedefAl() => _package.GetBelgeHedefStream();

        /// <summary>
        /// Şifreli paket içerisindeki BelgeHedef bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool BelgeHedefVarMi() => _package.BelgeHedefUriExists();

        /// <summary>
        /// Şifreli paket içerisinde imzalanan bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        public PaketOzeti PaketOzeti { get; set; }

        /// <summary>
        /// Şifreli paket içerisindeki PaketOzeti bileşenini STREAM olarak verir.
        /// </summary>
        public Stream PaketOzetiAl() => _package.GetPaketOzetiStream();

        /// <summary>
        /// Şifreli paket içerisindeki PaketOzeti bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool PaketOzetiVarMi() => _package.PaketOzetiExists();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli içeriğe ilişkin bilgileri içeren nesneye ulaşılır.
        /// </summary>
        public SifreliIcerikBilgisi SifreliIcerikBilgisi { get; set; }

        /// <summary>
        /// Şifreli paket içerisindeki SifreliIcerikBilgisi bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool SifreliIcerikBilgisiVarMi() => _package.SifreliIcerikBilgisiExists();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli dosyayı STREAM olarak verir.
        /// </summary>
        public Stream SifreliIcerikAl() => _package.GetSifreliIcerikStream();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli dosyanın adını döner.
        /// </summary>
        public string SifreliIcerikDosyasiAdiAl() => _package.GetSifreliIcerikDosyasiAdi();

        private SifreliPaketV1X(Stream stream, PaketModuTuru paketModu)
        {
            _dogrulamaHatalari = new List<DogrulamaHatasi>();
            _paketModu = paketModu;
            switch (paketModu)
            {
                case PaketModuTuru.Oku:
                    {
                        _package = Package.Open(stream, FileMode.Open, FileAccess.Read);

                        if (!_package.CoreRelationExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"Core\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_CORE).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"Core\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

                        if (_package.BelgeHedefUriExists())
                        {
                            try
                            {
                                Uri readedBelgeHedefUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).First().TargetUri);
                                Api.V1X.CT_BelgeHedef readedBelgeHedef = (Api.V1X.CT_BelgeHedef)new XmlSerializer(typeof(Api.V1X.CT_BelgeHedef)).Deserialize(_package.GetPartStream(readedBelgeHedefUri));
                                BelgeHedef = readedBelgeHedef.ToBelgeHedef();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeHedef) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeHedef) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            BelgeHedef = new BelgeHedef();


                        if (!_package.PaketOzetiExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.PaketOzeti) + "\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_PAKETOZETI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.PaketOzeti) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                        {
                            try
                            {
                                Uri readedPaketOzetiUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_PAKETOZETI).First().TargetUri);
                                Api.V1X.CT_PaketOzeti readedPaketOzeti = (Api.V1X.CT_PaketOzeti)new XmlSerializer(typeof(Api.V1X.CT_PaketOzeti)).Deserialize(_package.GetPartStream(readedPaketOzetiUri));
                                PaketOzeti = readedPaketOzeti.ToPaketOzeti();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.PaketOzeti) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                                PaketOzeti = null;
                            }
                        }

                        if (!_package.SifreliIcerikBilgisiExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.SifreliIcerikBilgisi) + "\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_SIFRELIICERIKBILGISI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.SifreliIcerikBilgisi) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                        {
                            try
                            {
                                Uri readedSifreliIcerikBilgisiUri = _package.GetRelationshipsByType(Constants.RELATION_TYPE_SIFRELIICERIKBILGISI).First().TargetUri;
                                Api.V1X.CT_SifreliIcerikBilgisi readedSifreliIcerikBilgisi = (Api.V1X.CT_SifreliIcerikBilgisi)new XmlSerializer(typeof(Api.V1X.CT_SifreliIcerikBilgisi)).Deserialize(_package.GetPartStream(readedSifreliIcerikBilgisiUri));
                                SifreliIcerikBilgisi = readedSifreliIcerikBilgisi.ToSifreliIcerikBilgisi();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.SifreliIcerikBilgisi) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                                SifreliIcerikBilgisi = null;
                            }
                        }

                        var hatalar = new List<DogrulamaHatasi>();
                        if (BelgeHedef != null && !BelgeHedef.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                        {
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Classes.BelgeHedef) + " bileşeni doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                            hatalar = new List<DogrulamaHatasi>();
                        }

                        if (PaketOzeti != null && !PaketOzeti.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                        {
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Classes.PaketOzeti) + " bileşeni doğrulanamamıştır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                            hatalar = new List<DogrulamaHatasi>();
                        }

                        break;
                    }
                case PaketModuTuru.Guncelle:
                    {
                        _package = Package.Open(stream, FileMode.Open, FileAccess.ReadWrite);

                        if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_CORE).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"Core\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

                        if (_package.BelgeHedefUriExists())
                        {
                            try
                            {
                                Uri readedBelgeHedefUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).First().TargetUri);
                                Api.V1X.CT_BelgeHedef readedBelgeHedef = (Api.V1X.CT_BelgeHedef)new XmlSerializer(typeof(Api.V1X.CT_BelgeHedef)).Deserialize(_package.GetPartStream(readedBelgeHedefUri));
                                BelgeHedef = readedBelgeHedef.ToBelgeHedef();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeHedef) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeHedef) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            BelgeHedef = new BelgeHedef();

                        if (_package.PaketOzetiExists())
                        {
                            try
                            {
                                Uri readedPaketOzetiUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_PAKETOZETI).First().TargetUri);
                                Api.V1X.CT_PaketOzeti readedPaketOzeti = (Api.V1X.CT_PaketOzeti)new XmlSerializer(typeof(Api.V1X.CT_PaketOzeti)).Deserialize(_package.GetPartStream(readedPaketOzetiUri));
                                PaketOzeti = readedPaketOzeti.ToPaketOzeti();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.PaketOzeti) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_PAKETOZETI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.PaketOzeti) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            PaketOzeti = new PaketOzeti();

                        if (_package.SifreliIcerikBilgisiExists())
                        {
                            try
                            {
                                Uri readedSifreliIcerikBilgisiUri = _package.GetRelationshipsByType(Constants.RELATION_TYPE_SIFRELIICERIKBILGISI).First().TargetUri;
                                Api.V1X.CT_SifreliIcerikBilgisi readedSifreliIcerikBilgisi = (Api.V1X.CT_SifreliIcerikBilgisi)new XmlSerializer(typeof(Api.V1X.CT_SifreliIcerikBilgisi)).Deserialize(_package.GetPartStream(readedSifreliIcerikBilgisiUri));
                                SifreliIcerikBilgisi = readedSifreliIcerikBilgisi.ToSifreliIcerikBilgisi();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.SifreliIcerikBilgisi) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_SIFRELIICERIKBILGISI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.SifreliIcerikBilgisi) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            SifreliIcerikBilgisi = new SifreliIcerikBilgisi();

                        break;
                    }
                case PaketModuTuru.Olustur:
                    {
                        _package = Package.Open(stream, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        PaketOzeti = new PaketOzeti();
                        BelgeHedef = new BelgeHedef();
                        SifreliIcerikBilgisi = new SifreliIcerikBilgisi();
                        break;
                    }
            }
            _stream = stream;
        }

        /// <summary>
        /// Yeni bir şifreli paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketStream">Şifreli pakete ilişkin STREAM objesidir.</param>
        public static ISifreliPaketV1XOlustur Olustur(Stream sifreliPaketStream)
        {
            if (sifreliPaketStream == null)
                sifreliPaketStream = new MemoryStream();
            return new SifreliPaketV1X(sifreliPaketStream, PaketModuTuru.Olustur);
        }

        /// <summary>
        /// Yeni bir şifreli paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketDosyaYolu">Şifreli pakete ilişkin dosya yoludur.</param>
        /// <exception cref="ArgumentNullException">sifreliPaketDosyaYolu boş olduğu durumlarda fırlatılır.</exception>
        public static ISifreliPaketV1XOlustur Olustur(string sifreliPaketDosyaYolu)
        {
            if (string.IsNullOrWhiteSpace(sifreliPaketDosyaYolu))
                throw new ArgumentNullException(nameof(sifreliPaketDosyaYolu), nameof(sifreliPaketDosyaYolu) + " boş olmamalıdır.");

            if (File.Exists(sifreliPaketDosyaYolu))
            {
                try { File.Delete(sifreliPaketDosyaYolu); } catch { }
            }
            return new SifreliPaketV1X(File.Open(sifreliPaketDosyaYolu, FileMode.OpenOrCreate, FileAccess.ReadWrite), PaketModuTuru.Olustur);
        }

        /// <summary>
        /// Şifresiz paketin PaketOzeti bileşenini şifreli pakete ekler.
        /// </summary>
        /// <param name="paketOzetiStream"></param>
        public ISifreliPaketV1XOlusturPaketOzeti PaketOzetiEkle(Stream paketOzetiStream)
        {
            if (paketOzetiStream == null || paketOzetiStream.Length == 0)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(Classes.PaketOzeti) + "bileşeni STREAM değeri boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                try
                {
                    Api.V1X.CT_PaketOzeti readedPaketOzeti = (Api.V1X.CT_PaketOzeti)new XmlSerializer(typeof(Api.V1X.CT_PaketOzeti)).Deserialize(paketOzetiStream);
                    PaketOzeti = readedPaketOzeti.ToPaketOzeti();

                    var hatalar = new List<DogrulamaHatasi>();
                    if (!PaketOzeti.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                    {
                        _dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(Classes.PaketOzeti) + "\" bileşeni doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                    }
                }
                catch (Exception ex)
                {
                    _dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.PaketOzeti) + "\" bileşeni hatalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik,
                        InnerException = ex
                    });
                }

                if (!KritikHataExists())
                {
                    paketOzetiStream.Position = 0;

                    if (_paketModu == PaketModuTuru.Guncelle)
                        _package.DeleteParafOzeti();

                    _package.AddPaketOzeti(paketOzetiStream);
                }
            }

            return this;
        }

        /// <summary>
        /// Şifresiz paketin şifrelenmiş halini pakete ekler.
        /// </summary>
        /// <param name="sifreliIcerikStream">Şifresiz paketin şifrelenmiş haline ait STREAM değeridir.</param>
        /// <param name="paketId">Şifresiz paketin Id değeridir.</param>
        public ISifreliPaketV1XOlusturSifreliIcerik SifreliIcerikEkle(Stream sifreliIcerikStream, Guid paketId)
        {
            if (_package.SifreliIcerikExists())
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = "Şifreli paket içerisinde SifreliIcerik bileşeni bulunmaktadır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (sifreliIcerikStream == null || sifreliIcerikStream.Length == 0)
                    _dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "SifreliIcerik bileşeni STREAM değeri boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else if (paketId == null || paketId == Guid.Empty)
                    _dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(paketId) + " değeri boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (!KritikHataExists())
                    _package.AddSifreliIcerik(sifreliIcerikStream, paketId, PaketVersiyonTuru.Versiyon1X);
            }

            return this;
        }

        /// <summary>
        /// Şifreli paketin oluşturan bilgisidir.
        /// </summary>
        /// <param name="olusturan">Şifresiz paketin Olusturan bileşenidir.</param>
        public ISifreliPaketV1XOlusturOlusturan OlusturanAta(Olusturan olusturan)
        {
            if (olusturan == null)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(Olusturan) + " bileşeni değeri boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });

            _package.PackageProperties.Creator = olusturan.GenerateOlusturanAd();
            return this;
        }

        /// <summary>
        /// Şifreli paketin iletileceği hedefleri barındıran BelgeHedef bileşenini paket içerisine ekler.
        /// </summary>
        /// <param name="belgeHedef">BelgeHedef bileşenidir.</param>
        public ISifreliPaketV1XOlusturBelgeHedef BelgeHedefIle(BelgeHedef belgeHedef)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!belgeHedef.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.BelgeHedef) + " bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
                BelgeHedef = belgeHedef;

            return this;
        }

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak şifreli paket bileşenlerini oluşturur.
        /// </summary>
        public ISifreliPaketV1XOlusturBilesen BilesenleriOlustur()
        {
            if (BelgeHedef != null && BelgeHedef != new BelgeHedef())
            {
                if (_paketModu == PaketModuTuru.Guncelle)
                    _package.DeleteBelgeHedef();

                _package.GenerateBelgeHedef(BelgeHedef, PaketVersiyonTuru.Versiyon1X);
            }

            SifreliIcerikBilgisi.Id = "";
            SifreliIcerikBilgisi.URI = new List<string> { Constants.SIFRELEME_YONTEMI_URI_1, Constants.SIFRELEME_YONTEMI_URI_2 };
            SifreliIcerikBilgisi.Yontem = Constants.SIFRELEME_YONTEMI;
            SifreliIcerikBilgisi.Versiyon = Constants.SIFRELEME_YONTEMI_VERSIYONU;

            if (_paketModu == PaketModuTuru.Guncelle)
                _package.DeleteSifreliIcerikBilgisi();

            _package.GenerateSifreliIcerikBilgisi(SifreliIcerikBilgisi, PaketVersiyonTuru.Versiyon1X);

            _package.PackageProperties.Category = Constants.SIFRELI_PAKET_KATEGORI;
            _package.PackageProperties.ContentType = Constants.PAKET_MIMETURU;
            _package.PackageProperties.Version = Constants.PAKET_VERSIYON_V1X;
            _package.PackageProperties.Revision = string.Format(Constants.PAKET_REVIZYON, System.Reflection.Assembly.GetAssembly(typeof(SifreliPaket)).GetName().Version);

            return this;
        }

        /// <summary>
        /// Oluşturulan şifreli paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Şifreli pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        public ISifreliPaketV1XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction)
        {
            dogrulamaAction.Invoke(KritikHataExists(), _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Var olan bir şifreli paketi okumak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketStream">Şifreli pakete ilişkin STREAM objesidir.</param>
        /// <exception cref="ArgumentNullException">sifreliPaketStream boş olduğu durumlarda fırlatılır.</exception>
        public static ISifreliPaketV1XOku Oku(Stream sifreliPaketStream)
        {
            if (sifreliPaketStream == null || sifreliPaketStream.Length == 0)
                throw new ArgumentNullException(nameof(sifreliPaketStream), nameof(sifreliPaketStream) + " boş olmamalıdır.");
            return new SifreliPaketV1X(sifreliPaketStream, PaketModuTuru.Oku);
        }

        /// <summary>
        /// Var olan bir şifreli paketi okumak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketDosyaYolu">Şifreli pakete ilişkin dosya yoludur.</param>
        /// <exception cref="ArgumentNullException">sifreliPaketDosyaYolu boş olduğu durumlarda fırlatılır.</exception>
        /// <exception cref="FileNotFoundException">Verilen dosya yolunun geçersiz olduğu ya da dosyaya erişim yetkisinin bulunmadığı durumlarda fırlatılır.</exception>
        public static ISifreliPaketV1XOku Oku(string sifreliPaketDosyaYolu)
        {
            if (string.IsNullOrWhiteSpace(sifreliPaketDosyaYolu))
                throw new ArgumentNullException(nameof(sifreliPaketDosyaYolu), nameof(sifreliPaketDosyaYolu) + " boş olmamalıdır.");
            else if (!File.Exists(sifreliPaketDosyaYolu))
                throw new FileNotFoundException(nameof(sifreliPaketDosyaYolu), "Verilen dosya yolu geçersizdir ya da dosyaya erişim yetkisi bulunmamaktadır.");
            return new SifreliPaketV1X(File.Open(sifreliPaketDosyaYolu, FileMode.Open, FileAccess.Read), PaketModuTuru.Oku);
        }

        /// <summary>
        /// Şifreli pakete ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Şifreli paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// ISifreliPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        public ISifreliPaketV1XOkuBilesenAl BilesenleriAl(Action<bool, ISifreliPaketV1XOkuBilesen, List<DogrulamaHatasi>> bilesenAction)
        {
            bilesenAction.Invoke(KritikHataExists(), this, _dogrulamaHatalari);
            return this;
        }

        public void Kapat() => Dispose();

        public void Dispose()
        {
            if (_package != null)
                _package.Close();

            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        private bool KritikHataExists() => _dogrulamaHatalari.Any(p => p.HataTuru == DogrulamaHataTuru.Kritik);

    }

    /// <summary>
    /// E-Yazışma Teknik Rehberi Sürüm 2.0 uygun şekilde şifreli paket işlemlerinin gerçekleştirilmesi için kullanılır.
    /// </summary>
    public sealed class SifreliPaketV2X : ISifreliPaketV2X
    {
        readonly Package _package;
        readonly Stream _stream;
        readonly PaketModuTuru _paketModu;
        readonly List<DogrulamaHatasi> _dogrulamaHatalari;

        /// <summary>
        /// Şifreli paketin iletileceği hedefleri barındıran nesneye ulaşılır.
        /// </summary>
        public BelgeHedef BelgeHedef { get; set; }

        /// <summary>
        /// Şifreli paket içerisindeki BelgeHedef bileşenini STREAM olarak verir.
        /// </summary>
        public Stream BelgeHedefAl() => _package.GetBelgeHedefStream();

        /// <summary>
        /// Şifreli paket içerisindeki BelgeHedef bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool BelgeHedefVarMi() => _package.BelgeHedefUriExists();

        /// <summary>
        /// Şifreli paket içerisinde imzalanan bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        public NihaiOzet NihaiOzet { get; set; }

        /// <summary>
        /// Şifreli paket içerisindeki NihaiOzet bileşenini STREAM olarak verir.
        /// </summary>
        public Stream NihaiOzetAl() => _package.GetNihaiOzetStream();

        /// <summary>
        /// Şifreli paket içerisindeki NihaiOzet bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool NihaiOzetVarMi() => _package.NihaiOzetExists();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli içeriğe ilişkin bilgileri içeren nesneye ulaşılır.
        /// </summary>
        public SifreliIcerikBilgisi SifreliIcerikBilgisi { get; set; }

        /// <summary>
        /// Şifreli paket içerisindeki SifreliIcerikBilgisi bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool SifreliIcerikBilgisiVarMi() => _package.SifreliIcerikBilgisiExists();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli dosyayı STREAM olarak verir.
        /// </summary>
        public Stream SifreliIcerikAl() => _package.GetSifreliIcerikStream();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli dosyanın adını döner.
        /// </summary>
        public string SifreliIcerikDosyasiAdiAl() => _package.GetSifreliIcerikDosyasiAdi();

        private SifreliPaketV2X(Stream stream, PaketModuTuru paketModu)
        {
            _dogrulamaHatalari = new List<DogrulamaHatasi>();
            _paketModu = paketModu;
            switch (paketModu)
            {
                case PaketModuTuru.Oku:
                    {
                        _package = Package.Open(stream, FileMode.Open, FileAccess.Read);

                        if (!_package.CoreRelationExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"Core\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_CORE).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"Core\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

                        if (_package.BelgeHedefUriExists())
                        {
                            try
                            {
                                Uri readedBelgeHedefUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).First().TargetUri);
                                Api.V2X.CT_BelgeHedef readedBelgeHedef = (Api.V2X.CT_BelgeHedef)new XmlSerializer(typeof(Api.V2X.CT_BelgeHedef)).Deserialize(_package.GetPartStream(readedBelgeHedefUri));
                                BelgeHedef = readedBelgeHedef.ToBelgeHedef();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeHedef) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeHedef) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            BelgeHedef = new BelgeHedef();


                        if (!_package.NihaiOzetExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiOzet) + "\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIOZET).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiOzet) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                        {
                            try
                            {
                                Uri readedNihaiOzetUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIOZET).First().TargetUri);
                                Api.V2X.CT_NihaiOzet readedNihaiOzet = (Api.V2X.CT_NihaiOzet)new XmlSerializer(typeof(Api.V2X.CT_NihaiOzet)).Deserialize(_package.GetPartStream(readedNihaiOzetUri));
                                NihaiOzet = readedNihaiOzet.ToNihaiOzet();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiOzet) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                                NihaiOzet = null;
                            }
                        }

                        if (!_package.SifreliIcerikBilgisiExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.SifreliIcerikBilgisi) + "\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_SIFRELIICERIKBILGISI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.SifreliIcerikBilgisi) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                        {
                            try
                            {
                                Uri readedSifreliIcerikBilgisiUri = _package.GetRelationshipsByType(Constants.RELATION_TYPE_SIFRELIICERIKBILGISI).First().TargetUri;
                                Api.V2X.CT_SifreliIcerikBilgisi readedSifreliIcerikBilgisi = (Api.V2X.CT_SifreliIcerikBilgisi)new XmlSerializer(typeof(Api.V2X.CT_SifreliIcerikBilgisi)).Deserialize(_package.GetPartStream(readedSifreliIcerikBilgisiUri));
                                SifreliIcerikBilgisi = readedSifreliIcerikBilgisi.ToSifreliIcerikBilgisi();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.SifreliIcerikBilgisi) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                                SifreliIcerikBilgisi = null;
                            }
                        }

                        var hatalar = new List<DogrulamaHatasi>();
                        if (BelgeHedef != null && !BelgeHedef.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                        {
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Classes.BelgeHedef) + " bileşeni doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                            hatalar = new List<DogrulamaHatasi>();
                        }

                        if (NihaiOzet != null && !NihaiOzet.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                        {
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Classes.NihaiOzet) + " bileşeni doğrulanamamıştır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                            hatalar = new List<DogrulamaHatasi>();
                        }

                        break;
                    }
                case PaketModuTuru.Olustur:
                    {
                        _package = Package.Open(stream, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        NihaiOzet = new NihaiOzet();
                        BelgeHedef = new BelgeHedef();
                        SifreliIcerikBilgisi = new SifreliIcerikBilgisi();
                        break;
                    }
            }
            _stream = stream;
        }

        /// <summary>
        /// Yeni bir şifreli paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketStream">Şifreli pakete ilişkin STREAM objesidir.</param>
        public static ISifreliPaketV2XOlustur Olustur(Stream sifreliPaketStream)
        {
            if (sifreliPaketStream == null)
                sifreliPaketStream = new MemoryStream();
            return new SifreliPaketV2X(sifreliPaketStream, PaketModuTuru.Olustur);
        }

        /// <summary>
        /// Yeni bir şifreli paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketDosyaYolu">Şifreli pakete ilişkin dosya yoludur.</param>
        /// <exception cref="ArgumentNullException">sifreliPaketDosyaYolu boş olduğu durumlarda fırlatılır.</exception>
        public static ISifreliPaketV2XOlustur Olustur(string sifreliPaketDosyaYolu)
        {
            if (string.IsNullOrWhiteSpace(sifreliPaketDosyaYolu))
                throw new ArgumentNullException(nameof(sifreliPaketDosyaYolu), nameof(sifreliPaketDosyaYolu) + " boş olmamalıdır.");

            if (File.Exists(sifreliPaketDosyaYolu))
            {
                try { File.Delete(sifreliPaketDosyaYolu); } catch { }
            }
            return new SifreliPaketV2X(File.Open(sifreliPaketDosyaYolu, FileMode.OpenOrCreate, FileAccess.ReadWrite), PaketModuTuru.Olustur);
        }

        /// <summary>
        /// Şifresiz paketin NihaiOzet bileşenini şifreli pakete ekler.
        /// </summary>
        /// <param name="nihaiOzetStream"></param>
        public ISifreliPaketV2XOlusturNihaiOzet NihaiOzetEkle(Stream nihaiOzetStream)
        {
            if (nihaiOzetStream == null || nihaiOzetStream.Length == 0)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(Classes.NihaiOzet) + "bileşeni STREAM değeri boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                try
                {
                    Api.V2X.CT_NihaiOzet readedNihaiOzet = (Api.V2X.CT_NihaiOzet)new XmlSerializer(typeof(Api.V2X.CT_NihaiOzet)).Deserialize(nihaiOzetStream);
                    NihaiOzet = readedNihaiOzet.ToNihaiOzet();

                    var hatalar = new List<DogrulamaHatasi>();
                    if (!NihaiOzet.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                    {
                        _dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(Classes.NihaiOzet) + "\" bileşeni doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });
                    }
                }
                catch (Exception ex)
                {
                    _dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiOzet) + "\" bileşeni hatalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik,
                        InnerException = ex
                    });
                }

                if (!KritikHataExists())
                {
                    nihaiOzetStream.Position = 0;

                    if (_paketModu == PaketModuTuru.Guncelle)
                        _package.DeleteNihaiOzet();

                    _package.AddNihaiOzet(nihaiOzetStream);
                }
            }

            return this;
        }

        /// <summary>
        /// Şifresiz paketin şifrelenmiş halini pakete ekler.
        /// </summary>
        /// <param name="sifreliIcerikStream">Şifresiz paketin şifrelenmiş haline ait STREAM değeridir.</param>
        /// <param name="paketId">Şifresiz paketin Id değeridir.</param>
        public ISifreliPaketV2XOlusturSifreliIcerik SifreliIcerikEkle(Stream sifreliIcerikStream, Guid paketId)
        {
            if (_package.SifreliIcerikExists())
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = "Şifreli paket içerisinde SifreliIcerik bileşeni bulunmaktadır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            }
            else
            {
                if (sifreliIcerikStream == null || sifreliIcerikStream.Length == 0)
                    _dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "SifreliIcerik bileşeni STREAM değeri boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else if (paketId == null || paketId == Guid.Empty)
                    _dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = nameof(paketId) + " değeri boş olmamalıdır.",
                        HataTuru = DogrulamaHataTuru.Kritik
                    });

                if (!KritikHataExists())
                    _package.AddSifreliIcerik(sifreliIcerikStream, paketId, PaketVersiyonTuru.Versiyon2X);
            }

            return this;
        }

        /// <summary>
        /// Şifreli paketin oluşturan bilgisidir.
        /// </summary>
        /// <param name="olusturan">Şifresiz paketin Olusturan bileşenidir.</param>
        public ISifreliPaketV2XOlusturOlusturan OlusturanAta(Olusturan olusturan)
        {
            if (olusturan == null)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(Olusturan) + " bileşeni değeri boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });

            _package.PackageProperties.Creator = olusturan.GenerateOlusturanAd();
            return this;
        }

        /// <summary>
        /// Şifreli paketin iletileceği hedefleri barındıran BelgeHedef bileşenini paket içerisine ekler.
        /// </summary>
        /// <param name="belgeHedef">BelgeHedef bileşenidir.</param>
        public ISifreliPaketV2XOlusturBelgeHedef BelgeHedefIle(BelgeHedef belgeHedef)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!belgeHedef.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.BelgeHedef) + " bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
                BelgeHedef = belgeHedef;

            return this;
        }

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak şifreli paket bileşenlerini oluşturur.
        /// </summary>
        public ISifreliPaketV2XOlusturBilesen BilesenleriOlustur()
        {
            if (BelgeHedef != null && BelgeHedef != new BelgeHedef())
            {
                if (_paketModu == PaketModuTuru.Guncelle)
                    _package.DeleteBelgeHedef();

                _package.GenerateBelgeHedef(BelgeHedef, PaketVersiyonTuru.Versiyon2X);
            }

            SifreliIcerikBilgisi.Id = "";
            SifreliIcerikBilgisi.URI = new List<string> { Constants.SIFRELEME_YONTEMI_URI_1, Constants.SIFRELEME_YONTEMI_URI_2 };
            SifreliIcerikBilgisi.Yontem = Constants.SIFRELEME_YONTEMI;
            SifreliIcerikBilgisi.Versiyon = Constants.SIFRELEME_YONTEMI_VERSIYONU;

            if (_paketModu == PaketModuTuru.Guncelle)
                _package.DeleteSifreliIcerikBilgisi();

            _package.GenerateSifreliIcerikBilgisi(SifreliIcerikBilgisi, PaketVersiyonTuru.Versiyon2X);

            _package.PackageProperties.Category = Constants.SIFRELI_PAKET_KATEGORI;
            _package.PackageProperties.ContentType = Constants.PAKET_MIMETURU;
            _package.PackageProperties.Version = Constants.PAKET_VERSIYON_V2X;
            _package.PackageProperties.Revision = string.Format(Constants.PAKET_REVIZYON, System.Reflection.Assembly.GetAssembly(typeof(SifreliPaket)).GetName().Version);

            return this;
        }

        /// <summary>
        /// Oluşturulan şifreli paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Şifreli pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        public ISifreliPaketV2XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction)
        {
            dogrulamaAction.Invoke(KritikHataExists(), _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Var olan bir şifreli paketi okumak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketStream">Şifreli pakete ilişkin STREAM objesidir.</param>
        /// <exception cref="ArgumentNullException">sifreliPaketStream boş olduğu durumlarda fırlatılır.</exception>
        public static ISifreliPaketV2XOku Oku(Stream sifreliPaketStream)
        {
            if (sifreliPaketStream == null || sifreliPaketStream.Length == 0)
                throw new ArgumentNullException(nameof(sifreliPaketStream), nameof(sifreliPaketStream) + " boş olmamalıdır.");
            return new SifreliPaketV2X(sifreliPaketStream, PaketModuTuru.Oku);
        }

        /// <summary>
        /// Var olan bir şifreli paketi okumak için kullanılır.
        /// </summary>
        /// <param name="sifreliPaketDosyaYolu">Şifreli pakete ilişkin dosya yoludur.</param>
        /// <exception cref="ArgumentNullException">sifreliPaketDosyaYolu boş olduğu durumlarda fırlatılır.</exception>
        /// <exception cref="FileNotFoundException">Verilen dosya yolunun geçersiz olduğu ya da dosyaya erişim yetkisinin bulunmadığı durumlarda fırlatılır.</exception>
        public static ISifreliPaketV2XOku Oku(string sifreliPaketDosyaYolu)
        {
            if (string.IsNullOrWhiteSpace(sifreliPaketDosyaYolu))
                throw new ArgumentNullException(nameof(sifreliPaketDosyaYolu), nameof(sifreliPaketDosyaYolu) + " boş olmamalıdır.");
            else if (!File.Exists(sifreliPaketDosyaYolu))
                throw new FileNotFoundException(nameof(sifreliPaketDosyaYolu), "Verilen dosya yolu geçersizdir ya da dosyaya erişim yetkisi bulunmamaktadır.");
            return new SifreliPaketV2X(File.Open(sifreliPaketDosyaYolu, FileMode.Open, FileAccess.Read), PaketModuTuru.Oku);
        }

        /// <summary>
        /// Şifreli pakete ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Şifreli paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// ISifreliPaketV2XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        public ISifreliPaketV2XOkuBilesenAl BilesenleriAl(Action<bool, ISifreliPaketV2XOkuBilesen, List<DogrulamaHatasi>> bilesenAction)
        {
            bilesenAction.Invoke(KritikHataExists(), this, _dogrulamaHatalari);
            return this;
        }

        public void Kapat() => Dispose();

        public void Dispose()
        {
            if (_package != null)
                _package.Close();

            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        private bool KritikHataExists() => _dogrulamaHatalari.Any(p => p.HataTuru == DogrulamaHataTuru.Kritik);

    }
}
