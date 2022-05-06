using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonnifyApp.Models
{
    public class MonnifyApp
    {

    }

    public class InitializeTransct
    {
        [Required(ErrorMessage = "amount is required")]
        public string amount { get; set; }

        [Required(ErrorMessage = "customerName is required")]
        public string customerName { get; set; }

        [Required(ErrorMessage = "customerEmail is required")]
        public string customerEmail { get; set; }

        [Required(ErrorMessage = "paymentReference is required")]
        public string paymentReference { get; set; }

        [Required(ErrorMessage = "paymentDescription is required")]
        public string paymentDescription { get; set; }

        [Required(ErrorMessage = "currencyCode is required")]
        public string currencyCode { get; set; }

        [Required(ErrorMessage = "contractCode is required")]
        public string contractCode { get; set; }

        [Required(ErrorMessage = "redirectUrl is required")]
        public string redirectUrl { get; set; }

        [Required(ErrorMessage = "paymentMethods is required")]
        public string paymentMethods { get; set; }
        public incomeSplitConfig[] incplit { get; set; }


    }

    public class incomeSplitConfig
    {
        [Required(ErrorMessage = "subAccountCode is required")]
        public string subAccountCode { get; set; }

        [Required(ErrorMessage = "feePercentage is required")]
        public string feePercentage { get; set; }

        [Required(ErrorMessage = "splitAmount is required")]
        public string splitAmount { get; set; }

        [Required(ErrorMessage = "feeBearer is required")]
        public string feeBearer { get; set; }
    }

    public class PayWithBnk
    {
        [Required(ErrorMessage = "transactionReference is required")]
        public string transactionReference { get; set; }

        [Required(ErrorMessage = "bankCode is required")]
        public string bankCode { get; set; }
    }

    public class PayWithCardNoOTP
    {
        public string transactionReference { get; set; }
        public string collectionChannel { get; set; }
        public string OTP { get; set; }
        public Card[] card { get; set; }
    }

    //public class PayWithCardOTP
    //{
    //    public string transactionReference { get; set; }
    //    public string collectionChannel { get; set; }

    //    public Card[] card { get; set; }
    //}

    public class Card
    {
        [Required(ErrorMessage = "number is required")]
        public string number { get; set; }

        [Required(ErrorMessage = "expiryMonth is required")]
        public string expiryMonth { get; set; }

        [Required(ErrorMessage = "expiryYear is required")]
        public string expiryYear { get; set; }

        [Required(ErrorMessage = "pin is required")]
        public string pin { get; set; }

        [Required(ErrorMessage = "cvv is required")]
        public string cvv { get; set; }
    }

    public class AuthorizeOTP
    {
        [Required(ErrorMessage = "transactionReference is required")]
        public string transactionReference { get; set; }

        [Required(ErrorMessage = "collectionChannel is required")]
        public string collectionChannel { get; set; }

        [Required(ErrorMessage = "tokenId is required")]
        public string tokenId { get; set; }

        [Required(ErrorMessage = "token is required")]
        public string token { get; set; }
    }


    public class Secure3DSAuth
    {
        [Required(ErrorMessage = "transactionReference is required")]
        public string transactionReference { get; set; }

        [Required(ErrorMessage = "collectionChannel is required")]
        public string collectionChannel { get; set; }

        [Required(ErrorMessage = "card is required")]
        public Card[] card { get; set; }
    }

}
