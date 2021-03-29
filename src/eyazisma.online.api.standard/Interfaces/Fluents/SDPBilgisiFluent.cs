using System;
using System.Collections.Generic;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface ISDPBilgisiFluent : IDisposable, ISDPBilgisiFluentAnaSdp, ISDPBilgisiFluentDigerSdp,
        ISDPBilgisiFluentDigerSdpler
    {
    }

    public interface ISDPBilgisiFluentAnaSdp
    {
        /// <summary>
        ///     Belgenin diğer Standart Dosya Planı bilgisidir.
        /// </summary>
        /// <param name="digerSdp">Diğer SDP bilgisi değeridir. SDP tipinde olmalıdır.</param>
        ISDPBilgisiFluentDigerSdp DigerSdpIle(SDP digerSdp);

        /// <summary>
        ///     Belgenin diğer Standart Dosya Planı bilgileridir.
        /// </summary>
        /// <param name="digerSdpler">Diğer SDP bilgisi değerleridir. SDP listesi tipinde olmalıdır.</param>
        ISDPBilgisiFluentDigerSdpler DigerSdplerIle(List<SDP> digerSdpler);

        SDPBilgisi Olustur();
    }

    public interface ISDPBilgisiFluentDigerSdp
    {
        /// <summary>
        ///     Belgenin diğer Standart Dosya Planı bilgisidir.
        /// </summary>
        /// <param name="digerSdp">Diğer SDP bilgisi değeridir. SDP tipinde olmalıdır.</param>
        ISDPBilgisiFluentDigerSdp DigerSdpIle(SDP digerSdp);

        SDPBilgisi Olustur();
    }

    public interface ISDPBilgisiFluentDigerSdpler
    {
        SDPBilgisi Olustur();
    }
}