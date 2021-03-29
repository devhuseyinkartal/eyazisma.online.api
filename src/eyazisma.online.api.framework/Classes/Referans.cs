namespace eyazisma.online.api.Classes
{
    public sealed class Referans
    {
        /// <summary>
        ///     Bir dosyaya ilişkin özet bilgisini barındıran elemandır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public Ozet Ozet { get; set; }

        /// <summary>
        ///     Bir dosyaya ilişkin özet bilgisini barındıran elemandır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        /// <remarks>Only for version 2.0</remarks>
        public Ozet Ozet1 { get; set; }

        public string Id { get; set; }

        public string URI { get; set; }

        public string Type { get; set; }
    }
}