using System;
using System.Collections.Generic;

namespace OnlineOrdering
{
    // Address class
    public class Address
    {
        private string street;
        private string city;
        private string state;
        private string country;

        public Address(string street, string city, string state, string country)
        {
            this.street = street;
            this.city = city;
            this.state = state;
            this.country = country;
        }

        public bool IsInUSA()
        {
            return country.Equals("USA", StringComparison.OrdinalIgnoreCase);
        }

        public string GetFullAddress()
        {
            return $"{street}\n{city}, {state}\n{country}";
        }
    }

    // Customer class
    public class Customer
    {
        private string name;
        private Address address;

        public Customer(string name, Address address)
        {
            this.name = name;
            this.address = address;
        }

        public bool LivesInUSA()
        {
            return address.IsInUSA();
        }

        public string GetName()
        {
            return name;
        }

        public string GetAddress()
        {
            return address.GetFullAddress();
        }
    }

    // Product class
    public class Product
    {
        private string name;
        private string productId;
        private decimal price;
        private int quantity;

        public Product(string name, string productId, decimal price, int quantity)
        {
            this.name = name;
            this.productId = productId;
            this.price = price;
            this.quantity = quantity;
        }

        public decimal GetTotalCost()
        {
            return price * quantity;
        }

        public string GetPackingLabel()
        {
            return $"{name} (ID: {productId})";
        }
    }

    // Order class
    public class Order
    {
        private List<Product> products;
        private Customer customer;

        public Order(Customer customer)
        {
            this.customer = customer;
            products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public decimal GetTotalCost()
        {
            decimal productTotal = 0;
            foreach (var product in products)
            {
                productTotal += product.GetTotalCost();
            }

            decimal shippingCost = customer.LivesInUSA() ? 5 : 35;
            return productTotal + shippingCost;
        }

        public string GetPackingLabel()
        {
            var packingLabel = "Packing Label:\n";
            foreach (var product in products)
            {
                packingLabel += $"- {product.GetPackingLabel()}\n";
            }
            return packingLabel;
        }

        public string GetShippingLabel()
        {
            return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create addresses
            Address address1 = new Address("123 Main St", "Springfield", "IL", "USA");
            Address address2 = new Address("456 Maple Ave", "Toronto", "ON", "Canada");

            // Create customers
            Customer customer1 = new Customer("John Doe", address1);
            Customer customer2 = new Customer("Jane Smith", address2);

            // Create products
            Product product1 = new Product("Laptop", "P1001", 999.99m, 1);
            Product product2 = new Product("Mouse", "P1002", 25.50m, 2);
            Product product3 = new Product("Keyboard", "P1003", 49.99m, 1);
            Product product4 = new Product("Monitor", "P1004", 150.00m, 1);

            // Create orders
            Order order1 = new Order(customer1);
            order1.AddProduct(product1);
            order1.AddProduct(product2);

            Order order2 = new Order(customer2);
            order2.AddProduct(product3);
            order2.AddProduct(product4);

            // Display order details
            List<Order> orders = new List<Order> { order1, order2 };

            foreach (var order in orders)
            {
                Console.WriteLine(order.GetPackingLabel());
                Console.WriteLine(order.GetShippingLabel());
                Console.WriteLine($"Total Cost: ${order.GetTotalCost():F2}");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
