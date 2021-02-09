using eyazisma.online.api.framework.Classes;
using System;
using System.IO;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IEkDosyaFluent : IDisposable,
                                      IEkDosyaFluentEk,
                                       IEkDosyaFluentDosya,
                                       IEkDosyaFluentDosyaAdi
    {
    }

    public interface IEkDosyaFluentEk
    {
        /// <summary>
        /// Elektronik dosyanın dijital verisidir.
        /// </summary>
        /// <param name="dosyaStream">Elektronik dosyanın dijital veri değeridir. Stream tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDosyaFluentDosya DosyaAta(Stream dosyaStream);

        /// <summary>
        /// Elektronik dosyanın dijital verisidir.
        /// </summary>
        /// <param name="dosyaYolu">Elektronik dosyanın dosya sistemindeki yoludur.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDosyaFluentDosya DosyaAta(string dosyaYolu);

    }

    public interface IEkDosyaFluentDosya
    {
        /// <summary>
        /// Elektronik dosyanın adıdır.
        /// </summary>
        /// <param name="dosyaAdi">Elektronik dosyanın ad değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IEkDosyaFluentDosyaAdi DosyaAdiAta(string dosyaAdi);
    }

    public interface IEkDosyaFluentDosyaAdi
    {
        EkDosya Olustur();
    }
}
