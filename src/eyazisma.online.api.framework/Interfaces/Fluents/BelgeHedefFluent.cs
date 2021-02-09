using eyazisma.online.api.Classes;
using System;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IBelgeHedefFluent : IDisposable, IBelgeHedefFluentHedef, IBelgeHedefFluentHedefler
    {
    }

    public interface IBelgeHedefFluentHedef
    {
        /// <summary>
        /// Belgenin iletileceği taraf bilgisidir.
        /// </summary>
        /// <param name="hedef">Taraf bilgisi değeridir. Hedef tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IBelgeHedefFluentHedef DigerHedefEkle(Hedef hedef);
        BelgeHedef Olustur();
    }

    public interface IBelgeHedefFluentHedefler
    {
        BelgeHedef Olustur();
    }
}
