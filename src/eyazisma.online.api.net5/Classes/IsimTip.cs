using System;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    public sealed class IsimTip
    {
        public IsimTip()
        {
        }

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
            private string _dilID;

            private readonly string _deger;

            private Kilavuz(string deger)
            {
                _deger = deger;
            }

            /// <summary>
            ///     DilID değerinin atanması için kullanılır.
            /// </summary>
            /// <param name="dilID">DilID değeridir.</param>
            public IIsimTipFluentDilID DilIDIle(string dilID)
            {
                _dilID = dilID;
                return this;
            }

            public IsimTip Olustur()
            {
                return new(_dilID, _deger);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Değer alanının atanması için kullanılır.
            /// </summary>
            /// <param name="deger">Değer alanı değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IIsimTipFluentDeger DegerAta(string deger)
            {
                return new Kilavuz(deger);
            }
        }
    }
}