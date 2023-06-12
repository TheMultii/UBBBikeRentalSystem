namespace UBBBikeRentalSystem_Tests {
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : PageTest {
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
            await Page.GotoAsync("https://localhost:7265/");

            var login = Page.Locator("text=Zaloguj");

            await Expect(login).ToHaveAttributeAsync("href", "/Login");

            await login.ClickAsync();

            var username = Page.Locator("input[name='Input.UserName']");
            var password = Page.Locator("input[name='Input.Password']");
            var submit = Page.Locator("input[type='submit']");

            await username.FillAsync("Administrator");
            await password.FillAsync("Admin123!");
            await submit.ClickAsync();

            var adminpanel = Page.Locator("text=⚙️");
            await adminpanel.ClickAsync();
        }

        [Test]
        public async Task D_UBBBikeRentalSystemCanPlaceAReservation() { }

        [Test]
        public async Task E_UBBBikeRentalSystemCanViewAReservationInAdminArea() { }

        [Test]
        public async Task F_UBBBikeRentalSystemCanAcceptAReservationInAdminArea() { }

        [Test]
        public async Task G_F_UBBBikeRentalSystemCanFinishAReservationInAdminArea() { }
    }
}