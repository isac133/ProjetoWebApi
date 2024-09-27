namespace Projeto_web_Api.Model
{
    public class Funcionario
    {  
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public int Idade { get; set; }

        public byte[]? Foto { get; set; }

    }
}
   