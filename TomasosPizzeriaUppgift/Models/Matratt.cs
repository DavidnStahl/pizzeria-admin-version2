using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TomasosPizzeriaUppgift.Models
{
    public partial class Matratt
    {
        public Matratt()
        {
            BestallningMatratt = new HashSet<BestallningMatratt>();
            MatrattProdukt = new HashSet<MatrattProdukt>();
        }

        public int MatrattId { get; set; }
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [StringLength(50, ErrorMessage = "Namn får vara max 50 tecken")]
        public string MatrattNamn { get; set; }
        public string Beskrivning { get; set; }
        public int Pris { get; set; }
        public int MatrattTyp { get; set; }
        
        public virtual MatrattTyp MatrattTypNavigation { get; set; }
        public virtual ICollection<BestallningMatratt> BestallningMatratt { get; set; }
        public virtual ICollection<MatrattProdukt> MatrattProdukt { get; set; }
    }
}
