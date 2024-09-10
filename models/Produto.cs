using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Categoria { get; set; }

        public ICollection<Lookbook> Lookbooks { get; set; } = new List<Lookbook>();
    }

}