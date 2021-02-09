using eyazisma.online.api.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    /// Gerçek şahsa ilişkin kişi bilgisidir.
    /// </summary>
    public sealed class Kisi
    {
        public Kisi() { }

        private Kisi(MetinTip onEk, IsimTip ilkAdi, IsimTip ikinciAdi, IsimTip soyadi, IsimTip unvan)
        {
            OnEk = onEk;
            IlkAdi = ilkAdi;
            IkinciAdi = ikinciAdi;
            Soyadi = soyadi;
            Unvan = unvan;
        }

        /// <summary>
        /// Kişinin ilk adıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public IsimTip IlkAdi { get; set; }

        /// <summary>
        /// Kişinin soyadıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public IsimTip Soyadi { get; set; }

        /// <summary>
        /// Kişinin ikinci adıdır.
        /// </summary>
        public IsimTip IkinciAdi { get; set; }

        /// <summary>
        /// Kişinin unvanıdır.
        /// </summary>
        public IsimTip Unvan { get; set; }

        /// <summary>
        /// Kişinin isminde kullandığı ön ektir.
        /// </summary>
        public MetinTip OnEk { get; set; }

        public sealed class Kilavuz : IKisiFluent
        {
            private IsimTip _ilkAdi, _soyadi, _ikinciAdi, _unvan;
            private MetinTip _onEk;

            private Kilavuz(IsimTip ilkAdi)
            {
                _ilkAdi = ilkAdi;
            }

            /// <summary>
            /// Kişinin ilk adıdır.
            /// </summary>
            /// <param name="ilkAdi">Kişinin ilk adının değeridir. IsimTip tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IKisiFluentIlkAdi IlkAdiAta(IsimTip ilkAdi) => new Kilavuz(ilkAdi);

            /// <summary>
            /// Kişinin ikinci adıdır.
            /// </summary>
            /// <param name="ikinciAdi">Kişinin ikinci adının değeridir. IsimTip tipinde olmalıdır.</param>
            public IKisiFluentIkinciAdi IkinciAdiIle(IsimTip ikinciAdi)
            {
                _ikinciAdi = ikinciAdi;
                return this;
            }

            /// <summary>
            /// Kişinin soyadıdır.
            /// </summary>
            /// <param name="soyadi">Kişinin soyadının değeridir. IsimTip tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IKisiFluentSoyadi SoyadiAta(IsimTip soyadi)
            {
                _soyadi = soyadi;
                return this;
            }

            /// <summary>
            /// Kişinin unvanıdır.
            /// </summary>
            /// <param name="unvan">Kişinin unvan bilgisi değeridir. IsimTip tipinde olmalıdır.</param>
            public IKisiFluentUnvan UnvanIle(IsimTip unvan)
            {
                _unvan = unvan;
                return this;
            }

            /// <summary>
            /// Kişinin isminde kullandığı ön ektir.
            /// </summary>
            /// <param name="onEk">Kişinin isminde kullandığı ön ekin değeridir. MetinTip tipinde olmalıdır.</param>
            public IKisiFluentOnEk OnEkIle(MetinTip onEk)
            {
                _onEk = onEk;
                return this;
            }

            public Kisi Olustur()
            {
                return new Kisi(_onEk, _ilkAdi, _ikinciAdi, _soyadi, _unvan);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
