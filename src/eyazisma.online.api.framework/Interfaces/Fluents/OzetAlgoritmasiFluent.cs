using System;
using System.Xml;
using eyazisma.online.api.Classes;

namespace eyazisma.online.api.Interfaces.Fluents
{
    public interface IOzetAlgoritmasiFluent : IDisposable, IOzetAlgoritmasiFluentAlgoritma, IOzetAlgoritmasiFluentAny
    {
    }

    public interface IOzetAlgoritmasiFluentAlgoritma
    {
        IOzetAlgoritmasiFluentAny AnyIle(XmlNode[] any);
        OzetAlgoritmasi Olustur();
    }

    public interface IOzetAlgoritmasiFluentAny
    {
        OzetAlgoritmasi Olustur();
    }
}