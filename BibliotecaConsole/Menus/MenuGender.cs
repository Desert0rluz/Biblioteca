using Data;
using BibliotecaShared.Models.Models;

namespace Menus;

public class MenuGender
{
    private readonly DAL<Gender> genderRepository;

    public MenuGender(BibliotecaContext context)
    {
        genderRepository = new DAL<Gender>(context);
    }

    public void MainMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("====== MENU ======");
            Console.WriteLine("1. Adicionar Gênero");
            Console.WriteLine("2. Listar Gêneros");
            Console.WriteLine("3. Atualizar Gênero");
            Console.WriteLine("4. Remover Gênero");
            Console.WriteLine("5. Retornar ao Menu Principal");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddGender();
                    break;
                case "2":
                    ListGenders();
                    break;
                case "3":
                    UpdateGender();
                    break;
                case "4":
                    RemoveGender();
                    break;
                case "5":
                    OpcaoMenuPrincipal.Main(null); // Supondo que exista uma opção de menu principal
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

    private void AddGender()
    {
        Gender newGender = new Gender();

        Console.Write("Nome do Gênero: ");
        newGender.Name = Console.ReadLine();

        Console.Write("Descrição do Gênero: ");
        newGender.Description = Console.ReadLine();

        genderRepository.Add(newGender);
        Console.WriteLine("Gênero adicionado com sucesso!");
    }

    private void ListGenders()
    {
        var genders = genderRepository.List().ToList();

        if (genders.Count == 0)
        {
            Console.WriteLine("Nenhum gênero cadastrado.");
            return;
        }

        foreach (var gender in genders)
        {
            Console.WriteLine($"ID: {gender.GenderId}");
            Console.WriteLine($"Nome: {gender.Name}");
            Console.WriteLine($"Descrição: {gender.Description}");
            Console.WriteLine(new string('-', 20));
        }
    }

    private void UpdateGender()
    {
        Console.Write("Digite o ID do gênero a ser atualizado: ");
        int id = int.Parse(Console.ReadLine());

        var genderToUpdate = genderRepository.GetBy(g => g.GenderId == id);

        if (genderToUpdate == null)
        {
            Console.WriteLine("Gênero não encontrado.");
            return;
        }

        Console.Write("Novo Nome (deixe em branco para manter o atual): ");
        string name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name)) genderToUpdate.Name = name;

        Console.Write("Nova Descrição (deixe em branco para manter a atual): ");
        string description = Console.ReadLine();
        if (!string.IsNullOrEmpty(description)) genderToUpdate.Description = description;

        genderRepository.Update(genderToUpdate);
        Console.WriteLine("Gênero atualizado com sucesso!");
    }

    private void RemoveGender()
    {
        Console.Write("Digite o ID do gênero a ser removido: ");
        int id = int.Parse(Console.ReadLine());

        var genderToRemove = genderRepository.GetBy(g => g.GenderId == id);

        if (genderToRemove == null)
        {
            Console.WriteLine("Gênero não encontrado.");
            return;
        }

        genderRepository.Delete(genderToRemove);
        Console.WriteLine("Gênero removido com sucesso!");
    }
}
