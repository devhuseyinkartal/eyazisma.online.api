using System;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IOzetFluent : IDisposable, IOzetFluentAlgoritma, IOzetFluentDeger
    {
    }

    public interface IOzetFluentAlgoritma
    {
        /// <summary>
        ///     Özetin değeridir.
        /// </summary>
        /// <param name="ozetDegeri">ByteArray tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IOzetFluentDeger OzetDegeriAta(byte[] ozetDegeri);
    }

    public interface IOzetFluentDeger
    {
        Ozet Olustur();
    }
}