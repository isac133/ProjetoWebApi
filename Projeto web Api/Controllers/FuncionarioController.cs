using Microsoft.AspNetCore.Mvc;
using Projeto_web_Api.Model;
using Projeto_web_Api.REPOSITORIO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projeto_web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioR _funcionarioRepo; // O repositório que contém GetAll()

        public FuncionarioController(FuncionarioR funcionarioRepo)
        {
            _funcionarioRepo = funcionarioRepo;
        }

        // GET: api/<FuncionarioController>
        [HttpGet]
        public ActionResult<List<Funcionario>> GetAll()
        {
            var funcionarios = _funcionarioRepo.GetAll();
            return Ok(funcionarios); // Retorna a lista de funcionários com status 200 OK
        }

        // GET api/<FuncionarioController>/5
        [HttpGet("{id}")]
        public ActionResult<Funcionario> GetById(int id)
        {
            var funcionario = _funcionarioRepo.GetById(id);


            if (funcionario == null)
            {
                return NotFound();
            }
            return Ok(funcionario);
        }

        // POST api/<FuncionarioController>     
        [HttpPost]
        public ActionResult<object> Post([FromForm] FuncionarioDto novoFuncionario)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var funcionario = new Funcionario
            {
                Nome = novoFuncionario.Nome,
                Idade = novoFuncionario.Idade
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _funcionarioRepo.Add(funcionario, novoFuncionario.Foto);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Nome = funcionario.Nome,
                Idade = funcionario.Idade
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromBody] Funcionario funcionarioAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var funcionarioExistente = _funcionarioRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (funcionarioExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            funcionarioExistente.Nome = funcionarioAtualizado.Nome;
            funcionarioExistente.Idade = funcionarioAtualizado.Idade;

            // Chama o método de atualização do repositório
            _funcionarioRepo.update(funcionarioExistente);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Nome = funcionarioExistente.Nome,
                Idade = funcionarioExistente.Idade
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<FuncionarioController>/5
        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var funcionarioExistente = _funcionarioRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (funcionarioExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _funcionarioRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Nome = funcionarioExistente.Nome,
                Idade = funcionarioExistente.Idade
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        } 
    }
}
