﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlyJournal.Data.Journal;
using OnlyJournalPage.Data;

namespace OnlyJournalPage.Pages.Journal
{
    public class EditModel : PageModel
    {
        private readonly OnlyJournalPage.Data.OnlyJournalContext _context;

        public EditModel(OnlyJournalPage.Data.OnlyJournalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OnlyJournal.Data.Journal.Journal Journal { get; set; }
        [BindProperty]
        public IEnumerable<SelectListItem> Categories { get; set; }
        [BindProperty]
        public string Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Journal = await _context.Journal.FirstOrDefaultAsync(m => m.Id == id);
            var values = new[] { JournalCategory.Daily, JournalCategory.Honor, JournalCategory.Tech };
            Categories = values.Select(x => new SelectListItem(x.ToString(), x.ToString()));
            Category = Journal.Category.ToString();

            if (Journal == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Journal.Category = (JournalCategory)Enum.Parse(typeof(JournalCategory), Category);
            Journal.TimeCreated = DateTime.Now;

            _context.Attach(Journal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JournalExists(Journal.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool JournalExists(int id)
        {
            return _context.Journal.Any(e => e.Id == id);
        }
    }
}
