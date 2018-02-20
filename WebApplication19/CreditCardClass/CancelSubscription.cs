using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using System.Diagnostics;
using WebApplication19.Models;

namespace WebApplication19.CreditCardClass
{

    public class CancelSubscription
    {
        public String ApiLoginID = "4Ng7NZnPw7f";
        public String ApiTransactionKey = "8tbXa5U943Q2ENpY";

        public void cancelSub()
        {

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            //Please change the subscriptionId according to your request
            var request = new ARBCancelSubscriptionRequest { subscriptionId = CreateSubscription.SubId };
            var controller = new ARBCancelSubscriptionController(request);                          // instantiate the contoller that will call the service
            controller.Execute();

            ARBCancelSubscriptionResponse response = controller.GetApiResponse();                   // get the response from the service (errors contained if any)

            //validate
            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response != null && response.messages.message != null)
                {
                    Debug.WriteLine("Success, Subscription Cancelled With RefID : " + response.refId);
                }
            }
            else
            {
                Debug.WriteLine("Error:with cancel " + response.messages.message[0].code + "  " + response.messages.message[0].text);
            }

        }
    }
}