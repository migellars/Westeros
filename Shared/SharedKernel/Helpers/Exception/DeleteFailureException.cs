namespace SharedKernel.Helpers.Exception
{
    public class DeleteFailureException : System.Exception
    {
        public DeleteFailureException(string name, object key, string message)
            : base($"Deletion of entity \"{name}\" ({key}) failed. {message}")
        {
        }
    }
}