using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Steam.Models.DOTA2;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System.Text.Json.Nodes;

namespace SteamTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("none input");
                return;
            }
            Dictionary<string, Func<Input, string>> funcDict = new Dictionary<string, Func<Input, string>>();
            //获取拥有的游戏 PlayerService.GetOwnedGames SteamId
            funcDict.Add("PlayerService.GetOwnedGames", (input) =>
            {
                var webInterfaceFactory = new SteamWebInterfaceFactory(input.Apikey);
                var steamPlayerInterface = webInterfaceFactory.CreateSteamWebInterface<PlayerService>();
                var ownedGames = steamPlayerInterface.GetOwnedGamesAsync(input.Parameters["SteamId"].Value<ulong>(), false, true).Result;
                return JsonConvert.SerializeObject(ownedGames.Data);
            });


            string jsonStr = args[0];
            try
            {
                Input input = JsonConvert.DeserializeObject<Input>(jsonStr);
                if (funcDict.TryGetValue(input.Method, out var func))
                {
                    Console.WriteLine(func.Invoke(input));
                }
                else
                {
                    Console.WriteLine("Method error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("================input================");
                Console.WriteLine(jsonStr);
            }
        }
    }
}