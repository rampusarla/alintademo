using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alinta.Core.Messages
{
    public static class ErrorMessages
    {
        public const string UnhandledExpcetion = "There is an unhandled error occurred. Please try again later.";
        public const string DateOfBirthGreaterThanCurrentDate = "Date of Birth can not be future. Please choose the correct date.";
        public const string DuplicateCustomer = "Customer with the same name and date of birth already exists. Please choose a different customer";
        public const string NameExceededMaxLength = "{0} has exceeded the max length of {1} characters";
        public const string CustomerNotFound = "Customer doesn't exist. Please choose the correct customer";
    }
}
