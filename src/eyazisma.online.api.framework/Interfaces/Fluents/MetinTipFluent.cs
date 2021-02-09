using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IMetinTipFluent : IDisposable, IMetinTipFluentDeger, IMetinTipFluentDilID { }

    public interface IMetinTipFluentDeger
    {
        /// <summary>
        /// DilID değerinin atanması için kullanılır.
        /// </summary>
        /// <param name="dilID">DilID değeridir.</param>
        IMetinTipFluentDilID DilIDIle(string dilID);
        MetinTip Olustur();
    }

    public interface IMetinTipFluentDilID
    {
        MetinTip Olustur();
    }
}
