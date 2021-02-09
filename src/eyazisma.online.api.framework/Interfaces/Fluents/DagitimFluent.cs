using eyazisma.online.api.framework.Classes;
using eyazisma.online.api.framework.Enums;
using System;
using System.Collections.Generic;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IDagitimFluent : IDisposable,
                                      IDagitimFluentOge,
                                      IDagitimFluentIvedilikTuru,
                                      IDagitimFluentDagitimTuru,
                                      IDagitimFluentMiat,
                                      IDagitimFluentKonulmamisEk,
                                      IDagitimFluentKonulmamisEkler
    {
    }

    public interface IDagitimFluentOge
    {
        /// <summary>
        /// Dağıtıma ait ivedilik bilgisidir.
        /// </summary>
        /// <param name="ivedilikTuru">Dağıtıma ait ivedilik bilgisi değeridir. IvedilikTuru tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IDagitimFluentIvedilikTuru IvedilikTuruAta(IvedilikTuru ivedilikTuru);
    }

    public interface IDagitimFluentIvedilikTuru
    {
        /// <summary>
        /// Dağıtımın türüdür.
        /// </summary>
        /// <param name="dagitimTuru">Dağıtım türü değeridir. DagitimTuru tipinde olmalıdır.</param>
        /// <remarks>Zorunlu alandır.</remarks>
        IDagitimFluentDagitimTuru DagitimTuruAta(DagitimTuru dagitimTuru);
    }

    public interface IDagitimFluentDagitimTuru
    {
        /// <summary>
        /// "Ivedilik" elemanının "GNL (Günlüdür)" olması durumunda girilmesi zorunlu olan son tarihi ifade eder.
        /// </summary>
        /// <param name="miat">Miat değeridir. TimeSpan tipinde olmalıdır.</param>
        /// <remarks> "Ivedilik" elemanının "GNL (Günlüdür)" olması durumunda bu alan zorunludur.</remarks>
        IDagitimFluentMiat MiatIle(TimeSpan miat);

        /// <summary>
        /// İlgili dağıtım için konulmamış ek bilgisidir.
        /// </summary>
        /// <param name="konulmamisEk">İlgili dağıtım için konulmamış ek bilgisi değeridir. KonulmamisEk tipinde olmalıdır.</param>
        IDagitimFluentKonulmamisEk KonulmamisEkIle(KonulmamisEk konulmamisEk);

        /// <summary>
        /// İlgili dağıtım için konulmamış ek bilgileridir.
        /// </summary>
        /// <param name="konulmamisEkler">İlgili dağıtım için konulmamış ek bilgileri değeridir. KonulmamisEk listesi tipinde olmalıdır.</param>
        IDagitimFluentKonulmamisEkler KonulmamisEklerIle(List<KonulmamisEk> konulmamisEkler);
        Dagitim Olustur();
    }

    public interface IDagitimFluentMiat
    {
        /// <summary>
        /// İlgili dağıtım için konulmamış ek bilgisidir.
        /// </summary>
        /// <param name="konulmamisEk">İlgili dağıtım için konulmamış ek bilgisi değeridir. KonulmamisEk tipinde olmalıdır.</param>
        IDagitimFluentKonulmamisEk KonulmamisEkIle(KonulmamisEk konulmamisEk);

        /// <summary>
        /// İlgili dağıtım için konulmamış ek bilgileridir.
        /// </summary>
        /// <param name="konulmamisEkler">İlgili dağıtım için konulmamış ek bilgileri değeridir. KonulmamisEk listesi tipinde olmalıdır.</param>
        IDagitimFluentKonulmamisEkler KonulmamisEklerIle(List<KonulmamisEk> konulmamisEkler);
        Dagitim Olustur();
    }

    public interface IDagitimFluentKonulmamisEk
    {
        /// <summary>
        /// İlgili dağıtım için konulmamış ek bilgisidir.
        /// </summary>
        /// <param name="konulmamisEk">İlgili dağıtım için konulmamış ek bilgisi değeridir. KonulmamisEk tipinde olmalıdır.</param>
        IDagitimFluentKonulmamisEk KonulmamisEkIle(KonulmamisEk konulmamisEk);

        /// <summary>
        /// İlgili dağıtım için konulmamış ek bilgileridir.
        /// </summary>
        /// <param name="konulmamisEkler">İlgili dağıtım için konulmamış ek bilgileri değeridir. KonulmamisEk listesi tipinde olmalıdır.</param>
        IDagitimFluentKonulmamisEkler KonulmamisEklerIle(List<KonulmamisEk> konulmamisEkler);
        Dagitim Olustur();
    }

    public interface IDagitimFluentKonulmamisEkler
    {
        Dagitim Olustur();
    }
}
