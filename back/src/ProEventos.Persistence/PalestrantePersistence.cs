using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class PalestrantePersistence : IPalestrantePersistence 
    {
        public readonly ProEventosContext _context;

        public PalestrantePersistence(ProEventosContext context)
        {
            _context =  context;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                                        .Include(p => p.RedesSociais);

            if(includeEventos) {
                query = query.Include(pe => pe.PalestrantesEventos)
                             .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                                        .Include(p => p.RedesSociais);

            if(includeEventos) {
                query = query.Include(pe => pe.PalestrantesEventos)
                             .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id)
                         .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                                        .Include(p => p.RedesSociais);

            if(includeEventos) {
                query = query.Include(pe => pe.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(p => p.Id)
                         .Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

    }
}