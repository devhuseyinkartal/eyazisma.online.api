using System;

namespace eyazisma.online.api.Enums
{
    /// <summary>
    ///     Belgenin güvenlik derecesini gösterir.
    /// </summary>
    public enum GuvenlikKoduTuru
    {
        /// <summary>
        ///     Yok
        /// </summary>
        /// <remarks>Only for version 2.0</remarks>
        YOK = 1,

        /// <summary>
        ///     Tasnif Dışı
        /// </summary>
        [Obsolete("Since version 2.0", false)] TSD = 2,

        /// <summary>
        ///     Hizmete Özel
        /// </summary>
        HZO = 3,

        /// <summary>
        ///     Özel
        /// </summary>
        OZL = 4,

        /// <summary>
        ///     Gizli
        /// </summary>
        GZL = 5,

        /// <summary>
        ///     Çok Gizli
        /// </summary>
        CGZ = 6,

        /// <summary>
        ///     Kişiye Özel
        /// </summary>
        [Obsolete("Since version 2.0", false)] KSO = 7
    }
}