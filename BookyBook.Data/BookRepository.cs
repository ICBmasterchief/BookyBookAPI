﻿using BookyBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Runtime.InteropServices;

namespace BookyBook.Data;
public class BookRepository : IBookRepository
{
    public List<Book>? BooksList = new();
    private readonly string BookJsonPath;
    public BookRepository()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            BookJsonPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "BookyBook.Data", "Data.Books.json");
        } else {
            BookJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "BookyBook.Data", "Data.Books.json");
        }
        GetRegisteredBooks();
    }
    public void AddBook(Book book)
    {
        BooksList.Add(book);
        SaveBookData();
    }
    public void GetRegisteredBooks()
    {
        try
        {
        string JsonBooks = File.ReadAllText(BookJsonPath);
        BooksList =  JsonSerializer.Deserialize<List<Book>>(JsonBooks);
        } catch (System.Exception)
        {
            Console.WriteLine("ERROR TRYING ACCESS DATA");
        }
    }
    public void SaveBookData()
    {
        string JsonBooks = JsonSerializer.Serialize(BooksList, new JsonSerializerOptions {WriteIndented = true});
        File.WriteAllText(BookJsonPath, JsonBooks);
    }
}
