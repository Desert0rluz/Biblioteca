﻿using Data;

namespace Menus;

public class OpcaoMenuPrincipal
{
    public static void Main(string[] args)
    {
        bool exit = false;

        using (var context = new BibliotecaContext())
        {
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("====== MENU ======");
                Console.WriteLine("1. Opção Livro");
                Console.WriteLine("2. Opção Dono");
                Console.WriteLine("3. Opção Amigo");
                Console.WriteLine("4. Opção Empréstimo");
                Console.WriteLine("5. Opção Gênero");
                Console.WriteLine("6. Sair");
                Console.Write("Escolha uma opção: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Você escolheu a Opção 1: Livro.");
                        Console.Clear();
                        var menuBooks = new MenuBooks(context);
                        menuBooks.MainMenu();
                        break;

                    case "2":
                        Console.WriteLine("Você escolheu a Opção 2: Dono.");
                        Console.Clear();
                        var menuOwner = new MenuOwner(context);
                        menuOwner.MainMenu();
                        break;

                    case "3":
                        Console.WriteLine("Você escolheu a Opção 3: Amigo.");
                        Console.Clear();
                        var menuFriend = new MenuFriend(context);
                        menuFriend.MainFriend();
                        break;

                    case "4":
                        Console.WriteLine("Você escolheu a Opção 4: Empréstimo.");
                        Console.Clear();
                        var menuLoan = new MenuLoans(context);
                        menuLoan.MainMenu();
                        break;

                    case "5":
                        Console.WriteLine("Você escolheu a Opção 5: Gênero.");
                        Console.Clear();
                        var menuGender = new MenuGender(context);
                        menuGender.MainMenu();
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
    }
}
