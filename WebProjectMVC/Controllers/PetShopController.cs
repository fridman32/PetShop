using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebProjectMVC.Data;

namespace WebProjectMVC.Controllers
{
    public class PetShopController : Controller
    {
        Context _context;
        IEnumerable AList;
        IEnumerable CList;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PetShopController(Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            AList = _context.Animals;
            CList = _context.categories;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Shop()
        {
            return View(_context.GetTopAnimals());
        }
        public IActionResult Catalog()
        {
            ViewBag.GetCategories = CList;
            var GetAnimals = AList;

            return View(GetAnimals);
        }
        public IActionResult FilterdCatalog(int id)
        {
            ViewBag.GetCategories = CList;
            var GetAnimals = _context.GetAnimalsByCategory(id);
            return View("Catalog", GetAnimals);
        }
        public IActionResult AnimalDetail(int id)
        {
            Models.Animal animal = _context.GetAnimalData(id);
            ViewBag.CategoryName = _context.GetCategoryData(animal.CategortId);
            ViewBag.Comments = _context.GetCommentsByAnimalId(id);
            return View(animal);
        }
        public IActionResult Administrator()
        {
            ViewBag.GetCategories = CList;
            var GetAnimals = AList;
            return View(GetAnimals);
        }
        public IActionResult FilterdAdministrator(int id)
        {
            ViewBag.GetCategories = CList;
            var GetAnimals = _context.GetAnimalsByCategory(id);
            return View("Administrator", GetAnimals);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _context.GetCategory();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Models.CreateNewAnimal animal)
        {
            if (animal.Age < 1 || animal.Name == null || animal.Picture == null || animal.Description == null || animal.CategoryId == 0)
            {
                TempData["ProcessMessage"] = "Please Fill All The Fields Correctly.";
                TempData["displayModal"] = "myModal";
                return RedirectToAction("Add");
            }
            else
            {
                string unique = null;
                if (animal.Picture != null)
                {
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                    unique = Guid.NewGuid().ToString() + "_" + animal.Picture.FileName;
                    string FilePath = Path.Combine(uploads, unique);
                    animal.Picture.CopyTo(new FileStream(FilePath, FileMode.Create));
                }
                Models.Animal newAnimal = new Models.Animal();
                {
                    newAnimal.Name = animal.Name;
                    newAnimal.Age = animal.Age;
                    newAnimal.Description = animal.Description;
                    newAnimal.CategortId = animal.CategoryId;
                    newAnimal.PictureName = unique;
                };
                _context.Add(newAnimal);
                _context.SaveChanges();
                return RedirectToAction("administrator");
            }
        }
        public IActionResult Delete(int id)
        {
            Models.Animal animal = _context.GetAnimalData(id);
            Thread.Sleep(2500);
            _context.Remove(animal);
            _context.SaveChanges();
            return RedirectToAction("Administrator");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _context.GetCategory();
            var a = _context.Animals.SingleOrDefault(ab => ab.AnimalID == id);
            Models.CreateNewAnimal createNewAnimal = new Models.CreateNewAnimal();
            {
                createNewAnimal.Age = a.Age;
                createNewAnimal.CategoryId = a.CategortId;
                createNewAnimal.Description = a.Description;
                createNewAnimal.Name = a.Name;
            }
            return View(createNewAnimal);
        }
        [HttpPost]
        public IActionResult Edit(Models.CreateNewAnimal animal, int id)
        {
            if (animal.Age < 1 || animal.Name == null ||  animal.Description == null || animal.CategoryId < 1)
            {
                TempData["ProcessMessage"] = "Please Fill All The Fields Correctly.";
                TempData["displayModal"] = "myModal";
                return RedirectToAction("Edit");
            }
            else
            {
                var EditAnimal = _context.GetAnimalData(id);

                string unique = null;
                if (animal.Picture != null)
                {
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                    unique = Guid.NewGuid().ToString() + "_" + animal.Picture.FileName;
                    string FilePath = Path.Combine(uploads, unique);
                    animal.Picture.CopyTo(new FileStream(FilePath, FileMode.Create));
                    EditAnimal.PictureName = unique;
                }

                EditAnimal.Name = animal.Name;
                EditAnimal.Age = animal.Age;
                EditAnimal.Description = animal.Description;
                EditAnimal.CategortId = animal.CategoryId;
                EditAnimal.Category = animal.Category;

                _context.Update(EditAnimal);
                _context.SaveChanges();
                return RedirectToAction("Administrator");
            }
        }
        public IActionResult AddComment(string comment, int id)
        {
            _context.AddComment(new Models.Comment { AnimalID = id, CommentText = comment });
            return RedirectToAction("AnimalDetail", new { id = id });
        }
    }
}
