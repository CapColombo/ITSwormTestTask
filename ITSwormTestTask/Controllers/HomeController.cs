using Microsoft.AspNetCore.Mvc;
using ITSwormTestTask.Repositories;
using ITSwormTestTask.Database;

namespace ITSwormTestTask.Controllers
{
    public class HomeController : Controller
    {
        // контекст базы данных
        private readonly FurnitureContext _context;
        private readonly FurnitureRepository repository;

        public HomeController(FurnitureContext context)
        {
            _context = context;
            repository = new FurnitureRepository(_context);
        }

        // начальная страница, выводит список мебели
        public IActionResult Index()
        {
            var furniture = repository.GetAll();

            return View(furniture);
        }

        // страница со списком панелей мебели
        public IActionResult Details(int id)
        {
            var furniture = repository.GetById(id);
            if (furniture != null)
                return View(furniture);
            else
                return Content("Furniture not found", "text/plain");
        }

        // страница со списком комплектующих панели
        public IActionResult PanelDetails(int id)
        {
            var panel = repository.GetPanelById(id);
            if (panel != null)
                return View(panel);
            else
                return Content("Panel not found", "text/plain");
        }

        // метод, обрабатывающий пост запрос на добавление мебели
        [HttpPost]
        public IActionResult Index(string type)
        {
            var furniture = repository.GetAll();

            if(type == null)
                return View(furniture);

            Furniture newFurniture = new() { Type = type };
            repository.Add(newFurniture);

            return RedirectToAction("Index");
        }

        // метод, обрабатывающий пост запрос на добавление панели
        [HttpPost]
        public IActionResult Details(int furnitureId, string name)
        {
            var furniture = repository.GetById(furnitureId);

            if(name == null)
                return View(furniture);

            Panel panel = new() { Name = name };
            repository.AddPanel(furnitureId, panel);

            return RedirectToAction("Details", furniture.Id);
        }

        // метод, обрабатывающий пост запрос на добавление комплектующих
        [HttpPost]
        public IActionResult PanelDetails(int panelId, string name, int amount, int value)
        {
            var panel = repository.GetPanelById(panelId);

            if(name == null)
                return View(panel);

            if (value == 1)
            {
                Fastener fastener = new() { Name = name, Amount = amount };
                repository.AddFastener(panelId, fastener);
            }
            else if(value == 2)
            {
                Accessorie accessorie = new() { Name = name, Amount = amount };
                repository.AddAccessorie(panelId, accessorie);
            }
            return RedirectToAction("PanelDetails", panel.Id);
        }

        #region DeleteActions
        public IActionResult DeleteFurniture(int id)
        {
            repository.Delete(repository.GetById(id));
            return RedirectToAction("Index");
        }

        public IActionResult DeletePanel(int id, [FromQuery]int fid)
        {
            repository.DeletePanel(repository.GetPanelById(id));
            return Redirect($"../../Home/Details/{fid}");
        }

        public IActionResult DeleteFastener(int id, [FromQuery] int pid)
        {
            Fastener fastener = repository.GetPanelById(pid).Fasteners.Where(f => f.Id == id).FirstOrDefault();

            if (fastener != null)
                repository.DeleteFastener(fastener);

            return Redirect($"../../Home/PanelDetails/{pid}");
        }

        public IActionResult DeleteAccessoire(int id, [FromQuery] int pid)
        {
            Accessorie accessorie = repository.GetPanelById(pid).Accessories.Where(a => a.Id == id).FirstOrDefault();

            if (accessorie != null)
                repository.DeleteAccessorie(accessorie);

            return Redirect($"../../Home/PanelDetails/{pid}");
        }
        #endregion
    }
}
