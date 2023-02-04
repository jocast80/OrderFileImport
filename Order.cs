using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFileImport
{
    class OrderImport
    {
        static void Main(string[] args)
        {

            ReadLines();


        }

        public static void ReadLines()
        {
            int counter = 0;

            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(@"c:\temp\OrderFile.txt"))
            {
                var LineType = line.Substring(0, 3);

                switch (LineType)
                {
                    case "100":
                        Console.WriteLine("Header");
                        //Create Header

                        break;
                    case "200":
                        Console.WriteLine("Address");
                        //Create Address Line

                        break;
                    case "300":
                        Console.WriteLine("OrderDetails");
                        //Create Order Details Line

                        break;
                    default:

                        break;


                }

                System.Console.WriteLine(line);
                counter++;
            }

            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.  
            System.Console.ReadLine();

        }
        private void ReadFile()
        {
            const string inputFilename = @"c:\temp\OrderFile.txt";
            DataTable dt = new DataTable();
            dt.TableName = "Orders";
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Num1", typeof(int));
            dt.Columns.Add("Num2", typeof(int));
            dt.Columns.Add("Num3", typeof(int));
            StreamReader reader = new StreamReader(inputFilename);
            string inputLine = "";
            while ((inputLine = reader.ReadLine()) != null)
            {
                if (inputLine.Trim().Length > 0)
                {
                    string[] inputArray = inputLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    dt.Rows.Add(new object[] {
                       inputArray[0],
                       int.Parse(inputArray[1]),
                       int.Parse(inputArray[2]),
                       int.Parse(inputArray[3])
                    });
                }
            }
            DataSet ds = new DataSet();
            ds.DataSetName = "MyDataSet";
            ds.Tables.Add(dt);
            // ds.WriteXml(outputFilename, XmlWriteMode.WriteSchema);

        }

    }
    class Order
    {
        public static int LineTypeIdentifier = 100;

        public decimal OrderNumber { get; set; }
        public int TotalItems { get; set; }

        public decimal TotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoine { get; set; }
        public string CustomerEmail { get; set; }
        public bool Paid { get; set; }
        public bool Shipped { get; set; }
        public bool Completed { get; set; }
    }
}
