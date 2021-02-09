using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IKisiFluent : IDisposable,
                                   IKisiFluentIlkAdi,
                                   IKisiFluentIkinciAdi,
                                   IKisiFluentOnEk,
                                   IKisiFluentSoyadi,
                                   IKisiFluentUnvan
    { }

    public interface IKisiFluentIlkAdi
    {
        /// <summary>
        /// Kişinin ikinci adıdır.
        /// </summary>
        /// <param name="ikinciAdi">Kişinin ikinci adının değeridir. IsimTip tipinde olmalıdır.</param>
        IKisiFluentIkinciAdi IkinciAdiIle(IsimTip ikinciAdi);
        /// <summary>
        /// Kişinin soyadıdır.
        /// </summary>
        /// <param name="soyadi">Kişinin soyadının değeridir. IsimTip tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IKisiFluentSoyadi SoyadiAta(IsimTip soyadi);
    }

    public interface IKisiFluentIkinciAdi
    {
        /// <summary>
        /// Kişinin soyadıdır.
        /// </summary>
        /// <param name="soyadi">Kişinin soyadının değeridir. IsimTip tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IKisiFluentSoyadi SoyadiAta(IsimTip soyadi);
    }

    public interface IKisiFluentSoyadi
    {
        /// <summary>
        /// Kişinin unvanıdır.
        /// </summary>
        /// <param name="unvan">Kişinin unvan bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
        IKisiFluentUnvan UnvanIle(IsimTip unvan);
        /// <summary>
        /// Kişinin isminde kullandığı ön ektir.
        /// </summary>
        /// <param name="onEk">Kişinin isminde kullandığı ön ekin değeridir. MetinTip tipinde olmalıdır.</param>
        IKisiFluentOnEk OnEkIle(MetinTip onEk);
        Kisi Olustur();
    }

    public interface IKisiFluentUnvan
    {
        /// <summary>
        /// Kişinin isminde kullandığı ön ektir.
        /// </summary>
        /// <param name="onEk">Kişinin isminde kullandığı ön ekin değeridir. MetinTip tipinde olmalıdır.</param>
        IKisiFluentOnEk OnEkIle(MetinTip onEk);
        Kisi Olustur();
    }

    public interface IKisiFluentOnEk
    {
        Kisi Olustur();
    }
}
