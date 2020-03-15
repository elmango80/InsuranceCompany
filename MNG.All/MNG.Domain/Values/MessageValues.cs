namespace MNG.Domain.Values
{
    public static class MessageValues
    {
        public static string ARGUMENT_NULL { get => "Parameters can't be null or empty."; }
        public static string JSON_READER { get => "No data was obtained regarding {0}."; }
        public static string CLIENT_NOT_FOUND { get => "A client with {0}: {1} was not found."; }
        public static string CLIENT_NOT_LINKED_POLICIES { get => "The client has no linked policy."; }
        public static string POLICY_NOT_FOUND { get => "A policy with {0}: {1} was not found."; }
        public static string CLIENTS_REPOSITORY_EMPTY { get => "No data has been obtained from the clients repository."; }
        public static string POLICIES_REPOSITORY_EMPTY { get => "No data has been obtained from the policies repository."; }

    }
}
