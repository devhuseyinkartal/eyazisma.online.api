using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IIlgiliFluent : IDisposable, IIlgiliFluentItem { }

    public interface IIlgiliFluentItem
    {
        Ilgili Olustur();
    }
}
