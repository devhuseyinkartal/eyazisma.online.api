using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IIletisimBilgisiFluent : IIletisimBilgisiFluentV1X, IIletisimBilgisiFluentV2X, IDisposable
    {
    }

    public interface IIletisimBilgisiFluentV1X
    {
        /// <summary>
        /// Telefon numarasıdır.
        /// </summary>
        /// <param name="telefon">Telefon numarası değeridir. String tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV1X TelefonIle(string telefon);
        /// <summary>
        /// Diğer telefon bilgisidir.
        /// </summary>
        /// <param name="telefonDiger">Diğer telefon bilgisi değeridir. String tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV1X TelefonDigerIle(string telefonDiger);
        /// <summary>
        /// e-Posta bilgisidir.
        /// </summary>
        /// <param name="ePosta">e-Posta bilgisi değeridir. String tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV1X EPostaIle(string ePosta);
        /// <summary>
        /// Faks numarasıdır.
        /// </summary>
        /// <param name="faks">Faks numarası değeridir. String tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV1X FaksIle(string faks);
        /// <summary>
        /// İnternet adresi bilgisidir.
        /// </summary>
        /// <param name="webAdresi">İnternet adresi bilgisi değeridir. String tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV1X WebAdresiIle(string webAdresi);
        /// <summary>
        /// Açık adres bilgisidir.
        /// </summary>
        /// <param name="adres">Açık adres bilgisi değeridir. MetinTip tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV1X AdresIle(MetinTip adres);
        /// <summary>
        /// İlçe bilgisidir.
        /// </summary>
        /// <param name="ilce">İlçe bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV1X IlceIle(IsimTip ilce);
        /// <summary>
        /// İl bilgisidir.
        /// </summary>
        /// <param name="il">İl bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV1X IlIle(IsimTip il);
        /// <summary>
        /// Ülke bilgisidir.
        /// </summary>
        /// <param name="ulke">Ülke bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV1X UlkeIle(IsimTip ulke);
        IletisimBilgisi Olustur();
    }

    public interface IIletisimBilgisiFluentV2X
    {
        /// <summary>
        /// Telefon numarasıdır.
        /// </summary>
        /// <param name="telefon">Telefon numarası değeridir. String tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV2X TelefonIle(string telefon);
        /// <summary>
        /// Diğer telefon bilgisidir.
        /// </summary>
        /// <param name="telefonDiger">Diğer telefon bilgisi değeridir. String tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV2X TelefonDigerIle(string telefonDiger);
        /// <summary>
        /// e-Posta bilgisidir.
        /// </summary>
        /// <param name="ePosta">e-Posta bilgisi değeridir. String tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV2X EPostaIle(string ePosta);
        /// <summary>
        /// Faks numarasıdır.
        /// </summary>
        /// <param name="faks">Faks numarası değeridir. String tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV2X FaksIle(string faks);
        /// <summary>
        /// İnternet adresi bilgisidir.
        /// </summary>
        /// <param name="webAdresi">İnternet adresi bilgisi değeridir. String tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV2X WebAdresiIle(string webAdresi);
        /// <summary>
        /// KEP adresi bilgisidir.
        /// </summary>
        /// <param name="kepAdresi">KEP adresi bilgisi değeridir. String tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV2X KepAdresiIle(string kepAdresi);
        /// <summary>
        /// Açık adres bilgisidir.
        /// </summary>
        /// <param name="adres">Açık adres bilgisi değeridir. MetinTip tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV2X AdresIle(MetinTip adres);
        /// <summary>
        /// İlçe bilgisidir.
        /// </summary>
        /// <param name="ilce">İlçe bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV2X IlceIle(IsimTip ilce);
        /// <summary>
        /// İl bilgisidir.
        /// </summary>
        /// <param name="il">İl bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV2X IlIle(IsimTip il);
        /// <summary>
        /// Ülke bilgisidir.
        /// </summary>
        /// <param name="ulke">Ülke bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
        IIletisimBilgisiFluentV2X UlkeIle(IsimTip ulke);
        IletisimBilgisi Olustur();
    }
}
