using System.ComponentModel.DataAnnotations;

namespace CludeTestApi.Entities
{
    public class Especialidade
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20)]
        [MinLength(3)]
        public string TipoDocumento { get; set; }
    }
}
