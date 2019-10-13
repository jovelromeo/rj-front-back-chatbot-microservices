using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Extensions
{
    /// <summary>
    /// Extension class for friendly error messages.
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// method for getting a list of errors description
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                             .Select(m => m.ErrorMessage)
                             .ToList();
        }
    }
}
