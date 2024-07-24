using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Domain.Interfaces;
using StallosDotnetPleno.Domain.Notifications;

namespace StallosDotnetPleno.Domain.Validators
{
    public class AddressValidator : IValidator<Address>
    {
        public void Validate(Address address, Notifier notifier)
        {
            // Add Address Validations
        }
    }
}
