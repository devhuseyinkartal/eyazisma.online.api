using eyazisma.online.api.framework.Classes;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IEkDEDFluent
    {
        /// <summary>
        /// Ekin paket içerisindeki tekil belirtecidir. Id değeri paketi oluşturan tarafından belirlenir.
        /// </summary>
        /// <param name="id">Ekin paket içerisindeki tekil belirtecinin değeridir. IdTip tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDEDFluentId IdAta(IdTip id);
    }

    public interface IEkDEDFluentId
    {
        /// <summary>
        /// Eklenen ek resmi bir belge ise bu alana söz konusu belgenin numarası verilir.
        /// </summary>
        /// <param name="belgeNo">Belge numarasının değeridir.</param>
        IEkDEDFluentBelgeNo BelgeNoIle(string belgeNo);
        /// <summary>
        /// Ekin dosya sistemindeki adıdır.
        /// </summary>
        /// <param name="dosyaAdi">Ekin dosya sistemindeki adının değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDEDFluentDosyaAdi DosyaAdiAta(string dosyaAdi);
    }

    public interface IEkDEDFluentBelgeNo
    {
        /// <summary>
        /// Ekin dosya sistemindeki adıdır.
        /// </summary>
        /// <param name="dosyaAdi">Ekin dosya sistemindeki adının değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDEDFluentDosyaAdi DosyaAdiAta(string dosyaAdi);
    }

    public interface IEkDEDFluentDosyaAdi
    {
        /// <summary>
        /// Eklenen dosyanın mime türü bilgisidir.
        /// </summary>
        /// <param name="mimeTuru">Eklenen dosyanın mime türü bilgisi değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDEDFluentMimeTuru MimeTuruAta(string mimeTuru);
    }

    public interface IEkDEDFluentMimeTuru
    {
        /// <summary>
        /// Ekin adıdır.
        /// </summary>
        /// <param name="ad">Ekin ad değeridir.</param>
        IEkDEDFluentAd AdIle(MetinTip ad);
        /// <summary>
        /// Belge üzerinde ek için belirtilen sıra bilgisidir.
        /// </summary>
        /// <param name="siraNo">Ek için belirtilen sıra bilgisinin değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDEDFluentSiraNo SiraNoAta(int siraNo);
    }

    public interface IEkDEDFluentAd
    {
        /// <summary>
        /// Belge üzerinde ek için belirtilen sıra bilgisidir.
        /// </summary>
        /// <param name="siraNo">Ek için belirtilen sıra bilgisinin değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDEDFluentSiraNo SiraNoAta(int siraNo);
    }

    public interface IEkDEDFluentSiraNo
    {
        /// <summary>
        /// Eke ait açıklamalardır.
        /// </summary>
        /// <param name="aciklama">Eke ait açıklamaların değeridir.</param>
        IEkDEDFluentAciklama AciklamaIle(MetinTip aciklama);
        /// <summary>
        /// Ekin üretildiği sistemdeki tekil anahtar değeridir. 
        /// </summary>
        /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        IEkDEDFluentOzId OzIdIle(TanimlayiciTip ozId);

        /// <summary>
        /// Ekin paket içerisinde imzalı olarak bulunup bulunmadığı bilgisidir.
        /// </summary>
        /// <param name="imzaliMi">Ekin paket içerisinde imzalı olarak bulunup bulunmadığı bilgisinin değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDEDFluentImzaliMi ImzaliMiAta(bool imzaliMi);
    }

    public interface IEkDEDFluentAciklama
    {
        /// <summary>
        /// Ekin üretildiği sistemdeki tekil anahtar değeridir. 
        /// </summary>
        /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        IEkDEDFluentOzId OzIdIle(TanimlayiciTip ozId);

        /// <summary>
        /// Ekin paket içerisinde imzalı olarak bulunup bulunmadığı bilgisidir.
        /// </summary>
        /// <param name="imzaliMi">Ekin paket içerisinde imzalı olarak bulunup bulunmadığı bilgisinin değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDEDFluentImzaliMi ImzaliMiAta(bool imzaliMi);
    }

    public interface IEkDEDFluentOzId
    {

        /// <summary>
        /// Ekin paket içerisinde imzalı olarak bulunup bulunmadığı bilgisidir.
        /// </summary>
        /// <param name="imzaliMi">Ekin paket içerisinde imzalı olarak bulunup bulunmadığı bilgisinin değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDEDFluentImzaliMi ImzaliMiAta(bool imzaliMi);
    }

    public interface IEkDEDFluentImzaliMi
    {
        Ek Olustur();
    }

    public interface IEkFZKFluent
    {
        /// <summary>
        /// Ekin paket içerisindeki tekil belirtecidir. Id değeri paketi oluşturan tarafından belirlenir.
        /// </summary>
        /// <param name="id">Ekin paket içerisindeki tekil belirtecinin değeridir. IdTip tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkFZKFluentId IdAta(IdTip id);
    }

    public interface IEkFZKFluentId
    {
        /// <summary>
        /// Eklenen ek resmi bir belge ise bu alana söz konusu belgenin numarası verilir.
        /// </summary>
        /// <param name="belgeNo">Belge numarasının değeridir.</param>
        IEkFZKFluentBelgeNo BelgeNoIle(string belgeNo);
        /// <summary>
        /// Ekin adıdır.
        /// </summary>
        /// <param name="ad">Ekin ad değeridir.</param>
        IEkFZKFluentAd AdIle(MetinTip ad);
        /// <summary>
        /// Belge üzerinde ek için belirtilen sıra bilgisidir.
        /// </summary>
        /// <param name="siraNo">Ek için belirtilen sıra bilgisinin değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkFZKFluentSiraNo SiraNoAta(int siraNo);
    }

    public interface IEkFZKFluentBelgeNo
    {
        /// <summary>
        /// Ekin adıdır.
        /// </summary>
        /// <param name="ad">Ekin ad değeridir.</param>
        IEkFZKFluentAd AdIle(MetinTip ad);
        /// <summary>
        /// Belge üzerinde ek için belirtilen sıra bilgisidir.
        /// </summary>
        /// <param name="siraNo">Ek için belirtilen sıra bilgisinin değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkFZKFluentSiraNo SiraNoAta(int siraNo);
    }

    public interface IEkFZKFluentAd
    {
        /// <summary>
        /// Belge üzerinde ek için belirtilen sıra bilgisidir.
        /// </summary>
        /// <param name="siraNo">Ek için belirtilen sıra bilgisinin değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkFZKFluentSiraNo SiraNoAta(int siraNo);
    }

    public interface IEkFZKFluentSiraNo
    {
        /// <summary>
        /// Eke ait açıklamalardır.
        /// </summary>
        /// <param name="aciklama">Eke ait açıklamaların değeridir.</param>
        IEkFZKFluentAciklama AciklamaIle(MetinTip aciklama);
        Ek Olustur();
    }

    public interface IEkFZKFluentAciklama
    {
        Ek Olustur();
    }

    public interface IEkHRFFluent
    {
        /// <summary>
        /// Ekin paket içerisindeki tekil belirtecidir. Id değeri paketi oluşturan tarafından belirlenir.
        /// </summary>
        /// <param name="id">Ekin paket içerisindeki tekil belirtecinin değeridir. IdTip tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkHRFFluentId IdAta(IdTip id);
    }

    public interface IEkHRFFluentId
    {
        /// <summary>
        /// Eklenen ek resmi bir belge ise bu alana söz konusu belgenin numarası verilir.
        /// </summary>
        /// <param name="belgeNo">Belge numarasının değeridir.</param>
        IEkHRFFluentBelgeNo BelgeNoIle(string belgeNo);
        /// <summary>
        /// Ekin adıdır.
        /// </summary>
        /// <param name="ad">Ekin ad değeridir.</param>
        IEkHRFFluentAd AdIle(MetinTip ad);
        /// <summary>
        /// Belge üzerinde ek için belirtilen sıra bilgisidir.
        /// </summary>
        /// <param name="siraNo">Ek için belirtilen sıra bilgisinin değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkHRFFluentSiraNo SiraNoAta(int siraNo);
    }

    public interface IEkHRFFluentBelgeNo
    {
        /// <summary>
        /// Ekin adıdır.
        /// </summary>
        /// <param name="ad">Ekin ad değeridir.</param>
        IEkHRFFluentAd AdIle(MetinTip ad);
        /// <summary>
        /// Belge üzerinde ek için belirtilen sıra bilgisidir.
        /// </summary>
        /// <param name="siraNo">Ek için belirtilen sıra bilgisinin değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkHRFFluentSiraNo SiraNoAta(int siraNo);
    }

    public interface IEkHRFFluentAd
    {
        /// <summary>
        /// Belge üzerinde ek için belirtilen sıra bilgisidir.
        /// </summary>
        /// <param name="siraNo">Ek için belirtilen sıra bilgisinin değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkHRFFluentSiraNo SiraNoAta(int siraNo);
    }

    public interface IEkHRFFluentSiraNo
    {
        /// <summary>
        /// Eke ait açıklamalardır.
        /// </summary>
        /// <param name="aciklama">Eke ait açıklamaların değeridir.</param>
        IEkHRFFluentAciklama AciklamaIle(MetinTip aciklama);
        /// <summary>
        /// Ekin kaynağını gösteren URI değeridir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkHRFFluentReferans ReferansAta(string referans);
    }

    public interface IEkHRFFluentAciklama
    {
        /// <summary>
        /// Ekin kaynağını gösteren URI değeridir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkHRFFluentReferans ReferansAta(string referans);
    }

    public interface IEkHRFFluentReferans
    {
        /// <summary>
        /// Ekin üretildiği sistemdeki tekil anahtar değeridir. 
        /// </summary>
        /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        IEkHRFFluentOzId OzIdIle(TanimlayiciTip ozId);
        Ek Olustur();
    }

    public interface IEkHRFFluentOzId
    {
        /// <summary>
        /// Referans verilmiş belgenin özet değerini barındırır.
        /// </summary>
        /// <param name="ozet">Referans verilmiş belgenin özet değeridir. Ozet tipinde olmalıdır.</param>
        IEkHRFFluentOzet OzetIle(Ozet ozet);
        Ek Olustur();
    }

    public interface IEkHRFFluentOzet
    {
        Ek Olustur();
    }
}
