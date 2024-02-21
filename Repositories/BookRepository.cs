// Repositories/BookRepository.cs
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using BookSearchApp.Models;

namespace BookSearchApp.Repositories
{
    public class BookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string query)
        {
            query = "dummy";
            // Check if the query is for retrieving dummy data
            if (query == "dummy")
            {
                return GetDummyBooks();
            }
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string sql = @"
                SELECT *
                FROM Books
                WHERE Title LIKE @Query OR Author LIKE @Query OR Description LIKE @Query
                ORDER BY Title;
            ";

            return await connection.QueryAsync<Book>(sql, new { Query = $"%{query}%" });
        }
        // Method to return dummy data
        private IEnumerable<Book> GetDummyBooks()
        {
            return new List<Book>
            {
                new Book { Id = 1, Title = "Dummy Book 1", Author = "Dummy Author 1", Description = "This is a dummy book 1", PublicationDate = new DateTime(2022, 1, 1) },
                new Book { Id = 2, Title = "Dummy Book 2", Author = "Dummy Author 2", Description = "This is a dummy book 2", PublicationDate = new DateTime(2022, 2, 1) },
                new Book { Id = 3, Title = "Dummy Book 3", Author = "Dummy Author 3", Description = "This is a dummy book 3", PublicationDate = new DateTime(2022, 3, 1) }
            };
        }
    }
}
