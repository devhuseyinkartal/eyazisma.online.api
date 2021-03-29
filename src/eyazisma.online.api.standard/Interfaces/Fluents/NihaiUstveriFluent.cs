using System;
using System.Collections.Generic;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface INihaiUstveriFluent : IDisposable,
        INihaiUstveriFluentTarih,
        INihaiUstveriFluentBelgeNo,
        INihaiUstveriFluentImza,
        INihaiUstveriFluentImzalar
    {
    }

    public interface INihaiUstveriFluentTarih
    {
        /// <summary>
        ///     Belge numarasıdır.
        /// </summary>
        /// <param name="belgeNo">Belge numarası değeridir.</param>
        /// <remarks>
        ///     Zorunlu alandır.
        ///     Resmi yazışmalara ilişkin mevzuatta belirtilen biçime uygun olmalıdır.
        /// </remarks>
        INihaiUstveriFluentBelgeNo BelgeNoAta(string belgeNo);
    }

    public interface INihaiUstveriFluentBelgeNo
    {
        /// <summary>
        ///     Belgenin üzerindeki her bir imzaya ait bilgilerdir.
        /// </summary>
        /// <param name="belgeImza">İmza değeridir. Imza tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        INihaiUstveriFluentImza BelgeImzaAta(Imza belgeImza);

        /// <summary>
        ///     Belgenin üzerindeki imzalara ait bilgilerdir.
        /// </summary>
        /// <param name="belgeImzalar">İmzalar değeridir. Imza listesi tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        INihaiUstveriFluentImzalar BelgeImzalarAta(List<Imza> belgeImzalar);
    }

    public interface INihaiUstveriFluentImza
    {
        /// <summary>
        ///     Belgenin üzerindeki her bir imzaya ait bilgilerdir.
        /// </summary>
        /// <param name="belgeImza">İmza değeridir. Imza tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        INihaiUstveriFluentImza BelgeImzaAta(Imza belgeImza);

        /// <summary>
        ///     Belgenin üzerindeki imzalara ait bilgilerdir.
        /// </summary>
        /// <param name="belgeImzalar">İmzalar değeridir. Imza listesi tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        INihaiUstveriFluentImzalar BelgeImzalarAta(List<Imza> belgeImzalar);

        NihaiUstveri Olustur();
    }

    public interface INihaiUstveriFluentImzalar
    {
        NihaiUstveri Olustur();
    }
}