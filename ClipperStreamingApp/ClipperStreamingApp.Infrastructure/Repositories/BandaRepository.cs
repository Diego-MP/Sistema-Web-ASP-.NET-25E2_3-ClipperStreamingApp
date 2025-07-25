﻿using ClipperStreamingApp.Domain.Playlist;
using ClipperStreamingApp.Domain.Playlist.Repository;
using Microsoft.EntityFrameworkCore;

namespace ClipperStreamingApp.Infrastructure.Repositories;
public class BandaRepository : IBandaRepository
{
    private readonly StreamingDbContext _context;
    public BandaRepository(StreamingDbContext context) { _context = context; }

    public async Task<Banda?> GetByIdWithMusicasAsync(int id)
    {
        return await _context.Bandas
            .Include(b => b.Musicas)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Banda>> SearchByNameAsync(string term)
    {
        return await _context.Bandas
            .Where(b => b.Nome.ToLower().Contains(term.ToLower()))
            .ToListAsync();
    }
}