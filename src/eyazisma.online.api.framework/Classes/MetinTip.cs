using eyazisma.online.api.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.Classes
{
    public sealed class MetinTip
    {
        public MetinTip() { }

        private MetinTip(string dilID, string deger)
        {
            DilID = dilID;
            Deger = deger;
        }

        public string DilID { get; set; }

        /// <remarks>Zorunlu alandır.</remarks>
        public string Deger { get; set; }

        public sealed class Kilavuz : IMetinTipFluent
        {
            private string _dilID,
                           _deger;

            private Kilavuz(string deger)
            {
                _deger = deger;
            }

            /// <summary>
            /// Değer alanının atanması için kullanılır.
            /// </summary>
            /// <param name="deger">Değer alanı değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IMetinTipFluentDeger DegerAta(string deger) => new Kilavuz(deger);

            /// <summary>
            /// DilID değerinin atanması için kullanılır.
            /// </summary>
            /// <param name="dilID">DilID değeridir.</param>
            public IMetinTipFluentDilID DilIDIle(string dilID)
            {
                _dilID = dilID;
                return this;
            }

            public MetinTip Olustur()
            {
                return new MetinTip(_dilID, _deger);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
