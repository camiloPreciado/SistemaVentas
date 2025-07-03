using System;
using System.IO;
using System.Web.Hosting;

namespace productos.api.Utils
{
    public static class Logger
    {
        private static readonly string RutaLogs = HostingEnvironment.MapPath("~/App_Data/logs.txt");

        public static void Info(string mensaje)
        {
            EscribirLog("INFO", mensaje);
        }

        public static void Warn(string mensaje)
        {
            EscribirLog("WARN", mensaje);
        }

        public static void Error(string mensaje)
        {
            EscribirLog("ERROR", mensaje);
        }

        private static void EscribirLog(string tipo, string mensaje)
        {
            string log = $"[{DateTime.Now}] {tipo}: {mensaje}\n";
            File.AppendAllText(RutaLogs, log);
        }
    }
}
