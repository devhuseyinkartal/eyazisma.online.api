using eyazisma.online.api.Classes;
using eyazisma.online.api.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace eyazisma.online.api.Interfaces
{
    public interface IPaket : IPaketOlustur,
                              IPaketOku,
                              IPaketOkuAction,
                              IPaketGuncelle,
                              IPaketGuncelleAction
    {

    }

    public interface IPaketOlustur
    {
        IPaketV1XOlustur Versiyon1X();
        IPaketV2XOlustur Versiyon2X();
    }

    public interface IPaketOku
    {
        /// <summary>
        /// Paket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// IPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        IPaketOkuAction Versiyon1XIse(Action<bool, IPaketV1XOkuBilesen, List<DogrulamaHatasi>> action);
    }

    public interface IPaketOkuAction
    {
        /// <summary>
        /// Paket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// IPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        void Versiyon2XIse(Action<bool, IPaketV2XOkuBilesen, List<DogrulamaHatasi>> action);
    }

    public interface IPaketGuncelle
    {
        IPaketGuncelleAction Versiyon1XIse(Action<IPaketV1XGuncelle> action);
    }

    public interface IPaketGuncelleAction
    {
        void Versiyon2XIse(Action<bool, IPaketV2XOkuBilesen, List<DogrulamaHatasi>> action);
    }

    public interface IPaketV1X : IPaketV1XOlustur,
                             IPaketV1XOlusturOzetAlgoritma,
                             IPaketV1XOlusturUstYazi,
                             IPaketV1XOlusturUstveri,
                             IPaketV1XOlusturBelgeHedef,
                             IPaketV1XOlusturBelgeImza,
                             IPaketV1XOlusturEkDosya,
                             IPaketV1XOlusturEkDosyalar,
                             IPaketV1XOlusturPaketBilgi,
                             IPaketV1XOlusturBilesen,
                             IPaketV1XOlusturImza,
                             IPaketV1XOlusturMuhur,
                             IPaketV1XOlusturDogrula,
                             IPaketV1XOku,
                             IPaketV1XOkuBilesenAl,
                             IPaketV1XOkuBilesen,
                             IPaketV1XGuncelle,
                             IPaketV1XGuncelleBelgeImza,
                             IPaketV1XGuncelleImza,
                             IPaketV1XGuncelleMuhur,
                             IPaketV1XGuncelleEkDosya,
                             IPaketV1XGuncellePaketBilgi,
                             IPaketV1XGuncelleDogrula
    {
    }

    #region Oku

    public interface IPaketV1XOku
    {
        /// <summary>
        /// Paket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// IPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        IPaketV1XOkuBilesenAl BilesenleriAl(Action<bool, IPaketV1XOkuBilesen, List<DogrulamaHatasi>> bilesenAction);
    }

    public interface IPaketV1XOkuBilesenAl
    {
        void Kapat();
    }

    public interface IPaketV1XOkuBilesen
    {
        /// <summary>
        /// Belgeye ait üstveri alanlarını barıdıran nesneye ulaşılır.
        /// </summary>
        Ustveri Ustveri { get; }

        /// <summary>
        /// Paket içerisindeki Ustveri bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool UstveriVarMi();

        /// <summary>
        /// Paket içerisindeki Ustveri bileşenini STREAM olarak verir.
        /// </summary>
        Stream UstveriAl();

        /// <summary>
        /// Paket içerisindeki üst yazı bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool UstYaziVarMi();

        /// <summary>
        /// Paket içerisindeki üst yazı bileşenini STREAM olarak verir.
        /// </summary>
        Stream UstYaziAl();

        /// <summary>
        /// Belgeye atılmış olan imzalara ilişkin üstveri bilgilerini içeren nesneye ulaşılır.
        /// </summary>
        BelgeImza BelgeImza { get; }

        /// <summary>
        /// Paket içerisindeki BelgeImza bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool BelgeImzaVarMi();

        /// <summary>
        /// Paket içerisindeki BelgeImza bileşenini STREAM olarak verir.
        /// </summary>
        Stream BelgeImzaAl();

        /// <summary>
        /// Paketin iletileceği hedefleri barındıran nesneye ulaşılır.
        /// </summary>
        BelgeHedef BelgeHedef { get; }

        /// <summary>
        /// Paket içerisindeki BelgeHedef bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool BelgeHedefVarMi();

        /// <summary>
        /// Paket içerisindeki BelgeHedef bileşenini STREAM olarak verir.
        /// </summary>
        Stream BelgeHedefAl();

        /// <summary>
        /// Id'si verilen eke ait dosyanın paket içerisinde olup olmadığını gösterir.
        /// </summary>
        bool EkDosyaVarMi(IdTip ekId);

        /// <summary>
        /// Id'si verilen eke ait dosyayı STREAM olarak verir.
        /// </summary>
        /// <param name="ekId">Ek id değeridir.</param>
        Stream EkDosyaAl(IdTip ekId);

        /// <summary>
        /// Paket içerisinde imzalanan bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        PaketOzeti PaketOzeti { get; }

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool PaketOzetiVarMi();

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşenini STREAM olarak verir.
        /// </summary>
        Stream PaketOzetiAl();

        /// <summary>
        /// Verilen PaketOzeti nesnesindeki özet değerleri ile paket içerisindeki bileşenlerin özet değerlerini doğrular.
        /// </summary>
        /// <param name="ozet">Doğrulanacak özet değerlerini barındıran PaketOzeti nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        bool PaketOzetiDogrula(PaketOzeti ozet, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null);

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin imzalı değeri olup olmadığını gösterir.
        /// </summary>
        bool ImzaVarMi();

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin imzalı (Ayrık olmayan CAdES-XL) değerini STREAM olarak verir.
        /// </summary>
        Stream ImzaAl();

        /// <summary>
        /// Paket içerisinde mühürlenen bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        NihaiOzet NihaiOzet { get; }

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool NihaiOzetVarMi();

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşenini STREAM olarak verir.
        /// </summary>
        Stream NihaiOzetAl();

        /// <summary>
        /// Verilen NihaiOzet nesnesindeki özet değerleri ile paket içerisindeki bileşenlerin özet değerlerini doğrular.
        /// </summary>
        /// <param name="ozet">Doğrulanacak özet değerlerini barındıran NihaiOzet nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        bool NihaiOzetDogrula(NihaiOzet ozet, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null);

        /// <summary>
        /// Paket içerisindeki mühür bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool MuhurVarMi();

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşeninin imzalı (Ayrık olmayan CAdES-XL) değerini STREAM olarak verir.
        /// </summary>
        Stream MuhurAl();

        bool ReferansDogrula(Referans referans, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null);

        void IlgileriDogrula(ref List<DogrulamaHatasi> sonuclar);

        void EkleriDogrula(ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null);
    }

    #endregion

    #region Olustur
    public interface IPaketV1XOlustur
    {
        /// <summary>
        /// Paket bileşenlerinin içerisine eklenecek özetlerin oluşturulmasında kullanılacak algoritmayı belirtir.
        /// </summary>
        /// <param name="ozetAlgoritma"></param>
        IPaketV1XOlusturOzetAlgoritma OzetAlgoritmaIle(OzetAlgoritmaTuru ozetAlgoritma);
        /// <summary>
        /// Paket içerisine üst yazı bileşeni ekler.
        /// </summary>
        /// <param name="ustYazi">Üst yazı bişeni verileridir.</param>
        IPaketV1XOlusturUstYazi UstYaziAta(UstYazi ustYazi);
    }

    public interface IPaketV1XOlusturOzetAlgoritma
    {
        /// <summary>
        /// Paket içerisine üst yazı bileşeni ekler.
        /// </summary>
        /// <param name="ustYazi">Üst yazı bişeni verileridir.</param>
        IPaketV1XOlusturUstYazi UstYaziAta(UstYazi ustYazi);
    }

    public interface IPaketV1XOlusturUstYazi
    {
        /// <summary>
        /// Belgeye ait üstveri alanlarını barındıran Ustveri bileşenini paket içerisine  ekler.
        /// </summary>
        /// <param name="ustveri">Ustveri bileşenidir.</param>
        IPaketV1XOlusturUstveri UstveriAta(Ustveri ustveri);
    }

    public interface IPaketV1XOlusturUstveri
    {
        /// <summary>
        ///  Paketin iletileceği hedefleri barındıran BelgeHedef bileşenini paket içerisine ekler.
        /// </summary>
        /// <param name="belgeHedef">BelgeHedef bileşenidir.</param>
        IPaketV1XOlusturBelgeHedef BelgeHedefAta(BelgeHedef belgeHedef);
    }

    public interface IPaketV1XOlusturBelgeHedef
    {
        /// <summary>
        /// Belgeye atılmış olan imzalara ilişkin üstveri bilgilerini içeren BelgeImza bileşenini paket içerisine ekler.
        /// </summary>
        /// <param name="belgeImza">BelgeImza bileşenidir.</param>
        IPaketV1XOlusturBelgeImza BelgeImzaIle(BelgeImza belgeImza);

        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eke ait dosya bileşenini ekler. 
        /// </summary>
        /// <param name="ekDosya">Eke ait dosya bileşeni verileridir.</param>
        IPaketV1XOlusturEkDosya EkDosyaIle(EkDosya ekDosya);

        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eklere ait dosya bileşenlerini ekler. 
        /// </summary>
        /// <param name="ekDosyalar">Eklere ait dosya bileşeni verileri listesidir.</param>
        IPaketV1XOlusturEkDosyalar EkDosyalarIle(List<EkDosya> ekDosyalar);

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV1XOlusturPaketBilgi PaketDurumuIle(string durum);

        /// <summary>
        /// Paketin oluşturulduğu tarihi ekler.
        /// </summary>
        /// <param name="olusturulmaTarihi">Oluşturulma tarihidir.</param>
        IPaketV1XOlusturPaketBilgi PaketOlusturulmaTarihiIle(DateTime? olusturulmaTarihi);

        /// <summary>
        /// Paket açıklamasını ekler.
        /// </summary>
        /// <param name="aciklama">Açıklama değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketAciklamasiIle(string aciklama);

        /// <summary>
        /// Genellikle indeksleme ve arama için kullanılan anahtar kelimeleri ekler.
        /// </summary>
        /// <param name="anahtarKelimeler">Ayraçlar kullanılarak ayrılmış bir cümledir.</param>
        IPaketV1XOlusturPaketBilgi PaketAnahtarKelimeleriIle(string anahtarKelimeler);

        /// <summary>
        /// Paket dilini ekler.
        /// </summary>
        /// <param name="dil">Dil değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketDiliIle(string dil);

        /// <summary>
        /// Paketi olduğu belgenin son yazdırılma tarihini ekler.
        /// </summary>
        /// <param name="sonYazdirilmaTarihi">Son yazdırılma tarihidir.</param>
        IPaketV1XOlusturPaketBilgi PaketSonYazdirilmaTarihiIle(DateTime? sonYazdirilmaTarihi);

        /// <summary>
        /// Paket başlığı ekler.
        /// </summary>
        /// <param name="baslik">Başlık değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketBasligiIle(string baslik);

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        IPaketV1XOlusturBilesen BilesenleriOlustur();
    }

    public interface IPaketV1XOlusturBelgeImza
    {
        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eke ait dosya bileşenini ekler. 
        /// </summary>
        /// <param name="ekDosya">Eke ait dosya bileşeni verileridir.</param>
        IPaketV1XOlusturEkDosya EkDosyaIle(EkDosya ekDosya);

        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eklere ait dosya bileşenlerini ekler. 
        /// </summary>
        /// <param name="ekDosyalar">Eklere ait dosya bileşeni verileri listesidir.</param>
        IPaketV1XOlusturEkDosyalar EkDosyalarIle(List<EkDosya> ekDosyalar);

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV1XOlusturPaketBilgi PaketDurumuIle(string durum);

        /// <summary>
        /// Paketin oluşturulduğu tarihi ekler.
        /// </summary>
        /// <param name="olusturulmaTarihi">Oluşturulma tarihidir.</param>
        IPaketV1XOlusturPaketBilgi PaketOlusturulmaTarihiIle(DateTime? olusturulmaTarihi);

        /// <summary>
        /// Paket açıklamasını ekler.
        /// </summary>
        /// <param name="aciklama">Açıklama değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketAciklamasiIle(string aciklama);

        /// <summary>
        /// Genellikle indeksleme ve arama için kullanılan anahtar kelimeleri ekler.
        /// </summary>
        /// <param name="anahtarKelimeler">Ayraçlar kullanılarak ayrılmış bir cümledir.</param>
        IPaketV1XOlusturPaketBilgi PaketAnahtarKelimeleriIle(string anahtarKelimeler);

        /// <summary>
        /// Paket dilini ekler.
        /// </summary>
        /// <param name="dil">Dil değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketDiliIle(string dil);

        /// <summary>
        /// Paketi olduğu belgenin son yazdırılma tarihini ekler.
        /// </summary>
        /// <param name="sonYazdirilmaTarihi">Son yazdırılma tarihidir.</param>
        IPaketV1XOlusturPaketBilgi PaketSonYazdirilmaTarihiIle(DateTime? sonYazdirilmaTarihi);

        /// <summary>
        /// Paket başlığı ekler.
        /// </summary>
        /// <param name="baslik">Başlık değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketBasligiIle(string baslik);

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        IPaketV1XOlusturBilesen BilesenleriOlustur();
    }

    public interface IPaketV1XOlusturEkDosya
    {
        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eke ait dosya bileşenini ekler. 
        /// </summary>
        /// <param name="ekDosya">Eke ait dosya bileşeni verileridir.</param>
        IPaketV1XOlusturEkDosya EkDosyaIle(EkDosya ekDosya);

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV1XOlusturPaketBilgi PaketDurumuIle(string durum);

        /// <summary>
        /// Paketin oluşturulduğu tarihi ekler.
        /// </summary>
        /// <param name="olusturulmaTarihi">Oluşturulma tarihidir.</param>
        IPaketV1XOlusturPaketBilgi PaketOlusturulmaTarihiIle(DateTime? olusturulmaTarihi);

        /// <summary>
        /// Paket açıklamasını ekler.
        /// </summary>
        /// <param name="aciklama">Açıklama değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketAciklamasiIle(string aciklama);

        /// <summary>
        /// Genellikle indeksleme ve arama için kullanılan anahtar kelimeleri ekler.
        /// </summary>
        /// <param name="anahtarKelimeler">Ayraçlar kullanılarak ayrılmış bir cümledir.</param>
        IPaketV1XOlusturPaketBilgi PaketAnahtarKelimeleriIle(string anahtarKelimeler);

        /// <summary>
        /// Paket dilini ekler.
        /// </summary>
        /// <param name="dil">Dil değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketDiliIle(string dil);

        /// <summary>
        /// Paketi olduğu belgenin son yazdırılma tarihini ekler.
        /// </summary>
        /// <param name="sonYazdirilmaTarihi">Son yazdırılma tarihidir.</param>
        IPaketV1XOlusturPaketBilgi PaketSonYazdirilmaTarihiIle(DateTime? sonYazdirilmaTarihi);

        /// <summary>
        /// Paket başlığı ekler.
        /// </summary>
        /// <param name="baslik">Başlık değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketBasligiIle(string baslik);

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        IPaketV1XOlusturBilesen BilesenleriOlustur();
    }

    public interface IPaketV1XOlusturEkDosyalar
    {
        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV1XOlusturPaketBilgi PaketDurumuIle(string durum);

        /// <summary>
        /// Paketin oluşturulduğu tarihi ekler.
        /// </summary>
        /// <param name="olusturulmaTarihi">Oluşturulma tarihidir.</param>
        IPaketV1XOlusturPaketBilgi PaketOlusturulmaTarihiIle(DateTime? olusturulmaTarihi);

        /// <summary>
        /// Paket açıklamasını ekler.
        /// </summary>
        /// <param name="aciklama">Açıklama değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketAciklamasiIle(string aciklama);

        /// <summary>
        /// Genellikle indeksleme ve arama için kullanılan anahtar kelimeleri ekler.
        /// </summary>
        /// <param name="anahtarKelimeler">Ayraçlar kullanılarak ayrılmış bir cümledir.</param>
        IPaketV1XOlusturPaketBilgi PaketAnahtarKelimeleriIle(string anahtarKelimeler);

        /// <summary>
        /// Paket dilini ekler.
        /// </summary>
        /// <param name="dil">Dil değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketDiliIle(string dil);

        /// <summary>
        /// Paketi olduğu belgenin son yazdırılma tarihini ekler.
        /// </summary>
        /// <param name="sonYazdirilmaTarihi">Son yazdırılma tarihidir.</param>
        IPaketV1XOlusturPaketBilgi PaketSonYazdirilmaTarihiIle(DateTime? sonYazdirilmaTarihi);

        /// <summary>
        /// Paket başlığı ekler.
        /// </summary>
        /// <param name="baslik">Başlık değeridir.</param>
        IPaketV1XOlusturPaketBilgi PaketBasligiIle(string baslik);

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        IPaketV1XOlusturBilesen BilesenleriOlustur();
    }

    public interface IPaketV1XOlusturPaketBilgi
    {
        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        IPaketV1XOlusturBilesen BilesenleriOlustur();
    }

    public interface IPaketV1XOlusturBilesen
    {
        /// <summary>
        /// Oluşturulan paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV1XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV1XOlusturImza ImzaEkle(Func<Stream, byte[]> imzaFunction);
    }

    public interface IPaketV1XOlusturImza
    {
        /// <summary>
        /// Oluşturulan paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV1XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="muhurFunction">
        /// Mühür ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak NihaiOzet bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek mühür değeridir.
        /// </param>
        /// <remarks>Eklenecek mühür "Ayrık olmayan CAdES-XL" türünde imza olmalıdır.</remarks>
        IPaketV1XOlusturMuhur MuhurEkle(Func<Stream, byte[]> muhurFunction);
    }

    public interface IPaketV1XOlusturMuhur
    {
        /// <summary>
        /// Oluşturulan paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV1XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
    }

    public interface IPaketV1XOlusturDogrula
    {
        void Kapat();
    }

    #endregion

    #region Guncelle
    public interface IPaketV1XGuncelle
    {
        IPaketV1XGuncellePaketBilgi PaketGuncellemeTarihiIle(DateTime? guncellemeTarihi);

        /// <summary>
        /// Paketi son güncelleyen taraf bilgisinin ekler.
        /// </summary>
        /// <param name="sonGuncelleyen">Son güncelleyen taraf bilgisidir.</param>
        IPaketV1XGuncellePaketBilgi PaketSonGuncelleyenIle(string sonGuncelleyen);

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV1XGuncellePaketBilgi PaketDurumuIle(string durum);

        /// <summary>
        /// Belgeye atılmış olan imzalara ilişkin üstveri bilgilerini içeren BelgeImza bileşenini paket içerisine ekler.
        /// </summary>
        /// <param name="belgeImza">BelgeImza bileşenidir.</param>
        IPaketV1XGuncelleBelgeImza BelgeImzaIle(BelgeImza belgeImza);

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV1XGuncelleImza ImzaEkle(Func<Stream, byte[]> imzaFunction);

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV1XGuncelleImza ImzaEkle(byte[] imza);

        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="muhurFunction">
        /// Mühür ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak NihaiOzet bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek mühür değeridir.
        /// </param>
        /// <remarks>Eklenecek mühür "Ayrık olmayan CAdES-XL" türünde imza olmalıdır.</remarks>
        IPaketV1XGuncelleMuhur MuhurEkle(Func<Stream, byte[]> muhurFunction);

        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek mühür değeridir. Eklenecek mühür "Ayrık olmayan CAdES-XL" türünde imza olmalıdır.</param>
        IPaketV1XGuncelleMuhur MuhurEkle(byte[] muhur);

        /// <summary>
        /// Paket içerisinden ek çıkarılmak için kullanılır.
        /// </summary>
        /// <param name="ekId">Çıkarılacak eke ait Id bilgisidir.</param>
        IPaketV1XGuncelleEkDosya EkDosyaCikar(IdTip ekId);
    }

    public interface IPaketV1XGuncelleBelgeImza
    {
        IPaketV1XGuncellePaketBilgi PaketGuncellemeTarihiIle(DateTime? guncellemeTarihi);

        /// <summary>
        /// Paketi son güncelleyen taraf bilgisinin ekler.
        /// </summary>
        /// <param name="sonGuncelleyen">Son güncelleyen taraf bilgisidir.</param>
        IPaketV1XGuncellePaketBilgi PaketSonGuncelleyenIle(string sonGuncelleyen);

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV1XGuncellePaketBilgi PaketDurumuIle(string durum);

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV1XGuncelleImza ImzaEkle(Func<Stream, byte[]> imzaFunction);

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV1XGuncelleImza ImzaEkle(byte[] imza);
    }

    public interface IPaketV1XGuncelleImza
    {
        /// <summary>
        /// Güncellenen paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV1XGuncelleDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
    }

    public interface IPaketV1XGuncelleMuhur
    {
        void Kapat();
    }

    public interface IPaketV1XGuncelleEkDosya
    {
        /// <summary>
        /// Paket içerisinden ek çıkarılmak için kullanılır.
        /// </summary>
        /// <param name="ekId">Çıkarılacak eke ait Id bilgisidir.</param>
        IPaketV1XGuncelleEkDosya EkDosyaCikar(IdTip ekId);

        /// <summary>
        /// Ek çıkarma işlemi yapılan paketin doğrulanmasını sağlar.
        /// </summary>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV1XGuncelleDogrula Dogrula(TanimlayiciTip dagitimTanimlayici, Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
    }

    public interface IPaketV1XGuncellePaketBilgi
    {
        IPaketV1XGuncellePaketBilgi PaketGuncellemeTarihiIle(DateTime? guncellemeTarihi);

        /// <summary>
        /// Paketi son güncelleyen taraf bilgisinin ekler.
        /// </summary>
        /// <param name="sonGuncelleyen">Son güncelleyen taraf bilgisidir.</param>
        IPaketV1XGuncellePaketBilgi PaketSonGuncelleyenIle(string sonGuncelleyen);

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV1XGuncellePaketBilgi PaketDurumuIle(string durum);

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV1XGuncelleImza ImzaEkle(Func<Stream, byte[]> imzaFunction);

        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV1XGuncelleImza ImzaEkle(byte[] imza);
    }

    public interface IPaketV1XGuncelleDogrula
    {
        void Kapat();
    }

    #endregion

    public interface IPaketV2X : IPaketV2XOlustur,
                                 IPaketV2XOlusturBilesen,
                                 IPaketV2XOlusturBilesen2,
                                 IPaketV2XOlusturDogrula,
                                 IPaketV2XOlusturEkDosya,
                                 IPaketV2XOlusturEkDosyalar,
                                 IPaketV2XOlusturImza,
                                 IPaketV2XOlusturMuhur,
                                 IPaketV2XOlusturNihaiUstveri,
                                 IPaketV2XOlusturOzetAlgoritma,
                                 IPaketV2XOlusturPaketBilgi,
                                 IPaketV2XOlusturParaf,
                                 IPaketV2XOlusturUstveri,
                                 IPaketV2XOlusturUstYazi,
                                 IPaketV2XOku,
                                 IPaketV2XOkuBilesen,
                                 IPaketV2XOkuBilesenAl,
                                 IPaketV2XGuncelle,
                                 IPaketV2XGuncelleBilesen,
                                 IPaketV2XGuncelleDogrula,
                                 IPaketV2XGuncelleImza,
                                 IPaketV2XGuncelleMuhur,
                                 IPaketV2XGuncelleNihaiUstveri,
                                 IPaketV2XGuncelleParaf,
                                 IPaketV2XGuncellePaketBilgi,
                                 IPaketV2XGuncelleEkDosya
    {
    }

    #region Oku

    public interface IPaketV2XOku
    {
        /// <summary>
        /// Paket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// IPaketV2XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        IPaketV2XOkuBilesenAl BilesenleriAl(Action<bool, IPaketV2XOkuBilesen, List<DogrulamaHatasi>> bilesenAction);
    }

    public interface IPaketV2XOkuBilesenAl
    {
        void Kapat();
    }

    public interface IPaketV2XOkuBilesen
    {
        /// <summary>
        /// Belgeye ait üstveri alanlarını barıdıran nesneye ulaşılır.
        /// </summary>
        Ustveri Ustveri { get; }

        /// <summary>
        /// Paket içerisindeki Ustveri bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool UstveriVarMi();

        /// <summary>
        /// Paket içerisindeki Ustveri bileşenini STREAM olarak verir.
        /// </summary>
        Stream UstveriAl();

        /// <summary>
        /// Belgeye ait nihai üstveri alanlarını barındıran nesneye ulaşılır.
        /// </summary>
        NihaiUstveri NihaiUstveri { get; }

        /// <summary>
        /// Paket içerisindeki NihaiUstveri bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool NihaiUstveriVarMi();

        /// <summary>
        /// Paket içerisindeki NihaiUstveri bileşenini STREAM olarak verir.
        /// </summary>
        Stream NihaiUstveriAl();

        /// <summary>
        /// Paket içerisindeki üst yazı bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool UstYaziVarMi();

        /// <summary>
        /// Paket içerisindeki üst yazı bileşenini STREAM olarak verir.
        /// </summary>
        Stream UstYaziAl();

        /// <summary>
        /// Id'si verilen eke ait dosyanın paket içerisinde olup olmadığını gösterir.
        /// </summary>
        bool EkDosyaVarMi(IdTip ekId);

        /// <summary>
        /// Id'si verilen eke ait dosyayı STREAM olarak verir.
        /// </summary>
        /// <param name="ekId">Ek id değeridir.</param>
        Stream EkDosyaAl(IdTip ekId);

        /// <summary>
        /// Paket içerisinde paraflanan bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        ParafOzeti ParafOzeti { get; }

        /// <summary>
        /// Paket içerisindeki ParafOzeti bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool ParafOzetiVarMi();

        /// <summary>
        /// Paket içerisindeki ParafOzeti bileşenini STREAM olarak verir.
        /// </summary>
        Stream ParafOzetiAl();

        /// <summary>
        /// Verilen ParafOzeti nesnesindeki özet değerleri ile paket içerisindeki bileşenlerin özet değerlerini doğrular.
        /// </summary>
        /// <param name="ozet">Doğrulanacak özet değerlerini barındıran ParafOzeti nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        bool ParafOzetiDogrula(ParafOzeti ozet, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null);

        /// <summary>
        /// Paket içerisinde imzalanan bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        PaketOzeti PaketOzeti { get; }

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool PaketOzetiVarMi();

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşenini STREAM olarak verir.
        /// </summary>
        Stream PaketOzetiAl();

        /// <summary>
        /// Verilen PaketOzeti nesnesindeki özet değerleri ile paket içerisindeki bileşenlerin özet değerlerini doğrular.
        /// </summary>
        /// <param name="ozet">Doğrulanacak özet değerlerini barındıran PaketOzeti nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        bool PaketOzetiDogrula(PaketOzeti ozet, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null);

        /// <summary>
        /// Paket içerisindeki ParafOzeti bileşeninin imzalı değeri olup olmadığını gösterir.
        /// </summary>
        bool ParafImzaVarMi();

        /// <summary>
        /// Paket içerisindeki ParafOzeti bileşeninin imzalı (Ayrık olmayan CAdES-XL) değerini STREAM olarak verir.
        /// </summary>
        Stream ParafImzaAl();

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin imzalı değeri olup olmadığını gösterir.
        /// </summary>
        bool ImzaVarMi();

        /// <summary>
        /// Paket içerisindeki PaketOzeti bileşeninin imzalı (Ayrık olmayan CAdES-XL) değerini STREAM olarak verir.
        /// </summary>
        Stream ImzaAl();

        /// <summary>
        /// Paket içerisinde mühürlenen bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        NihaiOzet NihaiOzet { get; }

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool NihaiOzetVarMi();

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşenini STREAM olarak verir.
        /// </summary>
        Stream NihaiOzetAl();

        /// <summary>
        /// Verilen NihaiOzet nesnesindeki özet değerleri ile paket içerisindeki bileşenlerin özet değerlerini doğrular.
        /// </summary>
        /// <param name="ozet">Doğrulanacak özet değerlerini barındıran NihaiOzet nesnesidir.</param>
        /// <param name="sonuclar">Tüm doğrulama hatalarını belirtir.</param>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <returns>Doğrulamanın başarılı olup olmadığını belirtir.</returns>
        bool NihaiOzetDogrula(NihaiOzet ozet, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null);

        /// <summary>
        /// Paket içerisindeki mühür bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool MuhurVarMi();

        /// <summary>
        /// Paket içerisindeki NihaiOzet bileşeninin imzalı (Ayrık olmayan CAdES-XL) değerini STREAM olarak verir.
        /// </summary>
        Stream MuhurAl();

        bool ReferansDogrula(Referans referans, ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null);

        void IlgileriDogrula(ref List<DogrulamaHatasi> sonuclar);

        void EkleriDogrula(ref List<DogrulamaHatasi> sonuclar, TanimlayiciTip dagitimTanimlayici = null);
    }

    #endregion

    #region Olustur

    public interface IPaketV2XOlustur
    {
        /// <summary>
        /// Paket bileşenlerinin içerisine eklenecek özetlerin oluşturulmasında kullanılacak algoritmayı belirtir.
        /// </summary>
        /// <param name="ozetAlgoritma"></param>
        IPaketV2XOlusturOzetAlgoritma OzetAlgoritmaIle(OzetAlgoritmaTuru ozetAlgoritma);
        /// <summary>
        /// Paket içerisine üst yazı bileşeni ekler.
        /// </summary>
        /// <param name="ustYazi">Üst yazı bişeni verileridir.</param>
        IPaketV2XOlusturUstYazi UstYaziAta(UstYazi ustYazi);
    }

    public interface IPaketV2XOlusturOzetAlgoritma
    {
        /// <summary>
        /// Paket içerisine üst yazı bileşeni ekler.
        /// </summary>
        /// <param name="ustYazi">Üst yazı bişeni verileridir.</param>
        IPaketV2XOlusturUstYazi UstYaziAta(UstYazi ustYazi);
    }

    public interface IPaketV2XOlusturUstYazi
    {
        /// <summary>
        /// Belgeye ait üstveri alanlarını barındıran Ustveri bileşenini paket içerisine  ekler.
        /// </summary>
        /// <param name="ustveri">Ustveri bileşenidir.</param>
        IPaketV2XOlusturUstveri UstveriAta(Ustveri ustveri);
    }

    public interface IPaketV2XOlusturUstveri
    {
        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eke ait dosya bileşenini ekler. 
        /// </summary>
        /// <param name="ekDosya">Eke ait dosya bileşeni verileridir.</param>
        IPaketV2XOlusturEkDosya EkDosyaIle(EkDosya ekDosya);

        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eklere ait dosya bileşenlerini ekler. 
        /// </summary>
        /// <param name="ekDosyalar">Eklere ait dosya bileşeni verileri listesidir.</param>
        IPaketV2XOlusturEkDosyalar EkDosyalarIle(List<EkDosya> ekDosyalar);

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV2XOlusturPaketBilgi PaketDurumuIle(string durum);

        /// <summary>
        /// Paketin oluşturulduğu tarihi ekler.
        /// </summary>
        /// <param name="olusturulmaTarihi">Oluşturulma tarihidir.</param>
        IPaketV2XOlusturPaketBilgi PaketOlusturulmaTarihiIle(DateTime? olusturulmaTarihi);

        /// <summary>
        /// Paket açıklamasını ekler.
        /// </summary>
        /// <param name="aciklama">Açıklama değeridir.</param>
        IPaketV2XOlusturPaketBilgi PaketAciklamasiIle(string aciklama);

        /// <summary>
        /// Genellikle indeksleme ve arama için kullanılan anahtar kelimeleri ekler.
        /// </summary>
        /// <param name="anahtarKelimeler">Ayraçlar kullanılarak ayrılmış bir cümledir.</param>
        IPaketV2XOlusturPaketBilgi PaketAnahtarKelimeleriIle(string anahtarKelimeler);

        /// <summary>
        /// Paket dilini ekler.
        /// </summary>
        /// <param name="dil">Dil değeridir.</param>
        IPaketV2XOlusturPaketBilgi PaketDiliIle(string dil);

        /// <summary>
        /// Paketi olduğu belgenin son yazdırılma tarihini ekler.
        /// </summary>
        /// <param name="sonYazdirilmaTarihi">Son yazdırılma tarihidir.</param>
        IPaketV2XOlusturPaketBilgi PaketSonYazdirilmaTarihiIle(DateTime? sonYazdirilmaTarihi);

        /// <summary>
        /// Paket başlığı ekler.
        /// </summary>
        /// <param name="baslik">Başlık değeridir.</param>
        IPaketV2XOlusturPaketBilgi PaketBasligiIle(string baslik);

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        /// <param name="parafOzetiOlusturulsun">ParafOzeti bileşenin oluşturulup oluşturulmayacağını belirtir.
        /// Varsayılan değeri \"false\" dır.
        /// </param>
        IPaketV2XOlusturBilesen BilesenleriOlustur(bool parafOzetiOlusturulsun = false);
    }

    public interface IPaketV2XOlusturEkDosya
    {
        /// <summary>
        /// Paket içerisine Ustveri bileşeninde belirtilen DED türündeki eke ait dosya bileşenini ekler. 
        /// </summary>
        /// <param name="ekDosya">Eke ait dosya bileşeni verileridir.</param>
        IPaketV2XOlusturEkDosya EkDosyaIle(EkDosya ekDosya);

        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV2XOlusturPaketBilgi PaketDurumuIle(string durum);

        /// <summary>
        /// Paketin oluşturulduğu tarihi ekler.
        /// </summary>
        /// <param name="olusturulmaTarihi">Oluşturulma tarihidir.</param>
        IPaketV2XOlusturPaketBilgi PaketOlusturulmaTarihiIle(DateTime? olusturulmaTarihi);

        /// <summary>
        /// Paket açıklamasını ekler.
        /// </summary>
        /// <param name="aciklama">Açıklama değeridir.</param>
        IPaketV2XOlusturPaketBilgi PaketAciklamasiIle(string aciklama);

        /// <summary>
        /// Genellikle indeksleme ve arama için kullanılan anahtar kelimeleri ekler.
        /// </summary>
        /// <param name="anahtarKelimeler">Ayraçlar kullanılarak ayrılmış bir cümledir.</param>
        IPaketV2XOlusturPaketBilgi PaketAnahtarKelimeleriIle(string anahtarKelimeler);

        /// <summary>
        /// Paket dilini ekler.
        /// </summary>
        /// <param name="dil">Dil değeridir.</param>
        IPaketV2XOlusturPaketBilgi PaketDiliIle(string dil);

        /// <summary>
        /// Paketi olduğu belgenin son yazdırılma tarihini ekler.
        /// </summary>
        /// <param name="sonYazdirilmaTarihi">Son yazdırılma tarihidir.</param>
        IPaketV2XOlusturPaketBilgi PaketSonYazdirilmaTarihiIle(DateTime? sonYazdirilmaTarihi);

        /// <summary>
        /// Paket başlığı ekler.
        /// </summary>
        /// <param name="baslik">Başlık değeridir.</param>
        IPaketV2XOlusturPaketBilgi PaketBasligiIle(string baslik);

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        /// <param name="parafOzetiOlusturulsun">ParafOzeti bileşenin oluşturulup oluşturulmayacağını belirtir.
        /// Varsayılan değeri \"false\" dır.
        /// </param>
        IPaketV2XOlusturBilesen BilesenleriOlustur(bool parafOzetiOlusturulsun = false);
    }

    public interface IPaketV2XOlusturEkDosyalar
    {
        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV2XOlusturPaketBilgi PaketDurumuIle(string durum);

        /// <summary>
        /// Paketin oluşturulduğu tarihi ekler.
        /// </summary>
        /// <param name="olusturulmaTarihi">Oluşturulma tarihidir.</param>
        IPaketV2XOlusturPaketBilgi PaketOlusturulmaTarihiIle(DateTime? olusturulmaTarihi);

        /// <summary>
        /// Paket açıklamasını ekler.
        /// </summary>
        /// <param name="aciklama">Açıklama değeridir.</param>
        IPaketV2XOlusturPaketBilgi PaketAciklamasiIle(string aciklama);

        /// <summary>
        /// Genellikle indeksleme ve arama için kullanılan anahtar kelimeleri ekler.
        /// </summary>
        /// <param name="anahtarKelimeler">Ayraçlar kullanılarak ayrılmış bir cümledir.</param>
        IPaketV2XOlusturPaketBilgi PaketAnahtarKelimeleriIle(string anahtarKelimeler);

        /// <summary>
        /// Paket dilini ekler.
        /// </summary>
        /// <param name="dil">Dil değeridir.</param>
        IPaketV2XOlusturPaketBilgi PaketDiliIle(string dil);

        /// <summary>
        /// Paketi olduğu belgenin son yazdırılma tarihini ekler.
        /// </summary>
        /// <param name="sonYazdirilmaTarihi">Son yazdırılma tarihidir.</param>
        IPaketV2XOlusturPaketBilgi PaketSonYazdirilmaTarihiIle(DateTime? sonYazdirilmaTarihi);

        /// <summary>
        /// Paket başlığı ekler.
        /// </summary>
        /// <param name="baslik">Başlık değeridir.</param>
        IPaketV2XOlusturPaketBilgi PaketBasligiIle(string baslik);

        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        /// <param name="parafOzetiOlusturulsun">ParafOzeti bileşenin oluşturulup oluşturulmayacağını belirtir.
        /// Varsayılan değeri \"false\" dır.
        /// </param>
        IPaketV2XOlusturBilesen BilesenleriOlustur(bool parafOzetiOlusturulsun = false);
    }

    public interface IPaketV2XOlusturPaketBilgi
    {
        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        IPaketV2XOlusturBilesen BilesenleriOlustur(bool parafOzetiOlusturulsun = false);
    }

    public interface IPaketV2XOlusturBilesen
    {
        /// <summary>
        /// Oluşturulan paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV2XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XOlusturImza ImzaEkle(Func<Stream, byte[]> imzaFunction);
        /// <summary>
        /// Paket içerisine ParafOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak ParafOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XOlusturParaf ParafImzaEkle(Func<Stream, byte[]> imzaFunction);
    }

    public interface IPaketV2XOlusturParaf
    {
        /// <summary>
        /// Oluşturulan paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV2XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XOlusturImza ImzaEkle(Func<Stream, byte[]> imzaFunction);
    }

    public interface IPaketV2XOlusturImza
    {
        /// <summary>
        /// Belgeye ait nihai üstveri alanlarını barındıran NihaiUstveri bileşenini paket içerisine  ekler.
        /// </summary>
        /// <param name="nihaiUstveri">NihaiUstveri bileşenidir.</param>
        IPaketV2XOlusturNihaiUstveri NihaiUstveriAta(NihaiUstveri nihaiUstveri);
    }

    public interface IPaketV2XOlusturNihaiUstveri
    {
        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        IPaketV2XOlusturBilesen2 BilesenleriOlustur();
    }

    public interface IPaketV2XOlusturBilesen2
    {
        /// <summary>
        /// Oluşturulan paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV2XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="muhurFunction">
        /// Mühür ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak NihaiOzet bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek mühür değeridir.
        /// </param>
        /// <remarks>Eklenecek mühür "Ayrık olmayan CAdES-XL" türünde imza olmalıdır.</remarks>
        IPaketV2XOlusturMuhur MuhurEkle(Func<Stream, byte[]> muhurFunction);
    }

    public interface IPaketV2XOlusturMuhur
    {
        /// <summary>
        /// Oluşturulan paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV2XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
    }

    public interface IPaketV2XOlusturDogrula
    {
        void Kapat();
    }

    #endregion

    #region Guncelle

    public interface IPaketV2XGuncelle
    {
        IPaketV2XGuncellePaketBilgi PaketGuncellemeTarihiIle(DateTime? guncellemeTarihi);
        /// <summary>
        /// Paketi son güncelleyen taraf bilgisinin ekler.
        /// </summary>
        /// <param name="sonGuncelleyen">Son güncelleyen taraf bilgisidir.</param>
        IPaketV2XGuncellePaketBilgi PaketSonGuncelleyenIle(string sonGuncelleyen);
        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV2XGuncellePaketBilgi PaketDurumuIle(string durum);
        /// <summary>
        /// Paket içerisine ParafOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak ParafOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XGuncelleParaf ParafImzaEkle(Func<Stream, byte[]> imzaFunction);
        /// <summary>
        /// Paket içerisine ParafOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek ParafOzeti bileşenine ait imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV2XGuncelleParaf ParafImzaEkle(byte[] imza);
        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XGuncelleImza ImzaEkle(Func<Stream, byte[]> imzaFunction);
        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek PaketOzeti bileşenine ait imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV2XGuncelleImza ImzaEkle(byte[] imza);
        /// <summary>
        /// Paket içerisinden ek çıkarılmak için kullanılır.
        /// </summary>
        /// <param name="ekId">Çıkarılacak eke ait Id bilgisidir.</param>
        IPaketV2XGuncelleEkDosya EkDosyaCikar(IdTip ekId);
    }

    public interface IPaketV2XGuncellePaketBilgi
    {
        IPaketV2XGuncellePaketBilgi PaketGuncellemeTarihiIle(DateTime? guncellemeTarihi);
        /// <summary>
        /// Paketi son güncelleyen taraf bilgisinin ekler.
        /// </summary>
        /// <param name="sonGuncelleyen">Son güncelleyen taraf bilgisidir.</param>
        IPaketV2XGuncellePaketBilgi PaketSonGuncelleyenIle(string sonGuncelleyen);
        /// <summary>
        /// Paketin durumunu ekler.
        /// </summary>
        /// <param name="durum">Durum bilgisidir. Taslak, Son vb. değerler kullanılabilir.</param>
        IPaketV2XGuncellePaketBilgi PaketDurumuIle(string durum);
        /// <summary>
        /// Paket içerisine ParafOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak ParafOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XGuncelleParaf ParafImzaEkle(Func<Stream, byte[]> imzaFunction);
        /// <summary>
        /// Paket içerisine ParafOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek ParafOzeti bileşenine ait imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV2XGuncelleParaf ParafImzaEkle(byte[] imza);
        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XGuncelleImza ImzaEkle(Func<Stream, byte[]> imzaFunction);
        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek PaketOzeti bileşenine ait imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV2XGuncelleImza ImzaEkle(byte[] imza);
    }

    public interface IPaketV2XGuncelleEkDosya
    {
        /// <summary>
        /// Paket içerisinden ek çıkarılmak için kullanılır.
        /// </summary>
        /// <param name="ekId">Çıkarılacak eke ait Id bilgisidir.</param>
        IPaketV2XGuncelleEkDosya EkDosyaCikar(IdTip ekId);

        /// <summary>
        /// Ek çıkarma işlemi yapılan paketin doğrulanmasını sağlar.
        /// </summary>
        /// <param name="dagitimTanimlayici">Doğrulama tek bir dağıtım için yapılacaksa burada belirtilir.</param>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV2XGuncelleDogrula Dogrula(TanimlayiciTip dagitimTanimlayici, Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
    }

    public interface IPaketV2XGuncelleParaf
    {
        /// <summary>
        /// Güncellenen paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV2XGuncelleDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imzaFunction">
        /// İmza ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak PaketOzeti bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek imza değeridir.
        /// </param>
        /// <remarks>Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</remarks>
        IPaketV2XGuncelleImza ImzaEkle(Func<Stream, byte[]> imzaFunction);
        /// <summary>
        /// Paket içerisine PaketOzeti bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="imza">Paket içerisine eklenecek PaketOzeti bileşenine ait imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV2XGuncelleImza ImzaEkle(byte[] imza);
    }

    public interface IPaketV2XGuncelleImza
    {
        /// <summary>
        /// Belgeye ait nihai üstveri alanlarını barındıran NihaiUstveri bileşenini paket içerisine  ekler.
        /// </summary>
        /// <param name="nihaiUstveri">NihaiUstveri bileşenidir.</param>
        IPaketV2XGuncelleNihaiUstveri NihaiUstveriAta(NihaiUstveri nihaiUstveri);
    }

    public interface IPaketV2XGuncelleNihaiUstveri
    {
        /// <summary>
        /// Verilen bileşen değerleri kullanılarak paket bileşenlerini oluşturur.
        /// </summary>
        IPaketV2XGuncelleBilesen BilesenleriOlustur();
    }

    public interface IPaketV2XGuncelleBilesen
    {
        /// <summary>
        /// Güncellenen paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV2XGuncelleDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="muhurFunction">
        /// Mühür ekleme işlemi için kullanılacak fonksiyondur.
        /// Stream -> İmzalanacak NihaiOzet bileşenine ait STREAM değeridir.
        /// byte[] -> Paket içerisine eklenecek mühür değeridir.
        /// </param>
        /// <remarks>Eklenecek mühür "Ayrık olmayan CAdES-XL" türünde imza olmalıdır.</remarks>
        IPaketV2XGuncelleMuhur MuhurEkle(Func<Stream, byte[]> muhurFunction);
        /// <summary>
        /// Paket içerisine NihaiOzet bileşeninin imzalı değerini ekler.
        /// </summary>
        /// <param name="muhur">Paket içerisine eklenecek NihaiOzet bileşenine ait imza değeridir. Eklenecek imza "Ayrık olmayan CAdES-XL" türünde olmalıdır.</param>
        IPaketV2XGuncelleImza MuhurEkle(byte[] muhur);
    }

    public interface IPaketV2XGuncelleMuhur
    {
        /// <summary>
        /// Güncellenen paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        IPaketV2XGuncelleDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
    }

    public interface IPaketV2XGuncelleDogrula
    {
        void Kapat();
    }

    #endregion
}
