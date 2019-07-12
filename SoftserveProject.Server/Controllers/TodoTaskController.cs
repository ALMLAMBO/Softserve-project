using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftserveProject.Server.DataAccess;
using SoftserveProject.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftserveProject.Server.Controllers {
	[Route("api/[controller]")]
	public class TodoTaskController : Controller {
		TaskDataAccessLayer taskDAL = new TaskDataAccessLayer();

		// GET: api/<controller>
		[HttpGet]
		[Route("get")]
		public Task<List<TodoTask>> Get() {
			return taskDAL.GetAllTasks();
		}

		// GET api/<controller>/5
		[HttpGet("get/{id}")]
		public Task<TodoTask> Get(string id) {
			return taskDAL.GetTodoData(id);
		}

		// POST api/<controller>
		[HttpPost]
		[Route("add-task")]
		public void Post([FromBody]TodoTask task) {
			taskDAL.AddTodoTask(task);
		}

		// PUT api/<controller>/5
		[HttpPut]
		[Route("update-task")]
		public void Put([FromBody]TodoTask task) {
			taskDAL.UpdateTodoTask(task);
		}

		// DELETE api/<controller>/5
		[HttpDelete("delete-task/{id}")]
		public void Delete(string id) {
			taskDAL.DeleteTodoTask(id);
		}
	}
}
