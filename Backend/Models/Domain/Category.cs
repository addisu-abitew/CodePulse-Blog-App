namespace CodePulse.Backend.Models.Domain;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UrlHandle { get; set; }
    public ICollection<Blog> Blogs { get; set; }
}
