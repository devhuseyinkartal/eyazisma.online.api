using System;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IBelgeImzaFluent : IDisposable, IBelgeImzaFluentImza, IBelgeImzaFluentImzalar
    {
    }

    public interface IBelgeImzaFluentImza
    {
        BelgeImza Olustur();
    }

    public interface IBelgeImzaFluentImzalar
    {
        BelgeImza Olustur();
    }
}