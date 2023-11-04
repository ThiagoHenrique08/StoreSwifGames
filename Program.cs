using SwiFGames.Entities;
using System.Globalization;
using SwiFGames.Entities.Enums;
using System.Security.Cryptography;
using System.Net.NetworkInformation;

namespace SwiFGames
{
    internal class Program
    {

        static void Main(string[] args)
        {
            BaseUsers baseUsers = new BaseUsers();
            RegisterUsersInTheBaseManually(baseUsers);
            Catalog catalog = new Catalog();
            OrderHistory orderHistory = new OrderHistory();

            RegisterProductsInCatalogManually(catalog);
            MainTitle();
            MainMenu(baseUsers, catalog, orderHistory);
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
        public static void MainMenu(BaseUsers baseUsers, Catalog catalog, OrderHistory orderHistory)
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
                        MainMenu(baseUsers, catalog, orderHistory);
                    }
                    break;
                case 2:
                    Console.Clear();
                    MainTitle();
                    LoginMenu(baseUsers, catalog, orderHistory);
                    break;
                default:
                    Console.WriteLine("Essa opção não existe no menu, favor escolher novamente!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    MainTitle();
                    MainMenu(baseUsers, catalog, orderHistory);
                    break;
            }
        }
        public static void LoginMenu(BaseUsers baseUsers, Catalog catalog, OrderHistory orderHistory)
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
                            Customer client = new Customer(user.UserId, user.Name, user.Email, user.Phone, user.Password, user.Category);
                            Console.Clear();
                            MainTitle();
                            Console.WriteLine();
                            FormatTitles("CUSTOMER MENU");
                            CustomerMenu(baseUsers, catalog, client, orderHistory);
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
                    LoginMenu(baseUsers, catalog, orderHistory);
                }
            }
            else
            {
                Console.Clear();
                MainTitle();
                MainMenu(baseUsers, catalog, orderHistory);
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
        public static void CustomerMenu(BaseUsers baseUsers, Catalog catalog, Customer customer, OrderHistory orderHistory)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Ver Catalogo\n2 - Ver pedidos\n3 - Histórico de Compras\n4 - Logout");
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
                        RegisterOrder(baseUsers, catalog, customer, orderHistory);
                    }
                    else if (op == 'n')
                    {
                        Console.Clear();
                        MainTitle();
                        CustomerMenu(baseUsers, catalog, customer, orderHistory);
                    }
                    break;
                case 2:
                    Console.Clear();
                    MainTitle();
                    FormatTitles("DADOS DO PEDIDO");
                    Console.WriteLine();
                    OrdersInProgress(baseUsers, catalog, customer, orderHistory);
                    FinalizePayment(baseUsers, catalog, customer, orderHistory);
                    break;

                case 3:
                    Console.Clear();
                    MainTitle();
                    PurchaseHistoric(baseUsers, catalog, customer, orderHistory);
                    break;
                case 4:
                    Console.Clear();
                    MainTitle();
                    MainMenu(baseUsers, catalog, orderHistory);
                    break;
            }
        }
        public static void AdministratorMenu() { }
        public static void RegisterProductsInCatalogManually(Catalog catalog)
        {
            Product p1 = new Product(1, "God Of War 1", "God 4 Para todos os amantes de Jogos Nordicos", 550.00, 1);
            Product p2 = new Product(2, "Beatle Field", "Jogo de Guerra para afortunados e amantes de tiro", 600.00, 1);
            Product p3 = new Product(3, "Residen Evil Village", "Jogo que mexe com sua adrenalina e seus maiores medos", 700.00, 1);
            Product p4 = new Product(4, "FIFA 2023", "Para você que é amantes de Futebol FIFA veio trazer a melhor experiência", 800.00, 1);
            catalog.AddToTheCatalog(p1);
            catalog.AddToTheCatalog(p2);
            catalog.AddToTheCatalog(p3);
            catalog.AddToTheCatalog(p4);
        }
        public static void RegisterOrder(BaseUsers baseUsers, Catalog catalog, Customer customer, OrderHistory orderHistory)
        {
            char controle;
            Console.Write("Deseja selecionar um produto da lista? (s/n): ");
            Console.WriteLine();
            Console.Write("Selecione a opção desejada: ");
            controle = char.Parse(Console.ReadLine()!);
            Product f;
            Order order = new Order();
            while (controle == 's')
            {

                Console.WriteLine();
                Console.Write("Digite o ID do Produto escolhido: ");
                int idproduct = int.Parse(Console.ReadLine()!);

                f = catalog.products.FirstOrDefault(x => x.ProductId == idproduct)!;
                Console.WriteLine();
                Console.Write("Digite a quantidade do produto: ");
                int quantity = int.Parse(Console.ReadLine()!);
                if (f != null)
                {
                    order.Products.Add(new Product(f.ProductId, f.Name, f.Description, f.Price * quantity, f.Quantity = quantity)); ;
                }

                Console.WriteLine();
                Console.Write("Deseja selecionar outro produto da lista? (s/n): ");
                controle = char.Parse(Console.ReadLine()!);

            }
            FormatTitles("Aguarde um instante que estamos finalizando seu pedido");

            StatusOrder status = Enum.Parse<StatusOrder>("Processing");
            Random aleatorio = new Random();
            int auxId = aleatorio.Next(100);

            while (orderHistory.orders.FirstOrDefault(x => x.OrderId == auxId) != null)
            {
                auxId = aleatorio.Next(100);
            }


            order.OrderId = auxId;
            order.Moment = DateTime.Now;
            order.Status = status;
            order.Customer = customer;


            orderHistory.AddOrder(order);
            Console.WriteLine();
            Thread.Sleep(3000);
            FormatTitles("Pedido realizado com sucesso!");

            Thread.Sleep(3000);

            Console.Clear();
            MainTitle();
            Console.WriteLine();
            CustomerMenu(baseUsers, catalog, customer, orderHistory);

        }

        public static void FinalizePayment(BaseUsers baseUsers, Catalog catalog, Customer customer, OrderHistory orderHistory)
        {
            Console.WriteLine();
            FormatTitles("Deseja finalizar algum pedido? (s/n): ");
            Console.Write("Digite sua opção: ");
            char op = char.Parse(Console.ReadLine()!);
            Console.WriteLine();

            switch (op)
            {
                case 's':
                    Console.WriteLine("Informe o ID do pedido: ");
                    int idPedido = int.Parse(Console.ReadLine()!);

                    Console.WriteLine();
                    FormatTitles("Aguarde um instante, pagamento está sendo processado!");

                    StatusOrder status = new StatusOrder();
                    status = Enum.Parse<StatusOrder>("Delivered");

                    foreach (Order orderHistoryList in orderHistory.orders)
                    {
                        if (orderHistoryList.OrderId == idPedido)
                        {
                            orderHistoryList.Status = status;
                            break;
                        }

                    }
                    Thread.Sleep(3000);
                    FormatTitles("Pagamento realizado com sucesso!");
                    FormatTitles("Obrigado por comprar conosco!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    MainTitle();
                    CustomerMenu(baseUsers, catalog, customer, orderHistory);
                    break;

                case 'n':
                    Console.Clear();
                    MainTitle();
                    CustomerMenu(baseUsers, catalog, customer, orderHistory);
                    break;
            }



        }

        public static void PurchaseHistoric(BaseUsers baseUsers, Catalog catalog, Customer customer, OrderHistory orderHistory)
        {
            StatusOrder status = new StatusOrder();
            status = Enum.Parse<StatusOrder>("Delivered");

            if (orderHistory.orders.FirstOrDefault(x => x.Customer.UserId == customer.UserId) != null &&
                orderHistory.orders.FirstOrDefault(x => x.Status == status) != null)
            {


                foreach (Order order in orderHistory.orders)
                {

                    if (order.Status == status && order.Customer.UserId == customer.UserId)
                    {
                        Console.Write("Id Pedido: " + order.OrderId + "\n");
                        Console.Write("Data do pedido: " + order.Moment + "\n");
                        Console.Write("Status: " + order.Status + "\n");
                        Console.Write("Cliente: " + order.Customer.Name + "\n");
                        FormatTitles("Produtos Adquiridos neste pedido: ");
                        foreach (Product product in order.Products)
                        {
                            Console.Write("Produto: " + product.Name + ", R$ " + product.Price.ToString("F2", CultureInfo.InvariantCulture) + "\n"); ;
                        }
                        Console.WriteLine("=====================================================");
                    }
                    
                }
                Console.WriteLine();
                Console.Write("Digite qualquer tecla para voltar ao menu principal: ");
                Console.ReadLine();
                Console.Clear();
                MainTitle();
                CustomerMenu(baseUsers, catalog, customer, orderHistory);
            }
            else
            {
                FormatTitles("USUÁRIO AINDA NÃO FINALIZOU UMA COMPRA!!! Para finalizar um pedido, favor entrar na opção Ver Pedido no Menu principal!");
                Thread.Sleep(3000);
                Console.Clear();
                MainTitle();
                CustomerMenu(baseUsers, catalog, customer, orderHistory);
            }
        }

        public static void OrdersInProgress(BaseUsers baseUsers, Catalog catalog, Customer customer, OrderHistory orderHistory)
        {
            StatusOrder status = new StatusOrder();
            status = Enum.Parse<StatusOrder>("Processing");

            if (orderHistory.orders.FirstOrDefault(x => x.Customer.UserId == customer.UserId) != null &&
                orderHistory.orders.FirstOrDefault(x => x.Status == status) != null)
            {

                foreach (Order order in orderHistory.orders)
                {


                    if ((order.Status == status && order.Customer.UserId == customer.UserId)) 
                    {
                        Console.Write("Id Pedido: " + order.OrderId + "\n");
                        Console.Write("Data do pedido: " + order.Moment + "\n");
                        Console.Write("Status: " + order.Status + "\n");
                        Console.Write("Cliente: " + order.Customer.Name + "\n");
                        FormatTitles("Produtos Adquiridos neste pedido: ");
                        foreach (Product product in order.Products)
                        {
                            Console.Write("Produto: " + product.Name + ", R$ " + product.Price.ToString("F2", CultureInfo.InvariantCulture) + "\n"); ;
                        }
                        Console.WriteLine("=====================================================");

                    }
                    
                }
            }else
            {
                FormatTitles("USUÁRIO AINDA NÃO POSSUI PEDIDO!!! Para realizar um pedido, favor entrar no catalogo e selecionar um produto!");
                Thread.Sleep(3000);
                Console.Clear();
                MainTitle();
                CustomerMenu(baseUsers, catalog, customer, orderHistory);
            }
        }


    }
}




