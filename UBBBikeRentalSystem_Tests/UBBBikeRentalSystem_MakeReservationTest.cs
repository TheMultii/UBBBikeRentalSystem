using Microsoft.Playwright;

namespace UBBBikeRentalSystem_Tests {
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class UBBBikeRentalSystem_MakeReservationTest : PageTest {
        private IPage? page;
        private string? expected_date;
        private DateTime? expected_date_time;
        private ILocator? reservation;

        [Test]
        public async Task A_UBBBikeRentalSystemIsRunning() {
            await Page.GotoAsync("https://localhost:7265/");

            await Expect(Page).ToHaveTitleAsync(new Regex("UBB Bike Rental System"));
        }

        [Test]
        public async Task B_UBBBikeRentalSystemHasZalogujButton() {
            await Page.GotoAsync("https://localhost:7265/");

            var login = Page.Locator("text=Zaloguj");

            await Expect(login).ToHaveAttributeAsync("href", "/Login");

            await login.ClickAsync();

            await Expect(Page).ToHaveTitleAsync(new Regex("Logowanie"));
        }

        [Test]
        public async Task C_UBBBikeRentalSystemCanLogIn() {
            page = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {
                Headless = true
            }).Result.NewPageAsync();

            await page.GotoAsync("https://localhost:7265/");

            var login = page.Locator("text=Zaloguj");

            await Expect(login).ToHaveAttributeAsync("href", "/Login");

            await login.ClickAsync();

            var username = page.Locator("input[name='Input.UserName']");
            var password = page.Locator("input[name='Input.Password']");
            var submit = page.Locator("input[type='submit']");

            await username.FillAsync("Administrator");
            await password.FillAsync("Admin123!");
            await submit.ClickAsync();

            var adminpanel = page.Locator("text=⚙️");
            await adminpanel.ClickAsync();
        }

        private static string Get_current_date() {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("yyyy-MM-ddTHH:mm");
        }

        [Test]
        public async Task D_UBBBikeRentalSystemCanPlaceAReservation() {
            if (page == null) throw new NullReferenceException("page variable that should be set in test C_ is somehow a null");

            expected_date = Get_current_date(); 
            await page.GotoAsync("https://localhost:7265/users/makereservation");

            var vehicle_select = page.Locator("select[name='SelectedVehicle']");
            var reservation_point_select = page.Locator("select[name='SelectedReservationPoint']");
            var reservation_date = page.Locator("input[name='SelectedReservationDate']");
            var submit = page.Locator("input[type='submit']");
            
            await vehicle_select.SelectOptionAsync("3");
            await reservation_point_select.SelectOptionAsync("2");
            expected_date_time = DateTime.Parse(expected_date);
            await Expect(reservation_date).ToHaveValueAsync(expected_date);
            await submit.ClickAsync();

            var dt_str = expected_date_time?.ToString("dd.MM.yyyy HH:mm");
            var _reservation = page.Locator($"tr:has-text('{dt_str}')");
            await Expect(_reservation).ToBeVisibleAsync();
        }

        [Test]
        public async Task E_UBBBikeRentalSystemCanViewAReservationInAdminArea() {
            if (page == null || expected_date == null || expected_date_time == null)
                throw new NullReferenceException("page, expected_date or expected_date_time variable that should be set in test C_ is somehow a null");
            await page.GotoAsync("https://localhost:7265/admin/reservations");

            var dt_str = expected_date_time?.ToString("dd.MM.yyyy HH:mm");
            reservation = page.Locator($"tr:has-text('{dt_str}')");
            await Expect(reservation).ToBeVisibleAsync();
        }

        [Test]
        public async Task F_UBBBikeRentalSystemCanAcceptAReservationInAdminArea() {
            if (page == null || expected_date == null || expected_date_time == null)
                throw new NullReferenceException("page, expected_date or expected_date_time variable that should be set in test C_ is somehow a null");
            if (reservation == null)
                throw new NullReferenceException("reservation variable that should be set in test E_ is somehow a null");

            var edit = reservation.Locator("a:has-text('Edytuj')");
            await edit.ClickAsync();

            var submit = page.Locator("input[type='submit']");
            await submit.ClickAsync();

            var dt_str = expected_date_time?.ToString("dd.MM.yyyy HH:mm");
            reservation = page.Locator($"tr:has-text('{dt_str}'):has-text('W trakcie')");
            await Expect(reservation).ToBeVisibleAsync();
        }

        [Test]
        public async Task G_UBBBikeRentalSystemCanFinishAReservationInAdminArea() {
            if (page == null || expected_date == null || expected_date_time == null)
                throw new NullReferenceException("page, expected_date or expected_date_time variable that should be set in test C_ is somehow a null");
            if (reservation == null)
                throw new NullReferenceException("reservation variable that should be set in test E_ or F_ is somehow a null");

            var edit = reservation.Locator("a:has-text('Edytuj')");
            await edit.ClickAsync();

            var return_point_select = page.Locator("select[name='SelectedReturnPoint']");
            var return_date = page.Locator("input[name='SelectedReturnDate']");
            var submit = page.Locator("input[type='submit']");

            await return_point_select.SelectOptionAsync("2");

            var _expected_date = Get_current_date();
            var _expected_date_time = DateTime.Parse(_expected_date);
            await Expect(return_date).ToHaveValueAsync(_expected_date);

            await submit.ClickAsync();

            var dtstart_str = expected_date_time?.ToString("dd.MM.yyyy HH:mm");
            var dtend_str = _expected_date_time.ToString("dd.MM.yyyy HH:mm");

            reservation = page.Locator($"tr:has-text('{dtstart_str}'):has-text('Zakończony'):has-text('{dtend_str}')");
            await Expect(reservation).ToBeVisibleAsync();
        }
    }
}