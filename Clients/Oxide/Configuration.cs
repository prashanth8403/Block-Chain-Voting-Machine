using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Oxide
{
    class Configuration
    {
        public static string CONFIGPATH { get; set; } = "Configuration.config";

        public static int FetchPort(int index)
        {
            int PortValue = 0;
            try
            {
                string _CONFIGURATION_DATA_ = File.ReadAllText(@CONFIGPATH);
                JObject BLOCKCHAIN = JObject.Parse(_CONFIGURATION_DATA_);
                JArray items = (JArray)BLOCKCHAIN["CONFIGURATION"];
                PortValue = (int)items[index]["PORT"];
            }
            catch (Exception ServerError)
            {
                Console.WriteLine("Error:" + ServerError.Message);
            }
            return PortValue;
        }

        public static string FetchServer(int index)
        {
            string ServerValue = "";
            try
            {
                string _CONFIGURATION_DATA_ = File.ReadAllText(@CONFIGPATH);
                JObject BLOCKCHAIN = JObject.Parse(_CONFIGURATION_DATA_);
                JArray items = (JArray)BLOCKCHAIN["CONFIGURATION"];
                ServerValue = (string)items[index]["SERVER"];
            }
            catch (Exception ServerError)
            {
                Console.WriteLine("Error:" + ServerError.Message);
            }
            return ServerValue;
        }
    }
}
