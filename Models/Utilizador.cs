using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaneGEST_Blazor_MySQL.Models
{
    [Table("utilizadores")]
    public class Utilizador
    {
        [Key]
        [Column("id_utilizador")]
        public int IdUtilizador { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = "";

        [Column("passwordhash")]
        public string PasswordHash { get; set; } = "";
    }
}
