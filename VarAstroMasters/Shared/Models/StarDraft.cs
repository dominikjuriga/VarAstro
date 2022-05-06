namespace VarAstroMasters.Shared.Models;

public class StarDraft
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Ra { get; set; }
    public string Dec { get; set; }
}