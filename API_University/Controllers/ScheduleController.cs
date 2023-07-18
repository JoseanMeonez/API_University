using API_University.Data;
using API_University.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_University.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScheduleController : ControllerBase
	{
		private readonly IScheduleRepository _scheduleRepository;

		public ScheduleController(IScheduleRepository scheduleRepository)
		{
			_scheduleRepository = scheduleRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var schedules = await _scheduleRepository.GetAll();
			return Ok(schedules);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var res = await _scheduleRepository.Get(id);
			return Ok(res);
		}

		[HttpPost]
		public async Task<ActionResult<Schedule>> Create([FromBody] Schedule data)
		{
			if (data.StartHour.Hour >= 8 && data.EndHour.Hour <= 17)
			{       
				var validate_prof = _scheduleRepository.ProfessorHourVerifier(data);
				var validate_classroom = _scheduleRepository.ClassroomHourVerifier(data);


				if (validate_prof.Count() == 0 && validate_classroom.Count() == 0)
				{
					var schedule = new Schedule()
					{
						StartHour = data.StartHour,
						EndHour = data.EndHour,
						ClassId = data.ClassId,
						ClassroomId = data.ClassroomId,
						ProfessorId = data.ProfessorId,
						StudentId = data.StudentId,
					};

					await _scheduleRepository.Create(schedule);
					return Created(string.Empty, data.Id);
				}
				else
				{
					if (validate_prof.Count() > 0 && validate_classroom.Count() > 0)
					{
						return BadRequest("El maestro y el Aula ya tiene clase en ese horario.");

					}
					else if (validate_prof.Count() > 0)
					{
						return BadRequest("El maestro ya tiene clase en ese horario."); 
					}
					else
					{
						return BadRequest("El Aula ya tiene clase asignada en ese horario.");
					}
				}
			}
			else
			{
				return BadRequest("Las horas están fuera del horario de clases permitido.");        
			}
		}

		[HttpPut]
		public async Task<ActionResult<Schedule>> Edit([FromBody] Schedule data)
		{
			if (ModelState.IsValid)
			{
				var schedules = await _scheduleRepository.Get(data.Id);

				if (schedules == null)
				{
					return NotFound();
				}

				schedules.StartHour = data.StartHour;
				schedules.EndHour = data.EndHour;
				schedules.ClassId = data.ClassId;
				schedules.ClassroomId = data.ClassroomId;

				schedules.ClassId = data.ClassId;

				var response = await _scheduleRepository.Update(schedules);
				return Ok(response);
			}
			else
			{
				return BadRequest("Uno o más campos ingresados son nulos o no se enviaron, por favor asergurate que todos los campos lleven su valor correspondiente.");
			}
		}
	}
}
