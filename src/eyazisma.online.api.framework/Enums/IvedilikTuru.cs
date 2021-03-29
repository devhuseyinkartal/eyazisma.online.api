using System;

namespace eyazisma.online.api.Enums
{
    /// <summary>
    ///     Dağıtıma ait ivedilik türünü belirtir.
    /// </summary>
    public enum IvedilikTuru
    {
        /// <summary>
        ///     Normal
        /// </summary>
        NRM = 1,

        /// <summary>
        ///     Acele
        /// </summary>
        /// <remarks>Only for version 2.0</remarks>
        ACL = 2,

        /// <summary>
        ///     Günlüdür
        /// </summary>
        GNL = 3,

        /// <summary>
        ///     İvedi
        /// </summary>
        [Obsolete("Since version 2.0", false)] IVD = 4,

        /// <summary>
        ///     Çok ivedi
        /// </summary>
        [Obsolete("Since version 1.2", false)] CIV = 5
    }
}