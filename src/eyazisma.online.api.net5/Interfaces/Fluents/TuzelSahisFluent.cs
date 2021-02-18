using eyazisma.online.api.Classes;
using System;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface ITuzelSahisFluent : IDisposable, ITuzelSahisFluentId, ITuzelSahisFluentAd, ITuzelSahisFluentIletisimBilgisi
    {
    }

    public interface ITuzelSahisFluentId
    {
        /// <summary>
        /// Tüzel kişinin adıdır.
        /// </summary>
        /// <param name="ad">Tüzel kişinin ad değeridir. IsimTip tipinde olmalıdır.</param>
        ITuzelSahisFluentAd AdIle(IsimTip ad);
        TuzelSahis Olustur();
    }

    public interface ITuzelSahisFluentAd
    {
        /// <summary>
        /// Tüzel kişinin iletişim bilgisidir.
        /// </summary>
        /// <param name="iletisimBilgisi">Tüzel kişinin iletişim bilgisi değeridir. IletisimBilgisi tipinde olmalıdır.</param>
        ITuzelSahisFluentIletisimBilgisi IletisimBilgisiIle(IletisimBilgisi iletisimBilgisi);
        TuzelSahis Olustur();
    }

    public interface ITuzelSahisFluentIletisimBilgisi
    {
        TuzelSahis Olustur();
    }
}
