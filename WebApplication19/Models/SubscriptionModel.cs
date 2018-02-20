using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth.Messages;
using System.Data.Entity;

namespace WebApplication19.Models
{
    public class SubscriptionModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "A first name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

       // [Required(ErrorMessage = "A last name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

      //  [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "Must Choose")]
        [Display(Name = "Who are you? :")]
        public int Radio { get; set; }

        //[Required(ErrorMessage = "A first name is required.")]
        [Display(Name = "First Name on Card")]
        public string FirstNameOnCard { get; set; }

        //[Required(ErrorMessage = "A last name is required.")]
        [Display(Name = "Last Name on Card")]
        public string LastNameOnCard { get; set; }

        //[Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Second Street Address")]
        public string SecStreetAddress { get; set; }

        //[Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public string City { get; set; }

        //[Required(ErrorMessage = "State is required.")]
        [Display(Name = "State")]
        public string State { get; set; }

       // [Required(ErrorMessage = "Zip is required.")]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

       // [Required(ErrorMessage = "Required.")]
        [StringLength(16, ErrorMessage = "Invalid Card", MinimumLength = 16)]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        //[Required(ErrorMessage = "Required.")]
        [Display(Name = "Expiration Date")]
        public string Expiration { get; set; }

        //[Required(ErrorMessage = "Required.")]
        [Display(Name = "CVV")]
        public string CVV { get; set; }

        [Display(Name = "How long do you wish to Subscribe?")]
        public short subscriptionLength { get; set; }


    }

    public class SubscriptionggDBContext : DbContext
    {
        public DbSet<SubscriptionModel> gg { get; set; }
    }
}