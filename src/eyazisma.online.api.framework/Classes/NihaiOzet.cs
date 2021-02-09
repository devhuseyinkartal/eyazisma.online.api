using System;
using System.Collections.Generic;

namespace eyazisma.online.api.framework.Classes
{
    /// <summary>
    /// Mühürlenecek paket bileşenlerinin özet değerleridir. 
    /// </summary>
    /// <remarks>"NihaiOzet" elemanında, bileşenlerinin özeti alınan pakete ait ID değerinin verilmesi mecburidir.</remarks>

    public sealed class NihaiOzet
    {
        /// <summary>
        /// Paket bileşenlerinin özet bilgileridir.
        /// </summary>
        public List<Referans> Referanslar { get; set; }

        /// <summary>
        /// Bileşenlerinin özeti alınan pakete ait ID değeridir.
        /// </summary>
        public Guid Id { get; set; }
    }
}
