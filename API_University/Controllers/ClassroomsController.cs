using API_University.Data;
using API_University.Repositories;
using API_University.Repositories.Verifiers;
using Microsoft.AspNetCore.Mvc;

namespace API_University.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassroomsController : ControllerBase
	{
		private readonly IClassroomRepository _classroomsRepository;
		
		public ClassroomsController(IClassroomRepository classroomsRepository, INameChecker checkerRepository)
		{
			_classroomsRepository = classroomsRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var classes = await _classroomsRepository.GetAll();
			return Ok(classes);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var res = await _classroomsRepository.Get(id);
			return Ok(res);
		}

		[HttpPost]
		public async Task<ActionResult<Classroom>> Create(Classroom data)
		{
			var lastId = _classroomsRepository.GetLastId();

			var classroom = new Classroom()
			{
				Id = data.Id,
				Code = "AU0" + (lastId + 1),
			};
			await _classroomsRepository.Create(classroom);
			return Created(string.Empty, classroom.Id);
		}
	}
}