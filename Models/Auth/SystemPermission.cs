namespace BookManagement.Models.Auth
{
    using System.Collections.Generic;
    public static class SystemPermission
    {
        public readonly static List<string> DefaultClaims = new()
        {
        CreateAuthor,
        UpdateAuthor,
        DeleteAuthor,
        ReadAuthor,
        CreateBook,
        UpdateBook,
        DeleteBook,
        ReadBook,
        CreateCatalog,
        UpdateCatalog,
        DeleteCatalog,
        ReadCatalog,
        AdminAccess,
        };


        //Permission
        public const string CreateAuthor = "Author.Create";
        public const string UpdateAuthor = "Author.Update";
        public const string DeleteAuthor = "Author.Delete";
        public const string ReadAuthor = "Author.Read";
        
        public const string CreateBook = "Book.Create";
        public const string UpdateBook = "Book.Update";
        public const string DeleteBook = "Book.Delete";
        public const string ReadBook = "Book.Read";
        
        public const string CreateCatalog = "Catalog.Create";
        public const string UpdateCatalog = "Catalog.Update";
        public const string DeleteCatalog = "Catalog.Delete";
        public const string ReadCatalog = "Catalog.Read";

        public const string AdminAccess = "Access.Admin";
    }
}