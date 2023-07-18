using API_University.Data;
using API_University.Repositories;
using API_University.Repositories.Verifiers;
using Microsoft.AspNetCore.Mvc;

namespace API_University.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProfessorClassesController : ControllerBase
	{
		private readonly IProfessorClassRepository _profClassesRepository;

		public ProfessorClassesController(IProfessorClassRepository professorClassesRepository)
		{
			_profClassesRepository = professorClassesRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var classes = await _profClassesRepository.GetAll();
			return Ok(classes);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var res = await _profClassesRepository.Get(id);
			return Ok(res);
		}

		[HttpPost]
		public async Task<ActionResult<ProfessorClass>> Create([FromBody] ProfessorClass data)
		{
			int registersCounter = _profClassesRepository.CountById(data.ProfessorId);

			if (registersCounter >= 0 && registersCounter < 4)
			{
				await _profClassesRepository.Create(data);
				return Created(string.Empty, data.Id);
			}
			else
			{
				return BadRequest("El Maestro que quiso ingresar ya imparte 4 clases, no puede impartir más.");
			}
		}

		[HttpPut]
		public async Task<ActionResult<ProfessorClass>> Edit([FromBody] ProfessorClass data)
		{
			if (ModelState.IsValid)
			{
				var classes = await _profClassesRepository.Get(data.Id);

				if (classes == null)
				{
					return NotFound();
				}

				classes.ProfessorId = data.ProfessorId;
				classes.ClassId = data.ClassId;

				var response = await _profClassesRepository.Update(classes);
				return Ok(response);
			}
			else
			{
				return BadRequest("Uno o más campos ingresados son nulos o no se enviaron, por favor asergurate que todos los campos lleven su valor correspondiente.");
			}
		}
	}
}
