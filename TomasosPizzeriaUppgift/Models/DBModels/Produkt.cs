using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TomasosPizzeriaUppgift.Models
{
    public partial class Produkt
    {
        public Produkt()
        {
            MatrattProdukt = new HashSet<MatrattProdukt>();
        }

        public int ProduktId { get; set; }
        [Required(ErrorMessage = "Minst 2 bokstäver och max 50")]
        [StringLength(50,MinimumLength = 2, ErrorMessage = "Minst 2 bokstäver och max 50")]
        public string ProduktNamn { get; set; }

        public virtual ICollection<MatrattProdukt> MatrattProdukt { get; set; }
    }
}
