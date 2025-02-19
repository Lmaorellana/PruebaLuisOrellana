using DatabaseFirst.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProyectoLuisOrellana.Aplication.Utils;
using ProyectoLuisOrellana.Infrastructure.DAL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Aplication.General
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly IRepositoryManager repositoryManager;
        private readonly IErrorLogService errorLog;
        private ResponseModel responseModel;

        public TokenService(IConfiguration configuration, IRepositoryManager repositoryManager, IErrorLogService errorLog)
        {
            this.configuration = configuration;
            this.repositoryManager = repositoryManager;
            this.errorLog = errorLog;
            responseModel = new ResponseModel();

        }
        public async Task<ResponseModel> GenerateTokenAsync(User user)
        {
            string Token = string.Empty;

            try
            {
                var _user = await this.repositoryManager.User.GetById(u => u.LogonName == user.LogonName && u.Password == user.Password);

                if (_user == null)
                {
                    
                    this.responseModel.AddMessageError("No Existe Usuario");
                    return this.responseModel;
                }

                var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                var issuedAt = DateTime.UtcNow;
                var expiresAt = issuedAt.AddMinutes(120);

                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Name, user.LogonName),            
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expiresAt,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                Token = tokenHandler.WriteToken(token);

                //Console.WriteLine($"Token issued at: {issuedAt}, expires at: {expiresAt}");

                await this.repositoryManager.UserToken.Insert(new UserToken
                {
                    UserId = _user.UserId,
                    JwtToken = Token,
                    ExpiryDate = expiresAt
                });

                //this.responseModel = new ResponseModel(true, "Operación Realizada Con Éxito", Token);
                this.responseModel.AddDefaultSuccess(Token);
            }
            catch (Exception ex)
            {
                this.errorLog.SaveErrorLog(ex);
                //this.responseModel = new ResponseModel(false, "Ocurrió un error", string.Empty);
                this.responseModel.AddDefaultError();
            }

            return responseModel;
        }


        public async Task<bool> IsTokenValidAsync(string token)
        {
            var a = await repositoryManager.UserToken.List(t => t.JwtToken == token);

            return a.Any();

        }
    }
}
