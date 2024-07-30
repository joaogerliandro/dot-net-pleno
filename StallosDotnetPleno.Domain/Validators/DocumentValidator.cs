using System.Text.RegularExpressions;

namespace StallosDotnetPleno.Domain.Validators
{
    public static class DocumentValidator
    {
        public static bool IsValidCpf(string cpf)
        {
            cpf = Regex.Replace(cpf, @"[^\d]", "");

            if (cpf.Length != 11)
                return false;

            if (Regex.IsMatch(cpf, @"(\d)\1{10}"))
                return false;

            int[] weights1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] weights2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf = cpf.Substring(0, 9);
            string digits = cpf.Substring(9, 2);

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * weights1[i];

            int remainder = sum % 11;
            int firstDigit = remainder < 2 ? 0 : 11 - remainder;

            if (firstDigit != int.Parse(digits[0].ToString()))
                return false;

            tempCpf += firstDigit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * weights2[i];

            remainder = sum % 11;
            int secondDigit = remainder < 2 ? 0 : 11 - remainder;

            return secondDigit == int.Parse(digits[1].ToString());
        }

        public static bool IsValidCnpj(string cnpj)
        {
            cnpj = Regex.Replace(cnpj, @"[^\d]", "");

            if (cnpj.Length != 14)
                return false;

            if (Regex.IsMatch(cnpj, @"(\d)\1{13}"))
                return false;

            int[] weights1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] weights2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            string digits = cnpj.Substring(12, 2);

            int sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * weights1[i];

            int remainder = sum % 11;
            int firstDigit = remainder < 2 ? 0 : 11 - remainder;

            if (firstDigit != int.Parse(digits[0].ToString()))
                return false;

            tempCnpj += firstDigit;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * weights2[i];

            remainder = sum % 11;
            int secondDigit = remainder < 2 ? 0 : 11 - remainder;

            return secondDigit == int.Parse(digits[1].ToString());
        }
    }
}
