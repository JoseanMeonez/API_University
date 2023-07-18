using API_University.Data;
using API_University.Repositories;
using API_University.Repositories.Verifiers;
using Microsoft.AspNetCore.Mvc;

namespace API_University.Controllers
{
  [Route("api/[controller]")]
	[ApiController]
	public class ClassController : ControllerBase
	{
		private readonly IClassRepository _classesRepository;
		private readonly INameChecker _checkerRepository;

		public ClassController(IClassRepository classesRepository, INameChecker checkerRepository)
		{
			_classesRepository = classesRepository;
			_checkerRepository = checkerRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var classes = await _classesRepository.GetAll();
			return Ok(classes);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var res = await _classesRepository.Get(id);
			return Ok(res);
		}

		[HttpPost]
		public async Task<ActionResult<Class>> Create([FromBody] Class data)
		{
			bool nameChecker = _checkerRepository.NameVerifier(data.Name);

			if (nameChecker != false)
			{
				return BadRequest("Hay un campo que ya existe en la base de datos, favor verifica lo ingresado.");
			}
			else
			{
				var classes = new Class()
				{
					Id = data.Id,
					Name = data.Name
				};

				await _classesRepository.Create(classes);
				return Created(string.Empty, classes.Id);
			}

		}

		[HttpPut]
		public async Task<ActionResult<Class>> Edit([FromBody] Class data)
		{
			if (ModelState.IsValid)
			{
				var classes = await _classesRepository.Get(data.Id);

				if (classes == null)
				{
					return NotFound();
				}

				classes.Name = data.Name;

				var response = await _classesRepository.Update(classes);
				return Ok(response);		
			}
			else
			{
				return BadRequest("Uno o más campos ingresados son nulos o no se enviaron, por favor asergurate que todos los campos lleven su valor correspondiente.");
			}
		}
	}
}
