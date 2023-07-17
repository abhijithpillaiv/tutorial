using System.Globalization;

class filecreator
{
    public filecreator(string message)
    {
        string filePath = "invoice.txt";

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(message);
            }

            Console.WriteLine("Data written to the file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred: " + ex.Message);
        }
    }
}
class card
{
    private int cardnumber { get; init; }
    private int cvv { get; init; }

    public card(int cardnumber, int cvv)
    {
        this.cardnumber = cardnumber;
        this.cvv = cvv;
    }
    public int[] getcard()
    {
        return new int[] { cardnumber,cvv };
    }
}
class customer
{
    public string id;
    public string name;
    public double discount { get; set; } = 0;
    public bool isvip{ get; set; }=false;
    
    List<string> cart = new List<string>();
    List<card> cards = new List<card>();

    public customer(string id, string name)
    {
        this.id = id;
        this.name = name;
    }
    public void changeprevilage()
    {
        isvip = !isvip;
    }
    public void addProduct(string product)
    {
        cart.Add(product);
    }
    public void PlaceOrder(List<products> product)
    {
        Console.WriteLine(".........Details of products in cart........\n");
        double totalPrice = 0;
        List<products> products = product.Where(p=>cart.Contains(p.id)).ToList();
        string invoice="";
        if (products.Count > 0)
        {
            for (int i = 0; i < products.Count; i++)
            {
                invoice += "\nNo :: " + i + "\nName :: " + products[i].name + "\nOriginal price :: " + products[i].price + "\nDiscount amount :: " + products[i].discount+"\nDiscount amount for user :: " + discount+ "\nFinal price :: " + (products[i].price - products[i].discount+ "\n********************\n");
                totalPrice += products[i].price - products[i].discount;
            }
            Console.WriteLine(invoice);
            Console.WriteLine("\nTotal payment of " + (totalPrice - discount) + " Rs");

            Console.WriteLine("\n\n Press 1 to make payment");
            string paymentconf = Console.ReadLine();
            if (paymentconf == "1")
            {
                if (cards.Count <= 0)
                {
                    Console.WriteLine("No card found add new card.");
                    Console.WriteLine("Enter card number.");
                    int num = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter CVV.");
                    int cvv = int.Parse(Console.ReadLine());

                    cards.Add(new card(num, cvv));
                }
                else
                {
                    Console.WriteLine("Available cards are");
                    for (int i = 0; i < cards.Count; i++)
                    {
                        Console.WriteLine("sl no :: " + i);
                        var tempcard = cards[i].getcard();
                        Console.WriteLine("Card NO :: " + tempcard[0]);
                    }
                    Console.WriteLine("Choose slno of card to make payment :: ");
                    string slno = Console.ReadLine();
                    var tempcards = cards[int.Parse(slno)].getcard();
                    Console.WriteLine("Payment initiated throung card :: " + tempcards[0]);
                }

                Console.WriteLine("Payment confirmed.  " +
                    "Cart cleared.");
                new filecreator(invoice);
                
                cart.Clear();

            }
            else
            {
                Console.WriteLine("Payment failed. ");
            }
        }
        else
        {
            Console.WriteLine("Noting to display.");
        }
    }
}
class products
{
    public string id;
    public string name;
    public double price;
    public double discount;
    public products(string id,string name,double price) 
    {
        this.id = id;
        this.name = name;
        this.price = price;
    }
    public void setdiscount(double discount)
    {
        this.discount = discount;
    }
}

class program
{
   
    public static void Main(string[] args)
    {
        List<products> products = new List<products> {
        new products("p1","prdt1",10),
        new products("p2", "prdt2", 20),
        new products("p3", "prdt3", 15),
};
        List<customer> customers = new List<customer>
    {
        new customer("c1", "name1"),
        new customer("c2", "name2"),
        new customer("c3", "name3"),
        new customer("c4", "name4"),
        new customer("c5", "name5"),

    };
        string opt;
        do
        {
            Console.WriteLine("\n\n 1. Add product\n 2. Place order\n 3. Add discount\n 4. make user vip\n" +
                " 5. Add discount for vip customer\n");
            opt = Console.ReadLine();
            if (opt=="1")
            {
                Console.WriteLine("Enter user id :: ");
                string userid = Console.ReadLine();
                Console.WriteLine("Enter product id :: ");
                string prdtid = Console.ReadLine();

                var userindex = customers.FindIndex(a=>a.id == userid);
                var product = products.Find(a => a.id == prdtid);

                if (product == null || userindex==-1) continue;
                customers[userindex].addProduct(product.id);
                Console.WriteLine("Done");

            }
            else if (opt == "2")
            {
                Console.WriteLine("Enter user id :: ");
                string userid = Console.ReadLine();

                var userindex = customers.FindIndex(a => a.id == userid);

                if (userindex == -1) continue;
                customers[userindex].PlaceOrder(products);
            }
            else if(opt == "3")
            {
                Console.WriteLine("Enter product id :: ");
                string prdtid = Console.ReadLine();

                Console.WriteLine("Enter discount amount :: ");
                double discount = double.Parse(Console.ReadLine());

                var product = products.FindIndex(a => a.id == prdtid);

                if (product == -1) continue;
                products[product].setdiscount(discount);
            }
            else if (opt == "4")
            {
                Console.WriteLine("Enter user id :: ");
                string userid = Console.ReadLine();

                var userindex = customers.FindIndex(a => a.id == userid);

                if (userindex == -1) continue;
                customers[userindex].changeprevilage();
            }
            else if (opt == "5")
            {
                 Console.WriteLine("Enter user id :: ");
                string userid = Console.ReadLine();

                var userindex = customers.FindIndex(a => a.id == userid);

                if (userindex == -1) continue;
                if (!customers[userindex].isvip)
                {
                    Console.WriteLine("\nNot vip.");
                    continue;
                }
                Console.WriteLine("Enter discount amount :: ");
                double discount = double.Parse(Console.ReadLine());
            }
        } while (true);
    }
}