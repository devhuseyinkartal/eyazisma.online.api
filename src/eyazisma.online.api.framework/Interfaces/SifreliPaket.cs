using eyazisma.online.api.framework.Classes;
using System;
using System.Collections.Generic;
using System.IO;

namespace eyazisma.online.api.framework.Interfaces
{
    public interface ISifreliPaket : ISifreliPaketOlustur,
                              ISifreliPaketOku,
                              ISifreliPaketOkuAction
    {

    }

    public interface ISifreliPaketOlustur
    {
        ISifreliPaketV1XOlustur Versiyon1X();
        ISifreliPaketV2XOlustur Versiyon2X();
    }

    public interface ISifreliPaketOku
    {
        /// <summary>
        /// Paket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// ISifreliPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        ISifreliPaketOkuAction Versiyon1XIse(Action<bool, ISifreliPaketV1XOkuBilesen, List<DogrulamaHatasi>> action);
    }

    public interface ISifreliPaketOkuAction
    {
        /// <summary>
        /// Paket ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// ISifreliPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        void Versiyon2XIse(Action<bool, ISifreliPaketV2XOkuBilesen, List<DogrulamaHatasi>> action);
    }

    public interface ISifreliPaketV1X : ISifreliPaketV1XOlustur,
                                        ISifreliPaketV1XOlusturPaketOzeti,
                                        ISifreliPaketV1XOlusturSifreliIcerik,
                                        ISifreliPaketV1XOlusturOlusturan,
                                        ISifreliPaketV1XOlusturBelgeHedef,
                                        ISifreliPaketV1XOlusturBilesen,
                                        ISifreliPaketV1XOlusturDogrula,
                                        ISifreliPaketV1XOku,
                                        ISifreliPaketV1XOkuBilesen,
                                        ISifreliPaketV1XOkuBilesenAl
    {
    }

    public interface ISifreliPaketV1XOlustur
    {
        /// <summary>
        /// Şifresiz paketin PaketOzeti bileşenini şifreli pakete ekler.
        /// </summary>
        /// <param name="paketOzetiStream">Şifresiz paketin PaketOzeti bileşenidir.</param>
        ISifreliPaketV1XOlusturPaketOzeti PaketOzetiEkle(Stream paketOzetiStream);
    }

    public interface ISifreliPaketV1XOlusturPaketOzeti
    {
        /// <summary>
        /// Şifresiz paketin şifrelenmiş halini pakete ekler.
        /// </summary>
        /// <param name="sifreliIcerikStream">Şifresiz paketin şifrelenmiş haline ait STREAM değeridir.</param>
        /// <param name="paketId">Şifresiz paketin Id değeridir.</param>
        ISifreliPaketV1XOlusturSifreliIcerik SifreliIcerikEkle(Stream sifreliIcerikStream, Guid paketId);
    }

    public interface ISifreliPaketV1XOlusturSifreliIcerik
    {
        /// <summary>
        /// Şifreli paketin oluşturan bilgisini ekler.
        /// </summary>
        /// <param name="olusturan">Şifresiz paketin Olusturan bileşenidir.</param>
        ISifreliPaketV1XOlusturOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface ISifreliPaketV1XOlusturOlusturan
    {
        /// <summary>
        /// Şifreli paketin iletileceği hedefleri barındıran BelgeHedef bileşenini paket içerisine ekler.
        /// </summary>
        /// <param name="belgeHedef">BelgeHedef bileşenidir.</param>
        ISifreliPaketV1XOlusturBelgeHedef BelgeHedefIle(BelgeHedef belgeHedef);
        /// <summary>
        /// Verilen bileşen değerleri kullanılarak şifreli paket bileşenlerini oluşturur.
        /// </summary>
        ISifreliPaketV1XOlusturBilesen BilesenleriOlustur();
    }

    public interface ISifreliPaketV1XOlusturBelgeHedef
    {
        /// <summary>
        /// Verilen bileşen değerleri kullanılarak şifreli paket bileşenlerini oluşturur.
        /// </summary>
        ISifreliPaketV1XOlusturBilesen BilesenleriOlustur();
    }

    public interface ISifreliPaketV1XOlusturBilesen
    {
        /// <summary>
        /// Oluşturulan şifreli paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Şifreli pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        ISifreliPaketV1XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
    }

