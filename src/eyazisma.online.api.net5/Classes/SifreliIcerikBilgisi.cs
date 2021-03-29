using System.Collections.Generic;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Şifreleme yöntemi bilgileridir.
    /// </summary>
    public sealed class SifreliIcerikBilgisi
    {
        /// <summary>
        ///     Şifreleme mekanizmasını tanımlayan URI bilgileridir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public List<string> URI { get; set; }

        public string Id { get; set; }

        /// <summary>
        ///     Şifreleme mekanizmasını tanımlayan dokümanın adıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string Yontem { get; set; }

        /// <summary>
        ///     Şifreleme mekanizmasını tanımlayan sürüm bilgisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string Versiyon { get; set; }
    }
}