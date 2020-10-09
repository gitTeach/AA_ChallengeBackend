using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Response;
using FirebaseAdmin.Auth;
using Microsoft.Net.Http.Headers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {

        public async Task<string> GetUserIdFromToken()
        {
            var req = Request.Headers[HeaderNames.Authorization];
            var sreq = req.ToString().Replace("Bearer ", "");
            var token = sreq;
            var response = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            return response.Uid;
        }


        

        

    }
}
