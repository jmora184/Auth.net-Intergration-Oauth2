using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication19.Models;
using WebApplication19.CreditCardClass;

namespace WebApplication19.Controllers
{
    public class SubscriptionModelsController : Controller
    {
        private SubscriptionggDBContext db = new SubscriptionggDBContext();

        // GET: SubscriptionModels
        public async Task<ActionResult> Index()
        {
            return View(await db.gg.ToListAsync());
        }
        public ActionResult Cancel()
        {
            CancelSubscription cxl = new CancelSubscription();
            CreditRefund cr = new CreditRefund();
            cr.refund();
            //cxl.cancelSub();
            return View();

        }

        // GET: SubscriptionModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionModel subscriptionModel = await db.gg.FindAsync(id);
            if (subscriptionModel == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionModel);
        }

        // GET: SubscriptionModels/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult UserPage(SubscriptionModel subModel)
        {
            return View(subModel.Email);
          
        }

        // POST: SubscriptionModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Email,FirstName,LastName,Company,Password,ConfirmPassword,Radio,FirstNameOnCard,LastNameOnCard,StreetAddress,SecStreetAddress,City,State,Zip,CardNumber,Expiration,CVV,subscriptionLength")] SubscriptionModel subscriptionModel)
        {
            CreditAuthorizations cA = new CreditAuthorizations();
            CreditCapturePrevious cP = new CreditCapturePrevious();
            ChargeCreditCard cc = new ChargeCreditCard();
            CreateSubscription cs = new CreateSubscription();
            
            
            if (ModelState.IsValid)
            {
                db.gg.Add(subscriptionModel);
                await db.SaveChangesAsync();
                return RedirectToAction("UserPage");
            }

          //  cA.Authorize(subscriptionModel);
            //cc.ChargeCC(subscriptionModel);
            //cs.CreateSub(subscriptionModel);
            
            return RedirectToAction("UserPage");
        }

        // GET: SubscriptionModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionModel subscriptionModel = await db.gg.FindAsync(id);
            if (subscriptionModel == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionModel);
        }

        // POST: SubscriptionModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Email,FirstName,LastName,Company,Password,ConfirmPassword,Radio,FirstNameOnCard,LastNameOnCard,StreetAddress,SecStreetAddress,City,State,Zip,CardNumber,Expiration,CVV,subscriptionLength")] SubscriptionModel subscriptionModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscriptionModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(subscriptionModel);
        }

        // GET: SubscriptionModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionModel subscriptionModel = await db.gg.FindAsync(id);
            if (subscriptionModel == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionModel);
        }

        // POST: SubscriptionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SubscriptionModel subscriptionModel = await db.gg.FindAsync(id);
            db.gg.Remove(subscriptionModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
