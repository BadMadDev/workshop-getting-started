using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSite.Data;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ToDosController : Controller
    {
        private readonly ToDoListDbContext _context;

        public ToDosController(ToDoListDbContext context)
        {
            _context = context;
        }

        // GET: ToDos
        public async Task<IActionResult> Index()
        {
            return View(await _context.ToDos.ToListAsync());
        }

        // GET: ToDos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoListItem = await _context.ToDos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoListItem == null)
            {
                return NotFound();
            }

            return View(toDoListItem);
        }

        // GET: ToDos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,IsDone")] ToDoListItem toDoListItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoListItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoListItem);
        }

        // GET: ToDos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoListItem = await _context.ToDos.FindAsync(id);
            if (toDoListItem == null)
            {
                return NotFound();
            }
            return View(toDoListItem);
        }

        // POST: ToDos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,IsDone")] ToDoListItem toDoListItem)
        {
            if (id != toDoListItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoListItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoListItemExists(toDoListItem.Id))
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
            return View(toDoListItem);
        }

        // GET: ToDos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoListItem = await _context.ToDos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoListItem == null)
            {
                return NotFound();
            }

            return View(toDoListItem);
        }

        // POST: ToDos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoListItem = await _context.ToDos.FindAsync(id);
            _context.ToDos.Remove(toDoListItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListItemExists(int id)
        {
            return _context.ToDos.Any(e => e.Id == id);
        }
    }
}
