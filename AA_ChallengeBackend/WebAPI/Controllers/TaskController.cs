﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Models.Response;
using Google.Apis.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    [Authorize]
    public class TaskController : BaseAPIController
    {
        
        private readonly IListService _listService;
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(IListService listService, ITaskService taskService, IMapper mapper)
        {
            _listService = listService ?? throw new ArgumentNullException(nameof(listService));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ActionName("GetTasksForList")]
        public ActionResult<IEnumerable<TaskDTO>> GetTasksForList(int idList)
        {
            try
            {
                if (!_listService.Exist(idList))
                {
                    return NotFound();
                }

                var data = _taskService.GetTasks(idList);
                return Ok(_mapper.Map<IEnumerable<TaskDTO>>(data));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetTasksDetail")]
        public ActionResult<TaskDetailResponse> GetTasksDetail(string userId, int idList, string category)
        {
            try
            {
                var data = _taskService.GetTasksDetail(userId, idList, category);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetTasksOverall")]
        public ActionResult<TaskDetailResponse> GetTasksOverall(string userId, string category)
        {
            try
            {
                var data = _taskService.GetTasksOverall(userId, category);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetTask")]
        public ActionResult<TaskDTO> GetTask(int idTask, string userId)
        {
            try
            {
                var data = _taskService.GetTask(idTask, userId);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<TaskDTO>(data));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        [ActionName("CreateTaskForList")]
        public ActionResult<TaskDTO> CreateTaskForList([FromBody] TaskToCreateDTO task)
        {
            try
            {
                if (!_listService.Exist(task.IdList))
                {
                    return NotFound();
                }

                var dbTask = _mapper.Map<Data.DbModels.TTask>(task);
                _taskService.AddTask(task.IdList, dbTask);

                var taskToReturn = _mapper.Map<TaskDTO>(dbTask);
                return Ok(taskToReturn);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut]
        [ActionName("UpdateTask")]
        public IActionResult UpdateTask( TaskToUpdateDTO task)
        {

            try
            {
                if (!_listService.Exist(task.IdList))
                {
                    return NotFound();
                }

                var dbTask = _taskService.GetTask(task.Id);

                if (dbTask == null)
                {
                    var taskToAdd = _mapper.Map<Data.DbModels.TTask>(task);

                    _taskService.AddTask(task.IdList, taskToAdd);

                    var taskToReturn = _mapper.Map<TaskDTO>(taskToAdd);
                    return Ok(taskToReturn);
                }

                _mapper.Map(task, dbTask);

                _taskService.UpdateTask(dbTask);

                return NoContent();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        

    }
}
