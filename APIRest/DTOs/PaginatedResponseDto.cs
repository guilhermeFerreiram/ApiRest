namespace APIRest.DTOs;

public class PaginatedResponseDto<T>
{
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public List<T> Itens { get; set; } = [];
}
