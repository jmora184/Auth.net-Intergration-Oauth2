using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Text;
using System.Threading.Tasks;

using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using WebApplication19.Models;
using System.Diagnostics;

namespace WebApplication19.CreditCardClass
{
    public class ChargeCreditCard
    {
        public String ApiLoginID = "4Ng7NZnPw7f";
        public String ApiTransactionKey = "8tbXa5U943Q2ENpY";
        public static String iDcode = "";

        public void ChargeCC(SubscriptionModel cCnumber)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = cCnumber.CardNumber,
                expirationDate = cCnumber.Expiration,
                cardCode = cCnumber.CVV
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card
                amount = 133.45m,
                payment = paymentType
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            iDcode = response.transactionResponse.authCode;

            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse != null)
                {
                    Debug.WriteLine("Success, Auth Code Charge : " + response.transactionResponse.authCode);
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                if (response.transactionResponse != null)
                {
                    Debug.WriteLine("Transaction Error Charge: " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
                }
            }
        }
    }
}