using System;
using System.Collections.Generic;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Elektronik imza ile paraflanacak paket bileşenlerinin özet değerleridir.
    /// </summary>
    /// <remarks>
    ///     "ParafOzeti" elemanında, bileşenlerinin özeti alınan pakete ait ID değerinin verilmesi mecburidir.
    ///     Only for version 2.0
    /// </remarks>
    public sealed class ParafOzeti
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