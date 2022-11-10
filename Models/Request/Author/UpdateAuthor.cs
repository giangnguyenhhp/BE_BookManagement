namespace BookManagement.Models.Request.Author;

public class UpdateAuthor
{
    public string Name { get; set; }
    public string DOB { get; set; }
    public string HomeTown { get; set; }
    public List<long> BookId { get; set; }
    
}