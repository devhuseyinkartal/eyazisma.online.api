using System;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IIsimTipFluent : IDisposable, IIsimTipFluentDeger, IIsimTipFluentDilID
    {
    }

    public interface IIsimTipFluentDeger
    {
        /// <summary>
        ///     DilID değerinin atanması için kullanılır.
        /// </summary>
        /// <param name="dilID">DilID değeridir.</param>
        IIsimTipFluentDilID DilIDIle(string dilID);

        IsimTip Olustur();
    }

    public interface IIsimTipFluentDilID
    {
        IsimTip Olustur();
    }
}