using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_API.Contracts;
using Web_API.Others;

namespace Web_API.Utility
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration; // Digunakan untuk mendapatkan data yang ada di JWT
        }
        public string GenerateToken(IEnumerable<Claim> claims) // 
        {
            // Diambil dari jwt json, keynya harus dalam bentuk byte sehingga harus di convert.
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            // Signature key, kita mengconvert dengan secret key yang kita punya. Selanjutnya di convert ke HmacSha256
            var singinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // var token digunakan untuk membentuk pola dari payload 
            var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10), // Mengatur token berlaku dalam berapa menit
                signingCredentials: singinCredentials);

            // Membuat token options, token apapun yang dibawa akan dikembalikan menjadi token string
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public ClaimVM ExtractClaimsFromJwt(string token)
        {
            if (token.IsNullOrEmpty()) return new ClaimVM();

            try
            {
                // Untuk memvalidasi token
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // sementara buat false karena belum berupa url
                    ValidateAudience = false, // sementara buat false karena belum berupa url
                    ValidateLifetime = true, // mengecek masa expire tokennya, jika berlaku masih di ekstrak
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])) //memvalidasi signature diambil dari jwt key kita
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken); // token pertama yang di input, token validation mengecek kesamaan dengan token yang ada, security token hasilnya

                if (securityToken != null && claimsPrincipal.Identity is ClaimsIdentity identity)
                {
                    // Dilakukan mapping
                    var claims = new ClaimVM
                    {
                        NameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                        Name = identity.FindFirst(ClaimTypes.Name)!.Value,
                        Email = identity.FindFirst(ClaimTypes.Email)!.Value
                    };

                    var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(claim => claim.Value).ToList();
                    claims.Roles = roles;

                    return claims;
                }
            }

            catch
            {
                return new ClaimVM();
            }
            
            return new ClaimVM();
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }

    }
}
