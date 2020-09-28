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
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class ListController : BaseAPIController
    {
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        public ListController(IListService listService, IMapper mapper)
        {   
            _listService = listService ?? throw new ArgumentNullException(nameof(listService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ActionName("GetListsForUser")]
        public ActionResult<IEnumerable<ListDTO>> GetListsForUser(string userId)
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

        [HttpGet]
        [ActionName("GetListForUser")]
        public ActionResult<ListDTO> GetListForUser(string userId, int listId)
        {
            try
            {
                var data = _listService.GetList(userId, listId);
                if (data == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ListDTO>(data));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpPost]
        [ActionName("CreateListForUser")]
        public ActionResult<ListDTO> CreateListForUser(string userId, ListToCreateDTO list)
        {
            try
            {
                var dblist = _mapper.Map<Data.DbModels.TList>(list);
                _listService.AddList(userId, dblist);

                var listToReturn = _mapper.Map<ListDTO>(dblist);
                return CreatedAtRoute("GetListForUser",new { userId = userId, listId = listToReturn.Id },listToReturn);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpPut]
        public IActionResult UpdateListForUser(string userId, int listId, ListToUpdateDTO list)
        {

            try
            {
                var dbList = _listService.GetList(userId, listId);

                if (dbList == null)
                {
                    var listToAdd = _mapper.Map<Data.DbModels.TList>(list);

                    _listService.AddList(userId, listToAdd);
                    
                    var listToReturn = _mapper.Map<ListDTO>(listToAdd);
                    return CreatedAtRoute("GetListForUser",new { userId = userId, listId = listToReturn.Id }, listToReturn);
                }

                _mapper.Map(list, dbList);

                _listService.UpdateList(dbList);

                return NoContent();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
