using System;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Tekil anahtar değeridir.
    /// </summary>
    public sealed class TanimlayiciTip
    {
        public TanimlayiciTip()
        {
        }

        private TanimlayiciTip(string semaID, string deger)
        {
            SemaID = semaID;
            Deger = deger;
        }

        /// <summary>
        ///     Tekil anahtar değeri için kullanılan veri türü / şemasıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string SemaID { get; set; }

        /// <remarks>Zorunlu alandır.</remarks>
        public string Deger { get; set; }

        public sealed class Kilavuz : ITanimlayiciTipFluent
        {
            private readonly string _semaID;
            private string _deger;

            private Kilavuz(string semaID)
            {
                _semaID = semaID;
            }

            /// <remarks>Zorunlu alandır.</remarks>
            public ITanimlayiciTipFluentDeger DegerAta(string deger)
            {
                _deger = deger;
                return this;
            }

            public TanimlayiciTip Olustur()
            {
                return new(_semaID, _deger);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Tekil anahtar değeri için kullanılan veri türü / şemasıdır.
            /// </summary>
            /// <param name="semaID">Tekil anahtar değeri için kullanılan veri türü / şeması değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static ITanimlayiciTipFluentSemaID SemaIDAta(string semaID)
            {
                return new Kilavuz(semaID);
            }
        }
    }
}