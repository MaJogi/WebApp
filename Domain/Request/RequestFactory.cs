using WebApp.Data.Request;

namespace WebApp.Domain.Request
{
    public class RequestFactory
    {
        public static Request Create(
            string description
        )
        {
            var requestData = new RequestData()
            {
                Description = description
            };
            return new Request(requestData);
        }
    }
}
