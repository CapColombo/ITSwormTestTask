using ITSwormTestTask.Database;
using Microsoft.EntityFrameworkCore;

namespace ITSwormTestTask.Repositories
{
    // класс, реализовывающий паттерн Репозиторий
    public class FurnitureRepository
    {
        private readonly FurnitureContext _context;

        public FurnitureRepository(FurnitureContext context)
        {
            _context = context;
        }

        public void Add(Furniture entity)
        {
            _context.Furniture.Add(entity);
            _context.SaveChanges();
        }

        public void AddPanel(int furnitureId, Panel panel)
        {
            var furniture = GetById(furnitureId);
            furniture?.Panels.Add(panel);
            _context.SaveChanges();
        }

        public void AddAccessorie(int panelId, Accessorie accessorie)
        {
            var panel = GetPanelById(panelId);
            panel?.Accessories.Add(accessorie);
            _context.SaveChanges();
        }

        public void AddFastener(int panelId, Fastener fastener)
        {
            var panel = GetPanelById(panelId);
            panel?.Fasteners.Add(fastener);
            _context.SaveChanges();
        }

        public void Delete(Furniture entity)
        {
            _context.Furniture.Remove(entity);
            _context.SaveChanges();
        }

        public void DeletePanel(Panel panel)
        {
            var item = GetPanelById(panel.Id);
            _context.Panels.Remove(item);
            _context.SaveChanges();
        }

        public void DeleteFastener(Fastener fastener)
        {
            var panel = GetPanelById(fastener.Panel.Id);
            panel?.Fasteners.Remove(fastener);
            _context.SaveChanges();
        }

        public void DeleteAccessorie(Accessorie accessorie)
        {
            var panel = GetPanelById(accessorie.Panel.Id);
            panel?.Accessories.Remove(accessorie);
            _context.SaveChanges();
        }

        public IEnumerable<Furniture> GetAll()
        {
            return _context.Furniture
                .Include(f => f.Panels).ThenInclude(p => p.Fasteners)
                    .Include(f => f.Panels).ThenInclude(p => p.Accessories)
                        .ToList();
        }

        public Furniture GetById(int id)
        {
            return _context.Furniture
                .Include(f => f.Panels).ThenInclude(p => p.Fasteners)
                    .Include(f => f.Panels).ThenInclude(p => p.Accessories)
                        .Where(f => f.Id == id).FirstOrDefault();
        }

        public Panel GetPanelById(int id)
        {
            return _context.Panels
                .Include(p => p.Accessories)
                .Include(p => p.Fasteners)
                    .Where(p => p.Id == id).FirstOrDefault();  
        }

        public Furniture GetByName(string name)
        {
            return _context.Furniture
                .Include(f => f.Panels).ThenInclude(p => p.Fasteners)
                    .Include(f => f.Panels).ThenInclude(p => p.Accessories)
                        .Where(f => f.Type == name).FirstOrDefault();
        }

        public void Update(Furniture entity)
        {
            _context.Furniture.Update(entity);
            _context.SaveChanges();
        }
    }
}
