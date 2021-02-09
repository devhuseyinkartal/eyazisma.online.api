using eyazisma.online.api.Classes;
using System;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IUstYaziFluent : IDisposable,
                                       IUstYaziFluentMimeTuru,
                                       IUstYaziFluentDosya,
                                       IUstYaziFluentDosyaAdi
    {
    }

    public interface IUstYaziFluentDosya
    {
        /// <summary>
        /// Elektronik dosyanın adıdır.
        /// </summary>
        /// <param name="dosyaAdi">Elektronik dosyanın ad değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstYaziFluentDosyaAdi DosyaAdiAta(string dosyaAdi);
    }

    public interface IUstYaziFluentDosyaAdi
    {
        /// <summary>
        /// Elektronik dosyanın dosya kimlik tanımlayıcısıdır.
        /// </summary>
        /// <param name="mimeTuru">Elektronik dosyanın dosya kimlik tanımlayıcı değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IUstYaziFluentMimeTuru MimeTuruAta(string mimeTuru);
    }

    public interface IUstYaziFluentMimeTuru
    {
        UstYazi Olustur();
    }
}
