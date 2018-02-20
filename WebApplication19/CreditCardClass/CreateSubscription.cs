using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using System.Diagnostics;
using WebApplication19.Models;


namespace WebApplication19.CreditCardClass
{
    public class CreateSubscription
    {
        public String ApiLoginID = "4Ng7NZnPw7f";
        public String ApiTransactionKey = "8tbXa5U943Q2ENpY";
        public static String SubId = "";

        public void CreateSub(SubscriptionModel cCnumber)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();

            interval.length = cCnumber.subscriptionLength;                       // months can be indicated between 1 and 12
            interval.unit = ARBSubscriptionUnitEnum.months;

            paymentScheduleType schedule = new paymentScheduleType
            {
                interval = interval,
                startDate = DateTime.Now.AddDays(1),      // start date should be tomorrow
                totalOccurrences = 9999,                          // 999 indicates no end date
                trialOccurrences = 3
            };

            #region Payment Information
            var creditCard = new creditCardType
            {
                cardNumber = cCnumber.CardNumber,
                expirationDate = cCnumber.Expiration
            };

            //standard api call to retrieve response
            paymentType cc = new paymentType { Item = creditCard };
            #endregion

            nameAndAddressType addressInfo = new nameAndAddressType()
            {
                firstName = cCnumber.FirstNameOnCard,
                lastName = cCnumber.LastNameOnCard
            };

            ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
            {
                amount = 35.55m,
                trialAmount = 0.00m,
                paymentSchedule = schedule,
                billTo = addressInfo,
                payment = cc
            };

            var request = new ARBCreateSubscriptionRequest { subscription = subscriptionType };

            var controller = new ARBCreateSubscriptionController(request);          // instantiate the contoller that will call the service
            controller.Execute();

            ARBCreateSubscriptionResponse response = controller.GetApiResponse();

                // get the response from the service (errors contained if any)

            SubId = response.subscriptionId.ToString();
            //validate
            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response != null && response.messages.message != null)
                {
                    Debug.WriteLine("Success, Create Subscription ID : " + response.subscriptionId.ToString());
                }
            }
            else
            {
                Debug.WriteLine("Error: subscription" + response.messages.message[0].code + "  " + response.messages.message[0].text);
            }

        }

    }
}