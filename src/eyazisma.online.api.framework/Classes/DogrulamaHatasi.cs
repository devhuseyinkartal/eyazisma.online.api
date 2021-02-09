using eyazisma.online.api.Enums;
using System;
using System.Collections.Generic;

namespace eyazisma.online.api.Classes
{

    public sealed class DogrulamaHatasi
    {
        /// <summary>
        /// Hata oluşan bileşenin paket içindeki URI'sidir.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Oluşan hatanın açıklamasıdır.
        /// </summary>
        public string Hata { get; set; }

        /// <summary>
        /// Hata türüdür.
        /// </summary>
        public DogrulamaHataTuru HataTuru { get; set; }

        /// <summary>
        /// Inner exception.
        /// </summary>
        public Exception InnerException { get; set; }

        public List<DogrulamaHatasi> AltDogrulamaHatalari { get; set; }
    }
}
