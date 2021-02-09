using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IGercekSahisFluent : IDisposable,
                                           IGercekSahisFluentKisi,
                                           IGercekSahisFluentTCKN,
                                           IGercekSahisFluentGorev,
                                           IGercekSahisFluentIletisim
    {
    }

    public interface IGercekSahisFluentKisi
    {
        /// <summary>
        /// Kişinin T.C. kimlik numarasının atanması için kullanılır.
        /// </summary>
        /// <param name="tckn">Kişinin T.C. kimlik numarası değeridir. String tipinde olmalıdır.</param>
        IGercekSahisFluentTCKN TCKNIle(string tckn);
        /// <summary>
        /// Kişinin görev bilgisi değerinin atanması için kullanılır.
        /// </summary>
        /// <param name="gorev">Kişinin görev bilgisi değeridir. MetinTip tipinde olmalıdır.</param>
        IGercekSahisFluentGorev GorevIle(MetinTip gorev);
        /// <summary>
        /// Kişiye ait iletişim bilgisi değerinin atanması için kullanılır.
        /// </summary>
        /// <param name="iletisimBilgisi">Kişiye ait iletişim bilgisi değeridir. IletisimBilgisi tipinde olmalıdır.</param>
        IGercekSahisFluentIletisim IletisimBilgisiIle(IletisimBilgisi iletisimBilgisi);
        GercekSahis Olustur();
    }

    public interface IGercekSahisFluentTCKN
    {
        /// <summary>
        /// Kişinin görev bilgisi değerinin atanması için kullanılır.
        /// </summary>
        /// <param name="gorev">Kişinin görev bilgisi değeridir. MetinTip tipinde olmalıdır.</param>
        IGercekSahisFluentGorev GorevIle(MetinTip gorev);
        /// <summary>
        /// Kişiye ait iletişim bilgisi değerinin atanması için kullanılır.
        /// </summary>
        /// <param name="iletisimBilgisi">Kişiye ait iletişim bilgisi değeridir. IletisimBilgisi tipinde olmalıdır.</param>
        IGercekSahisFluentIletisim IletisimBilgisiIle(IletisimBilgisi iletisimBilgisi);
        GercekSahis Olustur();
    }

    public interface IGercekSahisFluentGorev
    {
        /// <summary>
        /// Kişiye ait iletişim bilgisi değerinin atanması için kullanılır.
        /// </summary>
        /// <param name="iletisimBilgisi">Kişiye ait iletişim bilgisi değeridir. IletisimBilgisi tipinde olmalıdır.</param>
        IGercekSahisFluentIletisim IletisimBilgisiIle(IletisimBilgisi iletisimBilgisi);
        GercekSahis Olustur();
    }

    public interface IGercekSahisFluentIletisim
    {
        GercekSahis Olustur();
    }
}
