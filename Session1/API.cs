using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Session1GlobalLibrary;

namespace Session1
{
    public class API
    {
        WebClient webClient = new WebClient();
        string userConnection = "http://10.0.2.2:59964/Users";
        string connection = "http://10.0.2.2:59964";
        public API()
        {
            webClient.Headers.Add("Content-Type", "application/json");
        }
        public async Task<User> Login(string userID, string password)
        {
            var _url = Path.Combine(userConnection, $"Login?userID={userID}&password={password}");
            var response = await webClient.UploadDataTaskAsync(_url, "POST", Encoding.UTF8.GetBytes(""));
            var getString = Encoding.Default.GetString(response);
            if (getString != "\"Invalid User!\"")
            {
                var jsonUser = JsonConvert.DeserializeObject<User>(getString);
                return jsonUser;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<User_Type>> GetUserTypes()
        {
            var _url = Path.Combine(connection, $"User_Type");
            var response = await webClient.UploadDataTaskAsync(_url, "POST", Encoding.UTF8.GetBytes(""));
            var getString = Encoding.Default.GetString(response);
            var jsonList = JsonConvert.DeserializeObject<List<User_Type>>(getString);
            return jsonList;
        }
        public async Task<string> PostAsync(string url, object data)
        {
            var _url = Path.Combine(connection, url);
            var json = JsonConvert.SerializeObject(data);
            var response = await webClient.UploadDataTaskAsync(_url, "POST", Encoding.UTF8.GetBytes(json));
            return Encoding.Default.GetString(response);
        }
        public async Task<List<Skill>> GetSkillsAsync()
        {
            var _url = Path.Combine(connection, $"Skills");
            var response = await webClient.UploadDataTaskAsync(_url, "POST", Encoding.UTF8.GetBytes(""));
            var getString = Encoding.Default.GetString(response);
            var jsonList = JsonConvert.DeserializeObject<List<Skill>>(getString);
            return jsonList;
        }

        public async Task<List<Resource_Type>> GetResource_TypesAsync()
        {
            var _url = Path.Combine(connection, $"Resource_Type");
            var response = await webClient.UploadDataTaskAsync(_url, "POST", Encoding.UTF8.GetBytes(""));
            var getString = Encoding.Default.GetString(response);
            var jsonList = JsonConvert.DeserializeObject<List<Resource_Type>>(getString);
            return jsonList;
        }
    }
}
