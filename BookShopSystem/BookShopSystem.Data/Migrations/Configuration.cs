namespace BookShopSystem.Data.Migrations
{
    using System;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.IO;
    using System.Text;
    using System.Globalization;

    public sealed class Configuration : DbMigrationsConfiguration<BookShopSystem.Data.BookShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "BookShopSystem.Data.BookShopContext";
        }

        protected override void Seed(BookShopSystem.Data.BookShopContext context)
        {
            string path = @"..\..\Resources\authors.txt";

            using (var reader = new StreamReader(path, Encoding.UTF8))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(' ');
                    Author author = new Author()
                    {
                        FirstName = line[0],
                        LastName = line[1]
                    };
                    context.Authors.AddOrUpdate(a => new { a.FirstName, a.LastName }, author);
                }
                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                path = @"..\..\Resources\books.txt";
                var random = new Random();

                using (var reader = new StreamReader(path, Encoding.UTF8))
                {
                    var line = reader.ReadLine();
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        var data = line.Split(new[] { ' ' }, 6);
                        var edition = (EditionType)int.Parse(data[0]);
                        var releaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                        var copies = int.Parse(data[2]);
                        var price = decimal.Parse(data[3]);
                        var ageRestriction = (AgeRestriction)int.Parse(data[4]);
                        var title = data[5];
                        var authorId = random.Next(1, context.Authors.Count() + 1);
                        var author = context.Authors.Find(authorId);

                        context.Books.Add(new Book
                        {
                            AuthorId = authorId,
                            Author = author,
                            EditionType = edition,
                            ReleaseDate = releaseDate,
                            Copies = copies,
                            Price = price,
                            Title = title,
                            AgeRestriction = ageRestriction
                        });

                        line = reader.ReadLine();
                    }
                    context.SaveChanges();
                }
            }

            path = @"..\..\Resources\categories.txt";

            using (var reader = new StreamReader(path, Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                    var input = reader.ReadLine();
                    input = input.Trim();
                    context.Categories.AddOrUpdate(n => n.Name, new Category { Name = input });
                }
                context.SaveChanges();
            }

            //var randomBookId = new Random();
            //var randomCatId = new Random();
            //for (int i = 1; i <= 200; i++)
            //{
            //    var bookId = randomBookId.Next(1, 196);
            //    var categoryId = randomCatId.Next(1, 9);

            //    var book = context.Books.Find(bookId);
            //    var category = context.Categories.FirstOrDefault(id => id.CategoryId == categoryId);

            //    category.Books.Add(book);
            //    context.SaveChanges();
            //}
            
        }
    }
}
