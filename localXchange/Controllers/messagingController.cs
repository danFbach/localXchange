using System;
using System.IO;
using System.Web;
using System.Net;
using System.Linq;
using System.Data;
using System.Web.Mvc;
using localXchange.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;

namespace localXchange.Controllers
{
    public class messagingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: messaging
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.StatusMessage = TempData["ErrorMessage"].ToString();
            }
            if(TempData["StatusMessage"] != null)
            {
                ViewBag.addrBookMessage = TempData["StatusMessage"].ToString();
            }
            List<messagingModel> messagesRAW = db.messagingModel.ToList();
            InOutBox messagePack = new InOutBox();
            messagePack.messagesIN = new List<Models.messagingModel>();
            messagePack.messagesOUT = new List<Models.messagingModel>();
            messagePack.addresses = new List<SelectListItem>();
            foreach(var message in messagesRAW)
            {
                if(message.receiverID == User.Identity.GetUserId()) { message.sender = db.Users.Find(message.senderID); message.receiver = db.Users.Find(message.receiverID); messagePack.messagesIN.Add(message); }
                else if(message.senderID == User.Identity.GetUserId()) { message.receiver = db.Users.Find(message.receiverID); message.sender = db.Users.Find(message.senderID); messagePack.messagesOUT.Add(message); }
            }
            messagePack.messagesIN = messagePack.messagesIN.OrderByDescending(x => x.datetimeSent).Take(25).ToList();
            messagePack.messagesOUT = messagePack.messagesOUT.OrderByDescending(x => x.datetimeSent).Take(25).ToList();
            foreach (var item in db.addressBookModel.ToList())
            {
                if(item.bookOwnerUserID == User.Identity.GetUserId())
                {
                    item.bookOwnerUser = db.Users.Find(item.bookOwnerUserID);
                    item.bookEntryUser = db.Users.Find(item.bookEntryUserID);
                    messagePack.addresses.Add(new SelectListItem { Text = item.bookEntryUser.UserName, Value = item.bookEntryUser.UserName});
                }
            }
            return View(messagePack);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createNewMessage(string addressBookEntryUsername)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (string.IsNullOrEmpty(addressBookEntryUsername) || string.IsNullOrWhiteSpace(addressBookEntryUsername)) { return RedirectToAction("Index"); }
            ApplicationUser newUser = UserManager.FindByName(addressBookEntryUsername);
            if(newUser != null)
            {
                return RedirectToAction("sendNewMessage", "messaging", new { recipID = newUser.Id });
            }
            TempData["ErrorMessage"] = "No user was chosen.";
            return RedirectToAction("Index");
        }
        public ActionResult sendNewMessage(string recipID)
        {
            string sendingUser = User.Identity.GetUserId();
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (recipID == null || sendingUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                messagingModel newMes = new messagingModel();
                newMes.senderID = sendingUser;
                newMes.sender = db.Users.Find(sendingUser);
                newMes.receiverID = recipID;
                newMes.receiver = db.Users.Find(recipID);
                return View(newMes);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult sendNewMessagePost(string recipID)
        {
            string sendingUser = User.Identity.GetUserId();
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (recipID == null || sendingUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                messagingModel newMes = new messagingModel();
                newMes.senderID = sendingUser;
                newMes.sender = db.Users.Find(sendingUser);
                newMes.receiverID = recipID;
                newMes.receiver = db.Users.Find(recipID);
                return View("sendNewMessage", newMes);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult sendNewMessage(messagingModel messToSend)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            messToSend.datetimeSent = DateTime.Now;
            messToSend.readUnread = false;
            if (ModelState.IsValid)
            {
                db.messagingModel.Add(messToSend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(messToSend);
            }
        }

        public ActionResult removeUser(string username)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (var address in db.addressBookModel.ToList())
            {
                if(address.bookOwnerUserID == User.Identity.GetUserId())
                {
                    address.bookEntryUser = db.Users.Find(address.bookEntryUserID);
                    if (address.bookEntryUser.UserName == username)
                    {
                        db.addressBookModel.Remove(address);
                        db.SaveChangesAsync();
                        TempData["StatusMessage"] = "User Removed from Address Book.";
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult addAddressToBook(string userAddressUsername, string ReturnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            addressBookModel newBookEntry = new addressBookModel();
            newBookEntry.bookOwnerUserID = User.Identity.GetUserId();
            newBookEntry.bookOwnerUser = db.Users.Find(newBookEntry.bookOwnerUserID);
            newBookEntry.bookEntryUser = db.Users.Where(m => m.UserName == userAddressUsername).First();
            newBookEntry.bookEntryUserID = newBookEntry.bookEntryUser.Id;
            List<addressBookModel> existingAddresses = db.addressBookModel.Where(x => x.bookOwnerUserID == newBookEntry.bookOwnerUserID).ToList();
            List<addressBookModel> duplicateCheck = existingAddresses.Where(n => n.bookEntryUserID == newBookEntry.bookEntryUserID).ToList();
            if(duplicateCheck.Count() == 0)
            {
                db.addressBookModel.Add(newBookEntry);
                db.SaveChangesAsync();
                TempData["StatusMessage"] = userAddressUsername + " added to Address Book";
                return Redirect(ReturnUrl);
                
            }
            else
            {
                TempData["StatusMessage"] = userAddressUsername + " is already in your Address Book.";
                return Redirect(ReturnUrl);
            }
        }
        public ActionResult viewMessage(int? messageID)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (messageID == null)
            {
                return RedirectToAction("Index");
            }
            messagingModel message = db.messagingModel.Find(messageID);
            message.sender = db.Users.Find(message.senderID);
            message.receiver = db.Users.Find(message.receiverID);
            if(message.receiverID == User.Identity.GetUserId())
            {
                message.readUnread = true;
                db.Entry(message).State = EntityState.Modified;
                db.SaveChangesAsync();
            }
            return View(message);
        }
        // GET: messaging/Details/5
        public ActionResult Details(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: messaging/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: messaging/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: messaging/Edit/5
        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: messaging/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: messaging/Delete/5
        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: messaging/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult messageCount()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            string uid = User.Identity.GetUserId();
            var message = db.messagingModel.Where(x => x.receiverID == uid).Where(x => x.readUnread == false).Count();
            return PartialView(message);
        }

        #region helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion helpers
    }
}
