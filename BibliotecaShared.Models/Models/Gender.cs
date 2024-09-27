using Models;

namespace BibliotecaShared.Models.Models;

public class Gender
{
    public int GenderId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Book> Books { get; set; }
    public Gender() { }
}
