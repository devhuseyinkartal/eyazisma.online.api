using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
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
