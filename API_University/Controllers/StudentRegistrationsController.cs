using API_University.Data;
using API_University.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_University.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentRegistrationsController : ControllerBase
	{
		private readonly IStudentRegistrationRepository _stuRegistrationRepository;

		public StudentRegistrationsController(IStudentRegistrationRepository stuRegistrationRepository)
		{
			_stuRegistrationRepository = stuRegistrationRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var classes = await _stuRegistrationRepository.GetAll();
			return Ok(classes);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var res = await _stuRegistrationRepository.Get(id);
			return Ok(res);
		}

		[HttpPost]
		public async Task<ActionResult<StudentRegistration>> Create([FromBody] StudentRegistration data)
		{
			int registersCounter = _stuRegistrationRepository.CountById(data.StudentId);

			if (registersCounter >= 0 && registersCounter < 5)
			{
				await _stuRegistrationRepository.Create(data);
				return Created(string.Empty, data.Id);
			}
			else
			{
				return BadRequest("Un Alumno no puede matricular más de 5 clases.");
			}
		}

		[HttpPut]
		public async Task<ActionResult<StudentRegistration>> Edit([FromBody] StudentRegistration data)
		{
			if (ModelState.IsValid)
			{
				var classes = await _stuRegistrationRepository.Get(data.Id);

				if (classes == null)
				{
					return NotFound();
				}

				classes.ClassId = data.ClassId;
				classes.StudentId = data.StudentId;

				var response = await _stuRegistrationRepository.Update(classes);
				return Ok(response);
			}
			else
			{
				return BadRequest("Uno o más campos ingresados son nulos o no se enviaron, por favor asergurate que todos los campos lleven su valor correspondiente.");
			}
		}
	}
}
