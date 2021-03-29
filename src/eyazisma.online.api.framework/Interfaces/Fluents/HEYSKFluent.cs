using System;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IHEYSKFluent : IDisposable,
        IHEYSKFluentKod,
        IHEYSKFluentAd,
        IHEYSKFluentTanim
    {
    }

    public interface IHEYSKFluentKod
    {
        /// <summary>
        ///     Hizmet Envanteri Yönetim Sisteminde tanımlanmış hizmetin adıdır.
        /// </summary>
        /// <param name="ad">HEYS hizmet adı değeridir.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IHEYSKFluentAd AdAta(string ad);
    }

    public interface IHEYSKFluentAd
    {
        /// <summary>
        ///     Hizmet Envanteri Yönetim Sisteminde yer alan hizmetin tanımıdır.
        /// </summary>
        /// <param name="tanim">HEYS hizmet tanımı değeridir.</param>
        IHEYSKFluentTanim TanimIle(string tanim);

        HEYSK Olustur();
    }

    public interface IHEYSKFluentTanim
    {
        HEYSK Olustur();
    }
}