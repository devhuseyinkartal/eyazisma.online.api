using eyazisma.online.api.framework.Interfaces.Fluents;
using System;

namespace eyazisma.online.api.framework.Classes
{
    public sealed class IsimTip
    {
        public IsimTip() { }

        private IsimTip(string dilID, string deger)
        {
            DilID = dilID;
            Deger = deger;
        }

        public string DilID { get; set; }

        /// <remarks>Zorunlu alandır.</remarks>
        public string Deger { get; set; }

        public sealed class Kilavuz : IIsimTipFluent
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
            public static IIsimTipFluentDeger DegerAta(string deger) => new Kilavuz(deger);

            /// <summary>
            /// DilID değerinin atanması için kullanılır.
            /// </summary>
            /// <param name="dilID">DilID değeridir.</param>
            public IIsimTipFluentDilID DilIDIle(string dilID)
            {
                _dilID = dilID;
                return this;
            }

            public IsimTip Olustur()
            {
                return new IsimTip(_dilID, _deger);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
