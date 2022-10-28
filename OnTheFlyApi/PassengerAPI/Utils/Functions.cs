namespace PassengerAPI.Utils
{
    public class Functions
    {
        public static string FormatCPF(string unformatedCpf)
            => $"{unformatedCpf.Substring(0, 3)}." +
            $"{unformatedCpf.Substring(3, 3)}." +
            $"{unformatedCpf.Substring(6, 3)}-" +
            $"{unformatedCpf.Substring(9, 2)}";
        public static string UnformatCPF(string formattedCpf)
            => $"{formattedCpf.Substring(0, 3)}" +
            $"{formattedCpf.Substring(4, 3)}" +
            $"{formattedCpf.Substring(8, 3)}" +
            $"{formattedCpf.Substring(12, 2)}";
    }
}