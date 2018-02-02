namespace DatingApp.API.Data
{
    public class AuditRepository : IAuditRepository
    {
        private readonly DataContext _context;

        public AuditRepository(DataContext context)
        {
            _context = context;

        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}