    public interface ISifreliPaketV1XOlusturDogrula
    {
        void Kapat();
    }

    public interface ISifreliPaketV1XOku
    {
        /// <summary>
        /// Şifreli pakete ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Şifreli paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// ISifreliPaketV1XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        ISifreliPaketV1XOkuBilesenAl BilesenleriAl(Action<bool, ISifreliPaketV1XOkuBilesen, List<DogrulamaHatasi>> bilesenAction);
    }

    public interface ISifreliPaketV1XOkuBilesenAl
    {
        void Kapat();
    }

    public interface ISifreliPaketV1XOkuBilesen
    {
        /// <summary>
        /// Şifreli paketin iletileceği hedefleri barındıran nesneye ulaşılır.
        /// </summary>
        BelgeHedef BelgeHedef { get; set; }
        /// <summary>
        /// Şifreli paket içerisindeki BelgeHedef bileşenini STREAM olarak verir.
        /// </summary>
        Stream BelgeHedefAl();
        /// <summary>
        /// Şifreli paket içerisindeki BelgeHedef bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool BelgeHedefVarMi();

        /// <summary>
        /// Şifreli paket içerisinde imzalanan bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        PaketOzeti PaketOzeti { get; set; }
        /// <summary>
        /// Şifreli paket içerisindeki PaketOzeti bileşenini STREAM olarak verir.
        /// </summary>
        Stream PaketOzetiAl();
        /// <summary>
        /// Şifreli paket içerisindeki PaketOzeti bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool PaketOzetiVarMi();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli içeriğe ilişkin bilgileri içeren nesneye ulaşılır.
        /// </summary>
        SifreliIcerikBilgisi SifreliIcerikBilgisi { get; set; }

        /// <summary>
        /// Şifreli paket içerisindeki SifreliIcerikBilgisi bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool SifreliIcerikBilgisiVarMi();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli dosyayı STREAM olarak verir.
        /// </summary>
        Stream SifreliIcerikAl();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli dosyanın adını döner.
        /// </summary>
        string SifreliIcerikDosyasiAdiAl();
    }

    public interface ISifreliPaketV2X : ISifreliPaketV2XOlustur,
                                       ISifreliPaketV2XOlusturNihaiOzet,
                                       ISifreliPaketV2XOlusturSifreliIcerik,
                                       ISifreliPaketV2XOlusturOlusturan,
                                       ISifreliPaketV2XOlusturBelgeHedef,
                                       ISifreliPaketV2XOlusturBilesen,
                                       ISifreliPaketV2XOlusturDogrula,
                                       ISifreliPaketV2XOku,
                                       ISifreliPaketV2XOkuBilesen,
                                       ISifreliPaketV2XOkuBilesenAl
    {
    }

    public interface ISifreliPaketV2XOlustur
    {
        /// <summary>
        /// Şifresiz paketin NihaiOzet bileşenini şifreli pakete ekler.
        /// </summary>
        /// <param name="nihaiOzetStream">Şifresiz paketin PaketOzeti bileşenidir.</param>
        ISifreliPaketV2XOlusturNihaiOzet NihaiOzetEkle(Stream nihaiOzetStream);
    }

    public interface ISifreliPaketV2XOlusturNihaiOzet
    {
        /// <summary>
        /// Şifresiz paketin şifrelenmiş halini pakete ekler.
        /// </summary>
        /// <param name="sifreliIcerikStream">Şifresiz paketin şifrelenmiş haline ait STREAM değeridir.</param>
        /// <param name="paketId">Şifresiz paketin Id değeridir.</param>
        ISifreliPaketV2XOlusturSifreliIcerik SifreliIcerikEkle(Stream sifreliIcerikStream, Guid paketId);
    }

    public interface ISifreliPaketV2XOlusturSifreliIcerik
    {
        /// <summary>
        /// Şifreli paketin oluşturan bilgisini ekler.
        /// </summary>
        /// <param name="olusturan">Şifresiz paketin Olusturan bileşenidir.</param>
        ISifreliPaketV2XOlusturOlusturan OlusturanAta(Olusturan olusturan);
    }

