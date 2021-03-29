using System;
using System.Collections.Generic;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Belgeye ilişkin Standart Dosya Planları bilgisidir.
    /// </summary>
    /// <remarks>Only for version 2.0</remarks>
    public sealed class SDPBilgisi
    {
        public SDPBilgisi()
        {
        }

        private SDPBilgisi(SDP anaSdp, List<SDP> digerSdpler)
        {
            AnaSdp = anaSdp;
            DigerSdpler = digerSdpler;
        }

        /// <summary>
        ///     Belgenin ana Standart Dosya Planı bilgisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public SDP AnaSdp { get; set; }

        /// <summary>
        ///     Belgenin diğer Standart Dosya Planları bilgisidir.
        /// </summary>
        public List<SDP> DigerSdpler { get; set; }

        public sealed class Kilavuz : ISDPBilgisiFluent
        {
            private readonly SDP _anaSdp;
            private List<SDP> _digerSdpler;

            private Kilavuz(SDP anaSdp)
            {
                _anaSdp = anaSdp;
            }

            /// <summary>
            ///     Belgenin diğer Standart Dosya Planı bilgisidir.
            /// </summary>
            /// <param name="digerSdp">Diğer SDP bilgisi değeridir. SDP tipinde olmalıdır.</param>
            public ISDPBilgisiFluentDigerSdp DigerSdpIle(SDP digerSdp)
            {
                if (digerSdp != null)
                {
                    if (_digerSdpler == null)
                        _digerSdpler = new List<SDP>();

                    _digerSdpler.Add(digerSdp);
                }

                return this;
            }

            /// <summary>
            ///     Belgenin diğer Standart Dosya Planı bilgileridir.
            /// </summary>
            /// <param name="digerSdpler">Diğer SDP bilgisi değerleridir. SDP listesi tipinde olmalıdır.</param>
            public ISDPBilgisiFluentDigerSdpler DigerSdplerIle(List<SDP> digerSdpler)
            {
                if (digerSdpler != null && digerSdpler.Count > 0)
                {
                    if (_digerSdpler == null)
                        _digerSdpler = new List<SDP>();

                    _digerSdpler.AddRange(digerSdpler);
                }

                return this;
            }

            public SDPBilgisi Olustur()
            {
                return new(_anaSdp, _digerSdpler);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Belgenin ana Standart Dosya Planı bilgisidir.
            /// </summary>
            /// <param name="anaSdp">Ana SDP bilgisi değeridir. SDP tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static ISDPBilgisiFluentAnaSdp AnaSdpAta(SDP anaSdp)
            {
                return new Kilavuz(anaSdp);
            }
        }
    }
}