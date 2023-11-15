using SwiFGames.Entities;
using System.Globalization;
using SwiFGames.Entities.Enums;
using SwiFGames.Controlers;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Reflection.Emit;

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
            Console.Clear();


            Console.WriteLine(@"
█████████████████████████████████▀███████████████████████████
█─▄▄▄▄█▄─█▀▀▀█─▄█▄─▄█▄─▄▄─███─▄▄▄▄██▀▄─██▄─▀█▀─▄█▄─▄▄─█─▄▄▄▄█
█▄▄▄▄─██─█─█─█─███─███─▄█████─██▄─██─▀─███─█▄█─███─▄█▀█▄▄▄▄─█
▀▄▄▄▄▄▀▀▄▄▄▀▄▄▄▀▀▄▄▄▀▄▄▄▀▀▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▀▄▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀");



            FormatTitles("========================Seja Bem-Vindo!=======================");
        }
        public static void MainMenu(BaseUsers baseUsers, Catalog catalog, OrderHistory orderHistory)
        {
            Console.WriteLine();
            FormatTitles("***MENU PRINCIPAL***");
            Console.WriteLine("1 - Cadastre-se\n2 - Fazer Login\n");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            int optionMainMenu = int.Parse(Console.ReadLine()!);

            switch (optionMainMenu)
            {
                case 1:

                    MainTitle();
                    UserRegistrationMenu(baseUsers, catalog, orderHistory);
                    Console.Write("Deseja voltar para o Menu Principal? s/n: ");
                    char op = char.Parse(Console.ReadLine()!);
                    if (op == 's')
                    {

                        MainTitle();
                        MainMenu(baseUsers, catalog, orderHistory);
                    }
                    break;
                case 2:

                    MainTitle();
                    LoginMenu(baseUsers, catalog, orderHistory);
                    break;
                default:
                    Console.WriteLine("Essa opção não existe no menu, favor escolher novamente!");
                    Thread.Sleep(2000);

                    MainTitle();
                    MainMenu(baseUsers, catalog, orderHistory);
                    break;
            }
        }
        public static void LoginMenu(BaseUsers baseUsers, Catalog catalog, OrderHistory orderHistory)
        {
            FormatTitles("***MENU PRINCIPAL***");
            Console.WriteLine("1 - Fazer Login\n2 - Voltar ao Menu Principal");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            int op = int.Parse(Console.ReadLine()!);

            if (op == 1)
            {

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
                            Administrator administrator = new Administrator(user.UserId, user.Name!, user.Email, user.Phone!, user.Password, user.Category);

                            MainTitle();
                            Console.WriteLine();
                           
                            AdministratorMenu(baseUsers, catalog, administrator, orderHistory);

                        }
                        else if (user.Category == customer)
                        {
                            Customer client = new Customer(user.UserId, user.Name!, user.Email, user.Phone!, user.Password, user.Category);

                            MainTitle();
                            Console.WriteLine();

                            CustomerMenu(baseUsers, catalog, client, orderHistory);

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

                    MainTitle();
                    LoginMenu(baseUsers, catalog, orderHistory);
                }
            }
            else
            {

                MainTitle();
                MainMenu(baseUsers, catalog, orderHistory);
            }
        }
        public static void UserRegistrationMenu(BaseUsers baseUsers, Catalog catalog, OrderHistory orderHistory)
        {
            Console.WriteLine();
            FormatTitles("***TELA DE CADASTRO DE USUÁRIOS***");
            Console.WriteLine("1 - Cliente\n2 - Administrador\n");
            Console.WriteLine();
            Console.Write("Selecione a opção desejada: ");
            int op = int.Parse(Console.ReadLine()!);

            if (op == 1)
            {

                MainTitle();
                UserRegistration(baseUsers, "Customer", op, catalog, orderHistory);
            }
            else if (op == 2)
            {

                MainTitle();
                UserRegistration(baseUsers, "Administrator", op, catalog, orderHistory);
            }

            else
            {
                Console.WriteLine("Essa opção não existe no menu, favor escolher novamente!");
                Thread.Sleep(2000);

                MainTitle();
                UserRegistrationMenu(baseUsers, catalog, orderHistory);
            }
        }

        public static void UserRegistration(BaseUsers baseUsers, string category, int op, Catalog catalog, OrderHistory orderHistory)
        {
            Console.WriteLine();
            FormatTitles("***ENTRE COM OS DADOS DO USUÁRIO: ***");

            Random aleatorio = new Random();
            int auxId = aleatorio.Next(100);

            while (baseUsers.Users.FirstOrDefault(x => x.UserId == auxId) != null)
            {
                auxId = aleatorio.Next(100);
            }
            Console.WriteLine();
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
                baseUsers.AddNewUserAtBase(new Customer(auxId, name, email, phone, password, category));
            }
            else if (op == 2)
            {
                baseUsers.AddNewUserAtBase(new Administrator(auxId, name, email, phone, password, category));
            }

            Thread.Sleep(2000);
            Console.WriteLine();
            FormatTitles("***Usuário cadastrado com sucesso na base***");
            Console.WriteLine();
            Thread.Sleep(3000);
            MainTitle();
            MainMenu(baseUsers, catalog, orderHistory);
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
            FormatTitles("MENU CLIENTE");
            Console.WriteLine();
            Console.WriteLine("1 - Fazer um pedido\n2 - Ver pedidos\n3 - Histórico de Compras\n4 - Logout");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            int optionCustomerMenu = int.Parse(Console.ReadLine()!);

            switch (optionCustomerMenu)
            {
                case 1:

                    MainTitle();
                    RegisterOrder(baseUsers, catalog, customer, orderHistory);

                    break;
                case 2:

                    MainTitle();
                    FormatTitles("***DADOS DO PEDIDO***");
                    Console.WriteLine();
                    OrdersInProgress(baseUsers, catalog, customer, orderHistory);
                    FinalizePayment(baseUsers, catalog, customer, orderHistory);
                    break;

                case 3:

                    MainTitle();
                    PurchaseHistoric(baseUsers, catalog, customer, orderHistory);
                    break;
                case 4:

                    MainTitle();
                    MainMenu(baseUsers, catalog, orderHistory);
                    break;
            }
        }
        public static void AdministratorMenu(BaseUsers baseUsers, Catalog catalog, Administrator administrator, OrderHistory orderHistory)
        {
            FormatTitles("MENU ADMINISTRATOR");  
            Console.WriteLine("1 - Produtos\n2 - Relatórios\n3 - Usuários Cadastrados\n4 - Logout");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            int optionCustomerMenu = int.Parse(Console.ReadLine()!);

            switch (optionCustomerMenu)
            {
                case 1:

                    MainTitle();
                    Console.WriteLine();
                    FormatTitles("***MENU - OPÇÕES DE PRODUTO***");
                 
                    Console.WriteLine("1 - Cadastrar um novo produto\n2 - Remover um produto\n3 - Alterar dados de um produto\n4 - Ver Catalogo");
                    Console.Write("Digite a opção desejada: ");
                    int op = int.Parse(Console.ReadLine()!);
                    if (op == 1)
                    {
                        MainTitle();
                        Console.WriteLine();
                        FormatTitles("***CADASTRANDO UM NOVO PRODUTO***");
                        Console.WriteLine();
                        Random aleatorio = new Random();
                        int auxId = aleatorio.Next(100);

                        while (catalog.products.FirstOrDefault(x => x.ProductId == auxId) != null)
                        {
                            auxId = aleatorio.Next(100);
                        }
                        Console.Write("Digite o nome do produto: ");
                        string name = Console.ReadLine()!;
                        Console.Write("Digite uma descrição do produto: ");
                        string description = Console.ReadLine()!;
                        Console.Write("Digite o preço do produto: ");
                        double price = double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);
                        catalog.AddToProductToCatalog(new Product(auxId, name, description, price));
                        Console.WriteLine();
                        FormatTitles("Produto Adicionado com sucesso!");
                        Console.WriteLine();
                        Console.WriteLine("Aperte qualquer tecla para voltar");
                        Console.ReadLine();
                        MainTitle();
                        AdministratorMenu(baseUsers, catalog, administrator, orderHistory);

                    }
                    else if (op == 2)
                    {
                   

                        MainTitle();
                        Console.WriteLine(catalog);
                        FormatTitles("***REMOVENDO UM PRODUTO***:");
                        Console.WriteLine("Digite o ID do produto a ser removido");
                        int id = int.Parse(Console.ReadLine()!);

                        Product product = new Product();
                        product = catalog.products.FirstOrDefault(x => x.ProductId == id)!;

                        if (product != null)
                        {
                            catalog.RemoveProductToCatalog(product);
                            FormatTitles($"Produto de Id: {id}, removido com sucesso!");
                            Thread.Sleep(2000);
                            MainTitle();
                            AdministratorMenu(baseUsers, catalog, administrator, orderHistory);
                        }
                        else
                        {
                            Console.WriteLine("Id não localizado na Base!");
                            Thread.Sleep(2000);
                            MainTitle();
                            AdministratorMenu(baseUsers, catalog, administrator, orderHistory);
                        }
                    }

                    else if (op == 3)
                    {

                        MainTitle();
                        Console.WriteLine(catalog);
                        Console.WriteLine();
                        FormatTitles("***ALTERANDO OS DADOS DO PRODUTO***:");

                        Console.WriteLine("Digite o ID do produto a ser alterado");
                        int id = int.Parse(Console.ReadLine()!);

                        Product product = new Product();
                        product = catalog.products.FirstOrDefault(x => x.ProductId == id)!;

                        if (product != null)
                        {
                            Console.WriteLine("Deseja alterar qual dado? \n1 - Nome\n2 - Descrição\n3 - Preço");

                            int option = int.Parse(Console.ReadLine()!);

                            if (option == 1)
                            {
                                Console.Write("Nome: ");
                                product.Name = Console.ReadLine();
                            }
                            else if (option == 2)
                            {
                                Console.Write("Descrição: ");
                                product.Description = Console.ReadLine();
                            }
                            else if (option == 3)
                            {
                                Console.Write("Preço: ");
                                product.Price = double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);
                            }

                            Console.WriteLine("IMPRIMINDO DADOS ALTERADOS");

                            Console.WriteLine(product.ProductId);
                            Console.WriteLine(product.Name);
                            Console.WriteLine(product.Description);
                            Console.WriteLine(product.Price.ToString("F2", CultureInfo.InvariantCulture));

                            Console.WriteLine("========================");

                            catalog.ChangeCatalogProduct(product);

                            Console.WriteLine("Aperte qualquer tecla para voltar");
                            Console.ReadLine();
                            MainTitle();
                            AdministratorMenu(baseUsers, catalog, administrator, orderHistory);
                        }
                        else
                        {
                            Console.WriteLine("Id não localizado na Base!");
                            Thread.Sleep(2000);
                            MainTitle();
                            AdministratorMenu(baseUsers, catalog, administrator, orderHistory);
                        }


                    }
                    else if (op == 4)
                    {
                        MainTitle();
                        FormatTitles("***CATÁLOGO DA LOJA***");
                        Console.WriteLine(catalog);
                        Console.WriteLine();
                        Console.WriteLine("Aperte qualquer tecla para voltar");
                        Console.ReadLine();
                        MainTitle();
                        AdministratorMenu(baseUsers, catalog, administrator, orderHistory);
                    }
                    else
                    {

                        MainTitle();
                        AdministratorMenu(baseUsers, catalog, administrator, orderHistory);
                    }

                    break;
                case 2:

                    MainTitle();
                    Console.WriteLine();
                    FormatTitles("***MENU - OPÇÕES DE RELATÓRIO***");
                    Console.WriteLine();
                    Reports(baseUsers, catalog, administrator, orderHistory);
                    break;

                case 3:

                    MainTitle();
                    Console.WriteLine();
                    FormatTitles("***LISTA DE USUÁRIOS CADASTRADOS***");
                    Console.WriteLine(baseUsers);
                    Console.WriteLine();
                    Console.WriteLine("Aperte qualquer tecla para voltar");
                    Console.ReadLine();
                    MainTitle();
                    AdministratorMenu(baseUsers, catalog, administrator, orderHistory);

                    break;



                case 4:

                    MainTitle();
                    MainMenu(baseUsers, catalog, orderHistory);

                    break;
            }




        }
        public static void RegisterProductsInCatalogManually(Catalog catalog)
        {
            Product p1 = new Product(1, "God Of War 1", "God 4 Para todos os amantes de Jogos Nordicos", 550.00, 1);
            Product p2 = new Product(2, "Beatle Field", "Jogo de Guerra para afortunados e amantes de tiro", 600.00, 1);
            Product p3 = new Product(3, "Residen Evil Village", "Jogo que mexe com sua adrenalina e seus maiores medos", 700.00, 1);
            Product p4 = new Product(4, "FIFA 2023", "Para você que é amantes de Futebol FIFA veio trazer a melhor experiência", 800.00, 1);
            catalog.AddToProductToCatalog(p1);
            catalog.AddToProductToCatalog(p2);
            catalog.AddToProductToCatalog(p3);
            catalog.AddToProductToCatalog(p4);
        }
        public static void RegisterOrder(BaseUsers baseUsers, Catalog catalog, Customer customer, OrderHistory orderHistory)
        {
            Console.WriteLine();
            FormatTitles("***CATÁLOGO DA LOJA***");
            Console.WriteLine(catalog);

            char controle;
            Console.WriteLine();
            FormatTitles("***Deseja selecionar um produto da lista? (s/n)***");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            controle = char.Parse(Console.ReadLine()!);
            if (controle == 'n')
            {
                MainTitle();
                CustomerMenu(baseUsers, catalog, customer, orderHistory);
            }else if (controle != 'n' && controle != 's')
            {
                Console.WriteLine();
                FormatTitles("Opção inválida!");
                Thread.Sleep(3000);
                MainTitle();
                CustomerMenu(baseUsers, catalog, customer, orderHistory);
            }
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
                FormatTitles("***Deseja selecionar outro produto da lista? (s/n)***");
                Console.WriteLine();
                Console.Write("Digite a opção desejada: ");
                controle = char.Parse(Console.ReadLine()!);


            }

            Console.WriteLine();
            FormatTitles("***Aguarde um instante que estamos finalizando seu pedido***");

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
            FormatTitles("***Pedido realizado com sucesso!***");

            Thread.Sleep(3000);


            MainTitle();
            Console.WriteLine();
            CustomerMenu(baseUsers, catalog, customer, orderHistory);

        }

        public static void FinalizePayment(BaseUsers baseUsers, Catalog catalog, Customer customer, OrderHistory orderHistory)
        {
            Console.WriteLine();
            FormatTitles("***Deseja finalizar algum pedido? (s/n)***");
            Console.Write("Digite sua opção: ");
            char op = char.Parse(Console.ReadLine()!);

            Console.WriteLine();

            switch (op)
            {
                case 's':
                    Console.Write("Informe o ID do pedido: ");
                    int idPedido = int.Parse(Console.ReadLine()!);

                    Console.WriteLine();
                    FormatTitles("***Aguarde um instante, pagamento está sendo processado!***");

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
                    FormatTitles("***Pagamento realizado com sucesso!***");
                    Console.WriteLine();
                    FormatTitles("***Obrigado por comprar conosco!***");
                    Thread.Sleep(3000);

                    MainTitle();
                    CustomerMenu(baseUsers, catalog, customer, orderHistory);
                    break;

                case 'n':

                    MainTitle();
                    CustomerMenu(baseUsers, catalog, customer, orderHistory);
                    break;
            }



        }

        public static void PurchaseHistoric(BaseUsers baseUsers, Catalog catalog, Customer customer, OrderHistory orderHistory)
        {
            StatusOrder status = new StatusOrder();
            status = Enum.Parse<StatusOrder>("Delivered");
            Console.WriteLine("=====================================================");
            if (orderHistory.orders.FirstOrDefault(x => x.Customer.UserId == customer.UserId) != null &&
                orderHistory.orders.FirstOrDefault(x => x.Status == status) != null)
            {


                foreach (Order order in orderHistory.orders)
                {

                    if (order.Status == status && order.Customer.UserId == customer.UserId)
                    {
                        double valorTotalPedido = 0.00;
                        Console.Write("Id Pedido: " + order.OrderId + "\n");
                        Console.Write("Data do pedido: " + order.Moment + "\n");
                        Console.Write("Status: " + order.Status + "\n");
                        Console.Write("Cliente: " + order.Customer.Name + "\n");
                        FormatTitles("***Produtos Adquiridos neste pedido***");
                        foreach (Product product in order.Products)
                        {
                            Console.Write("Produto: " + product.Name + ", R$ " + product.Price.ToString("F2", CultureInfo.InvariantCulture) + "\n"); ;
                            valorTotalPedido += product.Price;
                        }
                        Console.WriteLine();
                        FormatTitles("***VALOR TOTAL PEDIDO: " + valorTotalPedido.ToString("F2", CultureInfo.InvariantCulture) + "***");
                        Console.WriteLine("=====================================================");
                    }

                }
                Console.WriteLine();
                Console.Write("Digite qualquer tecla para voltar ao menu principal: ");
                Console.ReadLine();

                MainTitle();
                CustomerMenu(baseUsers, catalog, customer, orderHistory);
            }
            else
            {
                FormatTitles("***USUÁRIO AINDA NÃO FINALIZOU UMA COMPRA!!! Para finalizar um pedido, favor entrar na opção Ver Pedido no Menu principal!***");
                Thread.Sleep(3000);

                MainTitle();
                CustomerMenu(baseUsers, catalog, customer, orderHistory);
            }
        }

        public static void OrdersInProgress(BaseUsers baseUsers, Catalog catalog, Customer customer, OrderHistory orderHistory)
        {
            StatusOrder status = new StatusOrder();
            status = Enum.Parse<StatusOrder>("Processing");
            Console.WriteLine("=====================================================");
            if (orderHistory.orders.FirstOrDefault(x => x.Customer.UserId == customer.UserId) != null &&
                orderHistory.orders.FirstOrDefault(x => x.Status == status) != null)
            {

                foreach (Order order in orderHistory.orders)
                {


                    if (order.Status == status && order.Customer.UserId == customer.UserId)
                    {
                        double valorTotalPedido = 0.00;
                        Console.Write("Id Pedido: " + order.OrderId + "\n");
                        Console.Write("Data do pedido: " + order.Moment + "\n");
                        Console.Write("Status: " + order.Status + "\n");
                        Console.Write("Cliente: " + order.Customer.Name + "\n");
                        FormatTitles("Produtos Adquiridos neste pedido: ");
                        foreach (Product product in order.Products)
                        {
                            Console.Write("Produto: " + product.Name + ", R$ " + product.Price.ToString("F2", CultureInfo.InvariantCulture) + "\n"); ;
                            valorTotalPedido += product.Price;
                        }
                        Console.WriteLine();

                        FormatTitles("*** VALOR TOTAL PEDIDO: R$ " + valorTotalPedido.ToString("F2", CultureInfo.InvariantCulture) + "***");

                        Console.WriteLine("=====================================================");

                    }

                }
            }
            else
            {
                FormatTitles("***USUÁRIO AINDA NÃO POSSUI PEDIDO!!! Para realizar um pedido, favor entrar no catalogo e selecionar um produto!***");
                Thread.Sleep(3000);

                MainTitle();
                CustomerMenu(baseUsers, catalog, customer, orderHistory);
            }
        }

        public static void Reports(BaseUsers baseUsers, Catalog catalog, Administrator administrator, OrderHistory orderHistory)
        {
            Console.WriteLine("1 - Total de Vendas por Clientes\n2 - Total Vendas Por Produto\n3 - Total de Pedidos não finalizados por cliente");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            int op = int.Parse(Console.ReadLine()!);
            Console.WriteLine();

            double totalVendido = 0.00;

            if (op == 1)
            {
                MainTitle();
                Console.WriteLine();
                StatusOrder status = Enum.Parse<StatusOrder>("Delivered");
                FormatTitles("***RELATÓRIO DE VENDAS POR CLIENTE***");
                Console.WriteLine();
                Console.WriteLine("=====================================================");
                foreach (User user in baseUsers.Users)
                {

                    foreach (Order order in orderHistory.orders)
                    {
                        if (order.Customer!.UserId == user.UserId && order.Status == status)
                        {
                            foreach (Product product in order.Products)
                            {
                                totalVendido += product.Price;
                            }
                            if (totalVendido != 0)
                            {
                                Console.WriteLine("Nome do cliente: " + user.Name + "\nTotal Vendido: R$ " + totalVendido.ToString("F2", CultureInfo.InvariantCulture));
                                Console.WriteLine("=====================================================");
                            }
                        }
                        totalVendido = 0;
                    }

                }
                Console.WriteLine();
                Console.WriteLine("Aperte qualquer tecla para voltar");
                Console.ReadLine();
                MainTitle();
                AdministratorMenu(baseUsers, catalog, administrator, orderHistory);
            }
            else if (op == 2)
            {
                MainTitle();
                Console.WriteLine();
                FormatTitles("***RELATÓRIO DE VENDAS POR PRODUTO***");
                StatusOrder status = Enum.Parse<StatusOrder>("Delivered");
                int totalPerProduct = 0;
                double valueTotalSaledPerProduct = 0.00;
             
                Console.WriteLine("=====================================================");
                foreach (Product product in catalog.products)
                {
                    foreach (Order order in orderHistory.orders)
                    {
                        foreach (Product auxProduct in order.Products)
                        {
                            if (auxProduct.ProductId == product.ProductId && order.Status == status)
                            {
                                totalPerProduct += product.Quantity;
                                valueTotalSaledPerProduct = product.Price * totalPerProduct;
                            }
                        }
                    }
                    if (totalPerProduct != 0)
                    {
                        Console.WriteLine("Nome do Produto: "
                                            + product.Name
                                            + "\nQuantidade Vendido: "
                                            + totalPerProduct + "\nValor Total Vendido: R$ "
                                            + valueTotalSaledPerProduct.ToString("F2", CultureInfo.InvariantCulture));
                        Console.WriteLine("=====================================================");
                        totalPerProduct = 0;
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Digite qualquer tecla para voltar ao menu principal: ");
                Console.ReadLine();
                MainTitle();
                AdministratorMenu(baseUsers, catalog, administrator, orderHistory);

            }
            else if (op == 3)
            {
              
                MainTitle();
                Console.WriteLine();
                FormatTitles("***RELATÓRIO DE PEDIDOS NÃO FINALIZADOS POR CLIENTE***");
                StatusOrder status = Enum.Parse<StatusOrder>("Delivered");
                int totalUnfinishedOrders = 0;
                Console.WriteLine("=====================================================");
                foreach (User user in baseUsers.Users)
                {
                    foreach (Order order in orderHistory.orders)
                    {
                        if (order.Customer!.UserId == user.UserId && order.Status != status)
                        {
                            totalUnfinishedOrders++;
                        }
                    }
                    if (totalUnfinishedOrders != 0)
                    {
                        Console.WriteLine("Nome do cliente:  "
                                        + user.Name
                                        + "\nQuantidade de Pedidos não finalizados: "
                                        + totalUnfinishedOrders);
                        Console.WriteLine("=====================================================");
                    }
                    totalUnfinishedOrders = 0;
                }
                Console.WriteLine();
                Console.WriteLine("Digite qualquer tecla para voltar ao menu principal: ");
                Console.ReadLine();
                MainTitle();
                AdministratorMenu(baseUsers, catalog, administrator, orderHistory);
            }
        }
    }
}




