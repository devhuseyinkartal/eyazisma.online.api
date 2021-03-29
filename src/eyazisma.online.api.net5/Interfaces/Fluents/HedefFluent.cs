using System;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IHedefFluent : IDisposable, IHedefFluentOge
    {
    }

    public interface IHedefFluentOge
    {
        Hedef Olustur();
    }
}