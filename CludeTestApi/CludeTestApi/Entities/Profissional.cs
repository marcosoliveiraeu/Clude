using System.ComponentModel.DataAnnotations;

namespace CludeTestApi.Entities
{
    public class Profissional
    {

        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        [MinLength(4)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(3)]
        public string NumeroDocumento { get; set; }

        [Required]
        public Especialidade Especialidade { get; set; }

    }
}
