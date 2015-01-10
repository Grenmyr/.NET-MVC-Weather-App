using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Validation
{

    // Not using this to check if valid data from webservices before saving it.

    //public class DbContextDataAnotationValidation : IDbContextDataAnotationValidation 
    //{
    //    public bool TryValidate(object @object, out ICollection<ValidationResult> results)
    //    {
    //        var validationContext = new ValidationContext(@object);
    //        results = new List<ValidationResult>();
    //        return Validator.TryValidateObject(@object, validationContext, results, true);
    //    }
    //}
}
