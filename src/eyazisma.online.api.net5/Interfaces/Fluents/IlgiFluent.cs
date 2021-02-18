using eyazisma.online.api.Classes;
using System;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IIlgiFluent : IDisposable,
        IIlgiFluentId,
        IIlgiFluentBelgeNo,
        IIlgiFluentTarih,
        IIlgiFluentEtiket,
        IIlgiFluentEkIdDeger,
        IIlgiFluentAd,
        IIlgiFluentAciklama,
        IIlgiFluentOzId
    {
    }

    public interface IIlgiFluentId
    {
        /// <summary>
        /// İlgi, resmi bir yazı ise bu alana söz konusu belgenin numarası verilir.
        /// </summary>
        /// <param name="belgeNo">Belge numarasının değeridir.</param>
        IIlgiFluentBelgeNo BelgeNoIle(string belgeNo);
        /// <summary>
        /// İlgi yazının tarihidir.
        /// </summary>
        /// <param name="tarih">İlgi yazının tarih değeridir.</param>
        IIlgiFluentTarih TarihIle(DateTime tarih);
        /// <summary>
        /// Eklenen ek dosyasının etiketidir. (İlgi a ve ilgi b gibi ilgiler için etiket değerleri sırasıyla "a" ve "b" olmalıdır.)
        /// </summary>
        /// <param name="etiket">Eklenen ek dosyasının etiket değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IIlgiFluentEtiket EtiketAta(char etiket);
    }

    public interface IIlgiFluentBelgeNo
    {
        /// <summary>
        /// İlgi yazının tarihidir.
        /// </summary>
        /// <param name="tarih">İlgi yazının tarih değeridir.</param>
        IIlgiFluentTarih TarihIle(DateTime tarih);
        /// <summary>
        /// Eklenen ek dosyasının etiketidir. (İlgi a ve ilgi b gibi ilgiler için etiket değerleri sırasıyla "a" ve "b" olmalıdır.)
        /// </summary>
        /// <param name="etiket">Eklenen ek dosyasının etiket değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IIlgiFluentEtiket EtiketAta(char etiket);
    }

    public interface IIlgiFluentTarih
    {
        /// <summary>
        /// Eklenen ek dosyasının etiketidir. (İlgi a ve ilgi b gibi ilgiler için etiket değerleri sırasıyla "a" ve "b" olmalıdır.)
        /// </summary>
        /// <param name="etiket">Eklenen ek dosyasının etiket değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IIlgiFluentEtiket EtiketAta(char etiket);
    }

    public interface IIlgiFluentEtiket
    {
        /// <summary>
        /// İlginin paket içerisinde ek olarak eklenmesi durumunda, ilgili ekin tekil anahtarı değeri bu alana verilir.
        /// </summary>
        /// <param name="ekIdDeger">İlgili ekin tekil anahtar değeridir. Guid tipinde olmalıdır.</param>
        IIlgiFluentEkIdDeger EkIdDegerIle(Guid ekIdDeger);
        /// <summary>
        ///  İlgi adıdır.
        /// </summary>
        /// <param name="ad">İlgi adı değeridir.</param>
        IIlgiFluentAd AdIle(MetinTip ad);
        /// <summary>
        /// İlgiye ait açıklamalardır.
        /// </summary>
        /// <param name="aciklama">İlgiye ait açıklamanın değeridir.</param>
        IIlgiFluentAciklama AciklamaIle(MetinTip aciklama);
        /// <summary>
        /// İlginin üretildiği sistemdeki tekil anahtar değeridir. 
        /// </summary>
        /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        IIlgiFluentOzId OzIdIle(TanimlayiciTip ozId);
        Ilgi Olustur();
    }

    public interface IIlgiFluentEkIdDeger
    {
        /// <summary>
        ///  İlgi adıdır.
        /// </summary>
        /// <param name="ad">İlgi adı değeridir.</param>
        IIlgiFluentAd AdIle(MetinTip ad);
        /// <summary>
        /// İlgiye ait açıklamalardır.
        /// </summary>
        /// <param name="aciklama">İlgiye ait açıklamanın değeridir.</param>
        IIlgiFluentAciklama AciklamaIle(MetinTip aciklama);
        /// <summary>
        /// İlginin üretildiği sistemdeki tekil anahtar değeridir. 
        /// </summary>
        /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        IIlgiFluentOzId OzIdIle(TanimlayiciTip ozId);
        Ilgi Olustur();
    }

    public interface IIlgiFluentAd
    {
        /// <summary>
        /// İlgiye ait açıklamalardır.
        /// </summary>
        /// <param name="aciklama">İlgiye ait açıklamanın değeridir.</param>
        IIlgiFluentAciklama AciklamaIle(MetinTip aciklama);
        /// <summary>
        /// İlginin üretildiği sistemdeki tekil anahtar değeridir. 
        /// </summary>
        /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        IIlgiFluentOzId OzIdIle(TanimlayiciTip ozId);
        Ilgi Olustur();
    }

    public interface IIlgiFluentAciklama
    {
        /// <summary>
        /// İlginin üretildiği sistemdeki tekil anahtar değeridir. 
        /// </summary>
        /// <param name="ozId">TanimlayiciTip tipinde olmalıdır.</param>
        /// <remarks>
        /// Tekil anahtar değeri için kullanılan veri türü/şeması, elemanın SemaID alanında verilir.
        /// Elemanın boş bırakılması ekin elektronik bir sistemde üretilmediği anlamına gelir.
        /// OzId değeri verilmesi durumunda, SemaID değerinin verilmesi zorunludur.
        /// </remarks>
        IIlgiFluentOzId OzIdIle(TanimlayiciTip ozId);
        Ilgi Olustur();
    }

    public interface IIlgiFluentOzId
    {
        Ilgi Olustur();
    }
}
