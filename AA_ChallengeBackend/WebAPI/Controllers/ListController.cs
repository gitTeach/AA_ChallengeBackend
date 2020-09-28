using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : BaseAPIController
    {
        public IListService _listService;
        private readonly IMapper _mapper;
        public ListController(IListService listService, IMapper mapper)
        {   
            _listService = listService ?? throw new ArgumentNullException(nameof(listService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ActionName("getAll")]
        public ActionResult<IEnumerable<ListDTO>> GetListForUser(string userId)
        {
            try
            {
                var data = _listService.GetLists(userId);
                return Ok(_mapper.Map<IEnumerable<ListDTO>>(data));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
