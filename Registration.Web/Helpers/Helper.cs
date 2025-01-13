namespace Registration.Web.Helpers
{
    public class Helper
    {
        public static string GenerateRandomCode()
        {
            Random random = new Random();
            int code = random.Next(1000, 10000);
            return code.ToString();
        }
    }
}
