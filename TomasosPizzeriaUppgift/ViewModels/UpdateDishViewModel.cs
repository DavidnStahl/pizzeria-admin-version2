using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;

namespace TomasosPizzeriaUppgift.ViewModels
{
    public class UpdateDishViewModel
    {
        public int id { get; set; }
        public List<Produkt> Ingredienses{ get; set; }
        [Required(ErrorMessage ="Maträtt måste ha ett namn")]
        [StringLength(50,MinimumLength =2 ,ErrorMessage ="Minst 2 max 50 bokstäver")]
        public string Matrattnamn { get; set; }
        public int MatrattstypID { get; set; }
        public List<Produkt> SelectedListItem { get; set; }
        public List<int> NewSelectedListItem { get; set; }
        public bool Matrattsnamnlength { get; set; }
        public bool MatrattsnamnTaken { get; set; }
        public bool Ingredienslow { get; set; }
        public bool IngrediensTaken { get; set; }
        public int Pris { get; set; }
        public List<MatrattTyp> Mattratttyper { get; set; }

        public UpdateDishViewModel()
        {
            NewSelectedListItem = new List<int>();
            Mattratttyper = new List<MatrattTyp>();
            Ingredienses = new List<Produkt>();
            SelectedListItem = new List<Produkt>();
        }
    }
}
