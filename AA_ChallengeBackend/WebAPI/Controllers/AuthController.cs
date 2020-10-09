using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Response;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken(TokenValidateRequest request)
        {
            try
            {   
                var response = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.Token);
                if (response != null)
                    return Ok("JWT - Valid");
            }
            catch (FirebaseException ex)
            {
                return BadRequest(ex.Message.ToString());
            }

            return BadRequest();
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken(TokenValidateRequest request)
        {

            try
            {
                var response = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.Token);
                await FirebaseAuth.DefaultInstance.RevokeRefreshTokensAsync(response.Uid);
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(response.Uid);

                if (user != null)
                    return Ok("Tokens revoked at: " + user.TokensValidAfterTimestamp);
            }
            catch (FirebaseException ex)
            {
                return BadRequest(ex.Message.ToString());
            }

            return BadRequest();
        }
    }
}
