namespace BookShopSystem.ConsoleClient
{
    using System;
    using System.Linq;
    using BookShopSystem.Data;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using BookShopSystem.Data.Migrations;
    using EntityFramework.Extensions;
    using Models;
    public class EntryPoint
    {
        public static void Main()
        {
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
        }
    }
}
