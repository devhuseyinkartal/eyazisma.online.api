using eyazisma.online.api.Classes;
using System;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IKurumKurulusFluent : IDisposable,
                                            IKurumKurulusFluentV1X,
                                            IKurumKurulusFluentV2X,
                                            IKurumKurulusFluentV1XKKK,
                                            IKurumKurulusFluentV2XKKK,
                                            IKurumKurulusFluentV2XBirimKKK,
                                            IKurumKurulusFluentAd,
                                            IKurumKurulusFluentIletisimBilgisi
    {
    }

    public interface IKurumKurulusFluentV1X
    {
        /// <summary>
        /// Kurum/kuruluşun DETSİS'te yer alan Türkiye Cumhuriyeti Devlet Teşkilatı Numarasıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        IKurumKurulusFluentV1XKKK KKKAta(string kkk);
    }

    public interface IKurumKurulusFluentV2X
    {
        /// <summary>
        /// Kurum/kuruluşun DETSİS'te yer alan Türkiye Cumhuriyeti Devlet Teşkilatı Numarasıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        IKurumKurulusFluentV2XKKK KKKAta(string kkk);
    }

    public interface IKurumKurulusFluentV1XKKK
    {
        /// <summary>
        /// Kurum / kuruluşun adıdır.
        /// </summary>
        /// <remarks>Bu alanın kullanılması durumunda DETSİS’teki kurum adı kullanılmalıdır.</remarks>
        IKurumKurulusFluentAd AdIle(IsimTip ad);
        /// <summary>
        /// Kurum / kuruluşun iletişim bilgisidir.
        /// </summary>
        IKurumKurulusFluentIletisimBilgisi IletisimBilgisiIle(IletisimBilgisi iletisimBilgisi);
        KurumKurulus Olustur();
    }

    public interface IKurumKurulusFluentV2XKKK
    {
        /// <summary>
        /// Paketi oluşturan veya muhatap alt birimlerin, DETSİS'te yer alan Türkiye Cumhuriyeti Devlet Teşkilatı Numarasıdır.
        /// </summary>
        /// <remarks>Only for version 2.0</remarks>
        IKurumKurulusFluentV2XBirimKKK BirimKKKIle(string birimKKK);
        /// <summary>
        /// Kurum / kuruluşun adıdır.
        /// </summary>
        /// <remarks>Bu alanın kullanılması durumunda DETSİS’teki kurum adı kullanılmalıdır.</remarks>
        IKurumKurulusFluentAd AdIle(IsimTip ad);
        /// <summary>
        /// Kurum / kuruluşun iletişim bilgisidir.
        /// </summary>
        IKurumKurulusFluentIletisimBilgisi IletisimBilgisiIle(IletisimBilgisi iletisimBilgisi);
        KurumKurulus Olustur();
    }

    public interface IKurumKurulusFluentV2XBirimKKK
    {
        /// <summary>
        /// Kurum / kuruluşun adıdır.
        /// </summary>
        /// <remarks>Bu alanın kullanılması durumunda DETSİS’teki kurum adı kullanılmalıdır.</remarks>
        IKurumKurulusFluentAd AdIle(IsimTip ad);
        // <summary>
        /// Kurum / kuruluşun iletişim bilgisidir.
        /// </summary>
        IKurumKurulusFluentIletisimBilgisi IletisimBilgisiIle(IletisimBilgisi iletisimBilgisi);
        KurumKurulus Olustur();
    }

    public interface IKurumKurulusFluentAd
    {
        // <summary>
        /// Kurum / kuruluşun iletişim bilgisidir.
        /// </summary>
        IKurumKurulusFluentIletisimBilgisi IletisimBilgisiIle(IletisimBilgisi iletisimBilgisi);
        KurumKurulus Olustur();
    }

    public interface IKurumKurulusFluentIletisimBilgisi
    {
        KurumKurulus Olustur();
    }
}
