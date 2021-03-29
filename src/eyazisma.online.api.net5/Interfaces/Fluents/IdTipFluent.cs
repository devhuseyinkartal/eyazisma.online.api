using System;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IIdTipFluent : IDisposable, IIdTipFluentDeger, IIdTipFluentEYazismaId
    {
    }

    public interface IIdTipFluentDeger
    {
        /// <summary>
        ///     Paket içerisindeki ilgi veya ekin başka bir e-Yazışma Paketi veya başka bir e-Yazışma Paketi'nin eki olup
        ///     olmadığını belirtir.
        /// </summary>
        /// <param name="eYazismaIdMi">Boolean tipinde olmalıdır.</param>
        IIdTipFluentEYazismaId EYazismaIdMiAta(bool eYazismaIdMi);

        IdTip Olustur();
    }

    public interface IIdTipFluentEYazismaId
    {
        IdTip Olustur();
    }
}