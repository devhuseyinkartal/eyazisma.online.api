using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IDogrulamaBilgisiFluent : IDisposable, IDogrulamaBilgisiFluentAdres
    {
    }

    public interface IDogrulamaBilgisiFluentAdres
    {
        DogrulamaBilgisi Olustur();
    }
}
