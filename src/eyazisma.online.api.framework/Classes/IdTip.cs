using eyazisma.online.api.framework.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.framework.Classes
{
    /// <summary>
    /// Paket içerisindeki ilgi ve ekler için tekil anahtar değeridir.
    /// </summary>
    public sealed class IdTip
    {
        public IdTip() { }

        private IdTip(string deger, bool eYazismaIdMi)
        {
            Deger = deger;
            EYazismaIdMi = eYazismaIdMi;
        }

        /// <summary>
        /// Tekil anahtar değeridir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string Deger { get; set; }

        /// <summary>
        ///  Paket içerisindeki ilgi veya ekin başka bir e-Yazışma Paketi veya başka bir e-Yazışma Paketi'nin eki olup olmadığını belirtir.
        /// </summary>
        public bool EYazismaIdMi { get; set; }

        public sealed class Kilavuz : IIdTipFluent
        {
            private string _deger;
            private bool _eYazismaIdMi;

            private Kilavuz(string deger)
            {
                _deger = deger;
            }

            /// <summary>
            /// Tekil anahtar değeridir.
            /// </summary>
            /// <param name="deger">Tekil anahtar değeridir. Guid tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IIdTipFluentDeger DegerAta(string deger) => new Kilavuz(deger);

            /// <summary>
            ///  Paket içerisindeki ilgi veya ekin başka bir e-Yazışma Paketi veya başka bir e-Yazışma Paketi'nin eki olup olmadığını belirtir.
            /// </summary>
            /// <param name="eYazismaIdMi">Boolean tipinde olmalıdır.</param>
            public IIdTipFluentEYazismaId EYazismaIdMiAta(bool eYazismaIdMi)
            {
                _eYazismaIdMi = eYazismaIdMi;
                return this;
            }

            public IdTip Olustur()
            {
                return new IdTip(_deger, _eYazismaIdMi);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
