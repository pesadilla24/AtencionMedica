using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AtencionMedica_v1.Models;
using System.IO;
using Newtonsoft.Json;

namespace AtencionMedica_v1.Controllers
{
    public class ConsultoriosController : Controller
    {
        private readonly MyDbContext _context;

        public ConsultoriosController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Consultorios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consultorio.ToListAsync());
        }

        public IActionResult CargaMasiva()
        {
            List<Consultorio> items;
            using (StreamReader r = new StreamReader("filesCarga/cargaConsultorio.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Consultorio>>(json);
            }

            foreach (var consultorio in items)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(consultorio);
                    _context.SaveChanges();
                }
            }
            ViewBag.mensaje = "Carga exitosa!";
            return View("CargaMasiva", items);
        }



        // GET: Consultorios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultorio = await _context.Consultorio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultorio == null)
            {
                return NotFound();
            }

            return View(consultorio);
        }

        // GET: Consultorios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consultorios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreMedico,Estado")] Consultorio consultorio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consultorio);
        }

        // GET: Consultorios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultorio = await _context.Consultorio.FindAsync(id);
            if (consultorio == null)
            {
                return NotFound();
            }
            return View(consultorio);
        }

        // POST: Consultorios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreMedico,Estado")] Consultorio consultorio)
        {
            if (id != consultorio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultorioExists(consultorio.Id))
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
            return View(consultorio);
        }

        // GET: Consultorios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultorio = await _context.Consultorio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultorio == null)
            {
                return NotFound();
            }

            return View(consultorio);
        }

        // POST: Consultorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultorio = await _context.Consultorio.FindAsync(id);
            _context.Consultorio.Remove(consultorio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultorioExists(int id)
        {
            return _context.Consultorio.Any(e => e.Id == id);
        }
    }
}
