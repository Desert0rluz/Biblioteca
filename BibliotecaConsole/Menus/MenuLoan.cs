using Models;
using Data;
using BibliotecaShared.Models.Models;

namespace Menus;

public class MenuLoans
{
    private readonly DAL<Loan> loanRepository;

    public MenuLoans(BibliotecaContext context)
    {
        loanRepository = new DAL<Loan>(context);
    }

    public void MainMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("====== MENU ======");
            Console.WriteLine("1. Registrar Empréstimo");
            Console.WriteLine("2. Listar Empréstimos");
            Console.WriteLine("3. Atualizar Empréstimo");
            Console.WriteLine("4. Remover Empréstimo");
            Console.WriteLine("5. Retornar ao Menu Principal");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddLoan();
                    break;
                case "2":
                    ListLoans();
                    break;
                case "3":
                    UpdateLoan();
                    break;
                case "4":
                    RemoveLoan();
                    break;
                case "5":
                    OpcaoMenuPrincipal.Main(null);
                    break;
                case "6":
                    Console.WriteLine("Saindo do programa...");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            if (!exit)
            {
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    private void AddLoan()
    {
        Loan newLoan = new Loan();

        Console.Write("ID do Amigo: ");
        newLoan.FriendId = int.Parse(Console.ReadLine());

        newLoan.Books = new List<Book>();
        bool addMoreBooks = true;
        while (addMoreBooks)
        {
            Console.Write("Digite o ID do Livro (ou deixe em branco para parar de adicionar): ");
            string bookIdInput = Console.ReadLine();
            if (string.IsNullOrEmpty(bookIdInput)) break;

            int bookId = int.Parse(bookIdInput);
            var bookToAdd = new DAL<Book>(new BibliotecaContext()).GetBy(b => b.Id == bookId);
            if (bookToAdd != null)
            {
                newLoan.Books.Add(bookToAdd);
                Console.WriteLine("Livro adicionado ao empréstimo.");
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        newLoan.LoanDate = DateOnly.FromDateTime(DateTime.Today);

        loanRepository.Add(newLoan);
        Console.WriteLine("Empréstimo registrado com sucesso!");
    }

    private void ListLoans()
    {
        var loans = loanRepository.List().ToList();

        if (loans.Count == 0)
        {
            Console.WriteLine("Nenhum empréstimo registrado.");
            return;
        }

        foreach (var loan in loans)
        {
            Console.WriteLine($"ID do Empréstimo: {loan.LoanId}");
            Console.WriteLine($"ID do Amigo: {loan.FriendId}");
            Console.WriteLine($"Data do Empréstimo: {loan.LoanDate}");
            Console.WriteLine("Livros emprestados:");

            foreach (var book in loan.Books)
            {
                Console.WriteLine($"- {book.Title} (ID: {book.Id})");
            }
            Console.WriteLine(new string('-', 20));
        }
    }

    private void UpdateLoan()
    {
        Console.Write("Digite o ID do empréstimo a ser atualizado: ");
        int id = int.Parse(Console.ReadLine());

        var loanToUpdate = loanRepository.GetBy(l => l.LoanId == id);

        if (loanToUpdate == null)
        {
            Console.WriteLine("Empréstimo não encontrado.");
            return;
        }

        Console.Write("Nova Data do Empréstimo (deixe em branco para manter a atual): ");
        string loanDateInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(loanDateInput))
            loanToUpdate.LoanDate = DateOnly.Parse(loanDateInput);

        bool addMoreBooks = true;
        while (addMoreBooks)
        {
            Console.Write("Digite o ID do Livro para adicionar (ou deixe em branco para parar de adicionar): ");
            string bookIdInput = Console.ReadLine();
            if (string.IsNullOrEmpty(bookIdInput)) break;

            int bookId = int.Parse(bookIdInput);
            var bookToAdd = new DAL<Book>(new BibliotecaContext()).GetBy(b => b.Id == bookId);
            if (bookToAdd != null)
            {
                loanToUpdate.Books.Add(bookToAdd);
                Console.WriteLine("Livro adicionado ao empréstimo.");
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        loanRepository.Update(loanToUpdate);
        Console.WriteLine("Empréstimo atualizado com sucesso!");
    }

    private void RemoveLoan()
    {
        Console.Write("Digite o ID do empréstimo a ser removido: ");
        int id = int.Parse(Console.ReadLine());

        var loanToRemove = loanRepository.GetBy(l => l.LoanId == id);

        if (loanToRemove == null)
        {
            Console.WriteLine("Empréstimo não encontrado.");
            return;
        }

        loanRepository.Delete(loanToRemove);
        Console.WriteLine("Empréstimo removido com sucesso!");
    }
}
