using AutoMapper;
using API_University.Data;
using API_University.DTOs.Professor;

namespace API_University.Mappings
{
  public class GeneralProfile : Profile
	{
		public GeneralProfile()
		{
			#region DTOs
			CreateMap<Professor, ProfessorDto>();
			#endregion
		}
	}
}
