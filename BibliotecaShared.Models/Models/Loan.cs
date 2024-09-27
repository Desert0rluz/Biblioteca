using Models;

namespace BibliotecaShared.Models.Models;

public class Loan
{
    public int LoanId { get; set; }
    public int FriendId { get; set; }
    public virtual ICollection<Book> Books { get; set; }
    public DateOnly LoanDate { get; set; }
    public Loan() { }
}
