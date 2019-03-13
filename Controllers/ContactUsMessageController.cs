using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Data.Entities;

namespace Portfolio.Controllers
{
    public class ContactUsMessageController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactUsMessageController(PortfolioContext context)
        {
            _context = context;
        }

        // GET: ContactUsMessage
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactUsMessages.ToListAsync());
        }

        // GET: ContactUsMessage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactUsMessage = await _context.ContactUsMessages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactUsMessage == null)
            {
                return NotFound();
            }

            return View(contactUsMessage);
        }

        // GET: ContactUsMessage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactUsMessage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContactName,Email,Message,Location")] ContactUsMessage contactUsMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactUsMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactUsMessage);
        }

        // GET: ContactUsMessage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactUsMessage = await _context.ContactUsMessages.FindAsync(id);
            if (contactUsMessage == null)
            {
                return NotFound();
            }
            return View(contactUsMessage);
        }

        // POST: ContactUsMessage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContactName,Email,Message,Location")] ContactUsMessage contactUsMessage)
        {
            if (id != contactUsMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactUsMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUsMessageExists(contactUsMessage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contactUsMessage);
        }

        // GET: ContactUsMessage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactUsMessage = await _context.ContactUsMessages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactUsMessage == null)
            {
                return NotFound();
            }

            return View(contactUsMessage);
        }

        // POST: ContactUsMessage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactUsMessage = await _context.ContactUsMessages.FindAsync(id);
            _context.ContactUsMessages.Remove(contactUsMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUsMessageExists(int id)
        {
            return _context.ContactUsMessages.Any(e => e.Id == id);
        }
    }
}
