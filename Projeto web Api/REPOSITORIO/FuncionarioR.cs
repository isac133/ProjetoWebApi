using Projeto_web_Api.Model;
using Projeto_web_Api.ORM;

namespace Projeto_web_Api.REPOSITORIO
{
    public class FuncionarioR : IFuncionarioRepositorio
      
    {
        private BdEmpresaContext _context;

        public FuncionarioR(BdEmpresaContext context)
        {
            _context = context;
        }

        public void Add(Funcionario funcionario, IFormFile foto)
        {
            // Verifica se uma foto foi enviada
            byte[] fotoBytes = null;
            if (foto != null && foto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    foto.CopyTo(memoryStream);
                    fotoBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbFuncionario = new TbFuncionario()
            {
                Nome = funcionario.Nome,
                Idade = funcionario.Idade,
                Foto = fotoBytes // Armazena a foto na entidade
            };

            // Adiciona a entidade ao contexto
            _context.TbFuncionarios.Add(tbFuncionario);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbFuncionario = _context.TbFuncionarios.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbFuncionario != null)
            {
                // Remove a entidade do contexto
                _context.TbFuncionarios.Remove(tbFuncionario);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }

        public List<Funcionario> GetAll()
        {
            List<Funcionario> listFun = new List<Funcionario>();

            foreach (var tb in _context.TbFuncionarios)
            {
                var funcionario = new Funcionario
                {
                    Id = tb.Id,
                    Nome = tb.Nome,
                    Idade = tb.Idade,

                    // Adicione aqui outras propriedades que precisar mapear
                };

                listFun.Add(funcionario);
            }

            return listFun;
        }
        
        public Funcionario GetById(int id)
        {
            var item = _context.TbFuncionarios.FirstOrDefault(f => f.Id == id);
            
            if (item == null) 
            {
                return null;
            }
            var funcionario = new Funcionario

            {
                Id = item.Id,
                Nome = item.Nome,
                Idade= item.Idade,
            };
            return funcionario;
        }               

        public void update(Funcionario funcionario)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbFuncionario = _context.TbFuncionarios.FirstOrDefault(f => f.Id == funcionario.Id);

            // Verifica se a entidade foi encontrada
            if (tbFuncionario != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbFuncionario.Nome = funcionario.Nome;
                tbFuncionario.Idade = funcionario.Idade;

                // Atualiza as informações no contexto
                _context.TbFuncionarios.Update(tbFuncionario);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }
    }
}
