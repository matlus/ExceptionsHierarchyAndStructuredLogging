using System.Collections;
using System.Text;

namespace ExceptionsHierarchy;
public sealed partial class ApplicationLogger
{
    private readonly record struct LogStateException : IReadOnlyList<KeyValuePair<string, object>>
    {
        private readonly List<KeyValuePair<string, object>> keyValuePairs = [];

        public LogStateException(
            string roleName,
            string methodName,
            InterviewBaseException exception)
        {
            keyValuePairs.Add(new KeyValuePair<string, object>("RoleName", roleName));
            keyValuePairs.Add(new KeyValuePair<string, object>("MethodName", methodName));
            keyValuePairs.Add(new KeyValuePair<string, object>("ExceptionType", exception.GetType().Name));
            keyValuePairs.Add(new KeyValuePair<string, object>("ExceptionCategory", exception.ExceptionCategory.ToString()));
            keyValuePairs.Add(new KeyValuePair<string, object>("Reason", exception.Reason));
            keyValuePairs.Add(new KeyValuePair<string, object>("MessageId", exception.ExceptionData.MessageId));

            foreach (DictionaryEntry de in exception.Data)
            {
                keyValuePairs.Add(new KeyValuePair<string, object>(de.Key.ToString()!, de.Value!));
            }

            Count = keyValuePairs.Count;
        }

        public int Count { get; }

        public KeyValuePair<string, object> this[int index] => keyValuePairs[index];

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            var errorMessage = new StringBuilder();

            foreach (var kvp in keyValuePairs)
            {
                errorMessage.Append($"{kvp.Key}: `{kvp.Value}`, ");
            }

            return errorMessage.ToString();
        }

        public static string Format(LogStateException logStateException, Exception? exception)
        {
            return exception is not null
                ? $"{exception.Message}. Additional Information - " + logStateException.ToString()
                : logStateException.ToString();
        }
    }
}
