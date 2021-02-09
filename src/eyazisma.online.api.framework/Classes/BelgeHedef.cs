using eyazisma.online.api.Interfaces.Fluents;
using System;
using System.Collections.Generic;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    /// Belgenin elektronik ortamda iletileceği tarafların bilgisidir.
    /// </summary>

    public sealed class BelgeHedef
    {
        public BelgeHedef() { }

        public BelgeHedef(List<Hedef> hedefler)
        {
            Hedefler = hedefler;
        }

        /// <summary>
        /// Belgenin iletileceği taraf bilgileridir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public List<Hedef> Hedefler { get; set; }

        public sealed class Kilavuz : IBelgeHedefFluent
        {
            private List<Hedef> _hedefler;

            private Kilavuz(List<Hedef> hedefler)
            {
                _hedefler = new List<Hedef>();
                if (hedefler != null && hedefler.Count > 0)
                    _hedefler.AddRange(hedefler);
            }

            /// <summary>
            /// Belgenin iletileceği taraf bilgisidir.
            /// </summary>
            /// <param name="hedef">Taraf bilgisi değeridir. Hedef tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IBelgeHedefFluentHedef HedefEkle(Hedef hedef) => new Kilavuz(new List<Hedef> { hedef });

            /// <summary>
            /// Belgenin iletileceği taraf bilgileridir.
            /// </summary>
            /// <param name="hedef">Taraf bilgisi değeridir. Hedef tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IBelgeHedefFluentHedefler HedeflerEkle(List<Hedef> hedefler) => new Kilavuz(hedefler);

            /// <summary>
            /// Belgenin iletileceği taraf bilgisidir.
            /// </summary>
            /// <param name="hedef">Taraf bilgisi değeridir. Hedef tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IBelgeHedefFluentHedef DigerHedefEkle(Hedef hedef)
            {
                _hedefler.Add(hedef);
                return this;
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            public BelgeHedef Olustur() => new BelgeHedef(_hedefler);
        }
    }
}
