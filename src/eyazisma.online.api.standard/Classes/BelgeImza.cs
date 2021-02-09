using eyazisma.online.api.Interfaces.Fluents;
using System;
using System.Collections.Generic;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    /// Belgenin üzerindeki imzalara ilişkin bilgilerdir.
    /// </summary>

    public sealed class BelgeImza
    {
        public BelgeImza() { }

        public BelgeImza(List<Imza> imzalar)
        {
            Imzalar = imzalar;
        }

        /// <summary>
        /// Belgenin üzerindeki imza bilgilerdir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public List<Imza> Imzalar { get; set; }

        public sealed class Kilavuz : IBelgeImzaFluent
        {
            private List<Imza> _imzalar;

            private Kilavuz(List<Imza> imzalar)
            {
                _imzalar = new List<Imza>();
                if (imzalar != null && imzalar.Count > 0)
                    _imzalar.AddRange(imzalar);
            }

            /// <summary>
            /// Belge üzerindeki imza bilgisidir.
            /// </summary>
            /// <param name="imza">Imza bilgisi değeridir. Imza tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IBelgeImzaFluentImza ImzaEkle(Imza imza) => new Kilavuz(new List<Imza> { imza });

            /// <summary>
            /// Belge üzerindeki imza bilgisidir.
            /// </summary>
            /// <param name="imza">Imza bilgisi değeridir. Imza tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IBelgeImzaFluentImzalar ImzalarEkle(List<Imza> imzalar) => new Kilavuz(imzalar);

            /// <summary>
            /// Belge üzerindeki imza bilgisidir.
            /// </summary>
            /// <param name="imza">Imza bilgisi değeridir. Imza tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IBelgeImzaFluentImza DigerImzaEkle(Imza imza)
            {
                _imzalar.Add(imza);
                return this;
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            public BelgeImza Olustur() => new BelgeImza(_imzalar);
        }
    }
}
