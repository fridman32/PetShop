using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjectMVC.Models;

namespace WebProjectMVC.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Mamal" },
                new Category { CategoryId = 2, CategoryName = "Reptile" },
                new Category { CategoryId = 3, CategoryName = "Aquatic" },
                new Category { CategoryId = 4, CategoryName = "Birds" }
                );


            modelBuilder.Entity<Comment>().HasData(
                new Comment { CommentID = 1, AnimalID = 1, CommentText = "very beautiful, i like this animal" },
                new Comment { CommentID = 2, AnimalID = 1, CommentText = "I want a dog too !" },
                new Comment { CommentID = 3, AnimalID = 1, CommentText = "dogs are very cute" },
                new Comment { CommentID = 4, AnimalID = 2, CommentText = "Fish cant talk" },
                new Comment { CommentID = 5, AnimalID = 2, CommentText = "I have 3 fish in my house" }
                );

            modelBuilder.Entity<Animal>().HasData(
                 new Animal
                 {
                     Name = "Dog",
                     Age = 5,
                     AnimalID = 1,
                     CategortId = 1,
                     PictureName = "DogIMG.jpg",
                     Description = "The dog is a domesticated carnivore of the family Canidae." +
                     "It is part of the wolf-like canids, and is the most widely abundant terrestrial carnivore."
                 },
                  new Animal
                  {
                      Name = "Fish",
                      Age = 2,
                      AnimalID = 2,
                      CategortId = 3,
                      PictureName = "FishIMG.jpg",
                      Description = "Fish are gill-bearing aquatic craniate animals that lack limbs with digits. " +
                      "They form a sister group to the tunicates, together forming the olfactores. " +
                      "Included in this definition are the living hagfish, lampreys, and cartilaginous and bony fish as well as various extinct related groups."
                  },
                   new Animal
                   {
                       Name = "Lion",
                       Age = 13,
                       AnimalID = 3,
                       CategortId = 1,
                       PictureName = "LionIMG.jpg",
                       Description = "The lion is a species in the family Felidae and a member of the genus Panthera." +
                       " It has a muscular, deep-chested body, short, rounded head, round ears, and a hairy tuft at the end of its tail. " +
                       "It is sexually dimorphic; adult male lions have a prominent mane." +
                       " With a typical head-to-body length of 184–208 cm they are larger than females at 160–184 cm"
                   },
                   new Animal
                   {
                       Name = "Snake",
                       Age = 8,
                       AnimalID = 4,
                       CategortId = 2,
                       PictureName = "SnakeIMG.jpg",
                       Description = "Snakes are elongated, legless, carnivorous reptiles of the suborder Serpentes." +
                       "Like all other squamates, snakes are ectothermic,amniote vertebrates covered in overlapping scales" +
                       "Many species of snakes have skulls with several more joints than their lizard ancestors," +
                       "enabling them to swallow prey much larger than their heads with their highly mobile jaws"
                   },
                   new Animal
                   {
                       Name = "Bird",
                       Age = 3,
                       AnimalID = 5,
                       CategortId = 4,
                       PictureName = "BirdIMG.jpg",
                       Description = "Birds are a group of warm-blooded vertebrates constituting the class Aves, characterized by" +
                       "feathers, toothless beaked jaws, the laying of hard-shelled eggs, a high metabolic rate," +
                       "a four-chambered heart, and a strong yet lightweight skeleton."
                   },
                   new Animal
                   {
                       Name = "Rabbit",
                       Age = 4,
                       AnimalID = 6,
                       CategortId = 1,
                       PictureName = "rabbit.jpg",
                       Description = "Rabbits are small mammals in the family Leporidae of the order Lagomorpha (along with the hare and the pika)." +
                       "Oryctolagus cuniculus includes the European rabbit species and its descendants, the world's 305 breeds of domestic rabbit." +
                       " Sylvilagus includes 13 wild rabbit species, among them the seven types of cottontail."
                   }
             ); ;
        }
        public Animal GetAnimalData(int ID)
        {
            Animal animal = Animals.SingleOrDefault(a => a.AnimalID == ID);
            return animal;
        }

        public Category GetCategoryData(int ID)
        {
            Category category = categories.SingleOrDefault(categories => categories.CategoryId == ID);
            return category;
        }

        public IEnumerable<Category> GetCategory()
        {
            return categories.ToList();
        }

        public IEnumerable<Animal> GetAnimalsByCategory(int id)
        {
            var list = Animals.Where(a => a.CategortId == id);
            return list;
        }

        public IEnumerable<Comment> GetCommentsByAnimalId(int id)
        {
            var listOfComments = Animals.Where(a => a.AnimalID.Equals(id)).SelectMany(b => b.CommentsCollection).ToList().AsEnumerable();
            return listOfComments;
        }

        public void AddComment(Comment comment)
        {
            comments.Add(comment);
            SaveChanges();
        }

        public IEnumerable<Animal> GetTopAnimals()
        {
            var topTwoAnimals = Animals.OrderByDescending(a => a.CommentsCollection.Count()).Take(2).ToList().AsEnumerable();

            return topTwoAnimals;
        }
    }
}
