using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface ISDPFluent : IDisposable,
                                   ISDPFluentKod,
                                   ISDPFluentAd,
                                   ISDPFluentAciklama
    {
    }

    public interface ISDPFluentKod
    {
        /// <summary>
        /// Standart Dosya Planı adıdır.
        /// </summary>
        /// <param name="ad">SDP adı değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        ISDPFluentAd AdAta(string ad);
    }

    public interface ISDPFluentAd
    {
        /// <summary>
        /// Standart Dosya Planı açıklamasıdır.
        /// </summary>
        /// <param name="aciklama">SDP açıklaması değeridir.</param>
        ISDPFluentAciklama AciklamaIle(string aciklama);
        SDP Olustur();
    }

    public interface ISDPFluentAciklama
    {
        SDP Olustur();
    }
}
