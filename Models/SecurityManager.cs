using LoginUsingJwtV2.Migrations;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LoginUsingJwtV2.Models
{
    public class SecurityManager
    {
        private LoginDbContext db=null;
        private JwtSettings _settings = null;
        public SecurityManager(LoginDbContext _db, JwtSettings settings)
        {
                  db = _db;
                 _settings = settings;
        }

        
        


        protected string BulidJwt(AppUserAuth AuthUser) {

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));

            List<Claim> JwtClaims = new List<Claim> () ;
            JwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub,AuthUser.UserName));
            JwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


            //Add custom claims

            JwtClaims.Add(new Claim("isAuthenticated",AuthUser.IsAuthenticated.ToString().ToLower()));


            //add custom claims from claim array
            foreach (var claim in AuthUser.Claims)
            {
                JwtClaims.Add(new Claim(claim.ClaimType,claim.ClaimValue));
            }

            // create the jwtSecurityToken
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audiance,
                claims: JwtClaims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_settings.MinutesToExpiration),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public AppUserAuth ValidateUser(AppUser user)
        {
            AppUserAuth ret = new AppUserAuth();
            AppUser authUser = null;

            //using (var db =new LoginDbContext())
            //{
                authUser = db.Users.Where(u =>
                        u.UserNme.ToLower() == user.UserNme.ToLower() && u.UserPasswordNme == user.UserPasswordNme)
                    .SingleOrDefault();

            //}

            if (authUser != null)
            {
                ret = BuildUserAuthObject(authUser);
            }

            return ret;
        }

        protected List<AppUserClaims> GetUserClaims(AppUser AuthUser)
        {
            List<AppUserClaims> List = new List<AppUserClaims>();
            try
            {
                //using (var db = new LoginDbContext())
                //{
                    List = db.Claims.Where(u => u.UserId == AuthUser.UserId).ToList();
             //   }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception trying to retrieve user claims :" + ex);
            }

            return List;
        }

        protected AppUserAuth BuildUserAuthObject(AppUser AuthUser)
        {
            AppUserAuth ret = new AppUserAuth();
            List<AppUserClaims> claims = new List<AppUserClaims>();

            ret.UserName = AuthUser.UserNme;
            ret.IsAuthenticated = true;
            ret.BearerToken = new Guid().ToString();

           ret.Claims = GetUserClaims(AuthUser);
           ret.BearerToken = BulidJwt(ret);
            //foreach (AppUserClaims claim in claims)
            //{
            //    try
            //    {
            //        typeof(AppUserAuth).GetProperty(claim.ClaimType)
            //            .SetValue(ret, Convert.ToBoolean(claim.ClaimValue), null);
            //    }
            //    catch 
            //    {

            //    }


            //}




            return ret;

        }
    }
}
