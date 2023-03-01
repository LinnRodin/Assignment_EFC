using Assignment_EFC.Contexts;
using Assignment_EFC.Services;

var menu = new MenuService();

while (true)
{

    Console.Clear();
    Console.WriteLine("1. Create a new ticket");
    Console.WriteLine("2. Show all tickets");
    Console.WriteLine("3. Show a specific ticket");
    Console.WriteLine("4. Update a specific ticket");
    Console.WriteLine("5. Delete a specific ticket");
    Console.Write("Choose one of the following options(1-5): ");


    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            await menu.CreateNewTicketAsync();
            break;

        case "2":
            Console.Clear();
            await menu.ListAllTicketsAsync();
            break;

        case "3":
            Console.Clear();
            await menu.ShowSpecificTicketAsync();
            break;

        case "4":
            Console.Clear();
            await menu.UpdateSpecificTicketAsync();
            break;

        case "5":
            Console.Clear();
            await menu.DeleteSpecificTicketAsync();
            break;
    }

    Console.WriteLine("\nPush any key to continue...");
    Console.ReadKey();

}