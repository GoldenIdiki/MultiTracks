using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Adds pagination to items that are completely pulled out from the database
/// </summary>
public class Paging
{
    public decimal TotalNumberOfItems { get; set; }

    public int TotalNumberOfPages { get; set; }

    public int PageNumber { get; set; }

    public int ItemsPerPage { get; set; }
    
    public ICollection<T> GetPaginated<T>(int pageNumber, int itemsPerPage, IList<T> items)
    {
        TotalNumberOfItems = items.Count;

        TotalNumberOfPages = (int)Math.Ceiling(TotalNumberOfItems / itemsPerPage);

        if (pageNumber > TotalNumberOfPages || pageNumber < 1)
        {
            return null;
        }
        var pagedItems = items.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();

        return pagedItems;
    }
}