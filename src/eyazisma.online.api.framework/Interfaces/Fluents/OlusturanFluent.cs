using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IOlusturanFluent : IDisposable, IOlusturanFluentItem { }

    public interface IOlusturanFluentItem
    {
        Olusturan Olustur();
    }
}
