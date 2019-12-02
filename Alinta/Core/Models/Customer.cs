using Alinta.Core.Messages;
using Alinta.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alinta.Core.Models
{
    public class CustomerViewModel : IValidatableObject
    {                        
        [Required]
        [MaxLength(50, ErrorMessage = ErrorMessages.NameExceededMaxLength)]        
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = ErrorMessages.NameExceededMaxLength)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {            
            var context = (CustomerDBContext)validationContext.GetService(typeof(CustomerDBContext));
                        
            if (DateOfBirth > DateTime.Now)
            {
                yield return new ValidationResult(ErrorMessages.DateOfBirthGreaterThanCurrentDate);
            }


            if(context.Customers.Any(c => c.FirstName.Trim().ToUpper() == FirstName.Trim().ToUpper() && c.LastName.Trim().ToUpper() == LastName.Trim().ToUpper() && c.DateOfBirth == DateOfBirth))
            {
                yield return new ValidationResult(ErrorMessages.DuplicateCustomer);
            }

        }
    }

    public class Customer : CustomerViewModel
    {
        public int Id { get; set; }
    }
}
