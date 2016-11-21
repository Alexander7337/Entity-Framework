namespace BookShopSystem.ConsoleClient
{
    using System;
    using System.Linq;
    using BookShopSystem.Data;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Collections.Generic;
    using BookShopSystem.Data.Migrations;
    using EntityFramework.Extensions;
    using Models;
    public class EntryPoint
    {
        public static void Main()
        {
            //Queries to Database

            BookShopContext context = new BookShopContext();

            #region Task 1
            //var titles = context.Books
            //    .Where(y => y.ReleaseDate.Value.Year > 2000)
            //    .Select(t => t)
            //    .ToList();

            //foreach (var title in titles)
            //{
            //    Console.WriteLine(title.Title + ' ' + title.ReleaseDate);
            //}
            #endregion

            #region Task 2
            //var authors = context.Authors
            //    .Where(b => b.Books.Any(rd => rd.ReleaseDate.Value.Year == 1990))
            //    .ToList();

            //foreach (var author in authors)
            //{
            //    Console.WriteLine(author.FirstName + ' ' + author.LastName);
            //}
            #endregion

            #region Task 3
            //var list = context.Authors
            //    .OrderByDescending(c => c.Books.Count)
            //    .ToArray();

            //Array.ForEach(list, e => Console.WriteLine(e.FirstName + ' ' + e.LastName + ' ' + e.Books.Count));
            #endregion

            #region Task 4
            //var books = context.Books
            //                .Where(a => a.Author.LastName == "Powell")
            //                .OrderByDescending(d => d.ReleaseDate)
            //                .ThenBy(t => t.Title)
            //                .ToArray();

            //Array.ForEach(books, book => Console.WriteLine($"{book.Title} {book.ReleaseDate.Value.ToString("dd/MM/yyyy")} {book.Copies}"));
            #endregion

            #region Task 5
            //var catBooks = context.Categories
            //    .OrderByDescending(c => c.Books.Count)
            //    .Select(b => 
            //    new
            //    {
            //        b.Name,
            //        BooksCount = b.Books.Count,
            //        Books = b.Books.OrderByDescending(d => d.ReleaseDate.Value.Year).ThenBy(n => n.Title).Take(3)
            //    });

            //foreach (var category in catBooks)
            //{
            //    Console.WriteLine($"--{category.Name}: {category.BooksCount} books");
            //    foreach (var book in category.Books)
            //    {
            //        Console.WriteLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
            //    }
            //}
            #endregion

            #region Task - Related Books

            //var takeThreeBooks = context.Books.Take(3).ToList();
            ////takeThreeBooks[0].RelatedBooks.Add(takeThreeBooks[1]);
            ////takeThreeBooks[1].RelatedBooks.Add(takeThreeBooks[0]);
            ////takeThreeBooks[0].RelatedBooks.Add(takeThreeBooks[2]);
            ////takeThreeBooks[2].RelatedBooks.Add(takeThreeBooks[0]);

            ////context.SaveChanges();

            //foreach (var book in takeThreeBooks)
            //{
            //    Console.WriteLine("--{0}", book.Title);
            //    foreach (var relatedBook in book.RelatedBooks)
            //    {
            //        Console.WriteLine(relatedBook.Title);
            //    }
            //}

            #endregion

            #region Delete/Update methods - EF Extended
            //context.Books.Where(b => b.BookId == 2).Delete();
            //context.Books.Where(b => b.BookId == 2).Update(b => new Book {Title = "New Title"});
            #endregion

            #region 1.Books Titles by Age Restriction (User's input)
            //string input = Console.ReadLine();

            //string[] titles = context.Books
            //    .Where(e => e.AgeRestriction.ToString().ToLower() == input.ToLower())
            //    .Select(e => e.Title)
            //    .ToArray();

            //Array.ForEach(titles, t => Console.WriteLine(t));
            #endregion

            #region 2.Golden Books
            //string[] titles = context.Books
            //    .Where(e => e.EditionType == EditionType.Gold
            //                && e.Copies < 5000)
            //    .Select(e => e.Title)
            //    .ToArray();

            //Array.ForEach(titles, t => Console.WriteLine(t));
            #endregion

            #region 3.Books by Price
            //var books = context.Books
            //    .Where(e => e.Price < 5 || e.Price > 40)
            //    .Select(e => new
            //    {
            //        e.Title,
            //        e.Price
            //    })
            //    .ToArray();

            //Array.ForEach(books, book => Console.WriteLine(book.Title + " - " + book.Price));
            #endregion

            #region 4.Not Released Books
            //int year = int.Parse(Console.ReadLine());

            //string[] titles = context.Books
            //    .Where(e => e.ReleaseDate.Value.Year != year)
            //    .Select(e => e.Title)
            //    .ToArray();

            //Array.ForEach(titles, t => Console.WriteLine(t));
            #endregion

            #region 5.Book Titles by Category
            //string[] categories = Console.ReadLine()
            //    .ToLower()
            //    .Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            //var booksCategorized = context.Categories
            //    .Where(e => categories.Contains(e.Name.ToLower()))
            //    .Select(e => e.Books)
            //    .ToArray();

            //foreach (var element in booksCategorized)
            //{
            //    foreach (var book in element)
            //    {
            //        Console.WriteLine(book.Title);
            //        foreach (var category in book.Categories)
            //        {
            //            Console.WriteLine($"--{category.Name}");
            //        }
            //    }
            //}
            #endregion

            #region 6.Books Released Before Date
            //DateTime date = DateTime.Parse(Console.ReadLine());

            //var books = context.Books
            //    .Where(e => e.ReleaseDate.Value.CompareTo(date) == -1)
            //    .Select(e => new
            //    {
            //        e.Title,
            //        e.EditionType,
            //        e.Price
            //    })
            //    .ToArray();

            //Array.ForEach(books, book => Console.WriteLine(book.Title));
            #endregion

            #region 7.Book Titles Search
            //string input = Console.ReadLine();

            //var books = context.Books
            //    .Where(e => e.Author.LastName.StartsWith(input))
            //    .Select(e => new
            //    {
            //        e.Title,
            //        e.Author.FirstName,
            //        e.Author.LastName
            //    })
            //    .ToArray();

            //Array.ForEach(books, book => Console.WriteLine($"{book.Title} ({book.FirstName} {book.LastName})"));
            #endregion

            #region 8.Total Book Copies
            //var stats = context.Books
            //    .GroupBy(e => e.Author)
            //    .OrderByDescending(e => e.Select(n => n.Copies).Sum())
            //    .Select(e => new
            //    {
            //        Author = e.Key,
            //        Copies = e.Select(n => n.Copies).Sum()
            //    });

            //foreach (var author in stats)
            //{
            //    Console.WriteLine($"{author.Author.FirstName} {author.Author.LastName} - {author.Copies}");
            //}
            #endregion

            #region 9.Find Profit
            //var booksByCategories = context.Categories
            //    .Select(e => new
            //    {
            //        e.Name,
            //        e.Books
            //    });

            //foreach (var booksByCategory in booksByCategories)
            //{
            //    decimal moneyPerCategory = 0m;
            //    foreach (var book in booksByCategory.Books)
            //    {
            //        moneyPerCategory += (book.Price*book.Copies);
            //    }

            //    Console.WriteLine($"{booksByCategory.Name} - ${moneyPerCategory}");
            //}
            #endregion

            #region 10.Most Recent Books
            //var top3books = context.Categories
            //    .OrderByDescending(e => e.Books.Count)
            //    .Select(e => new
            //    {
            //        e.Name,
            //        Number = e.Books.Count,
            //        Books = e.Books
            //        .OrderByDescending(b => b.ReleaseDate.Value.Year)
            //        .ThenBy(t => t.Title)
            //        .Take(3)
            //        .Select(b => new
            //        {
            //            b.Title,
            //            b.ReleaseDate
            //        })
            //    });

            //foreach (var category in top3books)
            //{
            //    Console.WriteLine($"--{category.Name}: {category.Number} books");
            //    foreach (var book in category.Books)
            //    {
            //        Console.WriteLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
            //    }
            //}
            #endregion
        }
    }
}
