using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Entities
{
    public class Questao
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(1000)]
        public string Enunciado { get; set; }
        [Required]
        [StringLength(1000)]
        public string A { get; set; }
        [Required]
        [StringLength(1000)]
        public string B { get; set; }
        [Required]
        [StringLength(1000)]
        public string C { get; set; }
        [Required]
        [StringLength(1000)]
        public string D { get; set; }
        [Required]
        public EnumAlternativas AlternativaCorreta { get; set; }
        [Required]
        [StringLength(100)]
        public string Materia { get; set; }
    }
}