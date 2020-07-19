using System;
using System.IO;
using Silicon;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace Node
{
    class Data
    {
        public static void SaveBlock(string DataStream, string _Salt)
        {
            try
            {
                string[] Data = DataStream.Split('$');
                string PrevAddress = "0";
                string __Block = "";
                if(Data[0] == "SYNCDATA")
                {
                    using (FileStream FILEHANDLE = File.Create(@Utility.DATAPATH))
                    {
                        string FileText = Data[1];
                        Byte[] ByteData = new UTF8Encoding(true).GetBytes(FileText);
                        FILEHANDLE.Write(ByteData, 0, ByteData.Length);
                        FILEHANDLE.Close();
                    }
                }
                else if (Data[0] == "SYNC")
                {
                    Utility.RecvLock = false;
                    string ___BLOCKDATA = File.ReadAllText(@Utility.DATAPATH);
                    string SERVER_IP = Data[1];
                    int PORT_NO = Convert.ToInt32(Data[2]);
                    TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
                    NetworkStream nwStream = client.GetStream();
                    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes("SYNCDATA$"+___BLOCKDATA);
                    nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[DATA SENT] >> " + SERVER_IP + ":" + PORT_NO.ToString());
                    Console.ForegroundColor = ConsoleColor.White;
                    client.Close();
                    Utility.RecvLock = true;
                }
                else
                {
                    if (Data[0] == "LOCK")
                    {
                        Console.WriteLine(" </> Lock Activated");
                        string ___BLOCKDATA = File.ReadAllText(@Utility.DATAPATH);
                        JObject BLOCKCHAIN = JObject.Parse(___BLOCKDATA);
                        JArray items = (JArray)BLOCKCHAIN["blocks"];

                        using (FileStream FILEHANDLE = File.Create(@Utility.LOCKPATH))
                        {
                            string FileText = "[DATA LOCK] - LOCK TRUE";
                            FileText += "\nBLOCKS : " + items.Count.ToString();
                            Byte[] ByteData = new UTF8Encoding(true).GetBytes(FileText);
                            FILEHANDLE.Write(ByteData, 0, ByteData.Length);
                            FILEHANDLE.Close();
                        }
                        Console.WriteLine(" [IO:] Data Locked");
                    }
                    else
                    {
                        if (File.Exists(@Utility.DATAPATH))
                        {
                            string ___BLOCKDATA = File.ReadAllText(@Utility.DATAPATH);
                            PrevAddress = GetPrevious(___BLOCKDATA);
                            __Block = GenerateBlock(Data[0], Data[1], PrevAddress, _Salt);
                            JObject BLOCKCHAIN = JObject.Parse(___BLOCKDATA);
                            JArray items = (JArray)BLOCKCHAIN["blocks"];
                            JObject NewBlock = JObject.Parse(__Block);
                            items.Add(NewBlock);
                            using (FileStream FILEHANDLE = File.Create(@Utility.DATAPATH))
                            {
                                string FileText = "{\"blocks\":\r\n" + items.ToString() + "\r\n}";
                                Byte[] ByteData = new UTF8Encoding(true).GetBytes(FileText);
                                FILEHANDLE.Write(ByteData, 0, ByteData.Length);
                                FILEHANDLE.Close();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Creating NewBlock");
                            __Block = GenerateBlock(Data[0], Data[1], PrevAddress, _Salt);

                            using (FileStream FILEHANDLE = File.Create(@Utility.DATAPATH))
                            {
                                string FileText = "{\"blocks\":[\r\n" + __Block + "\r\n]}";
                                Byte[] ByteData = new UTF8Encoding(true).GetBytes(FileText);
                                FILEHANDLE.Write(ByteData, 0, ByteData.Length);
                                FILEHANDLE.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception FILEEX)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + FILEEX.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static string GetPrevious(string BlockData)
        {
            JObject data = JObject.Parse(BlockData);
            JArray items = (JArray)data["blocks"];
            int length = items.Count;
            string ADDRESS = (string)items[length - 1]["blockhash"];
            return ADDRESS;
        }

        public static string GenerateBlock(string VOTEHASH, string TIMESTAMP, string ADDRESS, string SALT)
        {
            string NEWBLOCK = "{\r\n";
            NEWBLOCK += "\t\"previoushash\":\"" + ADDRESS + "\",\r\n";
            NEWBLOCK += "\t\"votehash\":\"" + VOTEHASH + "\",\r\n";
            NEWBLOCK += "\t\"timestamp\":\"" + TIMESTAMP + "\",\r\n";
            NEWBLOCK += "\t\"blockhash\":\"" + SILICON64.GenerateHash(VOTEHASH + TIMESTAMP + ADDRESS + SALT) + "\"\r\n";
            NEWBLOCK += "}";
            return NEWBLOCK;
        }
    }
}