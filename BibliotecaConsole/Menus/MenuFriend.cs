using Data;
using Models;

namespace Menus;

public class MenuFriend
{
    private readonly DAL<Friend> friendRepository;

    public MenuFriend(BibliotecaContext context)
    {
        friendRepository = new DAL<Friend>(context);
    }

    public void MainFriend()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("====== MENU ======");
            Console.WriteLine("1. Adicionar Amigo");
            Console.WriteLine("2. Listar Amigos");
            Console.WriteLine("3. Atualizar Amigo");
            Console.WriteLine("4. Remover Amigo");
            Console.WriteLine("5. Retornar ao Menu Principal");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddFriend();
                    break;
                case "2":
                    ListFriends();
                    break;
                case "3":
                    UpdateFriend();
                    break;
                case "4":
                    RemoveFriend();
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

    private void AddFriend()
    {
        var newFriend = new Friend();

        Console.Write("Nome: ");
        newFriend.Name = Console.ReadLine();

        Console.Write("Email: ");
        newFriend.Email = Console.ReadLine();

        Console.Write("Telefone: ");
        newFriend.PhoneNumber = Console.ReadLine();

        friendRepository.Add(newFriend);
        Console.WriteLine("Amigo adicionado com sucesso!");
    }

    private void ListFriends()
    {
        var friends = friendRepository.List();

        if (!friends.Any())
        {
            Console.WriteLine("Nenhum amigo cadastrado.");
            return;
        }

        foreach (var friend in friends)
        {
            Console.WriteLine($"ID do Amigo: {friend.FriendId}");
            Console.WriteLine($"Nome: {friend.Name}");
            Console.WriteLine($"Email: {friend.Email}");
            Console.WriteLine($"Telefone: {friend.PhoneNumber}");
            Console.WriteLine(new string('-', 20));
        }
    }

    private void UpdateFriend()
    {
        Console.Write("Digite o ID do amigo a ser atualizado: ");
        int id = int.Parse(Console.ReadLine());

        var friendToUpdate = friendRepository.GetBy(f => f.FriendId == id);

        if (friendToUpdate == null)
        {
            Console.WriteLine("Amigo não encontrado.");
            return;
        }

        Console.Write("Novo Nome (deixe em branco para manter o atual): ");
        string name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name)) friendToUpdate.Name = name;

        Console.Write("Novo Email (deixe em branco para manter o atual): ");
        string email = Console.ReadLine();
        if (!string.IsNullOrEmpty(email)) friendToUpdate.Email = email;

        Console.Write("Novo Telefone (deixe em branco para manter o atual): ");
        string phoneNumber = Console.ReadLine();
        if (!string.IsNullOrEmpty(phoneNumber)) friendToUpdate.PhoneNumber = phoneNumber;

        friendRepository.Update(friendToUpdate);
        Console.WriteLine("Amigo atualizado com sucesso!");
    }

    private void RemoveFriend()
    {
        Console.Write("Digite o ID do amigo a ser removido: ");
        int id = int.Parse(Console.ReadLine());

        var friendToRemove = friendRepository.GetBy(f => f.FriendId == id);

        if (friendToRemove == null)
        {
            Console.WriteLine("Amigo não encontrado.");
            return;
        }

        friendRepository.Delete(friendToRemove);
        Console.WriteLine("Amigo removido com sucesso!");
    }
}
