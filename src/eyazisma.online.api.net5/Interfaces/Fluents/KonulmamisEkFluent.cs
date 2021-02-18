using eyazisma.online.api.Classes;
using System;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IKonulmamisEkFluent : IDisposable, IKonulmamisEkFluentEkIdDeger
    {
    }

    public interface IKonulmamisEkFluentEkIdDeger
    {
        KonulmamisEk Olustur();
    }
}
