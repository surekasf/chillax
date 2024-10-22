using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chillax
{
    public class ContentService
    {
        public async Task<Dictionary<string,string>> GetRiddleFromApi(string userAge)
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("https://riddles-api.vercel.app/random");
            dynamic json = JsonConvert.DeserializeObject(response);
            Dictionary<string,string> map = new Dictionary<string,string>();
            map.Add(json.riddle.ToString(), json.answer.ToString());
            return map;
         

        }
        public async Task<string> GetJokeFromApi(string userAge)
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("https://v2.jokeapi.dev/joke/Any");
            dynamic json = JsonConvert.DeserializeObject(response);
            return json.joke.ToString();
        }
    }
}
