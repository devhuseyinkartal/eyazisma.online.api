using eyazisma.online.api.Classes;
using System;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IDogrulamaBilgisiFluent : IDisposable, IDogrulamaBilgisiFluentAdres
    {
    }

    public interface IDogrulamaBilgisiFluentAdres
    {
        DogrulamaBilgisi Olustur();
    }
}
