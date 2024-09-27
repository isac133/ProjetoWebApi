using Projeto_web_Api.Model;

namespace Projeto_web_Api.REPOSITORIO
{
    public interface IFuncionarioRepositorio
    {
        public void Add(Funcionario funcionario, IFormFile foto);

        List<Funcionario> GetAll();

        void update(Funcionario funcionario);

        void Delete(int id);


    }
}
