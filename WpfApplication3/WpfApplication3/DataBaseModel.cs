namespace WpfApplication3
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class DataBaseModel : DbContext
    {
        // Контекст настроен для использования строки подключения "DataBaseModel" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "WpfApplication3.DataBaseModel" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "DataBaseModel" 
        // в файле конфигурации приложения.
        public DataBaseModel()
            : base("name=DataBaseModel")
        {

        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    public class Order
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }

        // Ссылка на покупателя
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        // Ссылка на заказы
        public virtual List<Order> Orders { get; set; }

        
    }

}