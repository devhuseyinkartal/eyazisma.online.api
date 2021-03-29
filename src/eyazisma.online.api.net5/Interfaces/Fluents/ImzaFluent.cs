using System;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IImzaFluent : IDisposable,
        IImzaFluentImzalayan,
        IImzaFluentYetkiDevreden,
        IImzaFluentVekaletVeren,
        IImzaFluentMakam,
        IImzaFluentAmac,
        IImzaFluentAciklama,
        IImzaFluentTarih
    {
    }

    public interface IImzaFluentImzalayan
    {
        /// <summary>
        ///     İmzayı atan kişiye imza yetkisini devreden kişiye ait bilgilerdir.
        /// </summary>
        /// <param name="yetkiDevreden">
        ///     İmzayı atan kişiye imza yetkisini devreden kişiye ait bilgilerin değeridir. GercekSahis
        ///     tipinde olmalıdır.
        /// </param>
        IImzaFluentYetkiDevreden YetkiDevredenIle(GercekSahis yetkiDevreden);

        /// <summary>
        ///     İmzayı atan kişinin vekâlet ettiği kişiye ait bilgilerdir.
        /// </summary>
        /// <param name="vekaletVeren">
        ///     İmzayı atan kişinin vekâlet ettiği kişiye ait bilgilerin değeridir. GercekSahis tipinde
        ///     olmalıdır.
        /// </param>
        IImzaFluentVekaletVeren VekaletVerenIle(GercekSahis vekaletVeren);

        /// <summary>
        ///     İmzayı atan kişinin makam bilgisidir.
        /// </summary>
        /// <param name="makam">İmzayı atan kişinin makam bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
        IImzaFluentMakam MakamIle(IsimTip makam);

        /// <summary>
        ///     İmza amacıdır.
        /// </summary>
        /// <param name="amac">İmza amacının değeridir. MetinTip tipinde olmalıdır.</param>
        IImzaFluentAmac AmacIle(MetinTip amac);

        /// <summary>
        ///     İmzaya ilişkin açıklamadır.
        /// </summary>
        /// <param name="aciklama">İmzaya ilişkin açıklama değeridir. MetinTip tipinde olmalıdır.</param>
        IImzaFluentAciklama AciklamaIle(MetinTip aciklama);

        /// <summary>
        ///     İmzanın atıldığı tarih ve saat bilgisidir.
        /// </summary>
        /// <param name="tarih">İmzanın atıldığı tarih ve saat bilgisi değeridir. DateTime tipinde olmalıdır.</param>
        IImzaFluentTarih TarihIle(DateTime tarih);

        Imza Olustur();
    }

    public interface IImzaFluentYetkiDevreden
    {
        /// <summary>
        ///     İmzayı atan kişinin vekâlet ettiği kişiye ait bilgilerdir.
        /// </summary>
        /// <param name="vekaletVeren">
        ///     İmzayı atan kişinin vekâlet ettiği kişiye ait bilgilerin değeridir. GercekSahis tipinde
        ///     olmalıdır.
        /// </param>
        IImzaFluentVekaletVeren VekaletVerenIle(GercekSahis vekaletVeren);

        /// <summary>
        ///     İmzayı atan kişinin makam bilgisidir.
        /// </summary>
        /// <param name="makam">İmzayı atan kişinin makam bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
        IImzaFluentMakam MakamIle(IsimTip makam);

        /// <summary>
        ///     İmza amacıdır.
        /// </summary>
        /// <param name="amac">İmza amacının değeridir. MetinTip tipinde olmalıdır.</param>
        IImzaFluentAmac AmacIle(MetinTip amac);

        /// <summary>
        ///     İmzaya ilişkin açıklamadır.
        /// </summary>
        /// <param name="aciklama">İmzaya ilişkin açıklama değeridir. MetinTip tipinde olmalıdır.</param>
        IImzaFluentAciklama AciklamaIle(MetinTip aciklama);

        /// <summary>
        ///     İmzanın atıldığı tarih ve saat bilgisidir.
        /// </summary>
        /// <param name="tarih">İmzanın atıldığı tarih ve saat bilgisi değeridir. DateTime tipinde olmalıdır.</param>
        IImzaFluentTarih TarihIle(DateTime tarih);
    }

    public interface IImzaFluentVekaletVeren
    {
        /// <summary>
        ///     İmzayı atan kişinin makam bilgisidir.
        /// </summary>
        /// <param name="makam">İmzayı atan kişinin makam bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
        IImzaFluentMakam MakamIle(IsimTip makam);

        /// <summary>
        ///     İmza amacıdır.
        /// </summary>
        /// <param name="amac">İmza amacının değeridir. MetinTip tipinde olmalıdır.</param>
        IImzaFluentAmac AmacIle(MetinTip amac);

        /// <summary>
        ///     İmzaya ilişkin açıklamadır.
        /// </summary>
        /// <param name="aciklama">İmzaya ilişkin açıklama değeridir. MetinTip tipinde olmalıdır.</param>
        IImzaFluentAciklama AciklamaIle(MetinTip aciklama);

        /// <summary>
        ///     İmzanın atıldığı tarih ve saat bilgisidir.
        /// </summary>
        /// <param name="tarih">İmzanın atıldığı tarih ve saat bilgisi değeridir. DateTime tipinde olmalıdır.</param>
        IImzaFluentTarih TarihIle(DateTime tarih);
    }

    public interface IImzaFluentMakam
    {
        /// <summary>
        ///     İmza amacıdır.
        /// </summary>
        /// <param name="amac">İmza amacının değeridir. MetinTip tipinde olmalıdır.</param>
        IImzaFluentAmac AmacIle(MetinTip amac);

        /// <summary>
        ///     İmzaya ilişkin açıklamadır.
        /// </summary>
        /// <param name="aciklama">İmzaya ilişkin açıklama değeridir. MetinTip tipinde olmalıdır.</param>
        IImzaFluentAciklama AciklamaIle(MetinTip aciklama);

        /// <summary>
        ///     İmzanın atıldığı tarih ve saat bilgisidir.
        /// </summary>
        /// <param name="tarih">İmzanın atıldığı tarih ve saat bilgisi değeridir. DateTime tipinde olmalıdır.</param>
        IImzaFluentTarih TarihIle(DateTime tarih);
    }

    public interface IImzaFluentAmac
    {
        /// <summary>
        ///     İmzaya ilişkin açıklamadır.
        /// </summary>
        /// <param name="aciklama">İmzaya ilişkin açıklama değeridir. MetinTip tipinde olmalıdır.</param>
        IImzaFluentAciklama AciklamaIle(MetinTip aciklama);

        /// <summary>
        ///     İmzanın atıldığı tarih ve saat bilgisidir.
        /// </summary>
        /// <param name="tarih">İmzanın atıldığı tarih ve saat bilgisi değeridir. DateTime tipinde olmalıdır.</param>
        IImzaFluentTarih TarihIle(DateTime tarih);
    }

    public interface IImzaFluentAciklama
    {
        /// <summary>
        ///     İmzanın atıldığı tarih ve saat bilgisidir.
        /// </summary>
        /// <param name="tarih">İmzanın atıldığı tarih ve saat bilgisi değeridir. DateTime tipinde olmalıdır.</param>
        IImzaFluentTarih TarihIle(DateTime tarih);
    }

    public interface IImzaFluentTarih
    {
        Imza Olustur();
    }
}