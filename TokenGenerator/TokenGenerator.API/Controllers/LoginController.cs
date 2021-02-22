using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TokenGenerator.Domain.Models;

namespace TokenGenerator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody] UserModel model )
        {

            HttpClient client = new HttpClient(new HttpClientHandler());
            Uri uri = new Uri($"http://localhost:5000/api/Users/email/{model.Email}");
            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get,
            };

            
            var response = await client.SendAsync(request);
            var body = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<User>(body);
            
            if(user == null)
            {
                return NotFound(model.Email);
            }


            if(!user.Password.Equals(model.Password))
            {
                return Unauthorized("Bad password");
            }
            
            string token = "";

            return Ok(token);
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
