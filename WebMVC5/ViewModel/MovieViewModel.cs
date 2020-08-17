using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;

namespace WebMVC5.ViewModel
{
    public class MovieViewModel: IValidatableObject
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; } 
        public string Genre { get; set; }
        public decimal Price { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (this.Title == "昕力")
            {
                //yield return new ValidationResult("Title cannot match 昕力");
                //yield return new ValidationResult(
                //    "Title cannot match 昕力", new[] { nameof(Title) });
                results.Add(new ValidationResult("Title cannot match 昕力", new[] { nameof(Title) }));
            }

            if (this.Price < 100)
            {
                results.Add(new ValidationResult("Price 需大於100", new[] { nameof(Price) }));
            }

            return results;
        }
    }
}