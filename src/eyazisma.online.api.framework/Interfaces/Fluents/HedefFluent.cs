using eyazisma.online.api.Classes;
using System;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IHedefFluent : IDisposable, IHedefFluentOge { }

    public interface IHedefFluentOge
    {
        Hedef Olustur();
    }
}
