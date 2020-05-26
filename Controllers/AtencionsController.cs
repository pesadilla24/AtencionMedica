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
    public class AtencionsController : Controller
    {
        private readonly MyDbContext _context;

        public AtencionsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Atencions
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Atencion.Include(a => a.Consultorio).Include(a => a.Paciente);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Atencions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atencion = await _context.Atencion
                .Include(a => a.Consultorio)
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atencion == null)
            {
                return NotFound();
            }

            return View(atencion);
        }

        // GET: Atencions/Create
        public IActionResult Create()
        {

            var atencionquery =
              from paciente in _context.Paciente
              where paciente.Estado == 1
              orderby paciente.TipoTriageId ascending, paciente.Id
              select paciente;

            //  _context.Paciente = atencionquery;

            //var atencionquery1 = _context.Paciente.ToList().Select(u => new SelectListItem
            //{
            //    Text = u.Id + " " + u.Nombres,
            //    Value = u.Id.ToString()
            //});

            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "Id", "NombreMedico");
            ViewData["PacienteId"] = new SelectList(atencionquery, "Id", "Nombres");


            //ViewData["PacienteId"] = new SelectList(atencionquery, "Id", "Nombres");


            return View();
        }

        // POST: Atencions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConsultorioId,PacienteId")] Atencion atencion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atencion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "Id", "Id", atencion.ConsultorioId);
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Id", atencion.PacienteId);
            return View(atencion);
        }

        // GET: Atencions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atencion = await _context.Atencion.FindAsync(id);
            if (atencion == null)
            {
                return NotFound();
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "Id", "Id", atencion.ConsultorioId);
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Nombres", atencion.PacienteId);
            return View(atencion);
        }

        // POST: Atencions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConsultorioId,PacienteId")] Atencion atencion)
        {
            if (id != atencion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atencion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtencionExists(atencion.Id))
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
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "Id", "Id", atencion.ConsultorioId);
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Id", atencion.PacienteId);
            return View(atencion);
        }

        // GET: Atencions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atencion = await _context.Atencion
                .Include(a => a.Consultorio)
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atencion == null)
            {
                return NotFound();
            }

            return View(atencion);
        }

        // POST: Atencions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atencion = await _context.Atencion.FindAsync(id);
            _context.Atencion.Remove(atencion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtencionExists(int id)
        {
            return _context.Atencion.Any(e => e.Id == id);
        }
    }
}
