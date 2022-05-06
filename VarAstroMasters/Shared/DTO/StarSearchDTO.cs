namespace VarAstroMasters.Shared.DTO;

public class StarSearchDTO
{
    public List<StarDTO> Data { get; set; } = new();
    public int CurrentPage { get; set; }
    public int Pages { get; set; }
}