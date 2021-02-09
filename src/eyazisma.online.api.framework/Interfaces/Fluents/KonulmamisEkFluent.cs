using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IKonulmamisEkFluent : IDisposable, IKonulmamisEkFluentEkIdDeger
    {
    }

    public interface IKonulmamisEkFluentEkIdDeger
    {
        KonulmamisEk Olustur();
    }
}
