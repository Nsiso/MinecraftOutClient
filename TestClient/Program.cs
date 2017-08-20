using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using MinecraftOutClient.Modules;
using System.Net.Sockets;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //"121.12.172.47", 20206
                //"221.229.173.146", 25565
                Console.Clear();
                Console.Write("请输入您要检测的服务器IP:");
                string ip = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ip))
                {
                    Console.Write("您输入的IP为空,请重新输入。按任意键继续");
                    Console.ReadKey();
                    continue;
                }
                Console.Write("请输入您要检测的服务器端口(默认为25565):");
                string portstring = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(portstring))
                {
                    Console.Write("您输入的端口为空,请重新输入。按任意键继续");
                    Console.ReadKey();
                    continue;
                }
                int port;
                try
                {
                    port = int.Parse(portstring);
                }
                catch (FormatException)
                {
                    Console.Write("您输入的端口不合法。按任意键继续");
                    Console.ReadKey();
                    continue;
                }
                Console.WriteLine();
                Console.WriteLine("正在获取中...请稍后...");
                Console.WriteLine();
                try
                {
                    ServerInfo info = new ServerInfo(ip, port);
                    info.StartGetServerInfo();
                    //Console.WriteLine(info.JsonResult);
                    Console.WriteLine();
                    Console.WriteLine("服务器IP: {0} , 服务器端口: {1}", info.ServerIP, info.ServerPort);
                    Console.WriteLine("服务器MOTD: {0}", info.MOTD);
                    Console.WriteLine("在线人数: {0}/{1}", info.MaxPlayerCount, info.CurrentPlayerCount);
                    Console.WriteLine("服务器游戏版本: {0}", info.GameVersion);
                    Console.WriteLine("服务器Protocol版本: {0}", info.ProtocolVersion);
                    Console.WriteLine("服务器延迟: {0}", info.Ping);
                    Console.WriteLine();

                    if (info.ForgeInfo != null && info.ForgeInfo.Mods.Any())
                    {
                        Console.WriteLine("====该服务器含有MOD，以下为MOD列表====");
                        foreach (var item in info.ForgeInfo.Mods)
                        {
                            Console.WriteLine("MOD:" + item);
                        }
                        Console.WriteLine("======================================");
                        Console.WriteLine();
                    }



                    if (info.OnlinePlayersName != null && info.OnlinePlayersName.Any())
                    {
                        Console.WriteLine("=====该服务器内玩家有=====");
                        foreach (var item in info.OnlinePlayersName)
                        {
                            Console.WriteLine("玩家:" + item);
                        }
                        Console.WriteLine("==========================");
                        Console.WriteLine();
                    }

                    if (info.Icon != null)
                    {
                        Console.WriteLine("该服务器含有ICON，已保存在此目录下");
                        info.Icon.Save("icon.png");
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write("连接发生异常。按任意键继续");
                    Console.ReadKey();
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.Write("发生异常。按任意键继续");
                    Console.ReadKey();
                    continue;
                }
                Console.Write("运行结束。按任意键结束");
                Console.ReadKey();
                break;
            }
        }
    }
}
