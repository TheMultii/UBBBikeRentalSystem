using Microsoft.AspNetCore.Mvc;

namespace UBBBikeRentalSystem.Controllers {
    public interface ICRUD<T> {
        [HttpGet]
        public IActionResult Index();

        [HttpGet]
        public IActionResult Details(int id);

        [HttpGet]
        public IActionResult Create();

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(T _model);

        [HttpGet]
        public IActionResult Edit(int id);

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(T _model);

        [HttpGet]
        public IActionResult Delete(int id);

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(T vehicle);
    }
}
