namespace E_commerce.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDeafultMessageForStatusCode(statusCode);
        }

        public string? GetDeafultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad Request you have made",
                401 => "Authorized, You are not",
                404 => "Resource was not found",
                //ex
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger lead to hate. Hate lead to career change.",
                _ => null
            };
        }
    }
}
