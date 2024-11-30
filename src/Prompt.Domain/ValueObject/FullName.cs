using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Prompt.Domain.ValueObject
{
    public sealed record FullName
    {
        private const string NamePattern = @"^[\p{L}][\p{L}'\- ]+$"; 
        private const int MinNameLength = 2;
        private const int MaxNameLength = 50; 

        public string FirstName { get; init; }
        public string LastName { get; init; }
  
        public FullName(string firstName, string lastName)
        {
            FirstName = ValidateName(firstName, nameof(firstName));
            LastName = ValidateName(lastName, nameof(lastName));
        }

    
        private static string ValidateName(string name, string paramName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"{paramName} cannot be null or whitespace.", paramName);

            if (name.Length < MinNameLength || name.Length > MaxNameLength)
                throw new ArgumentException($"{paramName} must be between {MinNameLength} and {MaxNameLength} characters.", paramName);
            if (!Regex.IsMatch(name, NamePattern))

                throw new ArgumentException($"{paramName} contains invalid characters.", paramName);       
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
        }
        public static FullName Create(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name cannot be null or whitespace.", nameof(fullName));

            var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
                throw new ArgumentException("Full name must contain at least first name and last name.", nameof(fullName));

            string firstName = string.Join(' ', parts.Take(parts.Length - 1));
            string lastName = parts.Last();

            return new FullName(firstName, lastName);
        }

        public static FullName Create(string firstName, string lastName)
        {
            return new FullName(firstName, lastName);
        }
      
        public static explicit operator string(FullName fullName) => fullName.ToString();
        
        public static explicit operator FullName(string value) => Create(value);

        public override string ToString() => $"{FirstName} {LastName}";

        public string GetInitials()
        {
            char firstInitial = FirstName.FirstOrDefault(char.IsLetter);
            char lastInitial = LastName.FirstOrDefault(char.IsLetter);

            return $"{char.ToUpper(firstInitial)}.{char.ToUpper(lastInitial)}.";
        }
    }
}
