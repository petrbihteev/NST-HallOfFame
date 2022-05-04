using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HallOfFame
{
    public partial class Skill
    {
        public Skill()
        {
            ConPersonSkills = new HashSet<ConPersonSkill>();
        }

        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Наименование навыка пустое")]
        [StringLength(25, ErrorMessage = "Наименование навыка больше 25 символов!")]
        public string Name { get; set; } = null!;

        [InverseProperty("Skill")]
        public virtual ICollection<ConPersonSkill> ConPersonSkills { get; set; }
    }
}
