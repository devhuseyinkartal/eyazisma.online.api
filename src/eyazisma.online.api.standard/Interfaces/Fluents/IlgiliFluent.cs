using System;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IIlgiliFluent : IDisposable, IIlgiliFluentItem
    {
    }

    public interface IIlgiliFluentItem
    {
        Ilgili Olustur();
    }
}