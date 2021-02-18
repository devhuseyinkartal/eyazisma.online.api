using eyazisma.online.api.Interfaces.Fluents;
using System;
using System.IO;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    /// Eke ait elektronik dosyanın bilgileridir.
    /// </summary>
    public sealed class EkDosya
    {
        public EkDosya() { }

        private EkDosya(Ek ek, Stream dosya, string dosyaAdi)
        {
            Ek = ek;
            Dosya = dosya;
            DosyaAdi = dosyaAdi;
        }

        /// <summary>
        /// Elektronik dosyanın ilişkili olduğu ek referansıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public Ek Ek { get; set; }

        /// <summary>
        /// Elektronik dosyanın dijital verisidir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public Stream Dosya { get; set; }

        /// <summary>
        /// Elektronik dosyanın adıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string DosyaAdi { get; set; }

        public sealed class Kilavuz : IEkDosyaFluent
        {
            private Ek _ek;
            private Stream _dosyaStream;
            private string _dosyaAdi;

            private Kilavuz(Ek ek)
            {
                _ek = ek;
            }

            /// <summary>
            /// Elektronik dosyanın ilişkili olduğu ek referansıdır.
            /// </summary>
            /// <param name="ek">Elektronik dosyanın ilişkili olduğu ek referansı değeridir. Ek tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IEkDosyaFluentEk EkAta(Ek ek) => new Kilavuz(ek);

            /// <summary>
            /// Elektronik dosyanın dijital verisidir.
            /// </summary>
            /// <param name="dosyaStream">Elektronik dosyanın dijital veri değeridir. Stream tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkDosyaFluentDosya DosyaAta(Stream dosyaStream)
            {
                _dosyaStream = dosyaStream;
                return this;
            }

            /// <summary>
            /// Elektronik dosyanın dijital verisidir.
            /// </summary>
            /// <param name="dosyaYolu">Elektronik dosyanın dosya sistemindeki yoludur.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkDosyaFluentDosya DosyaAta(string dosyaYolu)
            {
                _dosyaStream = File.OpenRead(dosyaYolu);
                return this;
            }

            /// <summary>
            /// Elektronik dosyanın adıdır.
            /// </summary>
            /// <param name="dosyaAdi">Elektronik dosyanın ad değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IEkDosyaFluentDosyaAdi DosyaAdiAta(string dosyaAdi)
            {
                _dosyaAdi = dosyaAdi;
                return this;
            }

            public EkDosya Olustur()
            {
                return new EkDosya(_ek, _dosyaStream, _dosyaAdi);
            }

            public void Dispose()
            {
                if (_dosyaStream != null)
                    _dosyaStream.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
