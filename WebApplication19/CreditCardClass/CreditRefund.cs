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
    public class CreditRefund
    {
        public String ApiLoginID = "4Ng7NZnPw7f";
        public String ApiTransactionKey = "8tbXa5U943Q2ENpY";

        public void refund()
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var bankAccount = new bankAccountType
            {
                accountNumber = "4111111",
                routingNumber = "325070760",
                echeckType = echeckTypeEnum.WEB,   // change based on how you take the payment (web, telephone, etc)
                nameOnAccount = "Test Name"
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = bankAccount };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.refundTransaction.ToString(),    // refund type
                payment = paymentType,
                amount = 126.44m,
                refTransId = ChargeCreditCard.iDcode
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
                    Debug.WriteLine("Success, Got refund : " + response.transactionResponse.transId);
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                if (response.transactionResponse != null)
                {
                    Debug.WriteLine("Transaction did not get refund : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
                }
            }
        }
    }
}