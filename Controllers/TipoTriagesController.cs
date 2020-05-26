using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AtencionMedica_v1.Models;

namespace AtencionMedica_v1.Controllers
{
    public class TipoTriagesController : Controller
    {
        private readonly MyDbContext _context;

        public TipoTriagesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: TipoTriages
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoTriage.ToListAsync());
        }

        // GET: TipoTriages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTriage = await _context.TipoTriage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTriage == null)
            {
                return NotFound();
            }

            return View(tipoTriage);
        }

        // GET: TipoTriages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoTriages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Estado")] TipoTriage tipoTriage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoTriage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoTriage);
        }

        // GET: TipoTriages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTriage = await _context.TipoTriage.FindAsync(id);
            if (tipoTriage == null)
            {
                return NotFound();
            }
            return View(tipoTriage);
        }

        // POST: TipoTriages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Estado")] TipoTriage tipoTriage)
        {
            if (id != tipoTriage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoTriage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoTriageExists(tipoTriage.Id))
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
            return View(tipoTriage);
        }

        // GET: TipoTriages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTriage = await _context.TipoTriage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTriage == null)
            {
                return NotFound();
            }

            return View(tipoTriage);
        }

        // POST: TipoTriages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoTriage = await _context.TipoTriage.FindAsync(id);
            _context.TipoTriage.Remove(tipoTriage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoTriageExists(int id)
        {
            return _context.TipoTriage.Any(e => e.Id == id);
        }
    }
}
