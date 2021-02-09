using eyazisma.online.api.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    /// Belgenin üzerindeki her bir imzaya ait bilgilerdir.
    /// </summary>
    public sealed class Imza
    {
        public Imza() { }

        private Imza(GercekSahis imzalayan,
                    GercekSahis yetkiDevreden,
                    GercekSahis vekaletVeren,
                    IsimTip makam,
                    MetinTip amac,
                    MetinTip aciklama,
                    DateTime? tarih)
        {
            Imzalayan = imzalayan;
            YetkiDevreden = yetkiDevreden;
            VekaletVeren = vekaletVeren;
            Makam = makam;
            Amac = amac;
            Aciklama = aciklama;
            Tarih = tarih;
        }

        /// <summary>
        /// İmzayı atan kişiye ait bilgilerdir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public GercekSahis Imzalayan { get; set; }

        /// <summary>
        /// İmzayı atan kişiye imza yetkisini devreden kişiye ait bilgilerdir.
        /// </summary>
        public GercekSahis YetkiDevreden { get; set; }

        /// <summary>
        /// İmzayı atan kişinin vekâlet ettiği kişiye ait bilgilerdir.
        /// </summary>
        public GercekSahis VekaletVeren { get; set; }

        /// <summary>
        /// İmzayı atan kişinin makam bilgisidir.
        /// </summary>
        public IsimTip Makam { get; set; }

        /// <summary>
        /// İmza amacıdır.
        /// </summary>
        public MetinTip Amac { get; set; }

        /// <summary>
        /// İmzaya ilişkin açıklamadır.
        /// </summary>
        public MetinTip Aciklama { get; set; }

        /// <summary>
        /// İmzanın atıldığı tarih ve saat bilgisidir.
        /// </summary>
        public DateTime? Tarih { get; set; }

        /// <summary>
        /// DETSİS'te yer alan T.C. Yönetici Kodudur.
        /// </summary>
        [Obsolete("Since version 2.0", false)]
        public string TCYK { get; set; }


        public sealed class Kilavuz : IImzaFluent
        {
            private GercekSahis _imzalayan, _yetkiDevreden, _vekaletVeren;
            private IsimTip _makam;
            private MetinTip _amac, _aciklama;
            private DateTime? _tarih;

            private Kilavuz(GercekSahis imzalayan)
            {
                _imzalayan = imzalayan;
            }

            /// <summary>
            /// İmzayı atan kişiye ait bilgilerdir.
            /// </summary>
            /// <param name="imzalayan">İmzayı atan kişiye ait bilgilerin değeridir. GercekSahis tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IImzaFluentImzalayan ImzalayanAta(GercekSahis imzalayan) => new Kilavuz(imzalayan);

            /// <summary>
            /// İmzayı atan kişiye imza yetkisini devreden kişiye ait bilgilerdir.
            /// </summary>
            /// <param name="yetkiDevreden">İmzayı atan kişiye imza yetkisini devreden kişiye ait bilgilerin değeridir. GercekSahis tipinde olmalıdır.</param>
            public IImzaFluentYetkiDevreden YetkiDevredenIle(GercekSahis yetkiDevreden)
            {
                _yetkiDevreden = yetkiDevreden;
                return this;
            }

            /// <summary>
            /// İmzayı atan kişinin vekâlet ettiği kişiye ait bilgilerdir.
            /// </summary>
            /// <param name="vekaletVeren">İmzayı atan kişinin vekâlet ettiği kişiye ait bilgilerin değeridir. GercekSahis tipinde olmalıdır.</param>
            public IImzaFluentVekaletVeren VekaletVerenIle(GercekSahis vekaletVeren)
            {
                _vekaletVeren = vekaletVeren;
                return this;
            }

            /// <summary>
            /// İmzayı atan kişinin makam bilgisidir.
            /// </summary>
            /// <param name="makam">İmzayı atan kişinin makam bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
            public IImzaFluentMakam MakamIle(IsimTip makam)
            {
                _makam = makam;
                return this;
            }

            /// <summary>
            /// İmza amacıdır.
            /// </summary>
            /// <param name="amac">İmza amacının değeridir. MetinTip tipinde olmalıdır.</param>
            public IImzaFluentAmac AmacIle(MetinTip amac)
            {
                _amac = amac;
                return this;
            }

            /// <summary>
            /// İmzaya ilişkin açıklamadır.
            /// </summary>
            /// <param name="aciklama">İmzaya ilişkin açıklama değeridir. MetinTip tipinde olmalıdır.</param>
            public IImzaFluentAciklama AciklamaIle(MetinTip aciklama)
            {
                _aciklama = aciklama;
                return this;
            }

            /// <summary>
            /// İmzanın atıldığı tarih ve saat bilgisidir.
            /// </summary>
            /// <param name="tarih">İmzanın atıldığı tarih ve saat bilgisi değeridir. DateTime tipinde olmalıdır.</param>
            public IImzaFluentTarih TarihIle(DateTime tarih)
            {
                _tarih = tarih;
                return this;
            }

            public Imza Olustur()
            {
                return new Imza(_imzalayan, _yetkiDevreden, _vekaletVeren, _makam, _amac, _aciklama, _tarih);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
