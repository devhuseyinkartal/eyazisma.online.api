using eyazisma.online.api.Interfaces.Fluents;
using System;
using System.IO;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    /// Üst yazıya ait elektronik dosyanın bilgileridir.
    /// </summary>
    public sealed class UstYazi
    {
        public UstYazi() { }

        private UstYazi(Stream dosya, string dosyaAdi, string mimeTuru)
        {
            MimeTuru = mimeTuru;
            Dosya = dosya;
            DosyaAdi = dosyaAdi;
        }

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

        /// <summary>
        /// Elektronik dosyanın dosya kimlik tanımlayıcısıdır.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public string MimeTuru { get; set; }

        public sealed class Kilavuz : IUstYaziFluent
        {
            private Stream _dosyaStream;
            private string _dosyaAdi, _mimeTuru;

            private Kilavuz(Stream dosyaStream)
            {
                _dosyaStream = dosyaStream;
            }

            /// <summary>
            /// Elektronik dosyanın dijital verisidir.
            /// </summary>
            /// <param name="dosyaStream">Elektronik dosyanın dijital veri değeridir. Stream tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IUstYaziFluentDosya DosyaAta(Stream dosyaStream) => new Kilavuz(dosyaStream);

            /// <summary>
            /// Elektronik dosyanın dijital verisidir.
            /// </summary>
            /// <param name="dosyaYolu">Elektronik dosyanın dosya sistemindeki yoludur.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public static IUstYaziFluentDosya DosyaAta(string dosyaYolu) => new Kilavuz(File.OpenRead(dosyaYolu));

            /// <summary>
            /// Elektronik dosyanın adıdır.
            /// </summary>
            /// <param name="dosyaAdi">Elektronik dosyanın ad değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstYaziFluentDosyaAdi DosyaAdiAta(string dosyaAdi)
            {
                _dosyaAdi = dosyaAdi;
                return this;
            }

            /// <summary>
            /// Elektronik dosyanın dosya kimlik tanımlayıcısıdır.
            /// </summary>
            /// <param name="mimeTuru">Elektronik dosyanın dosya kimlik tanımlayıcı değeridir.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public IUstYaziFluentMimeTuru MimeTuruAta(string mimeTuru)
            {
                _mimeTuru = mimeTuru;
                return this;
            }

            public UstYazi Olustur()
            {
                return new UstYazi(_dosyaStream, _dosyaAdi, _mimeTuru);
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
