using System;
using eyazisma.online.api.Classes;

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