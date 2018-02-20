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
    public class CreditCapturePrevious
    {
        /// <summary>
        /// Capture a Transaction Previously Submitted Via CaptureOnly
        /// </summary>
        /// <param name="ApiLoginID">Your ApiLoginID</param>
        /// <param name="ApiTransactionKey">Your ApiTransactionKey</param>
        /// <param name="TransactionAmount">The amount submitted with CaptureOnly</param>
        /// <param name="TransactionID">The TransactionID of the previous CaptureOnly operation</param>
        /// 

        public String ApiLoginID = "4Ng7NZnPw7f";
        public String ApiTransactionKey = "8tbXa5U943Q2ENpY";
        public decimal TransactionAmount;
        public string TransactionID;


        public void CaptureAmount(SubscriptionModel cCnumber)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var creditCard = new creditCardType
            {
                cardNumber = cCnumber.CardNumber,
                expirationDate = "0718"
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.priorAuthCaptureTransaction.ToString(),    // capture prior only
                payment = paymentType,
                amount = TransactionAmount,
                refTransId = "2244814046"
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            //validate
            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse != null)
                {
                    Debug.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                if (response.transactionResponse != null)
                {
                    Debug.WriteLine("Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
                }
            }

        }
    }
}