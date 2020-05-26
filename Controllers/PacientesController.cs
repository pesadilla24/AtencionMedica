using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AtencionMedica_v1.Models;
using Newtonsoft.Json;
using System.IO;

namespace AtencionMedica_v1.Controllers
{
    public class PacientesController : Controller
    {
        private readonly MyDbContext _context;

        public PacientesController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult CargaMasiva()
        {
            List<Paciente> items;
            using (StreamReader r = new StreamReader("filesCarga/cargaPaciente.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Paciente>>(json);
            }

            foreach (var paciente in items)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(paciente);
                    _context.SaveChanges();
                }
            }
            ViewBag.mensaje = "Carga exitosa!";
            return View("CargaMasiva", items);
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Paciente.Include(p => p.TipoTriage);
            return View(await myDbContext.ToListAsync());
        }
        // GET: Pacientes
        public async Task<IActionResult> PacientesRecuperados()
        {
            ViewBag.datoReporte = 1;
            //var myDbContext = _context.Paciente.Include(p => p.TipoTriage);
            var myDbContext = _context.Paciente.Include(p => p.TipoTriage);
            //  var myDbContext = _context.Paciente.Include(p => p.TipoTriage);
            return View(await myDbContext.ToListAsync());
        }
        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .Include(p => p.TipoTriage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            ViewData["TipoTriageId"] = new SelectList(_context.TipoTriage, "Id", "Id");
            ViewData["Estado"] = new SelectList(_context.TipoTriage, "Id", "Id");
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Identificacion,Nombres,Edad,Sexo,CodigoADN,Diagnostico,Estado,TipoTriageId")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoTriageId"] = new SelectList(_context.TipoTriage, "Id", "Id", paciente.TipoTriageId);
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            ViewData["TipoTriageId"] = new SelectList(_context.TipoTriage, "Id", "Nombre", paciente.TipoTriageId);
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Identificacion,Nombres,Edad,Sexo,CodigoADN,Diagnostico,Estado,TipoTriageId")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            ViewData["TipoTriageId"] = new SelectList(_context.TipoTriage, "Id", "Id", paciente.TipoTriageId);
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .Include(p => p.TipoTriage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Paciente.FindAsync(id);
            _context.Paciente.Remove(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Paciente.Any(e => e.Id == id);
        }
    }
}
