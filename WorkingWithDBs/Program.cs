using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WorkingWithDBs
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                Console.WriteLine("Please type the name of the blog:");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };

                db.Blogs.Add(blog);
                db.SaveChanges();

                var query = from b in db.Blogs
                            orderby b.Name
                            select b;

                foreach (var item in query)
                {
                    Console.WriteLine("The name of the blog is: {0}", item.Name);
                }
            }
            Console.ReadLine();
        }
    }
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    public class User
    {
        [Key]
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }

    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
