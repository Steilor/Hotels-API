﻿using Hotelss.Domain.Constants;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using Hotelss.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotelss.Infrastructure.Repositories;

internal class HotelsRepository(HotelsDbContext dbContext) : IHotelsRepository
{
    public async Task<IEnumerable<Hotel>> GetAllAsync()
    {
        var hotels = await dbContext.Hotels.ToListAsync();
        return hotels;
    } 
    public async Task<(IEnumerable<Hotel>, int)> GetAllMatchingAsync(string? searchPhrase, 
        int pageSize, 
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Hotels
            .Where(r => searchPhraseLower == null || (r.Nombre.ToLower().Contains(searchPhraseLower)
                    || r.Description.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync();

        if(sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Hotel, object>>>
            {
                {nameof(Hotel.Nombre), r => r.Nombre },
                {nameof(Hotel.Description), r => r.Description },
                {nameof(Hotel.Category), r => r.Category },
            };
            
            var selectedColumn = columnsSelector[sortBy]; 

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var hotels = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (hotels, totalCount);
    }

    public async Task<Hotel?> GetByIdAsync(int id)
    {
        var hotel = await dbContext.Hotels
            .Include(r => r.Rooms)
            .FirstOrDefaultAsync(c=> c.Id == id);
        return hotel;
    }

    public async Task<int> Create(Hotel hotel)
    {
        dbContext.Hotels.Add(hotel);
        await dbContext.SaveChangesAsync();
        return hotel.Id;
    }

    public async Task Delete(Hotel hotel)
    {
         dbContext.Remove(hotel);
        await dbContext.SaveChangesAsync();

    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();

}
