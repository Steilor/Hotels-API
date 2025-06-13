namespace Hotelss.Application.Common;

public class PagedResult<T>
{
    public PagedResult(List<T> items, int totalCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalItemsCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);//math.celing (2.8) => 3
        ItemsFrom = pageSize * (pageNumber - 1) + 1; // Indice First elemento de la page actual
        ItemsTo = ItemsFrom + pageSize - 1; // Indice Last elemento de la page actual
    }
    public List<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
}
