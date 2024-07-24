using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Domain.Interfaces;
using StallosDotnetPleno.Domain.Notifications;

namespace StallosDotnetPleno.Domain.Validators
{
    public class PersonValidator : IValidator<Person>
    {
        public void Validate(Person person, Notifier notifier)
        {
            // Add Person Validations
        }
    }
}
