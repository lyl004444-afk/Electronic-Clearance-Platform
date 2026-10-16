
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Electronic_Clearance_Platform.Models;

public class ClearanceRequestsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClearanceRequestsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ClearanceRequests
    public async Task<IActionResult> Index(string searchString, int? pageNumber)
    {
        var requests = _context.ClearanceRequests.OrderByDescending(r => r.Id).AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            requests = requests.Where(r => r.FullName.Contains(searchString)
                                       || r.IdentificationNumber.Contains(searchString)
                                       || r.Department.Contains(searchString));
        }

        ViewData["CurrentFilter"] = searchString;
        return View(await requests.ToListAsync());
    }

    // GET: CLEARANCEREQUESTS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var clearancerequest = await _context.ClearanceRequests
            .FirstOrDefaultAsync(m => m.Id == id);
        if (clearancerequest == null)
        {
            return NotFound();
        }

        return View(clearancerequest);
    }

    // GET: CLEARANCEREQUESTS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CLEARANCEREQUESTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FullName,IdentificationNumber,Department,RequestDate,Status")] ClearanceRequest clearancerequest)
    {
        if (ModelState.IsValid)
        {
            clearancerequest.Status = "قيد المراجعة";
            _context.Add(clearancerequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(clearancerequest);
    }

    // GET: CLEARANCEREQUESTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var clearancerequest = await _context.ClearanceRequests.FindAsync(id);
        if (clearancerequest == null)
        {
            return NotFound();
        }
        return View(clearancerequest);
    }

    // POST: CLEARANCEREQUESTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,FullName,IdentificationNumber,Department,RequestDate,Status")] ClearanceRequest clearancerequest)
    {
        if (id != clearancerequest.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(clearancerequest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClearanceRequestExists(clearancerequest.Id))
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
        return View(clearancerequest);
    }

    // GET: CLEARANCEREQUESTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var clearancerequest = await _context.ClearanceRequests
            .FirstOrDefaultAsync(m => m.Id == id);
        if (clearancerequest == null)
        {
            return NotFound();
        }

        return View(clearancerequest);
    }

    // POST: CLEARANCEREQUESTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var clearancerequest = await _context.ClearanceRequests.FindAsync(id);
        if (clearancerequest != null)
        {
            _context.ClearanceRequests.Remove(clearancerequest);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClearanceRequestExists(int? id)
    {
        return _context.ClearanceRequests.Any(e => e.Id == id);
    }
    // POST: ClearanceRequests/ChangeStatus/5
    [HttpPost]
    public async Task<IActionResult> QuickUpdate (int id, string status)
    {
        var clearanceRequest = await _context.ClearanceRequests.FindAsync(id);
        if (clearanceRequest != null)
        {
            clearanceRequest.Status = status;
            _context.Update(clearanceRequest);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
