using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Weather.Domain.Validation
{
    interface IDbContextDataAnotationValidation
    {
        bool TryValidate(object @object, out ICollection<ValidationResult> results);
    }
}
