using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Forms.Mobile.Models;
using Newtonsoft.Json;

namespace Forms.Mobile.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<UserModel> Login(LoginModel loginModel)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri("http://10.0.2.2:14958/api/");
                    var content = JsonConvert.SerializeObject(loginModel);
                    HttpContent contentPost = new StringContent(content, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("users/login", contentPost);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException();
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<UserModel>(result);
                    }
                }
                catch (Exception exp)
                {
                    // TODO LOG.
                    return null;
                    
                }
            }

        }
    }
}
