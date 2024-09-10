using System.ComponentModel.DataAnnotations;

namespace lookbook_dotnet_api.models
{

    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public Categoria Categoria { get; set; }
    }

    public enum Categoria
    {
        Acessorio,
        Camisa,
        Sapato,
        Outro
    }

}