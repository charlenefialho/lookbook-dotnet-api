using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace lookbook_dotnet_api.models
{
    public class Lookbook
    {
        [Key]
        [SwaggerSchema("Identificador único do Lookbook")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [SwaggerSchema("Nome do lookbook", Description = "Nome dado ao lookbook")]
        public string Nome { get; set; }

        [Required]
        [SwaggerSchema("Descrição detalhada do lookbook")]
        public string Descricao { get; set; }

        [Required]
        [SwaggerSchema("Data de criação do lookbook")]
        public DateTime DataCriacao { get; set; }

        [SwaggerSchema("Lista de produtos associados a esse lookbook")]
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
