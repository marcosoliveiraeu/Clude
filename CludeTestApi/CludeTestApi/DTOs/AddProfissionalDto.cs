using System.ComponentModel.DataAnnotations;

namespace CludeTestApi.DTOs
{
    public class AddProfissionalDto
    {

        [Required]
        [StringLength(100)]
        [MinLength(4)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(3)]
        public string NumeroDocumento { get; set; }

        [Required]
        public int IdEspecialidade { get; set; }
    }
}
