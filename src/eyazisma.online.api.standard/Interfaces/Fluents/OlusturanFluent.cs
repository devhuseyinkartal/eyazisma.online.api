using System;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IOlusturanFluent : IDisposable, IOlusturanFluentItem
    {
    }

    public interface IOlusturanFluentItem
    {
        Olusturan Olustur();
    }
}