

using static Utility.Helper;

namespace DAL
{
    public class ProfileRepository : IProfileRepository
    {
        private IProfessorRepository ProfessorRepository { get; set; }
        private IStudentRepository StudentRepository { get; set; }
        public ProfileRepository(IProfessorRepository professorRepository, IStudentRepository studentRepository)
        {
            ProfessorRepository = professorRepository;
            StudentRepository = studentRepository;
        }
        public MasterProfile Select(string code, string password)
        {

            var student = StudentRepository.Select(code);
            if (student != null)
            {
                var checkPassword = StudentRepository.CheckPassword(code, password);
                if (checkPassword)
                    return new MasterProfile
                    {
                        Type = ProfileType.Student,
                        Profile = student
                    };

            }
            //var professor = ProfessorRepository.Select(code);
            //if (professor != null)
            //    return new MasterProfile
            //    {

            //    };

            return null;
        }
    }
}