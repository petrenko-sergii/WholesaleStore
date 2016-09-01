using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WholesaleStore.Models;

namespace WholesaleStore.Models
{
    public class InsertError
    {
        public string ErrorMessageLog { get; set; }
        public string ErrorMessagePassword { get; set; }
        public string ErrorMessageConfirmPassword { get; set; }
        public string ErrorMessageEmptyConfirmPassword { get; set; }
        public string ErrorMessageTooBigAmount { get; set; }
        public string ErrorMessageEmptyName { get; set; }
        public string ErrorMessageEmptyPhoneNumber { get; set; }
        public string ErrorMessageWrongPhone { get; set; }
        public string ErrorMessageEmptyEmail { get; set; }
        public string ErrorMessageWrongEmail { get; set; }
        public string ErrorMessageEmptyAddress { get; set; }
        public string ErrorMessageEmptyCustomer { get; set; }
        public string ErrorMessageEmptyBarCode { get; set; }
        public string ErrorMessageEmptyAddAmount { get; set; }
        public string ErrorMessageEmptyAmount { get; set; }
        public string ErrorMessageWrongAmount { get; set; }
        public string ErrorMessageWrongAddAmount { get; set; }
        public string ErrorMessageEmptyPrice { get; set; }
        public string ErrorMessageWrongPrice { get; set; }
        public string ErrorMessageWrongBarCode { get; set; }
        public string ErrorMessageEmptyUnit { get; set; }
        public string ErrorMessageEmptyEditUnit { get; set; }
        public string ErrorMessageEmptyCategory { get; set; }
        public string ErrorMessageEmptyEditCategory { get; set; }
        public string ErrorMessageEmptyEditCustomer { get; set; }
        public string ErrorMessageEmptyOrdersCustomer { get; set; }
        public string ErrorMessageEmptyProdCategory { get; set; }
        public string ErrorMessageEmptyUserType { get; set; }
        public string ErrorMessageEmptyItem { get; set; }
        public string ErrorMessageEmptyChangeAmountItem { get; set; }
        public string ErrorMessageEmptyProductId { get; set; }
        public string ErrorMessageEmptyEditProductId { get; set; }
        public string ErrorMessageEmptyAmountZeroProductId { get; set; }
        public string ErrorMessageEmptyAmountChangeProductId { get; set; }
        public string ErrorMessageEmptyAmountAddProductId { get; set; }
        public string ErrorMessageEmptyPriceProductId { get; set; }
        public string ErrorMessageEmptyOrder { get; set; }
        public string ErrorMessageEmptyShowOrder { get; set; }
    }
}