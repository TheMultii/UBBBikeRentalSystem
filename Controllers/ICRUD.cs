using Microsoft.AspNetCore.Mvc;

namespace UBBBikeRentalSystem.Controllers {
    public interface ICRUD<T, K> {
        [HttpGet]
        public IActionResult Index();

        [HttpGet]
        public IActionResult Details(K id);

        [HttpGet]
        public IActionResult Create();

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(T _model);

        [HttpGet]
        public IActionResult Edit(K id);

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(T _model);

        [HttpGet]
        public IActionResult Delete(K id);

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(T _model);
    }
}
