using eyazisma.online.api.Classes;
using System;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IIlgiliFluent : IDisposable, IIlgiliFluentItem { }

    public interface IIlgiliFluentItem
    {
        Ilgili Olustur();
    }
}
