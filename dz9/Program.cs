namespace dz9
{
    internal class Program
    {
        class Backpack
        {
            public string Color { get; set; }
            public string Brand { get; set; }
            public string Fabric { get; set; }
            public double Weight { get; set; }
            public double Volume { get; set; }
            public List<string> Contents { get; set; } = new List<string>();

            public Backpack(double volume)
            {
                Volume = volume;
            }

            public event Action<string> AddItem = delegate { };
            public event Action<string> RemoveItem = delegate { };
            public event Action<string, string> ChangeItem = delegate { };

            public void AddObject(string item)
            {
                double itemWeight = item.Length;
                if (itemWeight + Weight > Volume)
                {
                    throw new Exception("The item exceeds the backpack volume!");
                }

                AddItem(item);
                Contents.Add(item);
                Weight += itemWeight;
            }

            public void RemoveObject(string item)
            {
                RemoveItem(item);
                Contents.Remove(item);
                Weight -= item.Length;
            }

            public void ChangeObject(string oldItem, string newItem)
            {
                int index = Contents.IndexOf(oldItem);
                if (index != -1)
                {
                    ChangeItem(oldItem, newItem);
                    Contents[index] = newItem;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            // task 1
            Func<string, string> getRGB = (color) =>
            {
                switch (color.ToLower())
                {
                    case "red":
                        return "RGB(255, 0, 0)";
                    case "orange":
                        return "RGB(255, 165, 0)";
                    case "yellow":
                        return "RGB(255, 255, 0)";
                    case "green":
                        return "RGB(0, 255, 0)";
                    case "blue":
                        return "RGB(0, 0, 255)";
                    case "indigo":
                        return "RGB(75, 0, 130)";
                    case "violet":
                        return "RGB(238, 130, 238)";
                    default:
                        return "Invalid color";
                }
            };

            Console.WriteLine(getRGB("red"));
            Console.WriteLine(getRGB("green"));
            Console.WriteLine(getRGB("blue"));

            // task 2
            Backpack myBackpack = new Backpack(50);

            myBackpack.AddItem += (item) =>
            {
                Console.WriteLine($"Item '{item}' has been added to the backpack.");
            };

            myBackpack.RemoveItem += (item) =>
            {
                Console.WriteLine($"Item '{item}' has been removed from the backpack.");
            };

            myBackpack.ChangeItem += (oldItem, newItem) =>
            {
                Console.WriteLine($"Item '{oldItem}' has been changed to '{newItem}'.");
            };

            try
            {
                myBackpack.AddObject("Laptop");
                myBackpack.AddObject("Book");
                myBackpack.ChangeObject("Book", "Notebook");
                myBackpack.RemoveObject("Laptop");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // task 3
            int[] numbers = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

            Func<int, int> countMultiples = (divisor) =>
            {
                return numbers.Count(n => n % divisor == 0);
            };

            Console.WriteLine($"Numbers divisible by 7: {countMultiples(7)}");
            Console.WriteLine($"Numbers divisible by 5: {countMultiples(5)}");
            Console.WriteLine($"Numbers divisible by 10: {countMultiples(10)}");

            // task 4
            int[] numbers1 = { 1, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50 };

            Func<int, int, int> countInRange = (min, max) =>
            {
                return numbers1.Count(n => n >= min && n <= max);
            };

            Console.WriteLine($"Numbers in range 10-30: {countInRange(10, 30)}");
            Console.WriteLine($"Numbers in range 5-20: {countInRange(5, 20)}");
            Console.WriteLine($"Numbers in range 30-50: {countInRange(30, 50)}");

            // task 5
            Func<int[], IEnumerable<int>> getUniqueNegatives = arr =>
            arr.Where(x => x < 0).Distinct();

            int[] testArray1 = { -1, 2, -3, 5, 0, -2 };
            int[] testArray2 = { 1, 2, 3, 4, 5 };
            int[] testArray3 = { -5, -2, -1 };
            int[] testArray4 = { };
            int[] testArray5 = { -1, 0, -3, 2, -2, 2 };

            Console.WriteLine("Test 1: " + string.Join(", ", getUniqueNegatives(testArray1)));
            Console.WriteLine("Test 2: " + string.Join(", ", getUniqueNegatives(testArray2)));
            Console.WriteLine("Test 3: " + string.Join(", ", getUniqueNegatives(testArray3)));
            Console.WriteLine("Test 4: " + string.Join(", ", getUniqueNegatives(testArray4)));
            Console.WriteLine("Test 5: " + string.Join(", ", getUniqueNegatives(testArray5)));

            // task 6
            string text = "Hello world! The world is big and beautiful";
            string searchWord = "world";
            Func<string, string, int> countOccurrences = (t, w) =>
                t.Split(new char[] { ' ', '.', '!', '?', ',', ';', ':' }, StringSplitOptions.RemoveEmptyEntries)
                 .Count(word => word.Equals(w, StringComparison.OrdinalIgnoreCase));

            int occurrences = countOccurrences(text, searchWord);
            Console.WriteLine($"The word '{searchWord}' appears {occurrences} time(s) in the text.");

            // task 7
            Func<int, bool> validatePin = pin => pin == 1234;

            Action<decimal> displayBalance = balance => Console.WriteLine($"Your balance: {balance} UAH");

            Func<decimal, decimal, decimal> withdrawMoney = (balance, amount) =>
            {
                if (amount > balance)
                {
                    Console.WriteLine("Insufficient funds!");
                    return balance;
                }
                Console.WriteLine($"You withdrew {amount} UAH");
                return balance - amount;
            };

            Console.Write("Enter your PIN: ");
            int enteredPin = int.Parse(Console.ReadLine());

            if (!validatePin(enteredPin))
            {
                Console.WriteLine("Invalid PIN!");
                return;
            }

            decimal balance = 5000;
            displayBalance(balance);

            Console.Write("Enter withdrawal amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            balance = withdrawMoney(balance, amount);

            displayBalance(balance);
        }
    }
}
