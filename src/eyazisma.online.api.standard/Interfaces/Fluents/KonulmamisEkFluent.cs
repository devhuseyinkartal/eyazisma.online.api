using System;
using eyazisma.online.api.Classes;

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