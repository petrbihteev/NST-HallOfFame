using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HallOfFame
{
    public partial class ConPersonSkill
    {
        [Key]
        public long IdPersonSkill { get; set; }
        [Required(ErrorMessage = "Уровень пустой")]
        [Range(1, 10, ErrorMessage = "Уровень навыка может быть в диапазоне от 1 до 10")]
        public byte Level { get; set; }
        [ForeignKey(nameof(Person))]
        public long PersonId { get; set; }
        [ForeignKey(nameof(Skill))]
        public long SkillId { get; set; }

        [InverseProperty("ConPersonSkills")]
        public virtual Person Person { get; set; } = null!;
        [InverseProperty("ConPersonSkills")]
        public virtual Skill Skill { get; set; } = null!;
    }
}
