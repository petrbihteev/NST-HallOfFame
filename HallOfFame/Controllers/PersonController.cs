using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonDBContext _db;

        private static long IdPerson, IdSkill, IdSkillPerson;
        public PersonController(PersonDBContext db)
        {
            _db = db;
        }

        #region Person
        //GET
        [Route("api/v1/Persons")]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<Person> objPersonList = _db.Persons.Include(x => x.ConPersonSkills).ThenInclude(x => x.Skill).AsNoTracking().ToList();
                return View(objPersonList);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Get/{id}
        [Route("api/v1/Person/{id}")]
        public IActionResult Index(long? id)
        {
            try
            {
                IEnumerable<Person> objPersonList = _db.Persons.Where(x => x.Id == id).Include(x => x.ConPersonSkills).ThenInclude(x => x.Skill).AsNoTracking().ToList();
                return View(objPersonList);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("api/v1/Person/post")]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/v1/Person/post")]
        public IActionResult Create(Person obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(obj);
                }
                _db.Persons.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("api/v1/Person/put/{id}")]
        public IActionResult Edit(long? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var person = _db.Persons.Find(id);

                if (person == null)
                {
                    return NotFound();
                }
                return View(person);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/v1/Person/put/{id}")]
        public IActionResult Edit(Person obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(obj);
                }
                _db.Persons.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("api/v1/Person/delete/{id}")]
        public IActionResult Delete(long? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var person = _db.Persons.Find(id);

                if (person == null)
                {
                    return NotFound();
                }
                return View(person);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Delete
        [HttpPost]
        [Route("api/v1/Person/delete/{id}")]
        public IActionResult Delete(Person obj)
        {
            try
            {
                _db.Persons.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Skill
        public IActionResult ViewSkill()
        {
            try
            {
                IEnumerable<Skill> objPersonList = _db.Skills;
                return View(objPersonList);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult CreateSkill()
        {
            try
            {
                return View();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSkill(Skill obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(obj);
                }
                _db.Skills.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("ViewSkill");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult EditSkill(long? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var skill = _db.Skills.Find(id);

                if (skill == null)
                {
                    return NotFound();
                }
                return View(skill);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSkill(Skill obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(obj);
                }
                _db.Skills.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("ViewSkill");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult DeleteSkill(long? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var skill = _db.Skills.Find(id);

                if (skill == null)
                {
                    return NotFound();
                }
                return View(skill);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Delete
        [HttpPost]
        public IActionResult DeleteSkill(Skill obj)
        {
            try
            {
                _db.Skills.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("ViewSkill");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region SkillPerson
        public IActionResult Result(long? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                //var con = _db.ConPersonSkills.Where(x=>x.PersonId == id).ToList();
                var person = _db.Persons.Where(x => x.Id == id).FirstOrDefault();
                if (person == null)
                {
                    return NotFound();
                }
                var con = _db.ConPersonSkills.Where(x => x.PersonId == id).Include(x => x.Skill).AsNoTracking().ToList();
                if (con == null)
                {
                    return NotFound();
                }
                IdPerson = person.Id;
                ViewBag.Name = "Навыки - " + person.Name;
                ViewBag.data = con;
                return View(con);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }    
        }

        //Выпадающий список
        public void SkillDropDownList(object? selectedSkill = null)
        {
            try
            {
                var skillQuery = from skill in _db.Skills orderby skill.Name select skill;
                ViewBag.SkillId = new SelectList(skillQuery.AsNoTracking(), "Id", "Name", selectedSkill);
            }
            catch
            {
                return;
            }
        }

        public IActionResult CreateSkillPerson()
        {
            try
            {
                SkillDropDownList();
                return View();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSkillPerson(ConPersonSkill obj)
        {
            try
            {
                obj.PersonId = IdPerson;
                var item = _db.ConPersonSkills.Where(x => x.PersonId == obj.PersonId && x.SkillId == obj.SkillId).ToList();
                if (item.Count > 0)
                {
                    return Unauthorized("Такой навык у работника уже есть!");
                }
                else
                {
                    _db.ConPersonSkills.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult EditSkillPerson(long? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var skillperson = _db.ConPersonSkills.Where(x => x.IdPersonSkill == id).Include(x => x.Skill).FirstOrDefault();

                if (skillperson == null)
                {
                    return NotFound();
                }
                IdSkillPerson = skillperson.IdPersonSkill;
                IdSkill = skillperson.SkillId;
                return View(skillperson);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSkillPerson(ConPersonSkill obj)
        {
            try
            {
                obj.IdPersonSkill = IdSkillPerson;
                obj.PersonId = IdPerson;
                obj.SkillId = IdSkill;
                _db.ConPersonSkills.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult DeleteSkillPerson(long? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var skillperson = _db.ConPersonSkills.Where(x => x.IdPersonSkill == id).Include(x => x.Skill).FirstOrDefault();

                if (skillperson == null)
                {
                    return NotFound();
                }
                IdSkillPerson = skillperson.IdPersonSkill;
                return View(skillperson);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Delete
        [HttpPost]
        public IActionResult DeleteSkillPerson(ConPersonSkill obj)
        {
            try
            {
                obj.IdPersonSkill = IdSkillPerson;
                _db.ConPersonSkills.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}
