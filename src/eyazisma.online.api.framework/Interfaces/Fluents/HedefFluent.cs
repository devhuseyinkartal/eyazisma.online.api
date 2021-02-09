using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IHedefFluent : IDisposable, IHedefFluentOge { }

    public interface IHedefFluentOge
    {
        Hedef Olustur();
    }
}