    public interface ISifreliPaketV2XOlusturOlusturan
    {
        /// <summary>
        /// Şifreli paketin iletileceği hedefleri barındıran BelgeHedef bileşenini paket içerisine ekler.
        /// </summary>
        /// <param name="belgeHedef">BelgeHedef bileşenidir.</param>
        ISifreliPaketV2XOlusturBelgeHedef BelgeHedefIle(BelgeHedef belgeHedef);
        /// <summary>
        /// Verilen bileşen değerleri kullanılarak şifreli paket bileşenlerini oluşturur.
        /// </summary>
        ISifreliPaketV2XOlusturBilesen BilesenleriOlustur();
    }

    public interface ISifreliPaketV2XOlusturBelgeHedef
    {
        /// <summary>
        /// Verilen bileşen değerleri kullanılarak şifreli paket bileşenlerini oluşturur.
        /// </summary>
        ISifreliPaketV2XOlusturBilesen BilesenleriOlustur();
    }

    public interface ISifreliPaketV2XOlusturBilesen
    {
        /// <summary>
        /// Oluşturulan şifreli paketin doğrulama sonuçlarının alınmasını sağlar.
        /// </summary>
        /// <param name="dogrulamaAction">
        /// Doğrulama sonuçlarını almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// List -> Şifreli pakete ait tüm doğrulama hatalarını belirtir. 
        /// </param>
        ISifreliPaketV2XOlusturDogrula Dogrula(Action<bool, List<DogrulamaHatasi>> dogrulamaAction);
    }

    public interface ISifreliPaketV2XOlusturDogrula
    {
        void Kapat();
    }

    public interface ISifreliPaketV2XOku
    {
        /// <summary>
        /// Şifreli pakete ait bileşenlerin verilerinin alınması için kullanılır.
        /// </summary>
        /// <param name="bilesenAction">
        /// Şifreli paket bileşenlerini almak için kullanılacak fonksiyondur.
        /// bool -> Kritik hata olup olmadığını belirtir.
        /// ISifreliPaketV2XOkuBilesen -> Bileşen verileridir.
        /// List -> Pakete ait tüm doğrulama hatalarını belirtir.
        /// </param>
        ISifreliPaketV2XOkuBilesenAl BilesenleriAl(Action<bool, ISifreliPaketV2XOkuBilesen, List<DogrulamaHatasi>> bilesenAction);
    }

    public interface ISifreliPaketV2XOkuBilesenAl
    {
        void Kapat();
    }

    public interface ISifreliPaketV2XOkuBilesen
    {
        /// <summary>
        /// Şifreli paketin iletileceği hedefleri barındıran nesneye ulaşılır.
        /// </summary>
        BelgeHedef BelgeHedef { get; set; }
        /// <summary>
        /// Şifreli paket içerisindeki BelgeHedef bileşenini STREAM olarak verir.
        /// </summary>
        Stream BelgeHedefAl();
        /// <summary>
        /// Şifreli paket içerisindeki BelgeHedef bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool BelgeHedefVarMi();

        /// <summary>
        /// Şifreli paket içerisinde imzalanan bileşenlere ait özet bilgilerinin bulunduğu nesneye ulaşılır.
        /// </summary>
        NihaiOzet NihaiOzet { get; set; }
        /// <summary>
        /// Şifreli paket içerisindeki NihaiOzet bileşenini STREAM olarak verir.
        /// </summary>
        Stream NihaiOzetAl();
        /// <summary>
        /// Şifreli paket içerisindeki NihaiOzet bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool NihaiOzetVarMi();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli içeriğe ilişkin bilgileri içeren nesneye ulaşılır.
        /// </summary>
        SifreliIcerikBilgisi SifreliIcerikBilgisi { get; set; }

        /// <summary>
        /// Şifreli paket içerisindeki SifreliIcerikBilgisi bileşeninin olup olmadığını gösterir.
        /// </summary>
        bool SifreliIcerikBilgisiVarMi();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli dosyayı STREAM olarak verir.
        /// </summary>
        Stream SifreliIcerikAl();

        /// <summary>
        /// Şifreli paket içerisindeki şifreli dosyanın adını döner.
        /// </summary>
        string SifreliIcerikDosyasiAdiAl();
    }
}
