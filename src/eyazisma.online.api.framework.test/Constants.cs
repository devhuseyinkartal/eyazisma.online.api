using System;
using System.IO;

namespace eyazisma.online.api.framework.test
{
    public static class Constants
    {
        private static string BASE_DIRECTORY => AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", string.Empty);
        public static string TEST_BASE_DIRECTORY => Path.Combine(BASE_DIRECTORY, "testFiles");
        public static string RESULT_BASE_DIRECTORY => Path.Combine(BASE_DIRECTORY, "results");

        public static string USTYAZI_FILE_PATH => Path.Combine(TEST_BASE_DIRECTORY, "ustyazi.pdf");
        public static string USTYAZI_FILE_NAME => Path.GetFileName(USTYAZI_FILE_PATH);

        public static string EK1_FILE_PATH => Path.Combine(TEST_BASE_DIRECTORY, "ek1.pdf");
        public static string EK1_FILE_NAME => Path.GetFileName(EK1_FILE_PATH);

        public static string EK2_FILE_PATH => Path.Combine(TEST_BASE_DIRECTORY, "ek2.pdf");
        public static string EK2_FILE_NAME => Path.GetFileName(EK2_FILE_PATH);

        public static string MIME_TURU_PDF => "application/pdf";
    }
}
