using SwiFGames.Entities;

namespace SwiFGames
{
    internal class Program
    {

        static void Main(string[] args)
        {
            BaseUsers baseUsers = new BaseUsers();
            RegisterUsersInTheBaseManually(baseUsers);
            Catalog catalog = new Catalog();
            RegisterProductsInCatalogManually(catalog);
            MainTitle();
            MainMenu(baseUsers, catalog);
        }
        public static void MainTitle()
        {
            Console.WriteLine(@"
░██████╗░██╗░░░░░░░██╗██╗███████╗  ░██████╗░░█████╗░███╗░░░███╗███████╗░██████╗
██╔════╝░██║░░██╗░░██║██║██╔════╝  ██╔════╝░██╔══██╗████╗░████║██╔════╝██╔════╝
╚█████╗░░╚██╗████╗██╔╝██║█████╗░░  ██║░░██╗░███████║██╔████╔██║█████╗░░╚█████╗░
░╚═══██╗░░████╔═████║░██║██╔══╝░░  ██║░░╚██╗██╔══██║██║╚██╔╝██║██╔══╝░░░╚═══██╗
██████╔╝░░╚██╔╝░╚██╔╝░██║██║░░░░░  ╚██████╔╝██║░░██║██║░╚═╝░██║███████╗██████╔╝
╚═════╝░░░░╚═╝░░░╚═╝░░╚═╝╚═╝░░░░░  ░╚═════╝░╚═╝░░╚═╝╚═╝░░░░░╚═╝╚══════╝╚═════╝░");
            Console.WriteLine("================================Seja Bem-Vindo!================================");
        }
        public static void MainMenu(BaseUsers baseUsers, Catalog catalog)
        {
            Console.WriteLine();
            FormatTitles("MENU PRINCIPAL");
            Console.WriteLine("1 - Cadastre-se\n2 - Fazer Login\n");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            int optionMainMenu = int.Parse(Console.ReadLine()!);

            switch (optionMainMenu)
            {
                case 1:
                    Console.Clear();
                    MainTitle();
                    UserRegistrationMenu(baseUsers);
                    Console.WriteLine("Deseja voltar para o Menu Principal? s/n");
                    char op = char.Parse(Console.ReadLine()!);
                    if (op == 's')
                    {
                        Console.Clear();
                        MainTitle();
                        MainMenu(baseUsers, catalog);
                    }
                    break;
                case 2:
                    Console.Clear();
                    MainTitle();
                    LoginMenu(baseUsers, catalog);
                    break;
                default:
                    Console.WriteLine("Essa opção não existe no menu, favor escolher novamente!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    MainTitle();
                    MainMenu(baseUsers, catalog);
                    break;
            }
        }
        public static void UserRegistrationMenu(BaseUsers baseUsers)
        {
            Console.WriteLine();
            FormatTitles("TELA DE CADASTRO DE USUÁRIOS");
            Console.WriteLine("1 - Cliente\n2 - Administrador\n3 - Imprimir Lista de Usuarios Cadastrados");
            Console.WriteLine();
            Console.Write("Selecione a opção desejada: ");
            int op = int.Parse(Console.ReadLine()!);

            if (op == 1)
            {
                Console.Clear();
                MainTitle();
                UserRegistration(baseUsers, "Customer", op);
            }
            else if (op == 2)
            {
                Console.Clear();
                MainTitle();
                UserRegistration(baseUsers, "Administrator", op);
            }
            else if (op == 3)
            {
                Console.Clear();
                MainTitle();
                Console.WriteLine();
                FormatTitles("LISTA DE USUÁRIOS CADASTRADOS: ");
                Console.WriteLine(baseUsers);
            }
            else
            {
                Console.WriteLine("Essa opção não existe no menu, favor escolher novamente!");
                Thread.Sleep(2000);
                Console.Clear();
                MainTitle();
                UserRegistrationMenu(baseUsers);
            }
        }
        public static void LoginMenu(BaseUsers baseUsers, Catalog catalog)
        {
            FormatTitles("Escolha uma das opções abaixo: \n1 - Fazer Login\n2 - Voltar ao Menu Principal");
            int op = int.Parse(Console.ReadLine()!);

            if (op == 1)
            {
                Console.Clear();
                MainTitle();
                Console.WriteLine();
                FormatTitles("Faça seu login: ");
                Console.Write("E-mail:");
                string email = Console.ReadLine()!;
                Console.Write("Password:");
                string password = Console.ReadLine()!;

                string? administrador = "Administrator";
                string? customer = "Customer";

                int tamanhoList = baseUsers.Users.Count();
                int count = 0;
                foreach (User user in baseUsers.Users)
                {
                    if (user.Email == email && user.Password == password)
                    {
                        if (user.Category == administrador)
                        {
                            
                            Console.Clear();
                            MainTitle();
                            Console.WriteLine();
                            FormatTitles("ADMINISTRATOR MENU");
                            AdministratorMenu();
                            //CHAMAR MENU ADMIISTRADOR
                        }
                        else if (user.Category == customer)
                        {
                            Customer client = new Customer(user.UserId,user.Name,user.Email,user.Phone,user.Password,user.Category); 
                            Console.Clear();
                            MainTitle();
                            Console.WriteLine();
                            FormatTitles("CUSTOMER MENU");
                            CustomerMenu(catalog, client);
                            //CHAMAR MENU CUSTOMER
                        }
                    }
                    else
                    {
                        count++;
                    }

                }
                if (count == tamanhoList)
                {
                    Console.WriteLine();
                    Console.WriteLine("Credenciais inválidas, Digite Novamente!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    MainTitle();
                    LoginMenu(baseUsers, catalog);
                }
            }
            else
            {
                Console.Clear();
                MainTitle();
                MainMenu(baseUsers, catalog);
            }
        }
        public static void UserRegistration(BaseUsers baseUsers, string category, int op)
        {
            Console.WriteLine();
            FormatTitles("ENTRE COM OS DADOS DO USUÁRIO: ");
            Console.Write("Id: ");
            int userId = int.Parse(Console.ReadLine()!);
            if (baseUsers.Users.Find(x => x.UserId == userId) != null)
            {
                Console.WriteLine("Id já existente na base! Por gentileza Digitar novamente!");
                Thread.Sleep(3000);
                Console.Clear();
                MainTitle();
                UserRegistration(baseUsers, category, op);
            }

            Console.Write("Nome: ");
            string name = Console.ReadLine()!;
            Console.Write("Email: ");
            string email = Console.ReadLine()!;
            Console.Write("Phone: ");
            string phone = Console.ReadLine()!;
            Console.Write("Password: ");
            string password = Console.ReadLine()!;
            if (op == 1)
            {
                baseUsers.AddNewUserAtBase(new Customer(userId, name, email, phone, password, category));
            }
            else if (op == 2)
            {
                baseUsers.AddNewUserAtBase(new Administrator(userId, name, email, phone, password, category));
            }
            Console.Clear();
            MainTitle();
            Console.WriteLine();
            FormatTitles("Usuário cadastrado com sucesso na base");
            Console.WriteLine();
        }
        public static void RegisterUsersInTheBaseManually(BaseUsers baseUsers)
        {
            User user1 = new Customer(1, "Rick", "rick@gmail.com", "1191991-2342", "1234567", "Customer");
            User user2 = new Customer(2, "Thiago", "thiago@gmail.com", "1191291-2312", "1234567", "Customer");
            User user3 = new Customer(3, "Nicolas", "nicolas@gmail.com", "1198991-2242", "1234567", "Customer");
            User user4 = new Administrator(4, "Guilherme", "guilherme@gmail.com", "1191993-4341", "1234567", "Administrator");
            User user5 = new Administrator(5, "Ricardo", "ricardo@gmail.com", "1191791-2512", "1234567", "Administrator");
            User user6 = new Administrator(6, "Elizabeth", "elizabeth@gmail.com", "1191241-2342", "1234567", "Administrator");
            baseUsers.AddNewUserAtBase(user1);
            baseUsers.AddNewUserAtBase(user2);
            baseUsers.AddNewUserAtBase(user3);
            baseUsers.AddNewUserAtBase(user4);
            baseUsers.AddNewUserAtBase(user5);
            baseUsers.AddNewUserAtBase(user6);
        }
        public static void FormatTitles(string text)
        {
            int qtdCaracter = text.Length;
            string asterisco = string.Empty.PadLeft(qtdCaracter, '*');
            Console.WriteLine(asterisco);
            Console.WriteLine(text);
            Console.WriteLine(asterisco + "\n");
        }
        public static void CustomerMenu(Catalog catalog, Customer client)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Ver Catalogo\n2 - Ver pedidos\n3 - Histórico de Compras\n");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            int optionCustomerMenu = int.Parse(Console.ReadLine()!);

            switch (optionCustomerMenu)
            {
                case 1:
                    Console.Clear();
                    MainTitle();
                    FormatTitles("CATÁLOGO DA LOJA");
                    Console.WriteLine(catalog);

                    Console.WriteLine();
                    FormatTitles("Deseja fazer um pedido? (s/n)");
                    Console.WriteLine();
                    Console.Write("Digite a opção desejada: ");
                    char op = char.Parse(Console.ReadLine()!);
                    if (op == 's')
                    {
                        Console.Clear();
                        MainTitle();
                        RegisterOrder(catalog, client);
                    }
                    else if (op == 'n')
                    {
                        Console.Clear();
                        MainTitle();
                        CustomerMenu(catalog, client);
                    }
                    break;
                case 2:
                                
                    break;
            }
        }
        public static void AdministratorMenu() { }
        public static void RegisterProductsInCatalogManually(Catalog catalog)
        {
            Product p1 = new Product(1, "God Of War 1", "God 4 Para todos os amantes de Jogos Nordicos", 550.00);
            Product p2 = new Product(2, "Beatle Field", "Jogo de Guerra para afortunados e amantes de tiro", 600.00);
            Product p3 = new Product(3, "Residen Evil Village", "Jogo que mexe com sua adrenalina e seus maiores medos", 700.00);
            Product p4 = new Product(4, "FIFA 2023", "Para você que é amantes de Futebol FIFA veio trazer a melhor experiência", 800.00);
            catalog.AddToTheCatalog(p1);
            catalog.AddToTheCatalog(p2);
            catalog.AddToTheCatalog(p3);
            catalog.AddToTheCatalog(p4);
        }
        public static void RegisterOrder(Catalog catalog, Customer client)
        {
            Console.WriteLine("REGISTRAR UMA ORDEM");

        }

    }

}


