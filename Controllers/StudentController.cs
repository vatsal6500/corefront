using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Helper;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class StudentController : Controller
    {
        StudentApi api = new StudentApi();

        // GET: StudentController
        public async Task<ActionResult> Index(int? a1, int? a2, string uname)
        {
            List<Student> students = new List<Student>();
            HttpClient client = api.initial();
            HttpResponseMessage res = await client.GetAsync("api/Student");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<Student>>(result);
            }

            List<Student> name = students.Where(o => o.Age >= a1 && o.Age <=a2).ToList();

            if(a1==null && a2 == null)
            {
                return View(students);
            }

            return View(name);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {


            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                HttpClient client = api.initial();

                var data = JsonConvert.SerializeObject(student);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage res = client.PostAsync("api/Student", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var student = new Student();
            HttpClient client = api.initial();
            HttpResponseMessage res = await client.GetAsync($"api/Student/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<Student>(result);
            }

            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            try
            {

                HttpClient client = api.initial();

                var data = JsonConvert.SerializeObject(student);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage res = client.PutAsync($"api/Student/{student.StudenId}", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            HttpClient client = api.initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/Student/{id}");
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }


            return View();
        }

        // POST: StudentController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
