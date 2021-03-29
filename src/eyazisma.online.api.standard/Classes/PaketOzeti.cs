using System;
using System.Collections.Generic;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     İmzalanacak paket bileşenlerinin özet değerleridir.
    /// </summary>
    /// <remarks>"PaketOzeti" elemanında, bileşenlerinin özeti alınan pakete ait ID değerinin verilmesi mecburidir.</remarks>
    public sealed class PaketOzeti
    {
        /// <summary>
        ///     Paket bileşenlerinin özet bilgileridir.
        /// </summary>
        public List<Referans> Referanslar { get; set; }

        /// <summary>
        ///     Bileşenlerinin özeti alınan pakete ait ID değeridir.
        /// </summary>
        public Guid Id { get; set; }
    }
}