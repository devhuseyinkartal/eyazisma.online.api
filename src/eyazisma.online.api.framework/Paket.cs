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
    /// E-Yazışma Teknik Rehberi Sürümlerine uygun şekilde paket işlemlerinin gerçekleştirilmesi için kullanılır.
    /// </summary>
    public sealed class Paket : IPaket, IDisposable
    {
        readonly Stream _stream;
        readonly PaketVersiyonTuru _paketVersiyon;

        private Paket(Stream stream, PaketModuTuru paketModu)
        {
            _stream = stream;

            if (paketModu != PaketModuTuru.Olustur)
            {
                _paketVersiyon = PaketVersiyonuAl(stream);
                if (_paketVersiyon == PaketVersiyonTuru.TespitEdilemedi)
                    throw new ApplicationException("Paket versiyonu tespit edilememiştir.");
            }
        }

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Yeni bir paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="paketStream">Pakete ilişkin STREAM objesidir.</param>
        public static IPaketOlustur Olustur(Stream paketStream) =>
            new Paket(paketStream, PaketModuTuru.Olustur);

        /// <summary>
        /// Yeni bir paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="paketDosyaYolu">Pakete ilişkin dosya yoludur.</param>
        public static IPaketOlustur Olustur(string paketDosyaYolu)
        {
            if (File.Exists(paketDosyaYolu))
            {
                try { File.Delete(paketDosyaYolu); } catch { }
            }
            return new Paket(File.Open(paketDosyaYolu, FileMode.OpenOrCreate, FileAccess.ReadWrite), PaketModuTuru.Olustur);
        }

        public IPaketV1XOlustur Versiyon1X()
        {
            return PaketV1X.Olustur(_stream);
        }

        public IPaketV2XOlustur Versiyon2X()
        {
            return PaketV2X.Olustur(_stream);
        }

        /// <summary>
        /// Var olan bir paketi okumak için kullanılır.
        /// </summary>
        /// <param name="paketStream">Pakete ilişkin STREAM objesidir.</param>
        public static IPaketOku Oku(Stream paketStream) =>
            new Paket(paketStream, PaketModuTuru.Oku);

        /// <summary>
        /// Var olan bir paketi okumak için kullanılır.
        /// </summary>
        /// <param name="paketDosyaYolu">Pakete ilişkin dosya yoludur.</param>
        public static IPaketOku Oku(string paketDosyaYolu) =>
            new Paket(File.Open(paketDosyaYolu, FileMode.Open, FileAccess.Read), PaketModuTuru.Oku);

        /// <summary>
        /// Paket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// IPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        public IPaketOkuAction Versiyon1XIse(Action<bool, IPaketV1XOkuBilesen, List<DogrulamaHatasi>> action)
        {
            if (_paketVersiyon == PaketVersiyonTuru.Versiyon1X)
                PaketV1X.Oku(_stream).BilesenleriAl(action).Kapat();
            Dispose();
            return this;
        }

        /// <summary>
        /// Paket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// IPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        public void Versiyon2XIse(Action<bool, IPaketV2XOkuBilesen, List<DogrulamaHatasi>> action)
        {
            if (_paketVersiyon == PaketVersiyonTuru.Versiyon2X)
                PaketV2X.Oku(_stream).BilesenleriAl(action).Kapat();
            Dispose();
        }

        /// <summary>
        /// Var olan bir paketi güncellemek için kullanılır.
        /// </summary>
        /// <param name="paketStream">Pakete ilişkin STREAM objesidir.</param>
        public static IPaketGuncelle Guncelle(Stream paketStream) =>
            new Paket(paketStream, PaketModuTuru.Guncelle);

        /// <summary>
        /// Var olan bir paketi güncellemek için kullanılır.
        /// </summary>
        /// <param name="paketDosyaYolu">Pakete ilişkin dosya yoludur.</param>
        public static IPaketGuncelle Guncelle(string paketDosyaYolu) =>
            new Paket(File.Open(paketDosyaYolu, FileMode.Open, FileAccess.ReadWrite), PaketModuTuru.Guncelle);

        public IPaketGuncelleAction Versiyon1XIse(Action<IPaketV1XGuncelle> action)
        {
            if (_paketVersiyon == PaketVersiyonTuru.Versiyon1X)
                action.Invoke(PaketV1X.Guncelle(_stream));
            Dispose();
            return this;
        }

        public void Versiyon2XIse(Action<IPaketV2XGuncelle> action)
        {
            if (_paketVersiyon == PaketVersiyonTuru.Versiyon2X)
                action.Invoke(PaketV2X.Guncelle(_stream));
            Dispose();
        }

        /// <summary>
        /// Varolan bir paketin versiyon bilgisini almak için kullanılır.
        /// </summary>
        /// <param name="stream">Pakete ilişkin STREAM objesidir.</param>
        public static PaketVersiyonTuru PaketVersiyonuAl(Stream stream)
        {
            using (Package package = Package.Open(stream, FileMode.Open, FileAccess.Read))
                return package.GetPaketVersiyon();
        }

        /// <summary>
        /// Varolan bir paketin versiyon bilgisini almak için kullanılır.
        /// </summary>
        /// <param name="paketDosyaYolu">Pakete ilişkin dosya yoludur.</param>
        public static PaketVersiyonTuru PaketVersiyonuAl(string paketDosyaYolu) =>
            PaketVersiyonuAl(File.Open(paketDosyaYolu, FileMode.Open, FileAccess.Read));
    }

    /// <summary>
    /// E-Yazışma Teknik Rehberi Sürüm 1.3 uygun şekilde paket işlemlerinin gerçekleştirilmesi için kullanılır.
    /// </summary>
    public sealed class PaketV1X : IPaketV1X, IDisposable
    {
        readonly Package _package;
        readonly Stream _stream;
        readonly PaketModuTuru _paketModu;
        readonly List<DogrulamaHatasi> _dogrulamaHatalari;
        OzetAlgoritmaTuru _ozetAlgoritma;

        private PaketV1X(Stream stream, PaketModuTuru paketModu)
        {
            stream.Position = 0;
            _paketModu = paketModu;
            _ozetAlgoritma = OzetAlgoritmaTuru.SHA256;
            _dogrulamaHatalari = new List<DogrulamaHatasi>();

            switch (paketModu)
            {
                case PaketModuTuru.Oku:
                    {
                        _package = Package.Open(stream, FileMode.Open, FileAccess.Read);

                        if (!_package.GetRelationships().Any())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"İlişki\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

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

                        if (!_package.UstveriExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.Ustveri) + "\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTVERI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.Ustveri) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                        {
                            try
                            {
                                Uri readedUstveriUri = _package.GetRelationshipsByType(Constants.RELATION_TYPE_USTVERI).First().TargetUri;
                                Api.V1X.CT_Ustveri readedUstveri = (Api.V1X.CT_Ustveri)new XmlSerializer(typeof(Api.V1X.CT_Ustveri)).Deserialize(_package.GetPart(readedUstveriUri).GetStream(FileMode.Open));
                                Ustveri = readedUstveri.ToUstveri();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.Ustveri) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                                Ustveri = null;
                            }
                        }

                        if (!_package.BelgeHedefUriExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeHedef) + "\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeHedef) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                        {
                            try
                            {
                                Uri readedBelgeHedefUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).First().TargetUri);
                                Api.V1X.CT_BelgeHedef readedBelgeHedef = (Api.V1X.CT_BelgeHedef)new XmlSerializer(typeof(Api.V1X.CT_BelgeHedef)).Deserialize(_package.GetPart(readedBelgeHedefUri).GetStream(FileMode.Open));
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
                                BelgeHedef = null;
                            }
                        }

                        if (!_package.UstYaziExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.UstYazi) + "\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.UstYazi) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

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
                                Api.V1X.CT_PaketOzeti readedPaketOzeti = (Api.V1X.CT_PaketOzeti)new XmlSerializer(typeof(Api.V1X.CT_PaketOzeti)).Deserialize(_package.GetPart(readedPaketOzetiUri).GetStream(FileMode.Open));
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

                        if (!_package.ImzaExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"Imza\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_IMZA).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"Imza\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

                        if (_package.BelgeImzaUriExists())
                        {
                            try
                            {
                                Uri readedBelgeImzaUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEIMZA).First().TargetUri);
                                Api.V1X.CT_BelgeImza readedBelgeImza = (Api.V1X.CT_BelgeImza)(new XmlSerializer(typeof(Api.V1X.CT_BelgeImza))).Deserialize(_package.GetPart(readedBelgeImzaUri).GetStream(FileMode.Open));
                                BelgeImza = readedBelgeImza.ToBelgeImza();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeImza) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEIMZA).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeImza) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            BelgeImza = new BelgeImza();

                        if (_package.NihaiOzetExists())
                        {
                            try
                            {
                                Uri readedNihaiOzetUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIOZET).First().TargetUri);
                                Api.V1X.CT_NihaiOzet readedNihaiOzet = (Api.V1X.CT_NihaiOzet)new XmlSerializer(typeof(Api.V1X.CT_NihaiOzet)).Deserialize(_package.GetPart(readedNihaiOzetUri).GetStream(FileMode.Open));
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
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIOZET).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiOzet) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            NihaiOzet = new NihaiOzet();

                        var hatalar = new List<DogrulamaHatasi>();
                        if (Ustveri != null && !Ustveri.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                        {
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Classes.Ustveri) + " bileşeni doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                            hatalar = new List<DogrulamaHatasi>();
                        }

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

                        if (BelgeImza != null && BelgeImza.Imzalar != null && BelgeImza.Imzalar.Count > 0 && !BelgeImza.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                        {
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Classes.BelgeImza) + " bileşeni doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                            hatalar = new List<DogrulamaHatasi>();
                        }


                        if (NihaiOzet != null && !NihaiOzet.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
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
                case PaketModuTuru.Guncelle:
                    {
                        _package = Package.Open(stream, FileMode.Open, FileAccess.ReadWrite);

                        if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_CORE).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"Core\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

                        if (_package.UstveriExists())
                        {
                            try
                            {
                                Uri readedUstveriUri = _package.GetRelationshipsByType(Constants.RELATION_TYPE_USTVERI).First().TargetUri;
                                Api.V1X.CT_Ustveri readedUstveri = (Api.V1X.CT_Ustveri)new XmlSerializer(typeof(Api.V1X.CT_Ustveri)).Deserialize(_package.GetPart(readedUstveriUri).GetStream(FileMode.Open));
                                Ustveri = readedUstveri.ToUstveri();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.Ustveri) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTVERI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.Ustveri) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            Ustveri = new Ustveri();

                        if (_package.BelgeHedefUriExists())
                        {
                            try
                            {
                                Uri readedBelgeHedefUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEHEDEF).First().TargetUri);
                                Api.V1X.CT_BelgeHedef readedBelgeHedef = (Api.V1X.CT_BelgeHedef)new XmlSerializer(typeof(Api.V1X.CT_BelgeHedef)).Deserialize(_package.GetPart(readedBelgeHedefUri).GetStream(FileMode.Open));
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

                        if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.UstYazi) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

                        if (_package.PaketOzetiExists())
                        {
                            try
                            {
                                Uri readedPaketOzetiUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_PAKETOZETI).First().TargetUri);
                                Api.V1X.CT_PaketOzeti readedPaketOzeti = (Api.V1X.CT_PaketOzeti)new XmlSerializer(typeof(Api.V1X.CT_PaketOzeti)).Deserialize(_package.GetPart(readedPaketOzetiUri).GetStream(FileMode.Open));
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

                        if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_IMZA).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"Imza\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

                        if (_package.BelgeImzaUriExists())
                        {
                            try
                            {
                                Uri readedBelgeImzaUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEIMZA).First().TargetUri);
                                Api.V1X.CT_BelgeImza readedBelgeImza = (Api.V1X.CT_BelgeImza)(new XmlSerializer(typeof(Api.V1X.CT_BelgeImza))).Deserialize(_package.GetPart(readedBelgeImzaUri).GetStream(FileMode.Open));
                                BelgeImza = readedBelgeImza.ToBelgeImza();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeImza) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_BELGEIMZA).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.BelgeImza) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            BelgeImza = new BelgeImza();

                        if (_package.NihaiOzetExists())
                        {
                            try
                            {
                                Uri readedNihaiOzetUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIOZET).First().TargetUri);
                                Api.V1X.CT_NihaiOzet readedNihaiOzet = (Api.V1X.CT_NihaiOzet)new XmlSerializer(typeof(Api.V1X.CT_NihaiOzet)).Deserialize(_package.GetPart(readedNihaiOzetUri).GetStream(FileMode.Open));
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
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIOZET).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiOzet) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            NihaiOzet = new NihaiOzet();

                        break;
                    }
                case PaketModuTuru.Olustur:
                    {
                        _package = Package.Open(stream, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        Ustveri = new Ustveri();
                        PaketOzeti = new PaketOzeti();
                        NihaiOzet = new NihaiOzet();
                        BelgeImza = new BelgeImza();
                        BelgeHedef = new BelgeHedef();
                        break;
                    }
            }

            _stream = stream;
        }

        /// <summary>
        /// Paketin iletileceği hedefleri barındıran nesneye ulaşılır.
        /// </summary>
        public BelgeHedef BelgeHedef { get; set; }

        /// <summary>
        /// Paket içerisindeki BelgeHedef bileşenini STREAM olarak verir.
        /// </summary>
        public Stream BelgeHedefAl() => _package.GetBelgeHedefStream();

        /// <summary>
        /// Paket içerisindeki BelgeHedef bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool BelgeHedefVarMi() => _package.BelgeHedefUriExists();

        /// <summary>
        /// Belgeye atılmış olan imzalara ilişkin üstveri bilgilerini içeren nesneye ulaşılır.
        /// </summary>
        public BelgeImza BelgeImza { get; set; }

        /// <summary>
        /// Paket içerisindeki BelgeImza bileşenini STREAM olarak verir.
        /// </summary>
        public Stream BelgeImzaAl() => _package.GetBelgeImzaStream();

        /// <summary>
        /// Paket içerisindeki BelgeImza bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool BelgeImzaVarMi() => _package.BelgeImzaUriExists();

        /// <summary>
        /// Id'si verilen eke ait dosyanın paket içerisinde olup olmadığını gösterir.
        /// </summary>
        public bool EkDosyaVarMi(IdTip ekId) => _package.EkDosyaExists(ekId);

        /// <summary>
        /// Id'si verilen eke ait dosyayı STREAM olarak verir.
        /// </summary>
        /// <param name="ekId">Ek id değeridir.</param>
        public Stream EkDosyaAl(IdTip ekId) => _package.GetEkDosyaStream(ekId);

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin imzalı (Ayrık olmayan CAdES-XL) değerini STREAM olarak verir.
        /// </summary>
        public Stream ImzaAl() => _package.GetImzaStream();

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin imzalı değeri olup olmadığını gösterir.
        /// </summary>
        public bool ImzaVarMi() => _package.ImzaExists();

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşeninin imzalı (Ayrık olmayan CAdES-XL) değerini STREAM olarak verir.
        /// </summary>
        public Stream MuhurAl() => _package.GetMuhurStream(PaketVersiyonTuru.Versiyon1X);

        /// <summary>
        /// Paket içerisindeki mühür bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool MuhurVarMi() => _package.MuhurExists(PaketVersiyonTuru.Versiyon1X);

        /// <summary>
        /// Paket içerisinde mühürlenen bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        public NihaiOzet NihaiOzet { get; set; }

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşenini STREAM olarak verir.
        /// </summary>
        public Stream NihaiOzetAl() => _package.GetNihaiOzetStream();

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool NihaiOzetVarMi() => _package.NihaiOzetExists();

        /// <summary>
        /// Verilen NihaiOzet nesnesindeki özet değerleri ile paket içerisindeki bileşenlerin özet değerlerini doğrular.
        /// </summary>
        /// <param name="ozet">Doğrulanacak özet değerlerini barındıran NihaiOzet nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        public bool NihaiOzetDogrula(NihaiOzet ozet, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null)
        {
            return ozet.NihaiOzetDogrula(_package, PaketVersiyonTuru.Versiyon1X, ref sonuclar, Ustveri, dagitimTanimlayici);
        }

        /// <summary>
        /// Paket içerisinde imzalanan bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        public PaketOzeti PaketOzeti { get; set; }

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşenini STREAM olarak verir.
        /// </summary>
        public Stream PaketOzetiAl() => _package.GetPaketOzetiStream();

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool PaketOzetiVarMi() => _package.PaketOzetiExists();

        /// <summary>
        /// Verilen PaketOzeti nesnesindeki özet değerleri ile paket içerisindeki bileşenlerin özet değerlerini doğrular.
        /// </summary>
        /// <param name="ozet">Doğrulanacak özet değerlerini barındıran PaketOzeti nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        public bool PaketOzetiDogrula(PaketOzeti ozet, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null)
        {
            return ozet.PaketOzetiDogrula(_package, PaketVersiyonTuru.Versiyon1X, ref sonuclar, Ustveri, dagitimTanimlayici);
        }

        /// <summary>
        /// Belgeye ait üstveri alanlarını barıdıran nesneye ulaşılır.
        /// </summary>
        public Ustveri Ustveri { get; set; }

        /// <summary>
        /// Paket içerisindeki Ustveri bileşenini STREAM olarak verir.
        /// </summary>
        public Stream UstveriAl() => _package.GetUstveriStream();

        /// <summary>
        /// Paket içerisindeki Ustveri bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool UstveriVarMi() => _package.UstveriExists();

        /// <summary>
        /// Paket içerisindeki üst yazı bileşenini STREAM olarak verir.
        /// </summary>
        public Stream UstYaziAl() => _package.GetUstYaziStream();

        /// <summary>
        /// Paket içerisindeki üst yazı bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool UstYaziVarMi() => _package.UstYaziExists();

        /// <summary>
        /// Yeni bir paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="paketStream">Pakete ilişkin STREAM objesidir.</param>
        public static IPaketV1XOlustur Olustur(Stream paketStream) =>
            new PaketV1X(paketStream, PaketModuTuru.Olustur);

        /// <summary>
        /// Yeni bir paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="paketDosyaYolu">Pakete ilişkin dosya yoludur.</param>
        public static IPaketV1XOlustur Olustur(string paketDosyaYolu)
        {
            if (File.Exists(paketDosyaYolu))
            {
                try { File.Delete(paketDosyaYolu); } catch { }
            }
            return new PaketV1X(File.Open(paketDosyaYolu, FileMode.OpenOrCreate, FileAccess.ReadWrite), PaketModuTuru.Olustur);
        }

        /// <summary>
        /// Paket bileşenlerinin içerisine eklenecek özetlerin oluşturulmasında kullanılacak algoritmayı belirtir.
        /// </summary>
        /// <param name="ozetAlgoritma">Algortima değeridir.</param>
        public IPaketV1XOlusturOzetAlgoritma OzetAlgoritmaIle(OzetAlgoritmaTuru ozetAlgoritma)
        {
            if (ozetAlgoritma == OzetAlgoritmaTuru.RIPEMD160 || ozetAlgoritma == OzetAlgoritmaTuru.SHA1)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(OzetAlgoritmaTuru.SHA1) + " özet algoritması ve " + nameof(OzetAlgoritmaTuru.RIPEMD160) + " özet algoritması kullanımdan kaldırılmıştır.",
                    HataTuru = DogrulamaHataTuru.Onemli
                });
            else if (ozetAlgoritma == OzetAlgoritmaTuru.SHA384)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(OzetAlgoritmaTuru.SHA384) + " özet algoritması yalnızca e-Yazışma API 2.X versiyonlarında kullanılabilir.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });

            _ozetAlgoritma = ozetAlgoritma;
            return this;
        }

        /// <summary>
        /// Paket içerisine üst yazı bileşeni ekler.
        /// </summary>
        /// <param name="ustYazi">Üst yazı bileşeni verileridir.</param>
        public IPaketV1XOlusturUstYazi UstYaziAta(UstYazi ustYazi)
        {
            if (_package.UstYaziExists())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = "Paket içerisinde " + nameof(UstYazi) + " bileşeni bulunmaktadır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                var hatalar = new List<DogrulamaHatasi>();
                if (!ustYazi.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                    _dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(UstYazi) + " bileşeni doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });

                if (!KritikHataExists())
                {
                    _package.AddUstYazi(ustYazi);
                    Ustveri.MimeTuru = ustYazi.MimeTuru;
                }
            }
            return this;
        }

        /// <summary>
        /// Belgeye ait üstveri alanlarını barındıran Ustveri bileşenini paket içerisine  ekler.
        /// </summary>
        /// <param name="ustveri">Ustveri bileşenidir.</param>
        public IPaketV1XOlusturUstveri UstveriAta(Ustveri ustveri)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!ustveri.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.Ustveri) + " bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
            {
                ustveri.MimeTuru = Ustveri.MimeTuru;
                if (ustveri.GuvenlikKodu == GuvenlikKoduTuru.YOK)
                    ustveri.GuvenlikKodu = GuvenlikKoduTuru.TSD;

                ustveri.Dagitimlar.ForEach((dagitim) =>
                {
                    if (dagitim.IvedilikTuru == IvedilikTuru.ACL)
                        dagitim.IvedilikTuru = IvedilikTuru.IVD;
                });

                Ustveri = ustveri;
            }

            return this;
        }

        /// <summary>
        ///  Paketin iletileceği hedefleri barındıran BelgeHedef bileşenini paket içerisine ekler.
        /// </summary>
        /// <param name="belgeHedef">BelgeHedef bileşenidir.</param>
        public IPaketV1XOlusturBelgeHedef BelgeHedefAta(BelgeHedef belgeHedef)
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
        /// Belgeye atılmış olan imzalara ilişkin üstveri bilgilerini içeren BelgeImza bileşenini paket içerisine ekler.
        /// </summary>
        /// <param name="belgeImza">BelgeImza bileşenidir.</param>
        public IPaketV1XOlusturBelgeImza BelgeImzaIle(BelgeImza belgeImza)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!belgeImza.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.BelgeImza) + " bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });
            if (!KritikHataExists())
                BelgeImza = belgeImza;

            return this;
        }

        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eke ait dosya bileşenini ekler. 
        /// </summary>
        /// <param name="ekDosya">Eke ait dosya bileşeni verileridir.</param>
        public IPaketV1XOlusturEkDosya EkDosyaIle(EkDosya ekDosya)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!ekDosya.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(EkDosya) + " bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
            {
                if (_package.EkDosyaExists(ekDosya.Ek.Id))
                    _dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Paket içerisinde bu " + nameof(EkDosya) + " bileşeni bulunmaktadır. EkId: " + ekDosya.Ek.Id.Deger,
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else
                    _package.AddEkDosya(ekDosya);
            }

            return this;
        }

        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eklere ait dosya bileşenlerini ekler. 
        /// </summary>
        /// <param name="ekDosyalar">Eklere ait dosya bileşeni verileri listesidir.</param>
        public IPaketV1XOlusturEkDosyalar EkDosyalarIle(List<EkDosya> ekDosyalar)
        {
            if (ekDosyalar != null && ekDosyalar.Count > 0 && !KritikHataExists())
            {
                ekDosyalar.ForEach((ekDosya) =>
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!ekDosya.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                        _dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(EkDosya) + " bileşeni doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });

                    if (!KritikHataExists())
                    {
                        if (_package.EkDosyaExists(ekDosya.Ek.Id))
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "Paket içerisinde bu " + nameof(EkDosya) + " bileşeni bulunmaktadır. EkId: " + ekDosya.Ek.Id.Deger,
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            _package.AddEkDosya(ekDosya);
                    }
                });
            }

            return this;
        }

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        public IPaketV1XOlusturPaketBilgi PaketDurumuIle(string durum)
        {
            PaketDurumuInternal(durum);
            return this;
        }

        /// <summary>
        /// Paketin oluşturulduğu tarihi ekler.
        /// </summary>
        /// <param name="olusturulmaTarihi">Oluşturulma tarihidir.</param>
        public IPaketV1XOlusturPaketBilgi PaketOlusturulmaTarihiIle(DateTime? olusturulmaTarihi)
        {
            if (!KritikHataExists())
                _package.SetPaketOlusturulmaTarihi(olusturulmaTarihi);
            return this;
        }

        /// <summary>
        /// Paket açıklamasını ekler.
        /// </summary>
        /// <param name="aciklama">Açıklama değeridir.</param>
        public IPaketV1XOlusturPaketBilgi PaketAciklamasiIle(string aciklama)
        {
            if (!KritikHataExists())
                _package.SetPaketAciklamasi(aciklama);
            return this;
        }

        /// <summary>
        /// Genellikle indeksleme ve arama için kullanılan anahtar kelimeleri ekler.
        /// </summary>
        /// <param name="anahtarKelimeler">Ayraçlar kullanılarak ayrılmış bir cümledir.</param>
        public IPaketV1XOlusturPaketBilgi PaketAnahtarKelimeleriIle(string anahtarKelimeler)
        {
            if (!KritikHataExists())
                _package.SetPaketAnahtarKelimeleri(anahtarKelimeler);
            return this;
        }

        /// <summary>
        /// Paket dilini ekler.
        /// </summary>
        /// <param name="dil">Dil değeridir.</param>
        public IPaketV1XOlusturPaketBilgi PaketDiliIle(string dil)
        {
            if (!KritikHataExists())
                _package.SetPaketDili(dil);
            return this;
        }

        /// <summary>
        /// Paketi olduğu belgenin son yazdırılma tarihini ekler.
        /// </summary>
        /// <param name="sonYazdirilmaTarihi">Son yazdırılma tarihidir.</param>
        public IPaketV1XOlusturPaketBilgi PaketSonYazdirilmaTarihiIle(DateTime? sonYazdirilmaTarihi)
        {
            if (!KritikHataExists())
                _package.SetPaketSonYazdirilmaTarihi(sonYazdirilmaTarihi);
            return this;
        }

        /// <summary>
        /// Paket başlığı ekler.
        /// </summary>
        /// <param name="baslik">Başlık değeridir.</param>
        public IPaketV1XOlusturPaketBilgi PaketBasligiIle(string baslik)
        {
            if (!KritikHataExists())
                _package.SetPaketBasligi(baslik);
            return this;
        }

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        public IPaketV1XOlusturBilesen BilesenleriOlustur()
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!Ustveri.IlgileriDogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "İlgiler doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });
                hatalar = new List<DogrulamaHatasi>();
            }

            if (!Ustveri.EkleriDogrula(_package, PaketVersiyonTuru.Versiyon1X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "Ekler doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
            {
                _package.GenerateCore(Ustveri, PaketVersiyonTuru.Versiyon1X);
                _package.GenerateBelgeHedef(BelgeHedef);
                if (BelgeImza != null && BelgeImza != new BelgeImza())
                    _package.GenerateBelgeImza(BelgeImza);
                _package.GenerateUstveri(Ustveri, PaketVersiyonTuru.Versiyon1X);
                PaketOzetiInternal("oluşturulamaz");
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        public IPaketV1XOlusturImza ImzaEkle(Func<Stream, byte[]> imzaFunction)
        {
            if (!KritikHataExists())
            {
                var imzaByteArray = imzaFunction.Invoke(PaketOzetiAl());

                ImzaEkleInternal(imzaByteArray);
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="muhurFunction">
        /// Mühür ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak NihaiOzet bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek mühür değeridir.
        /// </param>
        /// <remarks>Eklenecek mühür "Ayrık olmayan CAdES-XL" türünde imza olmalıdır.</remarks>
        public IPaketV1XOlusturMuhur MuhurEkle(Func<Stream, byte[]> muhurFunction)
        {
            if (!KritikHataExists())
            {
                var muhurByteArray = muhurFunction.Invoke(NihaiOzetAl());
                MuhurEkleInternal(muhurByteArray);
            }
            return this;
        }

        /// <summary>
        /// Oluşturulan paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        public IPaketV1XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction)
        {
            dogrulamaAction.Invoke(KritikHataExists(), _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Var olan bir paketi okumak için kullanılır.
        /// </summary>
        /// <param name="paketStream">Pakete ilişkin STREAM objesidir.</param>
        public static IPaketV1XOku Oku(Stream paketStream) =>
            new PaketV1X(paketStream, PaketModuTuru.Oku);

        /// <summary>
        /// Var olan bir paketi okumak için kullanılır.
        /// </summary>
        /// <param name="paketDosyaYolu">Pakete ilişkin dosya yoludur.</param>
        public static IPaketV1XOku Oku(string paketDosyaYolu) =>
            new PaketV1X(File.Open(paketDosyaYolu, FileMode.Open, FileAccess.Read), PaketModuTuru.Oku);

        /// <summary>
        /// Paket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// IPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        public IPaketV1XOkuBilesenAl BilesenleriAl(Action<bool, IPaketV1XOkuBilesen, List<DogrulamaHatasi>> bilesenAction)
        {
            bilesenAction.Invoke(!KritikHataExists(), this, _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Var olan bir paketi güncellemek için kullanılır.
        /// </summary>
        /// <param name="paketStream">Pakete ilişkin STREAM objesidir.</param>
        public static IPaketV1XGuncelle Guncelle(Stream paketStream) =>
            new PaketV1X(paketStream, PaketModuTuru.Guncelle);

        /// <summary>
        /// Var olan bir paketi güncellemek için kullanılır.
        /// </summary>
        /// <param name="paketDosyaYolu">Pakete ilişkin dosya yoludur.</param>
        public static IPaketV1XGuncelle Guncelle(string paketDosyaYolu) =>
            new PaketV1X(File.Open(paketDosyaYolu, FileMode.Open, FileAccess.ReadWrite), PaketModuTuru.Guncelle);

        /// <summary>
        /// Paketin son güncelleme tarih bilgisinin ekler.
        /// </summary>
        /// <param name="guncellemeTarihi">Son güncelleme tarih bilgisidir.</param>
        public IPaketV1XGuncellePaketBilgi PaketGuncellemeTarihiIle(DateTime? guncellemeTarihi)
        {
            if (!KritikHataExists())
                _package.SetPaketGuncellemeTarihi(guncellemeTarihi);
            return this;
        }

        /// <summary>
        /// Paketi son güncelleyen taraf bilgisinin ekler.
        /// </summary>
        /// <param name="sonGuncelleyen">Son güncelleyen taraf bilgisidir.</param>
        public IPaketV1XGuncellePaketBilgi PaketSonGuncelleyenIle(string sonGuncelleyen)
        {
            if (!KritikHataExists())
                _package.SetPaketSonGuncelleyen(sonGuncelleyen);
            return this;
        }

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV1XGuncellePaketBilgi IPaketV1XGuncellePaketBilgi.PaketDurumuIle(string durum)
        {
            PaketDurumuInternal(durum);
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV1XGuncelleImza IPaketV1XGuncelle.ImzaEkle(Func<Stream, byte[]> imzaFunction)
        {
            if (!KritikHataExists())
            {
                var imzaByteArray = imzaFunction.Invoke(PaketOzetiAl());

                ImzaEkleInternal(imzaByteArray);
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV1XGuncelleImza IPaketV1XGuncelle.ImzaEkle(byte[] imza)
        {
            if (!KritikHataExists())
                ImzaEkleInternal(imza);
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV1XGuncelleImza IPaketV1XGuncelleBelgeImza.ImzaEkle(Func<Stream, byte[]> imzaFunction)
        {
            if (!KritikHataExists())
            {
                var imzaByteArray = imzaFunction.Invoke(PaketOzetiAl());

                ImzaEkleInternal(imzaByteArray);
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV1XGuncelleImza IPaketV1XGuncelleBelgeImza.ImzaEkle(byte[] imza)
        {
            if (!KritikHataExists())
                ImzaEkleInternal(imza);
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV1XGuncelleImza IPaketV1XGuncellePaketBilgi.ImzaEkle(Func<Stream, byte[]> imzaFunction)
        {
            if (!KritikHataExists())
            {
                var imzaByteArray = imzaFunction.Invoke(PaketOzetiAl());

                ImzaEkleInternal(imzaByteArray);
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV1XGuncelleImza IPaketV1XGuncellePaketBilgi.ImzaEkle(byte[] imza)
        {
            if (!KritikHataExists())
                ImzaEkleInternal(imza);
            return this;
        }

        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="muhurFunction">
        /// Mühür ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak NihaiOzet bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek mühür değeridir.
        /// </param>
        /// <remarks>Eklenecek mühür "Ayrık olmayan CAdES-XL" türünde imza olmalıdır.</remarks>
        IPaketV1XGuncelleMuhur IPaketV1XGuncelle.MuhurEkle(Func<Stream, byte[]> muhurFunction)
        {
            if (!KritikHataExists())
            {
                var muhurByteArray = muhurFunction.Invoke(NihaiOzetAl());
                MuhurEkleInternal(muhurByteArray);
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek mühür değeridir. Eklenecek mühür "Ayrık olmayan CAdES-XL" türünde imza olmalıdır.</param>
        IPaketV1XGuncelleMuhur IPaketV1XGuncelle.MuhurEkle(byte[] muhur)
        {
            if (!KritikHataExists())
                MuhurEkleInternal(muhur);
            return this;
        }

        /// <summary>
        /// Paket içerisinden ek çıkarılmak için kullanılır.
        /// </summary>
        /// <param name="ekId">Çıkarılacak eke ait Id bilgisidir.</param>
        IPaketV1XGuncelleEkDosya IPaketV1XGuncelle.EkDosyaCikar(IdTip ekId)
        {
            _package.DeleteEkDosya(ekId);
            return this;
        }

        /// <summary>
        /// Paket içerisinden ek çıkarılmak için kullanılır.
        /// </summary>
        /// <param name="ekId">Çıkarılacak eke ait Id bilgisidir.</param>
        IPaketV1XGuncelleEkDosya IPaketV1XGuncelleEkDosya.EkDosyaCikar(IdTip ekId)
        {
            _package.DeleteEkDosya(ekId);
            return this;
        }

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV1XGuncellePaketBilgi IPaketV1XGuncelle.PaketDurumuIle(string durum)
        {
            PaketDurumuInternal(durum);
            return this;
        }

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV1XGuncellePaketBilgi IPaketV1XGuncelleBelgeImza.PaketDurumuIle(string durum)
        {
            PaketDurumuInternal(durum);
            return this;
        }

        /// <summary>
        /// Belgeye atılmış olan imzalara ilişkin üstveri bilgilerini içeren BelgeImza bileşenini paket içerisine ekler.
        /// </summary>
        /// <param name="belgeImza">BelgeImza bileşenidir.</param>
        IPaketV1XGuncelleBelgeImza IPaketV1XGuncelle.BelgeImzaIle(BelgeImza belgeImza)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!belgeImza.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.BelgeImza) + " bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
            {
                BelgeImza = belgeImza;
                _package.GenerateBelgeImza(BelgeImza);
            }

            return this;
        }

        /// <summary>
        /// Güncellenen paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV1XGuncelleDogrula IPaketV1XGuncelleImza.Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction)
        {
            dogrulamaAction.Invoke(KritikHataExists(), _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Ek çıkarma işlemi yapılan paketin doğrulanmasını sağlar.
        /// </summary>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        public IPaketV1XGuncelleDogrula Dogrula(TanimlayiciTip dagitimTanimlayici, Action<bool, List<DogrulamaHatasi>> dogrulamaAction)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!Ustveri.IlgileriDogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "İlgiler doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });
                hatalar = new List<DogrulamaHatasi>();
            }

            if (!Ustveri.EkleriDogrula(_package, PaketVersiyonTuru.Versiyon1X, ref hatalar))
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "Ekler doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });
                hatalar = new List<DogrulamaHatasi>();
            }

            if (!PaketOzeti.PaketOzetiDogrula(_package, PaketVersiyonTuru.Versiyon1X, ref hatalar, Ustveri, dagitimTanimlayici))
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "PaketOzeti bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });
                hatalar = new List<DogrulamaHatasi>();
            }

            if (!NihaiOzet.NihaiOzetDogrula(_package, PaketVersiyonTuru.Versiyon1X, ref hatalar, Ustveri))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "NihaiOzet bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            dogrulamaAction.Invoke(KritikHataExists(), _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Tek bir özet değeri ile paket içerisindeki ilgili bileşenin özet değerlerini doğrular.
        /// </summary>
        /// <param name="referans">Doğrulanacak özet değerini barındıran Referans nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        public bool ReferansDogrula(Referans referans, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null)
        {
            return referans.ReferansDogrula(_package, PaketVersiyonTuru.Versiyon1X, ref sonuclar, Ustveri, dagitimTanimlayici);
        }

        /// <summary>
        /// Ustveri bileşeninde ek olarak belirtilmiş ilgiye ilişkin ekin paket içerisinde bulunup bulunmadığını doğrular.
        /// </summary>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        public void IlgileriDogrula(ref List<DogrulamaHatasi> sonuclar)
        {
            Ustveri.IlgileriDogrula(PaketVersiyonTuru.Versiyon1X, ref sonuclar);
        }

        /// <summary>
        /// Ustveri bileşeninde DED türünde ek olarak belirtilmiş ekin paket içerisinde bulunup bulunmadığını doğrular.
        /// </summary>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        public void EkleriDogrula(ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null)
        {
            Ustveri.EkleriDogrula(_package, PaketVersiyonTuru.Versiyon1X, ref sonuclar, dagitimTanimlayici);
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

        private void PaketOzetiInternal(string mesaj)
        {
            if (!BelgeHedefVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.BelgeHedef) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.PaketOzeti) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetBelgeHedef = _ozetAlgoritma.CalculateHash(BelgeHedefAl());
                PaketOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                        _ozetAlgoritma,
                                        ozetBelgeHedef,
                                        Constants.URI_BELGEHEDEF);
            }

            if (!UstYaziVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.UstYazi) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.PaketOzeti) + " " + mesaj + "."
                });
            else
            {
                var partUstYaziUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).First().TargetUri);
                byte[] ozetUstYazi = _ozetAlgoritma.CalculateHash(UstYaziAl());
                PaketOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                        _ozetAlgoritma,
                                        ozetUstYazi,
                                        partUstYaziUri);
            }

            if (!UstveriVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.Ustveri) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.PaketOzeti) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetUstveri = _ozetAlgoritma.CalculateHash(UstveriAl());
                PaketOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                        _ozetAlgoritma,
                                        ozetUstveri,
                                        Constants.URI_USTVERI);

                if (Ustveri.Ekler != null && Ustveri.Ekler.Count > 0)
                {
                    foreach (var ek in Ustveri.Ekler)
                    {
                        if (ek.Tur == EkTuru.DED && ek.ImzaliMi)
                        {
                            var relationShip = _package.GetRelationshipsByType(Constants.RELATION_TYPE_EK).SingleOrDefault(r => string.Compare(r.Id, Constants.ID_ROOT_EK(ek.Id.Deger), true) == 0);
                            var ekStream = _package.GetPart(relationShip.TargetUri).GetStream();
                            byte[] ekOzeti = _ozetAlgoritma.CalculateHash(ekStream);
                            ekStream.Position = 0;
                            PaketOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                                    _ozetAlgoritma,
                                                    ekOzeti,
                                                    relationShip.TargetUri);
                        }
                    }
                }

                PaketOzeti.Id = Ustveri.BelgeId;
            }

            var hatalar = new List<DogrulamaHatasi>();
            if (!PaketOzeti.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.PaketOzeti) + " bileşeni doğrulanamamıştır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                _package.DeletePaketOzeti();
                _package.GeneratePaketOzeti(PaketOzeti, PaketVersiyonTuru.Versiyon1X);
            }
        }

        private void NihaiOzetInternal(string mesaj)
        {
            if (!BelgeHedefVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.BelgeHedef) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetBelgeHedef = _ozetAlgoritma.CalculateHash(BelgeHedefAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                        _ozetAlgoritma,
                                        ozetBelgeHedef,
                                        Constants.URI_BELGEHEDEF);
            }

            if (BelgeImzaVarMi())
            {
                byte[] ozetBelgeImza = _ozetAlgoritma.CalculateHash(BelgeImzaAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                        _ozetAlgoritma,
                                        ozetBelgeImza,
                                        Constants.URI_BELGEIMZA);
            }

            if (!UstYaziVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.UstYazi) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            else
            {
                var partUstYaziUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).First().TargetUri);
                byte[] ozetUstYazi = _ozetAlgoritma.CalculateHash(UstYaziAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                        _ozetAlgoritma,
                                        ozetUstYazi,
                                        partUstYaziUri);
            }

            if (!UstveriVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.Ustveri) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetUstveri = _ozetAlgoritma.CalculateHash(UstveriAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                        _ozetAlgoritma,
                                        ozetUstveri,
                                        Constants.URI_USTVERI);

                if (Ustveri.Ekler != null && Ustveri.Ekler.Count > 0)
                {
                    foreach (var ek in Ustveri.Ekler)
                    {
                        if (ek.Tur == EkTuru.DED && ek.ImzaliMi)
                        {
                            var relationShip = _package.GetRelationshipsByType(Constants.RELATION_TYPE_EK).SingleOrDefault(r => string.Compare(r.Id, Constants.ID_ROOT_EK(ek.Id.Deger), true) == 0);
                            var ekStream = _package.GetPart(relationShip.TargetUri).GetStream();
                            byte[] ekOzeti = _ozetAlgoritma.CalculateHash(ekStream);
                            ekStream.Position = 0;
                            NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                                    _ozetAlgoritma,
                                                    ekOzeti,
                                                    relationShip.TargetUri);
                        }
                    }
                }

                NihaiOzet.Id = Ustveri.BelgeId;
            }


            if (!PaketOzetiVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.PaketOzeti) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetPaketOzeti = _ozetAlgoritma.CalculateHash(PaketOzetiAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                        _ozetAlgoritma,
                                        ozetPaketOzeti,
                                        Constants.URI_PAKETOZETI);
            }

            if (!ImzaVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = "Elektronik İmza bileşeni bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetImza = _ozetAlgoritma.CalculateHash(ImzaAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                        _ozetAlgoritma,
                                        ozetImza,
                                        Constants.URI_IMZA);
            }

            var coreRelations = _package.GetRelationshipsByType(Constants.RELATION_TYPE_CORE);
            if (coreRelations == null || !coreRelations.Any())
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = "Core bileşeni bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            }
            else
            {
                var corePart = _package.GetPart(coreRelations.First().TargetUri);
                var stream = corePart.GetStream();
                stream.Position = 0;
                byte[] ozetCore = _ozetAlgoritma.CalculateHash(stream);
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon1X,
                                        _ozetAlgoritma,
                                        ozetCore,
                                        coreRelations.First().TargetUri);
            }

            var hatalar = new List<DogrulamaHatasi>();
            if (!NihaiOzet.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.NihaiOzet) + " bileşeni doğrulanamamıştır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                _package.DeleteNihaiOzet();
                _package.GenerateNihaiOzet(NihaiOzet, PaketVersiyonTuru.Versiyon1X);
            }
        }

        private void ImzaEkleInternal(byte[] imzaByteArray)
        {
            if (imzaByteArray == null && imzaByteArray.Length == 0)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = "Elektronik imza bileşeni değeri boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                _package.DeleteImza();
                _package.AddImza(imzaByteArray);
                _package.GenerateCore(Ustveri, PaketVersiyonTuru.Versiyon1X);
                NihaiOzetInternal("oluşturulamaz");
            }
        }

        private void MuhurEkleInternal(byte[] muhurByteArray)
        {
            if (muhurByteArray == null || muhurByteArray.Length == 0)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = "Elektronik mühür bileşeni değeri boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                _package.DeleteMuhur(PaketVersiyonTuru.Versiyon1X);
                _package.AddMuhur(muhurByteArray, PaketVersiyonTuru.Versiyon1X);
            }
        }

        private void UstveriAtaInternal(Ustveri ustveri)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!ustveri.Dogrula(PaketVersiyonTuru.Versiyon1X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.Ustveri) + " bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
            {
                ustveri.MimeTuru = Ustveri.MimeTuru;
                if (ustveri.GuvenlikKodu == GuvenlikKoduTuru.YOK)
                    ustveri.GuvenlikKodu = GuvenlikKoduTuru.TSD;

                ustveri.Dagitimlar.ForEach((dagitim) =>
                {
                    if (dagitim.IvedilikTuru == IvedilikTuru.ACL)
                        dagitim.IvedilikTuru = IvedilikTuru.IVD;
                });

                Ustveri = ustveri;
            }
        }

        private void PaketDurumuInternal(string durum)
        {
            if (!KritikHataExists())
                _package.SetPaketDurumu(durum);
        }

        private bool KritikHataExists() => _dogrulamaHatalari.Any(p => p.HataTuru == DogrulamaHataTuru.Kritik);
    }

    /// <summary>
    /// E-Yazışma Teknik Rehberi Sürüm 2.0 uygun şekilde paket işlemlerinin gerçekleştirilmesi için kullanılır.
    /// </summary>
    public sealed class PaketV2X : IPaketV2X, IDisposable
    {
        readonly Package _package;
        readonly Stream _stream;
        readonly PaketModuTuru _paketModu;
        readonly List<DogrulamaHatasi> _dogrulamaHatalari;
        OzetAlgoritmaTuru _ozetAlgoritma;

        private PaketV2X(Stream stream, PaketModuTuru paketModu)
        {
            stream.Position = 0;
            _paketModu = paketModu;
            _ozetAlgoritma = OzetAlgoritmaTuru.SHA384;
            _dogrulamaHatalari = new List<DogrulamaHatasi>();

            switch (paketModu)
            {
                case PaketModuTuru.Oku:
                    {
                        _package = Package.Open(stream, FileMode.Open, FileAccess.Read);

                        if (!_package.GetRelationships().Any())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"İlişki\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

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

                        if (!_package.UstveriExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.Ustveri) + "\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTVERI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.Ustveri) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                        {
                            try
                            {
                                Uri readedUstveriUri = _package.GetRelationshipsByType(Constants.RELATION_TYPE_USTVERI).First().TargetUri;
                                Api.V2X.CT_Ustveri readedUstveri = (Api.V2X.CT_Ustveri)new XmlSerializer(typeof(Api.V2X.CT_Ustveri)).Deserialize(_package.GetPart(readedUstveriUri).GetStream(FileMode.Open));
                                Ustveri = readedUstveri.ToUstveri();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.Ustveri) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                                Ustveri = null;
                            }
                        }

                        if (!_package.UstYaziExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.UstYazi) + "\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.UstYazi) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });


                        if (!_package.NihaiUstveriExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiUstveri) + "\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIUSTVERI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiUstveri) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                        {
                            try
                            {
                                Uri readedNihaiUstveriUri = _package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIUSTVERI).First().TargetUri;
                                Api.V2X.CT_NihaiUstveri readedNihaiUstveri = (Api.V2X.CT_NihaiUstveri)new XmlSerializer(typeof(Api.V2X.CT_NihaiUstveri)).Deserialize(_package.GetPart(readedNihaiUstveriUri).GetStream(FileMode.Open));
                                NihaiUstveri = readedNihaiUstveri.ToNihaiUstveri();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiUstveri) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                                NihaiUstveri = null;
                            }
                        }

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
                                Api.V2X.CT_PaketOzeti readedPaketOzeti = (Api.V2X.CT_PaketOzeti)new XmlSerializer(typeof(Api.V2X.CT_PaketOzeti)).Deserialize(_package.GetPart(readedPaketOzetiUri).GetStream(FileMode.Open));
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

                        if (!_package.ImzaExists())
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"PaketOzeti Imza\" bileşeni yoktur.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_IMZA).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"PaketOzeti Imza\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

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
                                Api.V2X.CT_NihaiOzet readedNihaiOzet = (Api.V2X.CT_NihaiOzet)new XmlSerializer(typeof(Api.V2X.CT_NihaiOzet)).Deserialize(_package.GetPart(readedNihaiOzetUri).GetStream(FileMode.Open));
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

                        if (_package.ParafOzetiExists())
                        {
                            try
                            {
                                Uri readedParafOzetiUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_PARAFOZETI).First().TargetUri);
                                Api.V2X.CT_ParafOzeti readedParafOzeti = (Api.V2X.CT_ParafOzeti)(new XmlSerializer(typeof(Api.V2X.CT_ParafOzeti))).Deserialize(_package.GetPart(readedParafOzetiUri).GetStream(FileMode.Open));
                                ParafOzeti = readedParafOzeti.ToParafOzeti();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.ParafOzeti) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_PARAFOZETI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.ParafOzeti) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            ParafOzeti = new ParafOzeti();

                        var hatalar = new List<DogrulamaHatasi>();
                        if (Ustveri != null && !Ustveri.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                        {
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Classes.Ustveri) + " bileşeni doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                            hatalar = new List<DogrulamaHatasi>();
                        }

                        if (NihaiUstveri != null && !NihaiUstveri.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                        {
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Classes.NihaiUstveri) + " bileşeni doğrulanamamıştır.",
                                HataTuru = hatalar.GetDogrulamaHataTuru().Value
                            });
                            hatalar = new List<DogrulamaHatasi>();
                        }

                        if (PaketOzeti != null && !PaketOzeti.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                        {
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Classes.PaketOzeti) + " bileşeni doğrulanamamıştır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                            hatalar = new List<DogrulamaHatasi>();
                        }

                        if (ParafOzeti != null && ParafOzeti != new ParafOzeti() && !ParafOzeti.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                        {
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                AltDogrulamaHatalari = hatalar,
                                Hata = nameof(Classes.ParafOzeti) + " bileşeni doğrulanamamıştır.",
                                HataTuru = DogrulamaHataTuru.Kritik
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
                case PaketModuTuru.Guncelle:
                    {
                        _package = Package.Open(stream, FileMode.Open, FileAccess.ReadWrite);

                        if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_CORE).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"Core\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

                        if (_package.UstveriExists())
                        {
                            try
                            {
                                Uri readedUstveriUri = _package.GetRelationshipsByType(Constants.RELATION_TYPE_USTVERI).First().TargetUri;
                                Api.V1X.CT_Ustveri readedUstveri = (Api.V1X.CT_Ustveri)new XmlSerializer(typeof(Api.V1X.CT_Ustveri)).Deserialize(_package.GetPart(readedUstveriUri).GetStream(FileMode.Open));
                                Ustveri = readedUstveri.ToUstveri();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.Ustveri) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTVERI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.Ustveri) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            Ustveri = new Ustveri();

                        if (_package.NihaiUstveriExists())
                        {
                            try
                            {
                                Uri readedNihaiUstveriUri = _package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIUSTVERI).First().TargetUri;
                                Api.V2X.CT_NihaiUstveri readedNihaiUstveri = (Api.V2X.CT_NihaiUstveri)new XmlSerializer(typeof(Api.V2X.CT_NihaiUstveri)).Deserialize(_package.GetPart(readedNihaiUstveriUri).GetStream(FileMode.Open));
                                NihaiUstveri = readedNihaiUstveri.ToNihaiUstveri();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiUstveri) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIUSTVERI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiUstveri) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            NihaiUstveri = new NihaiUstveri();

                        if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.UstYazi) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

                        if (_package.PaketOzetiExists())
                        {
                            try
                            {
                                Uri readedPaketOzetiUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_PAKETOZETI).First().TargetUri);
                                Api.V1X.CT_PaketOzeti readedPaketOzeti = (Api.V1X.CT_PaketOzeti)new XmlSerializer(typeof(Api.V1X.CT_PaketOzeti)).Deserialize(_package.GetPart(readedPaketOzetiUri).GetStream(FileMode.Open));
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

                        if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_IMZA).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"Imza\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });

                        if (_package.ParafOzetiExists())
                        {
                            try
                            {
                                Uri readedParafOzetiUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_PARAFOZETI).First().TargetUri);
                                Api.V2X.CT_ParafOzeti readedParafOzeti = (Api.V2X.CT_ParafOzeti)(new XmlSerializer(typeof(Api.V2X.CT_ParafOzeti))).Deserialize(_package.GetPart(readedParafOzetiUri).GetStream(FileMode.Open));
                                ParafOzeti = readedParafOzeti.ToParafOzeti();
                            }
                            catch (Exception ex)
                            {
                                _dogrulamaHatalari.Add(new DogrulamaHatasi
                                {
                                    Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.ParafOzeti) + "\" bileşeni hatalıdır.",
                                    HataTuru = DogrulamaHataTuru.Kritik,
                                    InnerException = ex
                                });
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_PARAFOZETI).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.ParafOzeti) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            ParafOzeti = new ParafOzeti();

                        if (_package.NihaiOzetExists())
                        {
                            try
                            {
                                Uri readedNihaiOzetUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIOZET).First().TargetUri);
                                Api.V1X.CT_NihaiOzet readedNihaiOzet = (Api.V1X.CT_NihaiOzet)new XmlSerializer(typeof(Api.V1X.CT_NihaiOzet)).Deserialize(_package.GetPart(readedNihaiOzetUri).GetStream(FileMode.Open));
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
                            }
                        }
                        else if (_package.GetRelationshipsByType(Constants.RELATION_TYPE_NIHAIOZET).Count() > 1)
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "E-Yazışma Paketi geçersizdir. \"" + nameof(Classes.NihaiOzet) + "\" bileşeni birden fazla olmamalıdır.",
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            NihaiOzet = new NihaiOzet();

                        break;
                    }
                case PaketModuTuru.Olustur:
                    {
                        _package = Package.Open(stream, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        Ustveri = new Ustveri();
                        PaketOzeti = new PaketOzeti();
                        NihaiOzet = new NihaiOzet();
                        NihaiUstveri = new NihaiUstveri();
                        ParafOzeti = new ParafOzeti();
                        break;
                    }
            }

            _stream = stream;
        }

        /// <summary>
        /// Id'si verilen eke ait dosyanın paket içerisinde olup olmadığını gösterir.
        /// </summary>
        public bool EkDosyaVarMi(IdTip ekId) => _package.EkDosyaExists(ekId);

        /// <summary>
        /// Id'si verilen eke ait dosyayı STREAM olarak verir.
        /// </summary>
        /// <param name="ekId">Ek id değeridir.</param>
        public Stream EkDosyaAl(IdTip ekId) => _package.GetEkDosyaStream(ekId);

        /// <summary>
        /// Paket içerisindeki ParafOzeti bileşeninin imzalı değeri olup olmadığını gösterir.
        /// </summary>
        public bool ParafImzaVarMi() => _package.ParafImzaExists();

        /// <summary>
        /// Paket içerisindeki ParafOzeti bileşeninin imzalı (Ayrık olmayan CAdES-XL) değerini STREAM olarak verir.
        /// </summary>
        public Stream ParafImzaAl() => _package.GetParafImzaStream();

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin imzalı (Ayrık olmayan CAdES-XL) değerini STREAM olarak verir.
        /// </summary>
        public Stream ImzaAl() => _package.GetImzaStream();

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin imzalı değeri olup olmadığını gösterir.
        /// </summary>
        public bool ImzaVarMi() => _package.ImzaExists();

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşeninin imzalı (Ayrık olmayan CAdES-XL) değerini STREAM olarak verir.
        /// </summary>
        public Stream MuhurAl() => _package.GetMuhurStream(PaketVersiyonTuru.Versiyon2X);

        /// <summary>
        /// Paket içerisindeki mühür bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool MuhurVarMi() => _package.MuhurExists(PaketVersiyonTuru.Versiyon2X);

        /// <summary>
        /// Paket içerisinde mühürlenen bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        public NihaiOzet NihaiOzet { get; set; }

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşenini STREAM olarak verir.
        /// </summary>
        public Stream NihaiOzetAl() => _package.GetNihaiOzetStream();

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool NihaiOzetVarMi() => _package.NihaiOzetExists();

        /// <summary>
        /// Verilen NihaiOzet nesnesindeki özet değerleri ile paket içerisindeki bileşenlerin özet değerlerini doğrular.
        /// </summary>
        /// <param name="ozet">Doğrulanacak özet değerlerini barındıran NihaiOzet nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        public bool NihaiOzetDogrula(NihaiOzet ozet, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null)
        {
            return ozet.NihaiOzetDogrula(_package, PaketVersiyonTuru.Versiyon2X, ref sonuclar, Ustveri, dagitimTanimlayici);
        }

        /// <summary>
        /// Paket içerisinde paraflanan bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        public ParafOzeti ParafOzeti { get; set; }

        /// <summary>
        /// Paket içerisindeki ParafOzeti bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool ParafOzetiVarMi() => _package.ParafOzetiExists();

        /// <summary>
        /// Paket içerisindeki ParafOzeti bileşenini STREAM olarak verir.
        /// </summary>
        public Stream ParafOzetiAl() => _package.GetParafOzetiStream();

        /// <summary>
        /// Verilen ParafOzeti nesnesindeki özet değerleri ile paket içerisindeki bileşenlerin özet değerlerini doğrular.
        /// </summary>
        /// <param name="ozet">Doğrulanacak özet değerlerini barındıran ParafOzeti nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        public bool ParafOzetiDogrula(ParafOzeti ozet, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null)
        {
            return ozet.ParafOzetiDogrula(_package, PaketVersiyonTuru.Versiyon2X, ref sonuclar, Ustveri, dagitimTanimlayici);
        }

        /// <summary>
        /// Paket içerisinde imzalanan bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        public PaketOzeti PaketOzeti { get; set; }

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşenini STREAM olarak verir.
        /// </summary>
        public Stream PaketOzetiAl() => _package.GetPaketOzetiStream();

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool PaketOzetiVarMi() => _package.PaketOzetiExists();

        /// <summary>
        /// Verilen PaketOzeti nesnesindeki özet değerleri ile paket içerisindeki bileşenlerin özet değerlerini doğrular.
        /// </summary>
        /// <param name="ozet">Doğrulanacak özet değerlerini barındıran PaketOzeti nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        public bool PaketOzetiDogrula(PaketOzeti ozet, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null)
        {
            return ozet.PaketOzetiDogrula(_package, PaketVersiyonTuru.Versiyon2X, ref sonuclar, Ustveri, dagitimTanimlayici);
        }

        /// <summary>
        /// Belgeye ait üstveri alanlarını barıdıran nesneye ulaşılır.
        /// </summary>
        public Ustveri Ustveri { get; set; }

        /// <summary>
        /// Paket içerisindeki Ustveri bileşenini STREAM olarak verir.
        /// </summary>
        public Stream UstveriAl() => _package.GetUstveriStream();

        /// <summary>
        /// Paket içerisindeki Ustveri bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool UstveriVarMi() => _package.UstveriExists();

        /// <summary>
        /// Belgeye ait nihai üstveri alanlarını barındıran nesneye ulaşılır.
        /// </summary>
        public NihaiUstveri NihaiUstveri { get; set; }

        /// <summary>
        /// Paket içerisindeki NihaiUstveri bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool NihaiUstveriVarMi() => _package.NihaiUstveriExists();

        /// <summary>
        /// Paket içerisindeki NihaiUstveri bileşenini STREAM olarak verir.
        /// </summary>
        public Stream NihaiUstveriAl() => _package.GetNihaiUstveriStream();

        /// <summary>
        /// Paket içerisindeki üst yazı bileşenini STREAM olarak verir.
        /// </summary>
        public Stream UstYaziAl() => _package.GetUstYaziStream();

        /// <summary>
        /// Paket içerisindeki üst yazı bileşeninin olup olmadığını gösterir.
        /// </summary>
        public bool UstYaziVarMi() => _package.UstYaziExists();

        /// <summary>
        /// Yeni bir paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="paketStream">Pakete ilişkin STREAM objesidir.</param>
        public static IPaketV2XOlustur Olustur(Stream paketStream) =>
            new PaketV2X(paketStream, PaketModuTuru.Olustur);

        /// <summary>
        /// Yeni bir paket oluşturmak için kullanılır.
        /// </summary>
        /// <param name="paketDosyaYolu">Pakete ilişkin dosya yoludur.</param>
        public static IPaketV2XOlustur Olustur(string paketDosyaYolu)
        {
            if (File.Exists(paketDosyaYolu))
            {
                try { File.Delete(paketDosyaYolu); } catch { }
            }
            return new PaketV2X(File.Open(paketDosyaYolu, FileMode.OpenOrCreate, FileAccess.ReadWrite), PaketModuTuru.Olustur);
        }

        /// <summary>
        /// Paket bileşenlerinin içerisine eklenecek özetlerin oluşturulmasında kullanılacak algoritmayı belirtir.
        /// </summary>
        /// <param name="ozetAlgoritma">Algoritma değeridir.</param>
        public IPaketV2XOlusturOzetAlgoritma OzetAlgoritmaIle(OzetAlgoritmaTuru ozetAlgoritma)
        {
            if (ozetAlgoritma == OzetAlgoritmaTuru.RIPEMD160 || ozetAlgoritma == OzetAlgoritmaTuru.SHA1)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = nameof(OzetAlgoritmaTuru.SHA1) + " özet algoritması ve " + nameof(OzetAlgoritmaTuru.RIPEMD160) + " özet algoritması kullanımdan kaldırılmıştır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });

            _ozetAlgoritma = ozetAlgoritma;
            return this;
        }

        /// <summary>
        /// Paket içerisine üst yazı bileşeni ekler.
        /// </summary>
        /// <param name="ustYazi">Üst yazı bileşeni verileridir.</param>
        public IPaketV2XOlusturUstYazi UstYaziAta(UstYazi ustYazi)
        {
            if (_package.UstYaziExists())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = "Paket içerisinde " + nameof(UstYazi) + " bileşeni bulunmaktadır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                var hatalar = new List<DogrulamaHatasi>();
                if (!ustYazi.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                    _dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        AltDogrulamaHatalari = hatalar,
                        Hata = nameof(UstYazi) + " bileşeni doğrulanamamıştır.",
                        HataTuru = hatalar.GetDogrulamaHataTuru().Value
                    });

                if (!KritikHataExists())
                {
                    _package.AddUstYazi(ustYazi);
                    Ustveri.MimeTuru = ustYazi.MimeTuru;
                }
            }
            return this;
        }

        /// <summary>
        /// Belgeye ait üstveri alanlarını barındıran Ustveri bileşenini paket içerisine  ekler.
        /// </summary>
        /// <param name="ustveri">Ustveri bileşenidir.</param>
        public IPaketV2XOlusturUstveri UstveriAta(Ustveri ustveri)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!ustveri.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.Ustveri) + " bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
                Ustveri = ustveri;

            return this;
        }

        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eke ait dosya bileşenini ekler. 
        /// </summary>
        /// <param name="ekDosya">Eke ait dosya bileşeni verileridir.</param>
        public IPaketV2XOlusturEkDosya EkDosyaIle(EkDosya ekDosya)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!ekDosya.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(EkDosya) + " bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
            {
                if (_package.EkDosyaExists(ekDosya.Ek.Id))
                    _dogrulamaHatalari.Add(new DogrulamaHatasi
                    {
                        Hata = "Paket içerisinde bu " + nameof(EkDosya) + " bileşeni bulunmaktadır. EkId: " + ekDosya.Ek.Id.Deger,
                        HataTuru = DogrulamaHataTuru.Kritik
                    });
                else
                    _package.AddEkDosya(ekDosya);
            }

            return this;
        }

        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eklere ait dosya bileşenlerini ekler. 
        /// </summary>
        /// <param name="ekDosyalar">Eklere ait dosya bileşeni verileri listesidir.</param>
        public IPaketV2XOlusturEkDosyalar EkDosyalarIle(List<EkDosya> ekDosyalar)
        {
            if (ekDosyalar != null && ekDosyalar.Count > 0 && !KritikHataExists())
            {
                ekDosyalar.ForEach((ekDosya) =>
                {
                    var hatalar = new List<DogrulamaHatasi>();
                    if (!ekDosya.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                        _dogrulamaHatalari.Add(new DogrulamaHatasi
                        {
                            AltDogrulamaHatalari = hatalar,
                            Hata = nameof(EkDosya) + " bileşeni doğrulanamamıştır.",
                            HataTuru = hatalar.GetDogrulamaHataTuru().Value
                        });

                    if (!KritikHataExists())
                    {
                        if (_package.EkDosyaExists(ekDosya.Ek.Id))
                            _dogrulamaHatalari.Add(new DogrulamaHatasi
                            {
                                Hata = "Paket içerisinde bu " + nameof(EkDosya) + " bileşeni bulunmaktadır. EkId: " + ekDosya.Ek.Id.Deger,
                                HataTuru = DogrulamaHataTuru.Kritik
                            });
                        else
                            _package.AddEkDosya(ekDosya);
                    }
                });
            }

            return this;
        }

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        public IPaketV2XOlusturPaketBilgi PaketDurumuIle(string durum)
        {
            PaketDurumuInternal(durum);
            return this;
        }

        /// <summary>
        /// Paketin oluşturulduğu tarihi ekler.
        /// </summary>
        /// <param name="olusturulmaTarihi">Oluşturulma tarihidir.</param>
        public IPaketV2XOlusturPaketBilgi PaketOlusturulmaTarihiIle(DateTime? olusturulmaTarihi)
        {
            if (!KritikHataExists())
                _package.SetPaketOlusturulmaTarihi(olusturulmaTarihi);
            return this;
        }

        /// <summary>
        /// Paket açıklamasını ekler.
        /// </summary>
        /// <param name="aciklama">Açıklama değeridir.</param>
        public IPaketV2XOlusturPaketBilgi PaketAciklamasiIle(string aciklama)
        {
            if (!KritikHataExists())
                _package.SetPaketAciklamasi(aciklama);
            return this;
        }

        /// <summary>
        /// Genellikle indeksleme ve arama için kullanılan anahtar kelimeleri ekler.
        /// </summary>
        /// <param name="anahtarKelimeler">Ayraçlar kullanılarak ayrılmış bir cümledir.</param>
        public IPaketV2XOlusturPaketBilgi PaketAnahtarKelimeleriIle(string anahtarKelimeler)
        {
            if (!KritikHataExists())
                _package.SetPaketAnahtarKelimeleri(anahtarKelimeler);
            return this;
        }

        /// <summary>
        /// Paket dilini ekler.
        /// </summary>
        /// <param name="dil">Dil değeridir.</param>
        public IPaketV2XOlusturPaketBilgi PaketDiliIle(string dil)
        {
            if (!KritikHataExists())
                _package.SetPaketDili(dil);
            return this;
        }

        /// <summary>
        /// Paketi olduğu belgenin son yazdırılma tarihini ekler.
        /// </summary>
        /// <param name="sonYazdirilmaTarihi">Son yazdırılma tarihidir.</param>
        public IPaketV2XOlusturPaketBilgi PaketSonYazdirilmaTarihiIle(DateTime? sonYazdirilmaTarihi)
        {
            if (!KritikHataExists())
                _package.SetPaketSonYazdirilmaTarihi(sonYazdirilmaTarihi);
            return this;
        }

        /// <summary>
        /// Paket başlığı ekler.
        /// </summary>
        /// <param name="baslik">Başlık değeridir.</param>
        public IPaketV2XOlusturPaketBilgi PaketBasligiIle(string baslik)
        {
            if (!KritikHataExists())
                _package.SetPaketBasligi(baslik);
            return this;
        }

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        /// <param name="parafOzetiOlusturulsun">ParafOzeti bileşenin oluşturulup oluşturulmayacağını belirtir.
        /// Varsayılan değeri \"false\" dır.
        /// </param>
        public IPaketV2XOlusturBilesen BilesenleriOlustur(bool parafOzetiOlusturulsun)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!Ustveri.IlgileriDogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "İlgiler doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });
                hatalar = new List<DogrulamaHatasi>();
            }

            if (!Ustveri.EkleriDogrula(_package, PaketVersiyonTuru.Versiyon2X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "Ekler doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
            {
                _package.GenerateCore(Ustveri, PaketVersiyonTuru.Versiyon2X);
                _package.GenerateUstveri(Ustveri, PaketVersiyonTuru.Versiyon2X);
                if (parafOzetiOlusturulsun)
                    ParafOzetiInternal("oluşturulamaz");
            }
            return this;
        }

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        /// <param name="parafOzetiOlusturulsun">ParafOzeti bileşenin oluşturulup oluşturulmayacağını belirtir.
        /// Varsayılan değeri \"false\" dır.
        /// </param>
        IPaketV2XOlusturBilesen IPaketV2XOlusturPaketBilgi.BilesenleriOlustur(bool parafOzetiOlusturulsun)
        {
            return BilesenleriOlustur(parafOzetiOlusturulsun);
        }

        /// <summary>
        /// Paket içerisine ParafOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak ParafOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        public IPaketV2XOlusturParaf ParafImzaEkle(Func<Stream, byte[]> imzaFunction)
        {
            if (!KritikHataExists())
            {
                var imzaByteArray = imzaFunction.Invoke(ParafOzetiAl());

                ParafImzaEkleInternal(imzaByteArray);
                PaketOzetiInternal("oluşturulamaz");
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        public IPaketV2XOlusturImza ImzaEkle(Func<Stream, byte[]> imzaFunction)
        {
            PaketOzetiInternal("oluşturulamaz");

            if (!KritikHataExists())
            {
                var imzaByteArray = imzaFunction.Invoke(PaketOzetiAl());

                ImzaEkleInternal(imzaByteArray);
            }
            return this;
        }

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        public IPaketV2XOlusturBilesen2 BilesenleriOlustur()
        {
            if (!KritikHataExists())
            {
                _package.GenerateNihaiUstveri(NihaiUstveri);
                NihaiOzetInternal("oluşturulamaz");
            }
            return this;
        }

        /// <summary>
        /// Oluşturulan paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        public IPaketV2XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction)
        {
            dogrulamaAction.Invoke(KritikHataExists(), _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Belgeye ait nihai üstveri alanlarını barındıran NihaiUstveri bileşenini paket içerisine  ekler.
        /// </summary>
        /// <param name="nihaiUstveri">Ustveri bileşenidir.</param>
        public IPaketV2XOlusturNihaiUstveri NihaiUstveriAta(NihaiUstveri nihaiUstveri)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!nihaiUstveri.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.NihaiUstveri) + " bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
                NihaiUstveri = nihaiUstveri;

            return this;
        }

        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="muhurFunction">
        /// Mühür ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak NihaiOzet bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek mühür değeridir.
        /// </param>
        /// <remarks>Eklenecek mühür "Ayrık olmayan CAdES-XL" türünde imza olmalıdır.</remarks>
        public IPaketV2XOlusturMuhur MuhurEkle(Func<Stream, byte[]> muhurFunction)
        {
            if (!KritikHataExists())
            {
                var muhurByteArray = muhurFunction.Invoke(NihaiOzetAl());
                MuhurEkleInternal(muhurByteArray);
            }
            return this;
        }

        /// <summary>
        /// Var olan bir paketi okumak için kullanılır.
        /// </summary>
        /// <param name="paketStream">Pakete ilişkin STREAM objesidir.</param>
        public static IPaketV2XOku Oku(Stream paketStream) =>
            new PaketV2X(paketStream, PaketModuTuru.Oku);

        /// <summary>
        /// Var olan bir paketi okumak için kullanılır.
        /// </summary>
        /// <param name="paketDosyaYolu">Pakete ilişkin dosya yoludur.</param>
        public static IPaketV2XOku Oku(string paketDosyaYolu) =>
            new PaketV2X(File.Open(paketDosyaYolu, FileMode.Open, FileAccess.Read), PaketModuTuru.Oku);

        /// <summary>
        /// Paket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// IPaketV2XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        public IPaketV2XOkuBilesenAl BilesenleriAl(Action<bool, IPaketV2XOkuBilesen, List<DogrulamaHatasi>> bilesenAction)
        {
            bilesenAction.Invoke(!KritikHataExists(), this, _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Var olan bir paketi güncellemek için kullanılır.
        /// </summary>
        /// <param name="paketStream">Pakete ilişkin STREAM objesidir.</param>
        public static IPaketV2XGuncelle Guncelle(Stream paketStream) =>
            new PaketV2X(paketStream, PaketModuTuru.Guncelle);

        /// <summary>
        /// Var olan bir paketi güncellemek için kullanılır.
        /// </summary>
        /// <param name="paketDosyaYolu">Pakete ilişkin dosya yoludur.</param>
        public static IPaketV2XGuncelle Guncelle(string paketDosyaYolu) =>
            new PaketV2X(File.Open(paketDosyaYolu, FileMode.Open, FileAccess.ReadWrite), PaketModuTuru.Guncelle);

        /// <summary>
        /// Paketin son güncelleme tarih bilgisinin ekler.
        /// </summary>
        /// <param name="guncellemeTarihi">Son güncelleme tarih bilgisidir.</param>
        public IPaketV2XGuncellePaketBilgi PaketGuncellemeTarihiIle(DateTime? guncellemeTarihi)
        {
            if (!KritikHataExists())
                _package.SetPaketGuncellemeTarihi(guncellemeTarihi);
            return this;
        }

        /// <summary>
        /// Paketi son güncelleyen taraf bilgisinin ekler.
        /// </summary>
        /// <param name="sonGuncelleyen">Son güncelleyen taraf bilgisidir.</param>
        public IPaketV2XGuncellePaketBilgi PaketSonGuncelleyenIle(string sonGuncelleyen)
        {
            if (!KritikHataExists())
                _package.SetPaketSonGuncelleyen(sonGuncelleyen);
            return this;
        }

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV2XGuncellePaketBilgi IPaketV2XGuncelle.PaketDurumuIle(string durum)
        {
            PaketDurumuInternal(durum);
            return this;
        }

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV2XGuncellePaketBilgi IPaketV2XGuncellePaketBilgi.PaketDurumuIle(string durum)
        {
            PaketDurumuInternal(durum);
            return this;
        }

        /// <summary>
        /// Paket içerisine ParafOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak ParafOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XGuncelleParaf IPaketV2XGuncelle.ParafImzaEkle(Func<Stream, byte[]> imzaFunction)
        {
            if (!KritikHataExists())
            {
                var imzaByteArray = imzaFunction.Invoke(ParafOzetiAl());

                ParafImzaEkleInternal(imzaByteArray);
                PaketOzetiInternal("güncellenemez");
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine ParafOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak ParafOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XGuncelleParaf IPaketV2XGuncellePaketBilgi.ParafImzaEkle(Func<Stream, byte[]> imzaFunction)
        {
            if (!KritikHataExists())
            {
                var imzaByteArray = imzaFunction.Invoke(ParafOzetiAl());

                ParafImzaEkleInternal(imzaByteArray);
                PaketOzetiInternal("güncellenemez");
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine ParafOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek ParafOzeti bileşenine ait imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        public IPaketV2XGuncelleParaf ParafImzaEkle(byte[] imza)
        {
            if (!KritikHataExists())
            {
                ParafImzaEkleInternal(imza);
                PaketOzetiInternal("güncellenemez");
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XGuncelleImza IPaketV2XGuncelle.ImzaEkle(Func<Stream, byte[]> imzaFunction)
        {
            PaketOzetiInternal("güncellenemez");

            if (!KritikHataExists())
            {
                var imzaByteArray = imzaFunction.Invoke(PaketOzetiAl());

                ImzaEkleInternal(imzaByteArray);
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XGuncelleImza IPaketV2XGuncelleParaf.ImzaEkle(Func<Stream, byte[]> imzaFunction)
        {
            PaketOzetiInternal("güncellenemez");

            if (!KritikHataExists())
            {
                var imzaByteArray = imzaFunction.Invoke(PaketOzetiAl());

                ImzaEkleInternal(imzaByteArray);
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XGuncelleImza IPaketV2XGuncellePaketBilgi.ImzaEkle(Func<Stream, byte[]> imzaFunction)
        {
            PaketOzetiInternal("güncellenemez");

            if (!KritikHataExists())
            {
                var imzaByteArray = imzaFunction.Invoke(PaketOzetiAl());

                ImzaEkleInternal(imzaByteArray);
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek PaketOzeti bileşenine ait imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        public IPaketV2XGuncelleImza ImzaEkle(byte[] imza)
        {
            PaketOzetiInternal("güncellenemez");

            if (!KritikHataExists())
                ImzaEkleInternal(imza);
            return this;
        }

        /// <summary>
        /// Belgeye ait nihai üstveri alanlarını barındıran NihaiUstveri bileşenini paket içerisine  ekler.
        /// </summary>
        /// <param name="nihaiUstveri">NihaiUstveri bileşenidir.</param>
        IPaketV2XGuncelleNihaiUstveri IPaketV2XGuncelleImza.NihaiUstveriAta(NihaiUstveri nihaiUstveri)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!nihaiUstveri.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.NihaiUstveri) + " bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            if (!KritikHataExists())
                NihaiUstveri = nihaiUstveri;

            return this;
        }

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        IPaketV2XGuncelleBilesen IPaketV2XGuncelleNihaiUstveri.BilesenleriOlustur()
        {
            if (!KritikHataExists())
            {
                _package.GenerateCore(Ustveri, PaketVersiyonTuru.Versiyon2X);
                _package.GenerateNihaiUstveri(NihaiUstveri);
                NihaiOzetInternal("güncellenemez");
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="muhurFunction">
        /// Mühür ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak NihaiOzet bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek mühür değeridir.
        /// </param>
        /// <remarks>Eklenecek mühür "Ayrık olmayan CAdES-XL" türünde imza olmalıdır.</remarks>
        IPaketV2XGuncelleMuhur IPaketV2XGuncelleBilesen.MuhurEkle(Func<Stream, byte[]> muhurFunction)
        {
            if (!KritikHataExists())
            {
                var muhurByteArray = muhurFunction.Invoke(NihaiOzetAl());
                MuhurEkleInternal(muhurByteArray);
            }
            return this;
        }

        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="muhur">Paket içerisine eklenecek NihaiOzet bileşenine ait imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        public IPaketV2XGuncelleImza MuhurEkle(byte[] muhur)
        {
            if (!KritikHataExists())
                MuhurEkleInternal(muhur);
            return this;
        }

        /// <summary>
        /// Güncellenen paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV2XGuncelleDogrula IPaketV2XGuncelleBilesen.Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction)
        {
            dogrulamaAction.Invoke(KritikHataExists(), _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Güncellenen paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV2XGuncelleDogrula IPaketV2XGuncelleMuhur.Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction)
        {
            dogrulamaAction.Invoke(KritikHataExists(), _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Güncellenen paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV2XGuncelleDogrula IPaketV2XGuncelleParaf.Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction)
        {
            dogrulamaAction.Invoke(KritikHataExists(), _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Paket içerisinden ek çıkarılmak için kullanılır.
        /// </summary>
        /// <param name="ekId">Çıkarılacak eke ait Id bilgisidir.</param>
        public IPaketV2XGuncelleEkDosya EkDosyaCikar(IdTip ekId)
        {
            _package.DeleteEkDosya(ekId);
            return this;
        }

        /// <summary>
        /// Ek çıkarma işlemi yapılan paketin doğrulanmasını sağlar.
        /// </summary>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        public IPaketV2XGuncelleDogrula Dogrula(TanimlayiciTip dagitimTanimlayici, Action<bool, List<DogrulamaHatasi>> dogrulamaAction)
        {
            var hatalar = new List<DogrulamaHatasi>();
            if (!Ustveri.IlgileriDogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "İlgiler doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });
                hatalar = new List<DogrulamaHatasi>();
            }

            if (!Ustveri.EkleriDogrula(_package, PaketVersiyonTuru.Versiyon2X, ref hatalar))
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "Ekler doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });
                hatalar = new List<DogrulamaHatasi>();
            }

            if (!PaketOzeti.PaketOzetiDogrula(_package, PaketVersiyonTuru.Versiyon2X, ref hatalar, Ustveri, dagitimTanimlayici))
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "PaketOzeti bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });
                hatalar = new List<DogrulamaHatasi>();
            }

            if (!NihaiOzet.NihaiOzetDogrula(_package, PaketVersiyonTuru.Versiyon2X, ref hatalar, Ustveri))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = "NihaiOzet bileşeni doğrulanamamıştır.",
                    HataTuru = hatalar.GetDogrulamaHataTuru().Value
                });

            dogrulamaAction.Invoke(KritikHataExists(), _dogrulamaHatalari);
            return this;
        }

        /// <summary>
        /// Tek bir özet değeri ile paket içerisindeki ilgili bileşenin özet değerlerini doğrular.
        /// </summary>
        /// <param name="referans">Doğrulanacak özet değerini barındıran Referans nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        public bool ReferansDogrula(Referans referans, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null)
        {
            return referans.ReferansDogrula(_package, PaketVersiyonTuru.Versiyon2X, ref sonuclar, Ustveri, dagitimTanimlayici);
        }

        /// <summary>
        /// Ustveri bileşeninde ek olarak belirtilmiş ilgiye ilişkin ekin paket içerisinde bulunup bulunmadığını doğrular.
        /// </summary>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        public void IlgileriDogrula(ref List<DogrulamaHatasi> sonuclar)
        {
            Ustveri.IlgileriDogrula(PaketVersiyonTuru.Versiyon2X, ref sonuclar);
        }

        /// <summary>
        /// Ustveri bileşeninde DED türünde ek olarak belirtilmiş ekin paket içerisinde bulunup bulunmadığını doğrular.
        /// </summary>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        public void EkleriDogrula(ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null)
        {
            Ustveri.EkleriDogrula(_package, PaketVersiyonTuru.Versiyon2X, ref sonuclar, dagitimTanimlayici);
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

        private void ParafOzetiInternal(string mesaj)
        {
            if (!UstYaziVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.UstYazi) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.ParafOzeti) + " " + mesaj + "."
                });
            else
            {
                var partUstYaziUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).First().TargetUri);
                byte[] ozetUstYazi = _ozetAlgoritma.CalculateHash(UstYaziAl());
                byte[] ozetUstYaziSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(UstYaziAl());
                ParafOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetUstYazi,
                                        partUstYaziUri,
                                        ozetUstYaziSha512);
            }

            if (!UstveriVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.Ustveri) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.ParafOzeti) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetUstveri = _ozetAlgoritma.CalculateHash(UstveriAl());
                byte[] ozetUstveriSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(UstveriAl());
                ParafOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetUstveri,
                                        Constants.URI_USTVERI,
                                        ozetUstveriSha512);

                if (Ustveri.Ekler != null && Ustveri.Ekler.Count > 0)
                {
                    foreach (var ek in Ustveri.Ekler)
                    {
                        if (ek.Tur == EkTuru.DED && ek.ImzaliMi)
                        {
                            var relationShip = _package.GetRelationshipsByType(Constants.RELATION_TYPE_EK).SingleOrDefault(r => string.Compare(r.Id, Constants.ID_ROOT_EK(ek.Id.Deger), true) == 0);
                            var ekStream = _package.GetPart(relationShip.TargetUri).GetStream();
                            byte[] ekOzeti = _ozetAlgoritma.CalculateHash(ekStream);
                            ekStream.Position = 0;
                            byte[] ekOzetiSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(ekStream);
                            ekStream.Position = 0;
                            ParafOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                                    _ozetAlgoritma,
                                                    ekOzeti,
                                                    relationShip.TargetUri,
                                                    ekOzetiSha512);
                        }
                    }
                }

                ParafOzeti.Id = Ustveri.BelgeId;
            }

            var hatalar = new List<DogrulamaHatasi>();
            if (!ParafOzeti.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.ParafOzeti) + " bileşeni doğrulanamamıştır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                _package.DeleteParafOzeti();
                _package.GenerateParafOzeti(ParafOzeti);
            }
        }

        private void PaketOzetiInternal(string mesaj)
        {
            if (!UstYaziVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.UstYazi) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.PaketOzeti) + " " + mesaj + "."
                });
            else
            {
                var partUstYaziUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).First().TargetUri);
                byte[] ozetUstYazi = _ozetAlgoritma.CalculateHash(UstYaziAl());
                byte[] ozetUstYaziSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(UstYaziAl());
                PaketOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetUstYazi,
                                        partUstYaziUri,
                                        ozetUstYaziSha512);
            }

            if (!UstveriVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.Ustveri) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.PaketOzeti) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetUstveri = _ozetAlgoritma.CalculateHash(UstveriAl());
                byte[] ozetUstveriSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(UstveriAl());
                PaketOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetUstveri,
                                        Constants.URI_USTVERI,
                                        ozetUstveriSha512);

                if (Ustveri.Ekler != null && Ustveri.Ekler.Count > 0)
                {
                    foreach (var ek in Ustveri.Ekler)
                    {
                        if (ek.Tur == EkTuru.DED && ek.ImzaliMi)
                        {
                            var relationShip = _package.GetRelationshipsByType(Constants.RELATION_TYPE_EK).SingleOrDefault(r => string.Compare(r.Id, Constants.ID_ROOT_EK(ek.Id.Deger), true) == 0);
                            var ekStream = _package.GetPart(relationShip.TargetUri).GetStream();
                            byte[] ekOzeti = _ozetAlgoritma.CalculateHash(ekStream);
                            ekStream.Position = 0;
                            byte[] ekOzetiSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(ekStream);
                            ekStream.Position = 0;
                            PaketOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                                    _ozetAlgoritma,
                                                    ekOzeti,
                                                    relationShip.TargetUri,
                                                    ekOzetiSha512);
                        }
                    }
                }

                PaketOzeti.Id = Ustveri.BelgeId;
            }

            if (ParafOzetiVarMi())
            {
                byte[] ozetParafOzeti = _ozetAlgoritma.CalculateHash(ParafOzetiAl());
                byte[] ozetParafOzetiSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(ParafOzetiAl());
                PaketOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetParafOzeti,
                                        Constants.URI_PARAFOZETI,
                                        ozetParafOzetiSha512);
            }

            if (ParafImzaVarMi())
            {
                byte[] ozetParafImza = _ozetAlgoritma.CalculateHash(ParafImzaAl());
                byte[] ozetParafImzaSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(ParafImzaAl());
                PaketOzeti.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetParafImza,
                                        Constants.URI_PARAFIMZA,
                                        ozetParafImzaSha512);
            }

            var hatalar = new List<DogrulamaHatasi>();
            if (!PaketOzeti.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.PaketOzeti) + " bileşeni doğrulanamamıştır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                _package.DeletePaketOzeti();
                _package.GeneratePaketOzeti(PaketOzeti, PaketVersiyonTuru.Versiyon2X);
            }
        }

        private void NihaiOzetInternal(string mesaj)
        {
            if (!UstYaziVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.UstYazi) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            else
            {
                var partUstYaziUri = PackUriHelper.CreatePartUri(_package.GetRelationshipsByType(Constants.RELATION_TYPE_USTYAZI).First().TargetUri);
                byte[] ozetUstYazi = _ozetAlgoritma.CalculateHash(UstYaziAl());
                byte[] ozetUstYaziSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(UstYaziAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetUstYazi,
                                        partUstYaziUri,
                                        ozetUstYaziSha512);
            }

            if (!UstveriVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.Ustveri) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetUstveri = _ozetAlgoritma.CalculateHash(UstveriAl());
                byte[] ozetUstveriSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(UstveriAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetUstveri,
                                        Constants.URI_USTVERI,
                                        ozetUstveriSha512);

                if (Ustveri.Ekler != null && Ustveri.Ekler.Count > 0)
                {
                    foreach (var ek in Ustveri.Ekler)
                    {
                        if (ek.Tur == EkTuru.DED && ek.ImzaliMi)
                        {
                            var relationShip = _package.GetRelationshipsByType(Constants.RELATION_TYPE_EK).SingleOrDefault(r => string.Compare(r.Id, Constants.ID_ROOT_EK(ek.Id.Deger), true) == 0);
                            var ekStream = _package.GetPart(relationShip.TargetUri).GetStream();
                            byte[] ekOzeti = _ozetAlgoritma.CalculateHash(ekStream);
                            ekStream.Position = 0;
                            byte[] ekOzetiSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(ekStream);
                            ekStream.Position = 0;
                            NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                                    _ozetAlgoritma,
                                                    ekOzeti,
                                                    relationShip.TargetUri,
                                                    ekOzetiSha512);
                        }
                    }
                }

                NihaiOzet.Id = Ustveri.BelgeId;
            }

            if (ParafOzetiVarMi())
            {
                byte[] ozetParafOzeti = _ozetAlgoritma.CalculateHash(ParafOzetiAl());
                byte[] ozetParafOzetiSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(ParafOzetiAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetParafOzeti,
                                        Constants.URI_PARAFOZETI,
                                        ozetParafOzetiSha512);
            }

            if (ParafImzaVarMi())
            {
                byte[] ozetParafImza = _ozetAlgoritma.CalculateHash(ParafImzaAl());
                byte[] ozetParafImzaSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(ParafImzaAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetParafImza,
                                        Constants.URI_PARAFIMZA,
                                        ozetParafImzaSha512);
            }


            if (!PaketOzetiVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.PaketOzeti) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetPaketOzeti = _ozetAlgoritma.CalculateHash(PaketOzetiAl());
                byte[] ozetPaketOzetiSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(PaketOzetiAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetPaketOzeti,
                                        Constants.URI_PAKETOZETI,
                                        ozetPaketOzetiSha512);
            }

            if (!ImzaVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = "PaketOzeti bileşenine ait İmza değeri bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetImza = _ozetAlgoritma.CalculateHash(ImzaAl());
                byte[] ozetImzaSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(ImzaAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetImza,
                                        Constants.URI_IMZA,
                                        ozetImzaSha512);
            }

            if (!NihaiUstveriVarMi())
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = nameof(Classes.NihaiUstveri) + " bileşeni bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            else
            {
                byte[] ozetNihaiUstveri = _ozetAlgoritma.CalculateHash(NihaiUstveriAl());
                byte[] ozetNihaiUstveriSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(NihaiUstveriAl());
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetNihaiUstveri,
                                        Constants.URI_NIHAIUSTVERI,
                                        ozetNihaiUstveriSha512);
            }

            var coreRelations = _package.GetRelationshipsByType(Constants.RELATION_TYPE_CORE);
            if (coreRelations == null || !coreRelations.Any())
            {
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    HataTuru = DogrulamaHataTuru.Kritik,
                    Hata = "Core bileşeni bulunmadığı durumlarda " + nameof(Classes.NihaiOzet) + " " + mesaj + "."
                });
            }
            else
            {
                var corePart = _package.GetPart(coreRelations.First().TargetUri);
                var stream = corePart.GetStream();
                stream.Position = 0;
                byte[] ozetCore = _ozetAlgoritma.CalculateHash(stream);
                stream.Position = 0;
                byte[] ozetCoreSha512 = OzetAlgoritmaTuru.SHA512.CalculateHash(stream);
                stream.Position = 0;
                NihaiOzet.ReferansEkle(PaketVersiyonTuru.Versiyon2X,
                                        _ozetAlgoritma,
                                        ozetCore,
                                        coreRelations.First().TargetUri);
            }

            var hatalar = new List<DogrulamaHatasi>();
            if (!NihaiOzet.Dogrula(PaketVersiyonTuru.Versiyon2X, ref hatalar))
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    AltDogrulamaHatalari = hatalar,
                    Hata = nameof(Classes.NihaiOzet) + " bileşeni doğrulanamamıştır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                _package.DeleteNihaiOzet();
                _package.GenerateNihaiOzet(NihaiOzet, PaketVersiyonTuru.Versiyon2X);
            }
        }

        private void ParafImzaEkleInternal(byte[] imzaByteArray)
        {
            if (imzaByteArray == null && imzaByteArray.Length == 0)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = "ParafOzeti bileşenine ait elektronik imza değeri boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                _package.DeleteParafImza();
                _package.AddParafImza(imzaByteArray);
            }
        }

        private void ImzaEkleInternal(byte[] imzaByteArray)
        {
            if (imzaByteArray == null && imzaByteArray.Length == 0)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = "PaketOzeti bileşenine ait elektronik imza değeri boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                _package.DeleteImza();
                _package.AddImza(imzaByteArray);
            }
        }

        private void MuhurEkleInternal(byte[] muhurByteArray)
        {
            if (muhurByteArray == null || muhurByteArray.Length == 0)
                _dogrulamaHatalari.Add(new DogrulamaHatasi
                {
                    Hata = "Elektronik mühür bileşeni değeri boş olmamalıdır.",
                    HataTuru = DogrulamaHataTuru.Kritik
                });
            else
            {
                _package.DeleteMuhur(PaketVersiyonTuru.Versiyon2X);
                _package.AddMuhur(muhurByteArray, PaketVersiyonTuru.Versiyon2X);
            }
        }

        private bool KritikHataExists() => _dogrulamaHatalari.Any(p => p.HataTuru == DogrulamaHataTuru.Kritik);

        private void PaketDurumuInternal(string durum)
        {
            if (!KritikHataExists())
                _package.SetPaketDurumu(durum);
        }
    }
}
