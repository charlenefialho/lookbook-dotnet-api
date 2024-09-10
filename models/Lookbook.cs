using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lookbook_dotnet_api.models
{
    public class Lookbook
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
