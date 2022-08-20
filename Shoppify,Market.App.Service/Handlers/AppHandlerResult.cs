namespace Shoppify.Market.App.Service.Handlers
{
    public class AppHandlerResult
    {
        public bool Succeeded { get; set; }
        public bool Failed { get; protected set; }
        public bool InvalidProcess { get; set; }
        public IEnumerable<string>? Errors { get; protected set; }
        public object Data { get; private set; }

        /// <summary>
        /// Returns result with success state
        /// </summary>
        /// <returns></returns>
        public static AppHandlerResult Ok()
        {
            return new AppHandlerResult()
            {
                Succeeded = true,
                Errors = null
            };
        }

        /// <summary>
        /// Returns result with an extra object
        /// </summary>
        /// <param name="data">Any object</param>
        /// <returns></returns>
        public static AppHandlerResult Ok(object data)
        {
            return new AppHandlerResult()
            {
                Succeeded = true,
                Errors = null,
                Data = data
            };
        }

        /// <summary>
        /// Returns internal error (server error) state
        /// </summary>
        /// <returns></returns>
        public static AppHandlerResult Failure()
        {
            return new AppHandlerResult()
            {
                Failed = true,
                Errors = null
            };
        }

        /// <summary>
        /// Returns validation error
        /// </summary>
        /// <param name="error">Error text</param>
        /// <returns></returns>
        public static AppHandlerResult ValidationError(string error)
        {
            return new AppHandlerResult()
            {
                InvalidProcess = true,
                Errors = new string[] { error }
            };
        }

        /// <summary>
        /// Returns validation errors
        /// </summary>
        /// <param name="errors">List of errors</param>
        /// <returns></returns>
        public static AppHandlerResult ValidationError(IEnumerable<string> errors)
        {
            return new AppHandlerResult()
            {
                InvalidProcess = true,
                Errors = errors
            };
        }
    }
}
