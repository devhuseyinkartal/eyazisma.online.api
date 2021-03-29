using System;
using System.Collections.Generic;
using eyazisma.online.api.Interfaces.Fluents;

namespace eyazisma.online.api.Classes
{
    /// <summary>
    ///     Sadece belgenin son imzasını alması sırasında belirlenebilen, belgeye ilişkin kimlik bilgileridir.
    /// </summary>
    public sealed class NihaiUstveri
    {
        public NihaiUstveri()
        {
        }

        private NihaiUstveri(DateTime tarih, string belgeNo, List<Imza> belgeImzalar)
        {
            Tarih = tarih;
            BelgeNo = belgeNo;
            BelgeImzalar = belgeImzalar;
        }

        /// <summary>
        ///     Belgenin tarihidir.
        /// </summary>
        /// <remarks>
        ///     Zorunlu alandır.
        ///     UTC ofset değeri ile verilmesi tavsiye edilir.
        /// </remarks>
        public DateTime Tarih { get; set; }

        /// <summary>
        ///     Belge numarasıdır.
        /// </summary>
        /// <remarks>
        ///     Zorunlu alandır.
        ///     Resmi yazışmalara ilişkin mevzuatta belirtilen biçime uygun olmalıdır.
        /// </remarks>
        public string BelgeNo { get; set; }

        /// <summary>
        ///     Belgenin üzerindeki imzalara ilişkin bilgilerdir.
        /// </summary>
        /// <remarks>Zorunlu alandır.</remarks>
        public List<Imza> BelgeImzalar { get; set; }

        public sealed class Kilavuz : INihaiUstveriFluent
        {
            private List<Imza> _belgeImzalar;
            private string _belgeNo;
            private readonly DateTime _tarih;

            private Kilavuz(DateTime tarih)
            {
                _tarih = tarih;
            }

            /// <summary>
            ///     Belge numarasıdır.
            /// </summary>
            /// <param name="belgeNo">Belge numarası değeridir.</param>
            /// <remarks>
            ///     Zorunlu alandır.
            ///     Resmi yazışmalara ilişkin mevzuatta belirtilen biçime uygun olmalıdır.
            /// </remarks>
            public INihaiUstveriFluentBelgeNo BelgeNoAta(string belgeNo)
            {
                _belgeNo = belgeNo;
                return this;
            }

            /// <summary>
            ///     Belgenin üzerindeki her bir imzaya ait bilgilerdir.
            /// </summary>
            /// <param name="belgeImza">İmza değeridir. Imza tipinde olmalıdır.</param>
            /// <remarks>Zorunlu alandır.</remarks>
            public INihaiUstveriFluentImza BelgeImzaAta(Imza belgeImza)
            {
                if (belgeImza != null)
                {
                    if (_belgeImzalar == null)
                        _belgeImzalar = new List<Imza>();

                    _belgeImzalar.Add(belgeImza);
                }

                return this;
            }

            public INihaiUstveriFluentImzalar BelgeImzalarAta(List<Imza> belgeImzalar)
            {
                if (belgeImzalar != null && belgeImzalar.Count > 0)
                {
                    if (_belgeImzalar == null)
                        _belgeImzalar = new List<Imza>();

                    _belgeImzalar.AddRange(belgeImzalar);
                }

                return this;
            }

            public NihaiUstveri Olustur()
            {
                return new NihaiUstveri(_tarih, _belgeNo, _belgeImzalar);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            /// <summary>
            ///     Belgenin tarihidir.
            /// </summary>
            /// <param name="tarih">Belge tarihi değeridir.</param>
            /// <remarks>
            ///     Zorunlu alandır.
            ///     UTC ofset değeri ile verilmesi tavsiye edilir.
            /// </remarks>
            public static INihaiUstveriFluentTarih TarihAta(DateTime tarih)
            {
                return new Kilavuz(tarih);
            }
        }
    }
}