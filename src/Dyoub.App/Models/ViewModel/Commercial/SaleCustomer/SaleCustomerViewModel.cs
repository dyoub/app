// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.Customers;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.SaleCustomer
{
    public class SaleCustomerViewModel
    {
        [Required]
        public int? SaleOrderId { get; set; }

        public CustomerViewModel Customer { get; set; }

        public bool HasCustomerData
        {
            get { return Customer != null; }
        }

        public bool IsKnownCustomer
        {
            get { return HasCustomerData && Customer.Id != null; }
        }

        public Customer MapTo(Customer customer)
        {
            customer.Name = Customer.Name;
            customer.NationalId = Customer.NationalId;
            customer.Email = Customer.Email;
            customer.PhoneNumber = Customer.PhoneNumber;
            customer.AlternativePhoneNumber = Customer.AlternativePhoneNumber;

            return customer;
        }
    }
}
