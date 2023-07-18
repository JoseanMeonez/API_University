using API_University.Data;
using API_University.Repositories;
using API_University.Repositories.Verifiers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_University.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProfessorController : ControllerBase
	{
		private readonly IProfessorRepository _professorRepository;
		private readonly INameChecker _checkerRepository;
		private readonly IPhoneNumberChecker _phoneCheckerRepository;
		private readonly IEmailChecker _emailCheckerRepository;

		public ProfessorController(IProfessorRepository professorRepository, INameChecker checkerRepository, IPhoneNumberChecker phoneCheckerRepository, IEmailChecker emailCheckerRepository)
		{
			_professorRepository = professorRepository;
			_checkerRepository = checkerRepository;
			_phoneCheckerRepository = phoneCheckerRepository;
			_emailCheckerRepository = emailCheckerRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var professor = await _professorRepository.GetAll();
			return Ok(professor);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var res = await _professorRepository.Get(id);
			return Ok(res);
		}

		[HttpPost]
		public async Task<ActionResult<Professor>> Create([FromBody] Professor data)
		{
			bool nameChecker = _checkerRepository.NameVerifier(data.Name);
			bool emailChecker = _emailCheckerRepository.EmailVerifier(data.Email);
			bool phoneChecker = _phoneCheckerRepository.CellphoneVerifier(data.CelNumber);
			
			if (nameChecker != false || emailChecker != false || phoneChecker != false)
			{
				return BadRequest("Hay un campo que ya existe en la base de datos, favor verifica lo ingresado.");
			}
			else
			{
				var lastId = _professorRepository.GetLastId();

				var professor = new Professor()
				{
					Id = data.Id,
					Code = "MAE0" + (lastId + 1),
					Name = data.Name,
					CelNumber = data.CelNumber,
					Email = data.Email,
					EnrollmentDate = data.EnrollmentDate,
				};
				await _professorRepository.Create(professor);
				return Created(string.Empty, professor.Id);
			}
		}

		[HttpPut]
		public async Task<ActionResult<Professor>> Edit([FromBody] Professor data)
		{
			if (ModelState.IsValid)
			{
				var professor = await _professorRepository.Get(data.Id);

				if (professor == null)
				{
					return NotFound();
				}

				professor.Name = data.Name;
				professor.Code = data.Code;
				professor.CelNumber = data.CelNumber;
				professor.Email = data.Email;
				professor.EnrollmentDate = data.EnrollmentDate;

				var response = await _professorRepository.Update(professor);
				return Ok(response);
			}
			else
			{
				return BadRequest("Uno o más campos ingresados son nulos o no se enviaron, por favor asergurate que todos los campos lleven su valor correspondiente.");
			}
		}
	}
}
