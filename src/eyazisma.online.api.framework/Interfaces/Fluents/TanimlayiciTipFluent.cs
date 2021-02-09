using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface ITanimlayiciTipFluent : IDisposable, ITanimlayiciTipFluentDeger, ITanimlayiciTipFluentSemaID
    {
    }

    public interface ITanimlayiciTipFluentDeger
    {
        TanimlayiciTip Olustur();
    }

    public interface ITanimlayiciTipFluentSemaID
    {
        /// <remarks>Zorunlu alandır.</remarks>
        ITanimlayiciTipFluentDeger DegerAta(string value);
    }
}
