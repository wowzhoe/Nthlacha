using CodeFirst.Services;

namespace CodeFirst
{
    /// <summary>
    /// Dataless business logic implemented in independend services.
    /// (For data storing, use Main.Store)
    /// Service workflow:
    /// Store.Bind => Logic => Store.SetValue
    /// </summary>
    public class Servicer
    {
        private TestService testService;

        public Servicer()
        {
            testService = new TestService();
        }

        public void Start()
        {
            testService.Start();
        }
    }
}
