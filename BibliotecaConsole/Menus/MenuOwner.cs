using Data;
using Models;

namespace Menus;

public class MenuOwner
{
    private readonly DAL<Owner> ownerRepository;

    public MenuOwner(BibliotecaContext context)
    {
        ownerRepository = new DAL<Owner>(context);
    }

    public void MainMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("====== MENU ======");
            Console.WriteLine("1. Adicionar Dono");
            Console.WriteLine("2. Listar Donos");
            Console.WriteLine("3. Atualizar Dono");
            Console.WriteLine("4. Remover Dono");
            Console.WriteLine("5. Retornar ao Menu Principal");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddOwner();
                    break;
                case "2":
                    ListOwner();
                    break;
                case "3":
                    UpdateOwner();
                    break;
                case "4":
                    RemoveOwner();
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

    private void AddOwner()
    {
        var newOwner = new Owner();

        Console.Write("Nome: ");
        newOwner.Name = Console.ReadLine();

        Console.Write("Email: ");
        newOwner.Email = Console.ReadLine();

        Console.Write("Telefone: ");
        newOwner.PhoneNumber = Console.ReadLine();

        ownerRepository.Add(newOwner);
        Console.WriteLine("Dono adicionado com sucesso!");
    }

    private void ListOwner()
    {
        var owners = ownerRepository.List();

        if (!owners.Any())
        {
            Console.WriteLine("Nenhum dono cadastrado.");
            return;
        }

        foreach (var owner in owners)
        {
            Console.WriteLine($"ID: {owner.OwnerId}");
            Console.WriteLine($"Nome: {owner.Name}");
            Console.WriteLine($"Email: {owner.Email}");
            Console.WriteLine($"Telefone: {owner.PhoneNumber}");
            Console.WriteLine(new string('-', 20));
        }
    }

    private void UpdateOwner()
    {
        Console.Write("Digite o ID do dono a ser atualizado: ");
        int id = int.Parse(Console.ReadLine());

        var ownerToUpdate = ownerRepository.GetBy(o => o.OwnerId == id);

        if (ownerToUpdate == null)
        {
            Console.WriteLine("Dono não encontrado.");
            return;
        }

        Console.Write("Novo Nome (deixe em branco para manter o atual): ");
        string name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name)) ownerToUpdate.Name = name;

        Console.Write("Novo Email (deixe em branco para manter o atual): ");
        string email = Console.ReadLine();
        if (!string.IsNullOrEmpty(email)) ownerToUpdate.Email = email;

        Console.Write("Novo Telefone (deixe em branco para manter o atual): ");
        string phoneNumber = Console.ReadLine();
        if (!string.IsNullOrEmpty(phoneNumber)) ownerToUpdate.PhoneNumber = phoneNumber;

        ownerRepository.Update(ownerToUpdate);
        Console.WriteLine("Dono atualizado com sucesso!");
    }

    private void RemoveOwner()
    {
        Console.Write("Digite o ID do dono a ser removido: ");
        int id = int.Parse(Console.ReadLine());

        var ownerToRemove = ownerRepository.GetBy(o => o.OwnerId == id);

        if (ownerToRemove == null)
        {
            Console.WriteLine("Dono não encontrado.");
            return;
        }

        ownerRepository.Delete(ownerToRemove);
        Console.WriteLine("Dono removido com sucesso!");
    }
}
