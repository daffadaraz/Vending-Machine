/* Author : Daffa Daraz Aslam
 * Menggunakan .NET 4.7.2
 * 
 * 1. Terdapat 1 buah mesin vending machine
 * 2. Pada mesin tersebut tersedia 5 buah jenis makanan: Biskuit, Chips, Oreo, Tango, Cokelat
 * 3. Price list untuk tiap item sbb:
 *  Biskuit: 6000
 *  Chips: 8000
 *  Oreo: 10000
 *  Tango: 12000
 *  Cokelat: 15000
 * 4. Vending machine dapat menerima uang dengan pecahan : 2000, 5000, 10000, 20000, 50000.
 * 5. Vending Machine dapat melakukan pembelian, pengembalian uang dan mendeteksi jika ada makanan yang stok-nya sedang habis.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Item
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Item(string productName, decimal price, int stock)
        {
            this.ProductName = productName;
            this.Price = price;
            this.Stock = stock;
        }

        public void RemoveItem()
        {
            if (this.Stock > 0)
            {
                this.Stock--;
            }
        }
    }

    public class Money
    {
        public decimal Balance { get; set; }

        public Money(decimal balance)
        {
            this.Balance = balance;
        }

        public bool AddBalance(string amount)
        {
            decimal amountInserted = 0;
            switch (amount)
            {
                case "1":
                    amountInserted = 2000;
                    break;
                case "2":
                    amountInserted = 5000;
                    break;
                case "3":
                    amountInserted = 10000;
                    break;
                case "4":
                    amountInserted = 20000;
                    break;
                case "5":
                    amountInserted = 50000;
                    break;
                default:
                    amountInserted = 0;
                    return false;
            }

            this.Balance += amountInserted;
            return true;
        }

        public bool RemoveBalance(decimal amount)
        {
            if(this.Balance < amount)
            {
                return false;
            }

            this.Balance -= amount;
            return true;
        }

        //KEMBALIAN DALAM BENTUK PECAHAN
        public string ChangeMessage(
            int serb,
            int duarb,
            int limarb,
            int sepuluhrb,
            int duapuluhrb,
            int limapuluhrb)
        {
            string msg = "Kembalian yang diberikan dalam bentuk pecahan : \n";

            if(limapuluhrb > 0)
            {
                msg += $"{limapuluhrb}x {"- Rp.50000",2}\n";
            }
            if(duapuluhrb > 0)
            {
                msg += $"{duapuluhrb}x {"- Rp.20000",2}\n";
            }
            if (sepuluhrb > 0)
            {
                msg += $"{sepuluhrb}x {"- Rp.10000",2}\n";
            }
            if (limarb > 0)
            {
                msg += $"{limarb}x {"- Rp.5000",2}\n";
            }
            if (duarb > 0)
            {
                msg += $"{duarb}x {"- Rp.2000",2}\n";
            }
            if (serb > 0)
            {
                msg += $"{serb}x {"- Rp.1000",2}\n";
            }
            return msg;
        }

        public string GiveChange()
        {
            int serb = 0;
            int duarb = 0;
            int limarb = 0;
            int sepuluhrb = 0;
            int duapuluhrb = 0;
            int limapuluhrb = 0;

            string result;
            if (this.Balance > 0)
            {
                while (this.Balance > 0)
                {
                    if (this.Balance >= 50000)
                    {
                        limapuluhrb++;
                        this.RemoveBalance(50000);
                    }
                    else if (this.Balance >= 20000)
                    {
                        duapuluhrb++;
                        this.RemoveBalance(20000);
                    }
                    else if (this.Balance >= 10000)
                    {
                        sepuluhrb++;
                        this.RemoveBalance(10000);
                    }
                    else if (this.Balance >= 5000)
                    {
                        limarb++;
                        this.RemoveBalance(5000);
                    }
                    else if (this.Balance >= 2000)
                    {
                        duarb++;
                        this.RemoveBalance(2000);
                    }
                    else if (this.Balance >= 1000)
                    {
                        serb++;
                        this.RemoveBalance(1000);
                    }
                }
                result = ChangeMessage(serb, duarb, limarb, sepuluhrb, duapuluhrb, limapuluhrb);
            }
            else
            {
                result = "Tidak ada kembalian.";
            }

            return result;
        }
    }

    class Program
    {
        public static Dictionary<string, Item> Items = new Dictionary<string, Item>
            {
                { "1", new Item("Biskuit", 6000, 10) },
                { "2", new Item("Chips", 8000, 5) },
                { "3", new Item("Oreo", 10000, 7) },
                { "4", new Item("Tango", 12000, 3) },
                { "5", new Item("Cokelat", 15000, 0) }
            };
        public static Money Money = new Money(0);

        public static void PressKeyToContinue()
        {
            Console.Write("\n--\nTekan Tombol apa saja untuk melanjutkan");
            Console.ReadKey();
        }

        public static void DisplayItems(Dictionary<string, Item> Items)
        {
            Console.WriteLine($"\n{"#",-5} {"Stock",-5} { "Nama",-15} {"Harga"}");
            
            foreach(KeyValuePair<string, Item> i in Items)
            {
                if (i.Value.Stock > 0)
                {
                    string itemNumber = i.Key.PadRight(5);
                    string stock = i.Value.Stock.ToString().PadRight(5);
                    string productName = i.Value.ProductName.PadRight(15);
                    string price = i.Value.Price.ToString();
                    Console.WriteLine($"{itemNumber} {stock} {productName} Rp. {price}");
                }
                else
                {
                    string itemNumber = i.Key.PadRight(5);
                    string stock = i.Value.Stock.ToString().PadRight(5);
                    string productName = i.Value.ProductName.PadRight(15);
                    Console.WriteLine($"{itemNumber} {stock} {productName} HABIS!");
                }
            }
        }

        public static bool ItemExists(string itemNumber)
        {
            return Items.ContainsKey(itemNumber);
        }

        public static bool GetItem(string itemNumber)
        {
            if (ItemExists(itemNumber) &&
                Money.Balance >= Items[itemNumber].Price &&
                Items[itemNumber].Stock > 0)
            {
                Money.RemoveBalance(Items[itemNumber].Price);
                Items[itemNumber].RemoveItem();
                return true;
            }

            return false;
        }

        public static void BuyMenu(Dictionary<string, Item> Items)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Selamat Datang di Vending Machine Dapa! \n");
                Console.WriteLine(
                    "Pembelian\n" +
                    "1) Masukkan Uang\n" +
                    "2) Pilih Produk\n" +
                    "3) Selesai Membeli\n" +
                    "K) Kembali ke Menu Utama\n");
                Console.WriteLine($"Uang dalam Vending Machine: Rp.{Money.Balance}");
                Console.Write("Apa yang ingin anda lakukan? ");
                string input = Console.ReadLine();

                //MEMASUKKAN UANG
                if (input == "1")
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"Uang dalam Vending Machine: Rp.{Money.Balance}\n");
                        Console.WriteLine("Kami hanya menerima pecahan 2000, 5000, 10000, 20000, 50000");
                        Console.WriteLine(
                        "1) 2000\n" +
                        "2) 5000\n" +
                        "3) 10000\n" +
                        "4) 20000\n" +
                        "5) 50000\n" +
                        "K) Selesai");
                        Console.Write("Masukkan angka yang valid [1..5] : ");
                        string InputUang = Console.ReadLine();

                        if (Money.AddBalance(InputUang))
                        {
                            Console.WriteLine("Uang berhasil dimasukkan.");
                        }
                        else if (InputUang.ToUpper() == "K")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nSalah input! coba lagi");
                            PressKeyToContinue();
                        }
                    }
                }

                //MEMILIH PRODUK
                else if (input == "2")
                {
                    while (true)
                    {
                        Console.Clear();
                        DisplayItems(Items);
                        Console.WriteLine($"{"K",-5} Kembali ke menu pembelian");
                        Console.WriteLine($"\nUang dalam Vending Machine: Rp.{Money.Balance}");
                        Console.Write("Masukkan produk pilihan [1..5] : ");
                        string ProductChoice = Console.ReadLine();

                        if (GetItem(ProductChoice))
                        {
                            Console.WriteLine($"\nSelamat Menikmati {Items[ProductChoice].ProductName} Anda!");
                            Console.Write("\nIngin membeli lagi (Y)? ");
                            string choice = Console.ReadLine();
                            if (choice.ToUpper() != "Y")
                            {
                                break;
                            }
                        }
                        else if (ProductChoice.ToUpper() == "K")
                        {
                            break;
                        }
                        else if (!ItemExists(ProductChoice))
                        {
                            Console.WriteLine("\nSalah input! coba lagi.");
                            PressKeyToContinue();

                        }
                        else if (ItemExists(ProductChoice) && Items[ProductChoice].Stock == 0)
                        {
                            Console.WriteLine("\nPRODUK KOSONG.");
                            PressKeyToContinue();
                        }
                        else if (ItemExists(ProductChoice) && Items[ProductChoice].Price > Money.Balance)
                        {
                            Console.WriteLine("\nUang mu tidak cukup!.");
                            PressKeyToContinue();
                        }

                    }
                }

                //MEMBERI KEMBALIAN
                else if (input == "3" || (input.ToUpper() == "K" && Money.Balance > 0))
                {
                    Console.WriteLine("\nTransaksi Selesai");
                    Console.WriteLine($"Total : Rp.{Money.Balance}\n");
                    Console.WriteLine(Money.GiveChange());
                    return;
                }
                else if (input.ToUpper() == "K")
                {
                    Console.WriteLine("\nKembali ke menu utama..");
                    return;
                }
                else
                {
                    Console.WriteLine("\nSalah input! coba lagi");
                }

                Console.Clear();
            }
        }

        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Selamat Datang di Vending Machine Dapa! \n");
                Console.WriteLine(
                    "Menu Utama\n" +
                    "1) Tampilkan Produk Vending Machine\n" +
                    "2) Beli\n" +
                    "K) Keluar\n");
                Console.Write("Apa yang ingin anda lakukan? ");
                string input = Console.ReadLine();

                //MENAMPILKAN PRODUK
                if(input == "1")
                {
                    Console.WriteLine("\nMenampilkan Produk");
                    DisplayItems(Items);
                }
                //KE MENU PEMBELIAN
                else if(input == "2")
                {
                    BuyMenu(Items);
                }
                else if(input.ToUpper() == "K")
                {
                    Console.Clear();
                    Console.WriteLine("\nTerimakasih telah berbelanja di Vending Machine Dapa!");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("\nSalah input! coba lagi");
                }

                PressKeyToContinue();
                Console.Clear();
            }
        }
    }

}
