using Models;
using Data;
using BibliotecaShared.Models.Models;

namespace Menus;

public class MenuBooks
{
    private readonly DAL<Book> bookRepository;
    private readonly DAL<Gender> genderRepository;

    public MenuBooks(BibliotecaContext context)
    {
        bookRepository = new DAL<Book>(context);
    }

    public void MainMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("====== MENU ======");
            Console.WriteLine("1. Adicionar Livro");
            Console.WriteLine("2. Listar Livros");
            Console.WriteLine("3. Atualizar Livro");
            Console.WriteLine("4. Remover Livro");
            Console.WriteLine("5. Retornar ao Menu Principal");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    ListBooks();
                    break;
                case "3":
                    UpdateBook();
                    break;
                case "4":
                    RemoveBook();
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

    private void AddBook()
    {
        Book newBook = new Book();

        Console.Write("Título: ");
        newBook.Title = Console.ReadLine();

        Console.Write("Autor: ");
        newBook.Author = Console.ReadLine();

        Console.Write("ISBN: ");
        newBook.ISBN = Console.ReadLine();

        Console.Write("Ano de Publicação: ");
        newBook.PublishedYear = int.Parse(Console.ReadLine());

        Console.WriteLine("Selecione o gênero pelo ID ou Nome (deixe em branco para não adicionar gênero): ");
        string genreInput = Console.ReadLine();

        if (!string.IsNullOrEmpty(genreInput))
        {
            var selectedGender = genderRepository.GetBy(g => g.Name.ToLower() == genreInput.ToLower() || g.GenderId.ToString() == genreInput);

            if (selectedGender != null)
            {
                newBook.Genders = new List<Gender> { selectedGender };
            }
            else
            {
                Console.WriteLine("Gênero não encontrado. O livro será adicionado sem um gênero associado.");
            }
        }
    }

    private void ListBooks()
    {
        var books = bookRepository.List().ToList();

        if (books.Count == 0)
        {
            Console.WriteLine("Nenhum livro cadastrado.");
            return;
        }

        foreach (var book in books)
        {
            Console.WriteLine($"ID: {book.Id}");
            Console.WriteLine($"Título: {book.Title}");
            Console.WriteLine($"Autor: {book.Author}");
            Console.WriteLine($"ISBN: {book.ISBN}");
            Console.WriteLine($"Ano de Publicação: {book.PublishedYear}");
            Console.WriteLine($"Gênero: {book.Genders}");
            Console.WriteLine(new string('-', 20));
        }
    }

    private void UpdateBook()
    {
        Console.Write("Digite o ID do livro a ser atualizado: ");
        int id = int.Parse(Console.ReadLine());

        var bookToUpdate = bookRepository.GetBy(b => b.Id == id);

        if (bookToUpdate == null)
        {
            Console.WriteLine("Livro não encontrado.");
            return;
        }

        Console.Write("Novo Título (deixe em branco para manter o atual): ");
        string title = Console.ReadLine();
        if (!string.IsNullOrEmpty(title)) bookToUpdate.Title = title;

        Console.Write("Novo Autor (deixe em branco para manter o atual): ");
        string author = Console.ReadLine();
        if (!string.IsNullOrEmpty(author)) bookToUpdate.Author = author;

        Console.Write("Novo ISBN (deixe em branco para manter o atual): ");
        string isbn = Console.ReadLine();
        if (!string.IsNullOrEmpty(isbn)) bookToUpdate.ISBN = isbn;

        Console.Write("Novo Ano de Publicação (deixe em branco para manter o atual): ");
        string yearInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(yearInput)) bookToUpdate.PublishedYear = int.Parse(yearInput);

        Console.Write("Novo Gênero (deixe em branco para manter o atual): ");
        string genreInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(genreInput))
        {
            var selectedGender = genderRepository.GetBy(g => g.Name.ToLower() == genreInput.ToLower() || g.GenderId.ToString() == genreInput);
            if (selectedGender != null)
            {
                bookToUpdate.Genders.Clear();
                bookToUpdate.Genders.Add(selectedGender);
            }
            else
            {
                Console.WriteLine("Gênero não encontrado.");
            }
        }

        bookRepository.Update(bookToUpdate);
        Console.WriteLine("Livro atualizado com sucesso!");
    }

    private void RemoveBook()
    {
        Console.Write("Digite o ID do livro a ser removido: ");
        int id = int.Parse(Console.ReadLine());

        var bookToRemove = bookRepository.GetBy(b => b.Id == id);

        if (bookToRemove == null)
        {
            Console.WriteLine("Livro não encontrado.");
            return;
        }

        bookRepository.Delete(bookToRemove);
        Console.WriteLine("Livro removido com sucesso!");
    }
}
