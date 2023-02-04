using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderImport
{
    class Orders
    {
        private List<Order> orders;
        public Orders()
        {
            orders = new List<Order>();
        }

        public List<string> ParseFile(string FileLocation)
        {
            List<string> result = new List<string>();
            int counter = 0;
            int lineCounter = 0;
            string CurrentOrder = string.Empty;
            Order newOrder = null;
            int linesWithErrors = 0;

            int totalLines = System.IO.File.ReadLines(FileLocation).Count();

            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(FileLocation))
            {
                lineCounter++;
                if (line.Length <= 3)
                    continue;

                var LineType = line.Substring(0, 3);
                try
                {
                    switch (LineType)
                    {
                        case "100":

                            if (newOrder != null)
                            {
                                orders.Add(newOrder);
                            }

                            newOrder = new Order();
                           // Console.WriteLine("Header");
                            CurrentOrder = line.Substring(3, 10);
                            //Create Header

                            newOrder.OrderNumber = decimal.Parse(line.Substring(3, 10));
                            newOrder.TotalItems = int.Parse(line.Substring(13, 5));
                            newOrder.TotalCost = decimal.Parse(line.Substring(18, 10));
                            newOrder.OrderDate = DateTime.Parse(line.Substring(28, 19));
                            newOrder.CustomerName = line.Substring(47, 50);
                            newOrder.CustomerPhone = line.Substring(97, 30);
                            newOrder.CustomerEmail = line.Substring(127, 50);
                            newOrder.Paid = int.Parse(line.Substring(177, 1)) != 0;
                            newOrder.Shipped = int.Parse(line.Substring(178, 1)) != 0;
                            newOrder.Completed = int.Parse(line.Substring(179, 1)) != 0;
                            newOrder.ImportedCorreclty = true;

                            counter++;

                            break;
                        case "200":
                            //Console.WriteLine("Address");
                            //Create Address Line 
                            if (newOrder != null)
                            {
                                newOrder.Address.AddressLine1 = line.Substring(3, 50);
                                newOrder.Address.AddressLine2 = line.Substring(53, 50);
                                newOrder.Address.City = line.Substring(103, 50);
                                newOrder.Address.State = line.Substring(153, 2);
                                newOrder.Address.Zip = line.Substring(155); 
                            }

                            break;
                        case "300":
                            //Console.WriteLine("OrderDetails");
                            //Create Order Details Line
                            OrderDetails newOD = new OrderDetails
                            {
                                LineNumber = int.Parse(line.Substring(3, 2)),
                                Quantity = decimal.Parse(line.Substring(5, 5)),
                                CostEach = decimal.Parse(line.Substring(10, 10)),
                                TotalCost = decimal.Parse(line.Substring(20, 10)),
                                Description = line.Substring(30, 50)
                            };
                            if (newOrder != null)
                            {
                                newOrder.OrderDetails.Add(newOD);
                            }
                            break;
                        default:
                            break;
                    }                     

                    if (newOrder != null & lineCounter == totalLines )
                    {
                        orders.Add(newOrder);
                        counter++;
                    }


                }
                catch (Exception ex)
                {
                    string error = string.Format("Error: Order Number {0} {1} ", CurrentOrder.Trim(), ex.Message);

                    result.Add(error);

                    newOrder.ImportedCorreclty = false;
                    newOrder.ErrorMessages += error.ToString();
                    linesWithErrors++;
                }

            }

            result.Add("Imported Orders: " + counter.ToString());
            result.Add("Orders with Errors: " + linesWithErrors.ToString());


            return result;

        }
    }
    class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
            Address = new Address();
        }
        public decimal OrderNumber { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public bool Paid { get; set; }
        public bool Shipped { get; set; }
        public bool Completed { get; set; }
        public Address Address { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public bool ImportedCorreclty { get; set; }
        public string ErrorMessages { get; set; }
    }

    class OrderDetails
    {
       
        public int LineNumber { get; set; }
        public decimal Quantity { get; set; }
        public decimal CostEach { get; set; }
        public decimal TotalCost { get; set; }
        public string Description { get; set; } 

    }

    class Address
    {  
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; } 
    }


}
