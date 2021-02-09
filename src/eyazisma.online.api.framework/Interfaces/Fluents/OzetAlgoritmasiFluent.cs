using eyazisma.online.api.framework.Classes;
using System;

namespace eyazisma.online.api.framework.Interfaces.Fluents
{
    public interface IOzetAlgoritmasiFluent : IDisposable, IOzetAlgoritmasiFluentAlgoritma, IOzetAlgoritmasiFluentAny
    {
    }

    public interface IOzetAlgoritmasiFluentAlgoritma
    {
        IOzetAlgoritmasiFluentAny AnyIle(System.Xml.XmlNode[] any);
        OzetAlgoritmasi Olustur();
    }

    public interface IOzetAlgoritmasiFluentAny
    {
        OzetAlgoritmasi Olustur();
    }
}
