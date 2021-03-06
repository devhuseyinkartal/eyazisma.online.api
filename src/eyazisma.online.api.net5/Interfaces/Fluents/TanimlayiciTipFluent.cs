﻿using System;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
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