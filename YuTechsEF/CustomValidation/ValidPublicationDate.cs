using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuTechsEF.CustomValidation
{
    public class ValidPublicationDate: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
                DateTime publicationDate = (DateTime)value;

                // Validate between today and a week from today
                if (publicationDate >= DateTime.Today && publicationDate <= DateTime.Today.AddDays(7))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Publication date must be between today and a week from today.");
                }

        }

    }
}
