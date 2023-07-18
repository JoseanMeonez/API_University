using API_University.Data;
using API_University.Repositories;
using API_University.Repositories.Verifiers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_University.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly IStudentRepository _studentRepository;
		private readonly INameChecker _checkerRepository;
		private readonly IPhoneNumberChecker _phoneCheckerRepository;
		private readonly IEmailChecker _emailCheckerRepository;

		public StudentController(IStudentRepository studentRepository, INameChecker checkerRepository, IPhoneNumberChecker phoneCheckerRepository, IEmailChecker emailCheckerRepository)
		{
			_studentRepository = studentRepository;
			_checkerRepository = checkerRepository;
			_phoneCheckerRepository = phoneCheckerRepository;
			_emailCheckerRepository = emailCheckerRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var student = await _studentRepository.GetAll();
			return Ok(student);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var res = await _studentRepository.Get(id);
			return Ok(res);
		}

		[HttpPost]
		public async Task<ActionResult<Student>> Create([FromBody] Student data)
		{
			bool nameChecker = _checkerRepository.NameVerifier(data.Name);
			bool emailChecker = _emailCheckerRepository.EmailVerifier(data.Name);
			bool phoneChecker = _phoneCheckerRepository.CellphoneVerifier(data.CelNumber);

			if (nameChecker != false || emailChecker != false || phoneChecker != false)
			{
				return BadRequest("Hay un campo que ya existe en la base de datos, favor verifica lo ingresado.");
			} 
			else 
			{			
				var lastId = _studentRepository.GetLastId();

				var student = new Student()
				{
					Id = data.Id,
					Code = "AULM0" + (lastId + 1),
					Name = data.Name,
					CelNumber = data.CelNumber,
					Email = data.Email,
				};
				await _studentRepository.Create(student);
				return Created(string.Empty, student.Id);
			}
		}

		[HttpPut]
		public async Task<ActionResult<Student>> Edit([FromBody] Student data)
		{
			// Verifyng that all of the model's attributes are sended
			if (ModelState.IsValid)
			{
				var student = await _studentRepository.Get(data.Id);
				
				if (student == null)
				{
						return NotFound();
				}

				student.Name = data.Name;
				student.Code = data.Code;
				student.CelNumber = data.CelNumber;
				student.Email= data.Email;

				var response = await _studentRepository.Update(student);
				return Ok(response);
			}
			else
			{
				return BadRequest("Uno o más campos ingresados son nulos o no se enviaron, por favor asergurate que todos los campos lleven su valor correspondiente.");
			}

		}
	}
}
