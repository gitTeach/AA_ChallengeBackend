using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using FirebaseAdmin.Auth;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken(TokenValidateRequest request)
        {
            //var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;

            try
            {
                var http = HttpContext.Request;
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
